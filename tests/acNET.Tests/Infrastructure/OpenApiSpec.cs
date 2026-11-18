using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace AcNET.Tests.Infrastructure;

/// <summary>
/// 테스트 픽스처로 저장된 solved.ac v4 OpenAPI 문서를 읽고, 스키마로부터 예시 응답을 만듭니다.
/// </summary>
public sealed class OpenApiSpec
{
    public static OpenApiSpec Instance { get; } = Load();

    public JsonObject Root { get; }
    public IReadOnlyList<Operation> Operations { get; }

    OpenApiSpec(JsonObject root)
    {
        Root = root;
        List<Operation> operations = [];
        foreach (var (path, item) in root["paths"]!.AsObject())
        {
            foreach (var (method, op) in item!.AsObject())
            {
                if (op is JsonObject operation && operation.ContainsKey("responses"))
                    operations.Add(new Operation(method.ToUpperInvariant(), path, operation));
            }
        }
        Operations = operations;
    }

    static OpenApiSpec Load()
    {
        string file = Path.Combine(AppContext.BaseDirectory, "Fixtures", "solvedac-v4-openapi.json");
        return new OpenApiSpec(JsonNode.Parse(File.ReadAllText(file))!.AsObject());
    }

    /// <summary>
    /// 요청 메서드와 경로(/api/v4/...)에 맞는 operation 을 찾습니다. 고정 구간이 더 많은 템플릿을 우선합니다.
    /// </summary>
    public Operation? Match(string method, string path)
        => Operations
            .Where(o => o.Method == method && o.PathRegex.IsMatch(path))
            .OrderByDescending(o => o.LiteralSegments)
            .FirstOrDefault();

    public JsonObject Resolve(JsonObject schema)
    {
        if (schema["$ref"]?.GetValue<string>() is string reference)
        {
            JsonNode node = Root;
            foreach (string part in reference.TrimStart('#', '/').Split('/'))
                node = node[part]!;
            return node.AsObject();
        }
        return schema;
    }

    public sealed class Operation
    {
        public Operation(string method, string path, JsonObject node)
        {
            Method = method;
            Path = path;
            Node = node;
            string pattern = Regex.Replace(Regex.Escape(path), @"\\\{([^}]+)}", "(?<$1>[^/]+)");
            PathRegex = new Regex("^" + pattern + "$");
            LiteralSegments = path.Split('/').Count(s => s.Length > 0 && !s.StartsWith('{'));
        }

        public string Method { get; }
        public string Path { get; }
        public JsonObject Node { get; }
        public Regex PathRegex { get; }
        public int LiteralSegments { get; }
        public string Key => $"{Method} {Path}";

        public IEnumerable<JsonObject> Parameters(string location)
            => (Node["parameters"]?.AsArray() ?? []).Select(p => p!.AsObject()).Where(p => p["in"]!.GetValue<string>() == location);

        /// <summary>
        /// 첫 번째 2xx 응답의 (상태 코드, Json 스키마). 본문이 없으면 스키마는 null.
        /// </summary>
        public (int Status, JsonObject? Schema) SuccessResponse()
        {
            foreach (var (code, response) in Node["responses"]!.AsObject())
            {
                if (!code.StartsWith('2'))
                    continue;
                JsonObject? schema = response?["content"]?["application/json"]?["schema"]?.AsObject();
                return (int.Parse(code), schema);
            }
            throw new InvalidOperationException($"{Key} 에 2xx 응답이 없습니다.");
        }

        public JsonObject? RequestBodySchema()
            => Node["requestBody"]?["content"]?["application/json"]?["schema"]?.AsObject();
    }
}

/// <summary>
/// 예시 응답을 만드는 방식입니다.
/// </summary>
public enum SampleMode
{
    /// <summary>모든 속성(선택 속성 포함)을 null 이 아닌 값으로 채웁니다.</summary>
    Full,
    /// <summary>필수 속성만 넣고, null 이 될 수 있는 속성은 모두 null 로 둡니다.</summary>
    Minimal,
}

public static class SampleGenerator
{
    public static JsonNode? Create(OpenApiSpec spec, JsonObject schema, SampleMode mode, int depth = 0)
    {
        schema = spec.Resolve(schema);

        foreach (string combinator in new[] { "oneOf", "anyOf" })
        {
            if (schema[combinator] is JsonArray variants)
            {
                JsonObject? chosen = variants
                    .Select(v => spec.Resolve(v!.AsObject()))
                    .FirstOrDefault(v => !IsNullOnly(v));
                return chosen is null ? null : Create(spec, chosen, mode, depth);
            }
        }

        List<string> types = Types(schema);
        bool nullable = types.Contains("null");
        if (mode == SampleMode.Minimal && nullable)
            return null;
        string? type = types.FirstOrDefault(t => t != "null");

        if (schema["enum"] is JsonArray values)
        {
            JsonNode? first = values.FirstOrDefault(v => v is not null);
            return first?.DeepClone();
        }

        if (type is null && schema.ContainsKey("properties"))
            type = "object";

        switch (type)
        {
            case "object":
                {
                    JsonObject result = [];
                    HashSet<string> required = (schema["required"]?.AsArray() ?? []).Select(r => r!.GetValue<string>()).ToHashSet();
                    if (schema["properties"] is JsonObject properties)
                    {
                        foreach (var (name, propertySchema) in properties)
                        {
                            if (mode == SampleMode.Minimal && !required.Contains(name))
                                continue;
                            result[name] = Create(spec, propertySchema!.AsObject(), mode, depth + 1);
                        }
                    }
                    else if (schema["additionalProperties"] is JsonObject additional && mode == SampleMode.Full)
                    {
                        result["ko"] = Create(spec, additional, mode, depth + 1);
                    }
                    return result;
                }
            case "array":
                {
                    JsonArray result = [];
                    if (depth < 8 && schema["items"] is JsonObject items)
                        result.Add(Create(spec, items, mode, depth + 1));
                    return result;
                }
            case "string":
                return JsonValue.Create("sample");
            case "integer":
            case "number":
                return JsonValue.Create(1);
            case "boolean":
                return JsonValue.Create(true);
            default:
                // 형식이 정해지지 않은 스키마 ({}): 아무 Json 이나 올 수 있음
                return new JsonObject { ["any"] = 1 };
        }
    }

    static bool IsNullOnly(JsonObject schema)
        => Types(schema) is ["null"];

    static List<string> Types(JsonObject schema) => schema["type"] switch
    {
        JsonArray array => array.Select(t => t!.GetValue<string>()).ToList(),
        JsonValue value => [value.GetValue<string>()],
        _ => [],
    };
}
