using AcNET.Type;

namespace AcNET.Problem;

/// <summary>
/// 다른 응답에 포함되는 가장 작은 문제 정보입니다. (번호, 난이도, 풀이 상태)
/// </summary>
public class SolvedProblemRef : Jsonable
{
    /// <summary>
    /// 문제 번호입니다.
    /// </summary>
    public int ProblemId { get; set; }
    /// <summary>
    /// 하위 문제 ID 입니다. 일반 문제는 빈 문자열입니다.
    /// </summary>
    public string SubId { get; set; } = string.Empty;
    /// <summary>
    /// 각 저지에서 이 문제를 부르는 번호입니다.
    /// </summary>
    public List<SolvedJudgeId> JudgeIds { get; set; } = [];
    /// <summary>
    /// 문제의 레벨(티어)값입니다. (0: Unrated, 1: Bronze V, ..., 30: Ruby I)
    /// </summary>
    public int Level { get; set; }
    /// <summary>
    /// 난이도 계산에 반영된 기여 수입니다.
    /// </summary>
    public int EffectiveVoteCount { get; set; }
    /// <summary>
    /// 레이팅을 주지 않는 문제인지 여부입니다.
    /// </summary>
    public bool GivesNoRating { get; set; }
    /// <summary>
    /// 표준 문제인지 여부입니다.
    /// </summary>
    public bool IsStandard { get; set; }
    /// <summary>
    /// 요청한 사용자의 풀이 상태입니다. 사용자 토큰에 <c>solves</c> scope 가 없거나 시도하지 않았다면 null 입니다.
    /// </summary>
    public SolvedUserStatus? UserStatus { get; set; }
    /// <summary>
    /// 문제 레벨에 맞는 티어 이름을 가져옵니다.
    /// </summary>
    public string? GetTierName => SolvedConverter.LevelName(Level);
    /// <summary>
    /// 문제 레벨에 맞는 티어 색깔을 가져옵니다. (7자리 Hex 코드입니다.)
    /// </summary>
    public string GetTierColor => SolvedConverter.LevelColor(Level);
}

/// <summary>
/// 제목이 포함된 문제 정보입니다.
/// </summary>
public class SolvedProblemSummary : SolvedProblemRef
{
    /// <summary>
    /// 언어별 문제 제목입니다.
    /// </summary>
    public List<SolvedProblemTitle> Titles { get; set; } = [];

    /// <summary>
    /// 해당 언어의 제목을 가져옵니다. 없으면 원문 제목, 그것도 없으면 첫 번째 제목을 가져옵니다.
    /// </summary>
    /// <param name="language">언어 (예: "ko", "en")</param>
    /// <returns>제목이 하나도 없으면 null</returns>
    public string? GetTitle(string? language = null)
        => (Titles.Find(t => t.Language == language) ?? Titles.Find(t => t.IsOriginal) ?? Titles.FirstOrDefault())?.Title;
}

/// <summary>
/// 문제 정보입니다. (문제 목록, 검색 결과)
/// </summary>
public class SolvedProblem : SolvedProblemSummary
{
    /// <summary>
    /// solved.ac 내부 문제 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 기여된 난이도의 평균입니다.
    /// </summary>
    public double LevelAverage { get; set; }
    /// <summary>
    /// 기여된 난이도의 표준편차입니다.
    /// </summary>
    public double LevelStdev { get; set; }
    /// <summary>
    /// 난이도 기여자의 수입니다.
    /// </summary>
    public int VotedUserCount { get; set; }
    /// <summary>
    /// 맞은 사람 수입니다.
    /// </summary>
    public int SolvedUserCount { get; set; }
    /// <summary>
    /// 레이팅을 주지 않는 이유입니다. ("unofficial", "legacy", "staff", "unsettled")
    /// </summary>
    public List<string> NoRatingReasons { get; set; } = [];
    /// <summary>
    /// 레이팅에 대한 경고입니다. (같은 문제, 검색 가능 등)
    /// </summary>
    public List<SolvedNoRatingWarning> NoRatingWarnings { get; set; } = [];
    /// <summary>
    /// 문제가 출제된 대회 목록입니다.
    /// </summary>
    public List<SolvedProblemContest> Contests { get; set; } = [];
    /// <summary>
    /// 태그 목록입니다. 태그 이름은 <see cref="SolvedAPI.GetTagListAsync"/> 로 가져온 목록에서 key 로 찾을 수 있습니다.
    /// </summary>
    public List<SolvedTagRef> Tags { get; set; } = [];
    /// <summary>
    /// 이 문제가 들어있는 요청한 사용자의 북마크 폴더입니다. 목록 응답에서만 채워집니다.
    /// </summary>
    public List<SolvedFolderRef> ViewerFolders { get; set; } = [];
}

/// <summary>
/// 문제 상세 정보입니다. (<see cref="SolvedAPI.GetProblemAsync"/>)
/// </summary>
public class SolvedProblemDetail : SolvedProblem
{
    /// <summary>
    /// 각 저지의 문제 페이지 링크입니다.
    /// </summary>
    public List<SolvedOjLink> OjLinks { get; set; } = [];
}

/// <summary>
/// 한 저지에서 문제를 부르는 번호입니다.
/// </summary>
public class SolvedJudgeId : Jsonable
{
    /// <summary>
    /// 저지 ID 입니다. (예: "boj")
    /// </summary>
    public string OjId { get; set; } = string.Empty;
    /// <summary>
    /// 그 저지의 문제 번호입니다.
    /// </summary>
    public string ProblemId { get; set; } = string.Empty;
    /// <summary>
    /// 그 저지의 하위 문제 ID 입니다.
    /// </summary>
    public string SubId { get; set; } = string.Empty;
}

/// <summary>
/// 문제의 제목에 대한 정보
/// </summary>
public class SolvedProblemTitle : Jsonable
{
    /// <summary>
    /// 언어입니다.
    /// </summary>
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// 문제의 제목입니다.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// 원문 제목인지 여부입니다.
    /// </summary>
    public bool IsOriginal { get; set; }
    /// <summary>
    /// 제목에 LaTeX 수식이 들어있는지 여부입니다.
    /// </summary>
    public bool Latex { get; set; }
    /// <summary>
    /// 제목에 HTML 이 들어있는지 여부입니다.
    /// </summary>
    public bool Html { get; set; }
}

/// <summary>
/// 문제가 출제된 대회입니다.
/// </summary>
public class SolvedProblemContest : Jsonable
{
    /// <summary>
    /// 대회 ID 입니다. (<see cref="SolvedAPI.GetSourceContestAsync"/>)
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 대회 안에서의 문제 표시 번호입니다. (예: "A")
    /// </summary>
    public string DisplayId { get; set; } = string.Empty;
    /// <summary>
    /// 대회 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 대회 이름의 약어입니다.
    /// </summary>
    public string? Abbreviation { get; set; }
}

/// <summary>
/// 다른 응답에 포함되는 태그 정보입니다.
/// </summary>
public class SolvedTagRef : Jsonable
{
    /// <summary>
    /// 태그 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 태그의 key 입니다. (예: "dp")
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 메타 태그인지 여부입니다.
    /// </summary>
    public bool IsMeta { get; set; }
}

/// <summary>
/// 다른 응답에 포함되는 북마크 폴더 정보입니다.
/// </summary>
public class SolvedFolderRef : Jsonable
{
    /// <summary>
    /// 폴더의 slug 입니다.
    /// </summary>
    public string Slug { get; set; } = string.Empty;
    /// <summary>
    /// 폴더 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 폴더 색입니다.
    /// </summary>
    public SolvedFolderColor? Color { get; set; }
}

/// <summary>
/// 저지의 문제 페이지 링크입니다.
/// </summary>
public class SolvedOjLink : Jsonable
{
    /// <summary>
    /// 저지 ID 입니다.
    /// </summary>
    public string OjId { get; set; } = string.Empty;
    /// <summary>
    /// 저지 이름입니다.
    /// </summary>
    public string OjName { get; set; } = string.Empty;
    /// <summary>
    /// 그 저지의 문제 번호입니다.
    /// </summary>
    public string NativeProblemId { get; set; } = string.Empty;
    /// <summary>
    /// 문제 페이지 주소입니다.
    /// </summary>
    public string Url { get; set; } = string.Empty;
    /// <summary>
    /// 저지 로고 주소입니다.
    /// </summary>
    public string? LogoUrl { get; set; }
}

/// <summary>
/// 레이팅에 대한 경고입니다.
/// </summary>
public class SolvedNoRatingWarning : Jsonable
{
    /// <summary>
    /// 경고 종류입니다. ("same", "searchable", "wrong-inputs", "insufficient-conditions", "no-special-judge", "wrong-conditions", "wrong-statement")
    /// </summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// 관련된 문제입니다. (같은 문제 등)
    /// </summary>
    public SolvedProblemRef? Problem { get; set; }
}
