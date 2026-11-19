using AcNET.Type;
using AcNET.User;

namespace AcNET.Problem;

/// <summary>
/// 문제 난이도 기여입니다.
/// </summary>
public class SolvedVote : Jsonable
{
    /// <summary>
    /// 기여 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 기여한 사용자입니다. 숨겨진 경우 null 입니다.
    /// </summary>
    public SolvedUserRef? User { get; set; }
    /// <summary>
    /// 기여한 시각입니다.
    /// </summary>
    public string VotedAt { get; set; } = string.Empty;
    /// <summary>
    /// 기여한 난이도(레벨)입니다.
    /// </summary>
    public int? Level { get; set; }
    /// <summary>
    /// 난이도 계산에서의 가중치입니다.
    /// </summary>
    public double Weight { get; set; }
    /// <summary>
    /// 무효화된 기여인지 여부입니다.
    /// </summary>
    public bool Invalidated { get; set; }
    /// <summary>
    /// 난이도 계산에서 빠진 기여인지 여부입니다.
    /// </summary>
    public bool NotInCalculation { get; set; }
    /// <summary>
    /// 기여 정지된 사용자의 기여인지 여부입니다.
    /// </summary>
    public bool Banned { get; set; }
    /// <summary>
    /// 더 새로운 기여로 대체되었는지 여부입니다.
    /// </summary>
    public bool Superseded { get; set; }
    /// <summary>
    /// 이상치로 잘린 기여인지 여부입니다.
    /// </summary>
    public bool Cutoff { get; set; }
    /// <summary>
    /// 기여 코멘트입니다.
    /// </summary>
    public string? Comment { get; set; }
    /// <summary>
    /// 코멘트의 언어입니다.
    /// </summary>
    public string? CommentLanguage { get; set; }
    /// <summary>
    /// 함께 단 이모티콘의 ID 입니다.
    /// </summary>
    public string? EmoticonId { get; set; }
    /// <summary>
    /// 함께 단 이모티콘입니다.
    /// </summary>
    public SolvedVoteEmoticon? Emoticon { get; set; }
    /// <summary>
    /// 기여가 들어온 경로입니다.
    /// </summary>
    public string Source { get; set; } = string.Empty;
    /// <summary>
    /// 기여한 태그입니다.
    /// </summary>
    public List<SolvedVoteTag> Tags { get; set; } = [];
    /// <summary>
    /// 표준 문제 기여인지 여부입니다.
    /// </summary>
    public bool IsStandard { get; set; }
    /// <summary>
    /// 도움이 돼요 / 도움이 안 돼요 평가입니다.
    /// </summary>
    public SolvedVoteMarks? Marks { get; set; }
    /// <summary>
    /// 기여한 난이도의 티어 이름을 가져옵니다.
    /// </summary>
    public string? GetTierName => SolvedConverter.LevelName(Level);
}

/// <summary>
/// 기여에 단 이모티콘입니다.
/// </summary>
public class SolvedVoteEmoticon : Jsonable
{
    /// <summary>
    /// 이모티콘의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 이모티콘 이미지 주소입니다.
    /// </summary>
    public string Url { get; set; } = string.Empty;
}

/// <summary>
/// 기여한 태그입니다.
/// </summary>
public class SolvedVoteTag : Jsonable
{
    /// <summary>
    /// 태그입니다. 지금은 없는 태그면 null 입니다.
    /// </summary>
    public SolvedTagRef? Tag { get; set; }
    /// <summary>
    /// 지금은 없는 옛 태그의 key 입니다.
    /// </summary>
    public string? LegacyKey { get; set; }
}

/// <summary>
/// 기여에 대한 도움이 돼요 / 도움이 안 돼요 평가입니다.
/// </summary>
public class SolvedVoteMarks : Jsonable
{
    /// <summary>
    /// 평가 점수입니다.
    /// </summary>
    public int Score { get; set; }
    /// <summary>
    /// 평가 수입니다. 공개되지 않으면 null 입니다.
    /// </summary>
    public SolvedVoteMarkCounts? Counts { get; set; }
    /// <summary>
    /// 요청한 사용자의 평가입니다. ("helpful", "unhelpful")
    /// </summary>
    public string? Mine { get; set; }
    /// <summary>
    /// 요청한 사용자 본인의 기여인지 여부입니다.
    /// </summary>
    public bool Own { get; set; }
}

/// <summary>
/// 평가 수입니다.
/// </summary>
public class SolvedVoteMarkCounts : Jsonable
{
    /// <summary>
    /// 도움이 돼요 수입니다.
    /// </summary>
    public int Helpful { get; set; }
    /// <summary>
    /// 도움이 안 돼요 수입니다.
    /// </summary>
    public int Unhelpful { get; set; }
}

/// <summary>
/// 문제의 난이도 기여 목록 한 페이지입니다. (페이지 크기 30)
/// </summary>
public class SolvedVotePage : Jsonable
{
    /// <summary>
    /// 기여 목록입니다.
    /// </summary>
    public List<SolvedVote> Items { get; set; } = [];
    /// <summary>
    /// 전체 기여 수입니다.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 현재 페이지입니다.
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// 페이지 크기입니다.
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// 정렬 기준입니다. ("recent", "level", "weight")
    /// </summary>
    public string Sort { get; set; } = string.Empty;
    /// <summary>
    /// 요청한 사용자가 기여를 평가할 수 없는 이유입니다. 평가할 수 있으면 null 입니다.
    /// ("UNAUTHENTICATED", "REPUTATION_TOO_LOW", "TIER_TOO_LOW", "ACCOUNT_TOO_NEW", "NOT_SOLVED", "BANNED")
    /// </summary>
    public string? MarkRefusal { get; set; }
    /// <summary>
    /// 도움이 안 돼요 평가가 잠겨있는지 여부입니다.
    /// </summary>
    public bool UnhelpfulLocked { get; set; }
}

/// <summary>
/// 문제의 난이도 기여 요약입니다. (그래프용)
/// </summary>
public class SolvedVoteSummary : Jsonable
{
    /// <summary>
    /// 시간순 기여 점들입니다.
    /// </summary>
    public List<SolvedVotePoint> Items { get; set; } = [];
    /// <summary>
    /// 태그별 기여 분포입니다.
    /// </summary>
    public List<SolvedVoteTagDistribution> TagDistribution { get; set; } = [];
}

/// <summary>
/// 기여 그래프의 한 점입니다.
/// </summary>
public class SolvedVotePoint : Jsonable
{
    /// <summary>
    /// 기여한 시각입니다.
    /// </summary>
    public string VotedAt { get; set; } = string.Empty;
    /// <summary>
    /// 기여한 난이도(레벨)입니다.
    /// </summary>
    public int? Level { get; set; }
    /// <summary>
    /// 가중치입니다.
    /// </summary>
    public double Weight { get; set; }
    /// <summary>
    /// 무효화된 기여인지 여부입니다.
    /// </summary>
    public bool Invalidated { get; set; }
    /// <summary>
    /// 난이도 계산에서 빠진 기여인지 여부입니다.
    /// </summary>
    public bool NotInCalculation { get; set; }
    /// <summary>
    /// 기여 정지된 사용자의 기여인지 여부입니다.
    /// </summary>
    public bool Banned { get; set; }
    /// <summary>
    /// 이상치로 잘린 기여인지 여부입니다.
    /// </summary>
    public bool Cutoff { get; set; }
}

/// <summary>
/// 태그별 기여 분포입니다.
/// </summary>
public class SolvedVoteTagDistribution : Jsonable
{
    /// <summary>
    /// 태그입니다.
    /// </summary>
    public SolvedTagRef Tag { get; set; } = new();
    /// <summary>
    /// 이 태그를 단 기여 수(가중치 반영)입니다.
    /// </summary>
    public double Count { get; set; }
    /// <summary>
    /// 분모가 되는 전체 기여 수(가중치 반영)입니다.
    /// </summary>
    public double Denominator { get; set; }
}

/// <summary>
/// 문제 신고입니다.
/// </summary>
public class SolvedProblemReport : Jsonable
{
    /// <summary>
    /// 신고 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 신고한 사용자입니다. 숨겨진 경우 null 입니다.
    /// </summary>
    public SolvedUserRef? User { get; set; }
    /// <summary>
    /// 신고 사유입니다.
    /// </summary>
    public SolvedProblemReportReason Reason { get; set; } = new();
    /// <summary>
    /// 같은 문제로 신고된 문제들입니다.
    /// </summary>
    public List<SolvedProblemRef> SameProblems { get; set; } = [];
    /// <summary>
    /// 신고 내용입니다.
    /// </summary>
    public string Comment { get; set; } = string.Empty;
    /// <summary>
    /// 처리 상태입니다. ("pending", "accepted", "rejected", "resolved")
    /// </summary>
    public string Status { get; set; } = string.Empty;
    /// <summary>
    /// 운영진의 처리 내역입니다.
    /// </summary>
    public SolvedProblemReportStatusChange? StatusChange { get; set; }
    /// <summary>
    /// 신고한 시각입니다.
    /// </summary>
    public string CreatedAt { get; set; } = string.Empty;
}

/// <summary>
/// 문제 신고 사유입니다. <see cref="Type"/> 에 따라 채워지는 속성이 다릅니다.
/// </summary>
public class SolvedProblemReportReason : Jsonable
{
    /// <summary>
    /// 사유 종류입니다. ("same-problem", "issue", "issue-resolved")
    /// </summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// 같은 문제라고 신고한 문제 번호들입니다. ("same-problem")
    /// </summary>
    public List<int>? ProblemIds { get; set; }
    /// <summary>
    /// 이슈 주소입니다. ("issue")
    /// </summary>
    public string? Url { get; set; }
    /// <summary>
    /// 해결된 신고의 ID 입니다. ("issue-resolved")
    /// </summary>
    public long? ProblemReportId { get; set; }
}

/// <summary>
/// 문제 신고의 처리 내역입니다.
/// </summary>
public class SolvedProblemReportStatusChange : Jsonable
{
    /// <summary>
    /// 처리한 운영진입니다.
    /// </summary>
    public SolvedUserRef? By { get; set; }
    /// <summary>
    /// 처리한 시각입니다.
    /// </summary>
    public string At { get; set; } = string.Empty;
    /// <summary>
    /// 처리 코멘트입니다.
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 문제 신고 목록 한 페이지입니다. (페이지 크기 10)
/// </summary>
public class SolvedProblemReportPage : Jsonable
{
    /// <summary>
    /// 신고 목록입니다.
    /// </summary>
    public List<SolvedProblemReport> Items { get; set; } = [];
    /// <summary>
    /// 전체 신고 수입니다.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 현재 페이지입니다.
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// 페이지 크기입니다.
    /// </summary>
    public int PageSize { get; set; }
}
