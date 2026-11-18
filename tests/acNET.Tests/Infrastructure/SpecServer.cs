using System.Collections.Concurrent;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Web;

namespace AcNET.Tests.Infrastructure;

/// <summary>
/// OpenAPI 문서대로 동작하는 가짜 solved.ac 서버입니다.
/// 요청이 문서의 어떤 operation 에 해당하는지 찾고, 쿼리/본문이 문서와 맞는지 검사한 뒤, 문서의 스키마로 만든 예시 응답을 돌려줍니다.
/// </summary>
public sealed class SpecServer : HttpMessageHandler
{
    readonly OpenApiSpec spec = OpenApiSpec.Instance;
    readonly SampleMode mode;

    public SpecServer(SampleMode mode) => this.mode = mode;

    /// <summary>호출된 operation 들 (METHOD /api/v4/...)</summary>
    public ConcurrentBag<string> Hits { get; } = [];

    /// <summary>문서와 맞지 않는 요청 내용</summary>
    public ConcurrentBag<string> Violations { get; } = [];

    /// <summary>
    /// 응답을 엄격하게 읽는 SolvedAPI 를 만듭니다.
    /// Json 에 모델에 없는 속성이 있거나(누락된 속성), nullable 이 아닌 속성에 null 이 오면 실패합니다.
    /// </summary>
    public SolvedAPI CreateClient(string? accessToken = "user-token")
    {
        SolvedAPI api = new(new SolvedAPIOptions { ClientId = "client-id", ClientSecret = "client-secret", AccessToken = accessToken, Language = "ko" }, new HttpClient(this));
        api.JsonOptions = StrictOptions;
        return api;
    }

    public static readonly JsonSerializerOptions StrictOptions = new(JsonSerializerDefaults.Web)
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        RespectNullableAnnotations = true,
    };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string path = request.RequestUri!.AbsolutePath;
        OpenApiSpec.Operation? operation = spec.Match(request.Method.Method, path);
        if (operation is null)
        {
            Violations.Add($"문서에 없는 요청: {request.Method} {path}");
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        Hits.Add(operation.Key);

        CheckHeaders(request, operation);
        CheckPath(path, operation);
        CheckQuery(request, operation);
        await CheckBodyAsync(request, operation, cancellationToken);

        var (status, schema) = operation.SuccessResponse();
        HttpResponseMessage response = new((HttpStatusCode)status);
        if (schema is not null)
        {
            JsonNode? sample = SampleGenerator.Create(spec, schema, mode);
            response.Content = new StringContent(sample?.ToJsonString() ?? "null", Encoding.UTF8, "application/json");
        }
        response.Headers.Add("X-RateLimit-Limit", "600");
        response.Headers.Add("X-RateLimit-Remaining", "599");
        return response;
    }

    void CheckHeaders(HttpRequestMessage request, OpenApiSpec.Operation operation)
    {
        if (!request.Headers.TryGetValues("x-client-id", out _) || !request.Headers.TryGetValues("x-client-secret", out _))
            Violations.Add($"{operation.Key}: x-client-id / x-client-secret 헤더가 없습니다.");
    }

    void CheckPath(string path, OpenApiSpec.Operation operation)
    {
        var match = operation.PathRegex.Match(path);
        foreach (JsonObject parameter in operation.Parameters("path"))
        {
            string name = parameter["name"]!.GetValue<string>();
            string value = Uri.UnescapeDataString(match.Groups[name].Value);
            if (parameter["schema"]?["enum"] is JsonArray values && !values.Any(v => v?.ToString() == value))
                Violations.Add($"{operation.Key}: 경로 '{name}' 의 값 '{value}' 가 문서의 enum ({values.ToJsonString()}) 에 없습니다.");
        }
    }

    void CheckQuery(HttpRequestMessage request, OpenApiSpec.Operation operation)
    {
        var query = HttpUtility.ParseQueryString(request.RequestUri!.Query);
        Dictionary<string, JsonObject> declared = operation.Parameters("query").ToDictionary(p => p["name"]!.GetValue<string>());

        foreach (string? key in query.AllKeys)
        {
            if (key is null || !declared.TryGetValue(key, out JsonObject? parameter))
            {
                Violations.Add($"{operation.Key}: 문서에 없는 쿼리 '{key}'");
                continue;
            }
            string value = query[key]!;
            if (parameter["schema"]?["enum"] is JsonArray values && !values.Any(v => v?.ToString() == value))
                Violations.Add($"{operation.Key}: 쿼리 '{key}' 의 값 '{value}' 가 문서의 enum ({values.ToJsonString()}) 에 없습니다.");
            if (parameter["schema"]?["pattern"]?.GetValue<string>() is string pattern && !System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
                Violations.Add($"{operation.Key}: 쿼리 '{key}' 의 값 '{value}' 가 패턴 {pattern} 과 맞지 않습니다.");
        }

        foreach (var (name, parameter) in declared)
        {
            if (parameter["required"]?.GetValue<bool>() == true && query[name] is null)
                Violations.Add($"{operation.Key}: 필수 쿼리 '{name}' 이 없습니다.");
        }
    }

    async Task CheckBodyAsync(HttpRequestMessage request, OpenApiSpec.Operation operation, CancellationToken cancellationToken)
    {
        JsonObject? schema = operation.RequestBodySchema();
        if (request.Content is null)
        {
            if (schema is not null && operation.Node["requestBody"]?["required"]?.GetValue<bool>() == true)
                Violations.Add($"{operation.Key}: 요청 본문이 없습니다.");
            return;
        }
        if (operation.Path.StartsWith("/api/v4/oauth/"))
            return; // form-urlencoded, 별도 테스트에서 검사

        string body = await request.Content.ReadAsStringAsync(cancellationToken);
        if (schema is null)
        {
            Violations.Add($"{operation.Key}: 문서에 없는 요청 본문 {body}");
            return;
        }
        JsonObject json = JsonNode.Parse(body)!.AsObject();
        JsonObject properties = spec.Resolve(schema)["properties"]!.AsObject();
        foreach (var (name, value) in json)
        {
            if (!properties.TryGetPropertyValue(name, out JsonNode? propertySchema))
            {
                Violations.Add($"{operation.Key}: 본문에 문서에 없는 속성 '{name}'");
                continue;
            }
            if (propertySchema?["enum"] is JsonArray values && !values.Any(v => JsonNode.DeepEquals(v, value)))
                Violations.Add($"{operation.Key}: 본문 속성 '{name}' 의 값 {value?.ToJsonString()} 이 문서의 enum 에 없습니다.");
        }
        foreach (JsonNode? required in spec.Resolve(schema)["required"]?.AsArray() ?? [])
        {
            if (!json.ContainsKey(required!.GetValue<string>()))
                Violations.Add($"{operation.Key}: 본문에 필수 속성 '{required}' 이 없습니다.");
        }
    }
}
