using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace AcNET;

/// <summary>
/// acNET 이 사용하는 System.Text.Json 설정입니다.
/// </summary>
internal static class SolvedJson
{
    /// <summary>
    /// 응답을 읽을 때 사용하는 설정입니다. (camelCase, 대소문자 무시)
    /// </summary>
    public static readonly JsonSerializerOptions Read = new(JsonSerializerDefaults.Web);

    /// <summary>
    /// 요청 본문을 쓸 때 사용하는 설정입니다. null 인 속성은 보내지 않습니다.
    /// </summary>
    public static readonly JsonSerializerOptions Write = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = Utf8Encoder,
    };

    /// <summary>
    /// <see cref="Type.Jsonable.ToJson"/> 출력용 설정입니다. 한글 등이 \uXXXX 로 바뀌지 않도록 합니다.
    /// </summary>
    public static readonly JsonSerializerOptions Display = new(JsonSerializerDefaults.Web)
    {
        Encoder = Utf8Encoder,
    };

    static JavaScriptEncoder Utf8Encoder => JavaScriptEncoder.Create(UnicodeRanges.All);
}
