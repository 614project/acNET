using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Text.Unicode;

namespace AcNET;

/// <summary>
/// acNET 이 사용하는 System.Text.Json 설정입니다.
/// 모두 소스 생성된 <see cref="SolvedJsonContext"/> 를 사용하므로 리플렉션 없이 동작합니다. (Native AOT, 트리밍 지원)
/// </summary>
internal static class SolvedJson
{
    /// <summary>
    /// 응답을 읽을 때 사용합니다. (camelCase, 대소문자 무시)
    /// </summary>
    public static readonly SolvedJsonContext Read = new(new JsonSerializerOptions(JsonSerializerDefaults.Web));

    /// <summary>
    /// 요청 본문을 쓸 때 사용합니다. null 인 속성은 보내지 않습니다.
    /// </summary>
    public static readonly SolvedJsonContext Write = new(new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = Utf8Encoder,
    });

    /// <summary>
    /// <see cref="Type.Jsonable.ToJson"/> 출력용입니다. 한글 등이 \uXXXX 로 바뀌지 않도록 합니다.
    /// </summary>
    public static readonly SolvedJsonContext Display = new(new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        Encoder = Utf8Encoder,
    });

    static JavaScriptEncoder Utf8Encoder => JavaScriptEncoder.Create(UnicodeRanges.All);

    /// <summary>
    /// 형식에 맞는 <see cref="JsonTypeInfo{T}"/> 를 가져옵니다.
    /// </summary>
    public static JsonTypeInfo<T> TypeInfo<T>(Func<System.Type, JsonTypeInfo?> resolver)
        => resolver(typeof(T)) as JsonTypeInfo<T> ?? throw NotSupported(typeof(T));

    public static NotSupportedException NotSupported(System.Type type)
        => new($"'{type}' 은(는) acNET 의 Json 형식이 아닙니다. acNET 모델만 직렬화할 수 있습니다.");
}
