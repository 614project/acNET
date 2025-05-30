using Newtonsoft.Json;

namespace AcNET.Emoticon;

/// <summary>
/// solved.ac의 이모티콘 정보
/// </summary>
public class SolvedEmoticon
{
    /// <summary>
    /// 고유 식별자입니다.
    /// </summary>
    [JsonProperty("emoticonId")]
    public string EmoticonId { get; set; } = string.Empty;
    /// <summary>
    /// 사진으로 가는 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("emoticonUrl")]
    public string EmoticonUrl { get; set; } = string.Empty;
    /// <summary>
    /// 한국어 이름입니다.
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;
    /// <summary>
    /// 해금 여부입니다.
    /// </summary>
    [JsonProperty("unlocked")]
    public bool Unlocked { get; set; }
}
