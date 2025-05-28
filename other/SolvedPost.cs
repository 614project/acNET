using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET;

/// <summary>
/// solved.ac의 게시글입니다.
/// </summary>
public class SolvedPost : Jsonable
{
    /// <summary>
    /// 게시글의 아이디입니다.
    /// </summary>
    [JsonProperty("postId")]
    public string PostId { get; set; } = string.Empty;
    /// <summary>
    /// 게시글의 제목입니다.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// 게시글의 내용입니다.
    /// </summary>
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
    /// <summary>
    /// 게시글이 작성된 언어입니다.
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// 게시글 내용의 타입입니다.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;
}