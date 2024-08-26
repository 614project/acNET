
using acNET.Type;

namespace acNET;

/// <summary>
/// solved.ac의 게시글입니다.
/// </summary>
public class Post : Jsonable
{
    /// <summary>
    /// 게시글의 아이디입니다.
    /// </summary>
    public string postId {get; set;}
    /// <summary>
    /// 게시글의 제목입니다.
    /// </summary>
    public string title {get; set;}
    /// <summary>
    /// 게시글의 내용입니다.
    /// </summary>
    public string content {get; set;}
    /// <summary>
    /// 게시글이 작성된 언어입니다.
    /// </summary>
    public string language {get; set;}
    /// <summary>
    /// 게시글 내용의 타입입니다.
    /// </summary>
    public string type { get; set; }
}