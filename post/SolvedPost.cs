using System.Text.Json;
using AcNET.Type;

namespace AcNET.Post;

/// <summary>
/// 공지사항 또는 도움말 글입니다.
/// </summary>
public class SolvedPost : Jsonable
{
    /// <summary>게시판입니다. ("notice", "help")</summary>
    public string Board { get; init; } = string.Empty;
    /// <summary>글 경로입니다.</summary>
    public string Path { get; init; } = string.Empty;
    /// <summary>실제로 받은 글의 언어입니다. 요청한 언어와 다를 수 있습니다.</summary>
    public string Language { get; init; } = string.Empty;
    /// <summary>이 글을 볼 수 있는 언어들입니다.</summary>
    public List<string> Languages { get; init; } = [];
    /// <summary>제목입니다.</summary>
    public string Title { get; init; } = string.Empty;
    /// <summary>요약입니다.</summary>
    public string? Summary { get; init; }
    /// <summary>본문입니다. (Tiptap/ProseMirror 문서 Json)</summary>
    public JsonElement Content { get; init; }
    /// <summary>게시한 시각입니다.</summary>
    public string PublishedAt { get; init; } = string.Empty;
    /// <summary>수정한 시각입니다.</summary>
    public string UpdatedAt { get; init; } = string.Empty;
    /// <summary>고정된 글인지 여부입니다.</summary>
    public bool Pinned { get; init; }
    /// <summary>표지 이미지 주소입니다.</summary>
    public string? CoverImageUrl { get; init; }
    /// <summary>내용의 기준 날짜입니다.</summary>
    public string? BasisDate { get; init; }
    /// <summary>상위 글 경로입니다.</summary>
    public List<SolvedPostLink> Breadcrumbs { get; init; } = [];
    /// <summary>이전 글입니다.</summary>
    public SolvedPostLink? Previous { get; init; }
    /// <summary>다음 글입니다.</summary>
    public SolvedPostLink? Next { get; init; }
}

/// <summary>
/// 다른 글로 가는 링크입니다.
/// </summary>
public class SolvedPostLink : Jsonable
{
    /// <summary>글 경로입니다.</summary>
    public string Path { get; init; } = string.Empty;
    /// <summary>제목입니다.</summary>
    public string Title { get; init; } = string.Empty;
}

/// <summary>
/// 공지사항 목록의 글입니다.
/// </summary>
public class SolvedNoticeItem : Jsonable
{
    /// <summary>글 경로입니다. (<see cref="SolvedAPI.GetNoticeAsync"/>)</summary>
    public string Path { get; init; } = string.Empty;
    /// <summary>언어입니다.</summary>
    public string Language { get; init; } = string.Empty;
    /// <summary>제목입니다.</summary>
    public string Title { get; init; } = string.Empty;
    /// <summary>요약입니다.</summary>
    public string? Summary { get; init; }
    /// <summary>게시한 시각입니다.</summary>
    public string PublishedAt { get; init; } = string.Empty;
    /// <summary>고정된 글인지 여부입니다.</summary>
    public bool Pinned { get; init; }
    /// <summary>새 글인지 여부입니다.</summary>
    public bool IsNew { get; init; }
    /// <summary>표지 이미지 주소입니다.</summary>
    public string? CoverImageUrl { get; init; }
}

/// <summary>
/// 공지사항 목록 한 페이지입니다. (<see cref="SolvedAPI.GetNoticeListAsync"/>)
/// </summary>
public class SolvedNoticePage : Jsonable
{
    /// <summary>글 목록입니다. (고정 글 먼저, 그 다음 최신순)</summary>
    public List<SolvedNoticeItem> Items { get; init; } = [];
    /// <summary>전체 글 수입니다.</summary>
    public int Total { get; init; }
    /// <summary>현재 페이지입니다.</summary>
    public int Page { get; init; }
    /// <summary>페이지 크기입니다.</summary>
    public int PerPage { get; init; }
}

/// <summary>
/// 도움말 센터의 문서 트리 노드입니다.
/// </summary>
public class SolvedHelpTreeNode : Jsonable
{
    /// <summary>문서 경로입니다. (<see cref="SolvedAPI.GetHelpPageAsync"/>)</summary>
    public string Path { get; init; } = string.Empty;
    /// <summary>제목입니다.</summary>
    public string Title { get; init; } = string.Empty;
    /// <summary>내용이 있는 문서인지 여부입니다. (false 면 분류만 하는 노드)</summary>
    public bool HasPage { get; init; }
    /// <summary>하위 문서입니다.</summary>
    public List<SolvedHelpTreeNode> Children { get; init; } = [];
}

/// <summary>
/// 도움말 글입니다. (<see cref="SolvedAPI.GetHelpArticleAsync"/>)
/// </summary>
public class SolvedHelpArticle : Jsonable
{
    /// <summary>글의 slug 입니다.</summary>
    public string Slug { get; init; } = string.Empty;
    /// <summary>실제로 받은 글의 언어입니다.</summary>
    public string Language { get; init; } = string.Empty;
    /// <summary>제목입니다.</summary>
    public string Title { get; init; } = string.Empty;
    /// <summary>본문입니다. (Tiptap/ProseMirror 문서 Json)</summary>
    public JsonElement Content { get; init; }
    /// <summary>수정한 시각입니다.</summary>
    public string UpdatedAt { get; init; } = string.Empty;
}

/// <summary>
/// 정책 문서의 한 버전입니다. (<see cref="SolvedAPI.GetPolicyAsync"/>)
/// </summary>
public class SolvedPolicy : Jsonable
{
    /// <summary>정책입니다.</summary>
    public SolvedPolicyKind Slug { get; init; }
    /// <summary>버전입니다.</summary>
    public string Version { get; init; } = string.Empty;
    /// <summary>버전 표시 이름입니다. (예: "v3")</summary>
    public string Label { get; init; } = string.Empty;
    /// <summary>공지된 시각입니다.</summary>
    public string? AnnouncedAt { get; init; }
    /// <summary>시행 시각입니다.</summary>
    public string EffectiveAt { get; init; } = string.Empty;
    /// <summary>실제로 받은 문서의 언어입니다.</summary>
    public string Language { get; init; } = string.Empty;
    /// <summary>제목입니다.</summary>
    public string Title { get; init; } = string.Empty;
    /// <summary>본문입니다.</summary>
    public SolvedPolicyContent Content { get; init; } = new();
    /// <summary>개정 시각입니다.</summary>
    public string? RevisedAt { get; init; }
    /// <summary>지금 시행 중인 버전인지 여부입니다.</summary>
    public bool IsCurrent { get; init; }
    /// <summary>지금 시행 중인 버전의 표시 이름입니다.</summary>
    public string? CurrentLabel { get; init; }
    /// <summary>모든 버전입니다.</summary>
    public List<SolvedPolicyVersion> Versions { get; init; } = [];
}

/// <summary>
/// 정책 문서의 본문입니다. <see cref="Format"/> 에 따라 <see cref="Body"/> 나 <see cref="Doc"/> 중 하나가 채워집니다.
/// </summary>
public class SolvedPolicyContent : Jsonable
{
    /// <summary>본문 형식입니다. ("markdown", "tiptap")</summary>
    public string Format { get; init; } = string.Empty;
    /// <summary>Markdown 본문입니다. ("markdown")</summary>
    public string? Body { get; init; }
    /// <summary>Tiptap/ProseMirror 문서 Json 입니다. ("tiptap")</summary>
    public JsonElement? Doc { get; init; }
}

/// <summary>
/// 정책 문서의 버전 정보입니다.
/// </summary>
public class SolvedPolicyVersion : Jsonable
{
    /// <summary>버전입니다.</summary>
    public string Version { get; init; } = string.Empty;
    /// <summary>버전 표시 이름입니다.</summary>
    public string Label { get; init; } = string.Empty;
    /// <summary>공지된 시각입니다.</summary>
    public string? AnnouncedAt { get; init; }
    /// <summary>시행 시각입니다.</summary>
    public string EffectiveAt { get; init; } = string.Empty;
}
