using AcNET.Problem;
using AcNET.Type;

namespace AcNET.Source;

/// <summary>
/// 출처 그룹입니다. (대회 시리즈, 기관 등)
/// </summary>
public class SolvedSourceGroup : Jsonable
{
    /// <summary>그룹 ID 입니다. (<see cref="SolvedAPI.GetSourceGroupAsync"/>)</summary>
    public long Id { get; init; }
    /// <summary>그룹 이름입니다.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>그룹 이름의 약어입니다.</summary>
    public string? Abbreviation { get; init; }
    /// <summary>하위 그룹 수입니다.</summary>
    public int GroupCount { get; init; }
    /// <summary>대회 수입니다.</summary>
    public int ContestCount { get; init; }
    /// <summary>문제 수입니다.</summary>
    public int? ProblemCount { get; init; }
    /// <summary>요청한 사용자가 푼 문제 수입니다.</summary>
    public int? SolvedCount { get; init; }
    /// <summary>난이도 분포입니다.</summary>
    public SolvedSourceLevel? Level { get; init; }
    /// <summary>문제 난이도 순서입니다. ("ascending", "random", "mixed")</summary>
    public string? Order { get; init; }
}

/// <summary>
/// 대회입니다.
/// </summary>
public class SolvedSourceContest : Jsonable
{
    /// <summary>대회 ID 입니다. (<see cref="SolvedAPI.GetSourceContestAsync"/>)</summary>
    public long Id { get; init; }
    /// <summary>대회 이름입니다.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>대회 이름의 약어입니다.</summary>
    public string? Abbreviation { get; init; }
    /// <summary>대회 시작 시각입니다.</summary>
    public string? StartsAt { get; init; }
    /// <summary>문제 수입니다.</summary>
    public int ProblemCount { get; init; }
    /// <summary>요청한 사용자가 푼 문제 수입니다.</summary>
    public int? SolvedCount { get; init; }
    /// <summary>난이도 분포입니다.</summary>
    public SolvedSourceLevel? Level { get; init; }
}

/// <summary>
/// 대회 상세 정보입니다.
/// </summary>
public class SolvedSourceContestDetail : SolvedSourceContest
{
    /// <summary>대회 페이지 주소입니다.</summary>
    public string? Url { get; init; }
    /// <summary>대회 장소입니다.</summary>
    public string? Venue { get; init; }
}

/// <summary>
/// 출처의 난이도 분포입니다.
/// </summary>
public class SolvedSourceLevel : Jsonable
{
    /// <summary>난이도 하위 25% 값입니다.</summary>
    public int Q25 { get; init; }
    /// <summary>난이도 중앙값입니다.</summary>
    public int Q50 { get; init; }
    /// <summary>난이도 상위 25% 값입니다.</summary>
    public int Q75 { get; init; }
    /// <summary>레벨별 문제 수입니다. (레벨로 색인)</summary>
    public List<int> TierCounts { get; init; } = [];
    /// <summary>난이도가 매겨진 문제 수입니다.</summary>
    public int RatedCount { get; init; }
    /// <summary>문제 난이도 순서입니다. ("ascending", "random", "mixed")</summary>
    public string? Order { get; init; }
    /// <summary>요청한 사용자의 진행도입니다.</summary>
    public SolvedSourceProgress? Progress { get; init; }
}

/// <summary>
/// 출처에 대한 요청한 사용자의 진행도입니다.
/// </summary>
public class SolvedSourceProgress : Jsonable
{
    /// <summary>푼 문제 수입니다.</summary>
    public int Solved { get; init; }
    /// <summary>각 사분위 난이도까지 모두 풀었는지 여부입니다.</summary>
    public SolvedSourceProgressThrough Through { get; init; } = new();
}

/// <summary>
/// 각 사분위 난이도까지 모두 풀었는지 여부입니다.
/// </summary>
public class SolvedSourceProgressThrough : Jsonable
{
    /// <summary>하위 25% 까지</summary>
    public bool Q25 { get; init; }
    /// <summary>중앙값까지</summary>
    public bool Q50 { get; init; }
    /// <summary>상위 25% 까지</summary>
    public bool Q75 { get; init; }
}

/// <summary>
/// 출처 경로의 한 단계입니다.
/// </summary>
public class SolvedSourcePathItem : Jsonable
{
    /// <summary>그룹 ID 입니다.</summary>
    public long Id { get; init; }
    /// <summary>그룹 이름입니다.</summary>
    public string Name { get; init; } = string.Empty;
}

/// <summary>
/// 저지의 출처 페이지 링크입니다.
/// </summary>
public class SolvedSourceLink : Jsonable
{
    /// <summary>저지 ID 입니다.</summary>
    public string OjId { get; init; } = string.Empty;
    /// <summary>저지 이름입니다.</summary>
    public string OjName { get; init; } = string.Empty;
    /// <summary>페이지 주소입니다.</summary>
    public string Url { get; init; } = string.Empty;
}

/// <summary>
/// 출처 그룹 페이지입니다. (<see cref="SolvedAPI.GetSourceGroupAsync"/>)
/// </summary>
public class SolvedSourceGroupPage : Jsonable
{
    /// <summary>그룹입니다.</summary>
    public SolvedSourceGroup Group { get; init; } = new();
    /// <summary>최상위부터 이 그룹까지의 경로입니다.</summary>
    public List<SolvedSourcePathItem> Path { get; init; } = [];
    /// <summary>저지 링크입니다.</summary>
    public List<SolvedSourceLink> Links { get; init; } = [];
    /// <summary>이 그룹의 문제를 찾는 검색 쿼리입니다.</summary>
    public string? Query { get; init; }
    /// <summary>하위 그룹입니다.</summary>
    public List<SolvedSourceGroup> Groups { get; init; } = [];
    /// <summary>대회 목록입니다.</summary>
    public List<SolvedSourceContest> Contests { get; init; } = [];
}

/// <summary>
/// 대회 페이지입니다. (<see cref="SolvedAPI.GetSourceContestAsync"/>)
/// </summary>
public class SolvedSourceContestPage : Jsonable
{
    /// <summary>대회입니다.</summary>
    public SolvedSourceContestDetail Contest { get; init; } = new();
    /// <summary>최상위부터 이 대회까지의 경로입니다.</summary>
    public List<SolvedSourcePathItem> Path { get; init; } = [];
    /// <summary>저지 링크입니다.</summary>
    public List<SolvedSourceLink> Links { get; init; } = [];
    /// <summary>이 대회의 문제를 찾는 검색 쿼리입니다.</summary>
    public string? Query { get; init; }
    /// <summary>대회 순서대로 정렬된 문제들입니다.</summary>
    public List<SolvedContestProblem> Problems { get; init; } = [];
}

/// <summary>
/// 대회의 문제입니다.
/// </summary>
public class SolvedContestProblem : Jsonable
{
    /// <summary>대회 안에서의 문제 번호입니다. (예: "A")</summary>
    public string NumberInContest { get; init; } = string.Empty;
    /// <summary>문제입니다.</summary>
    public SolvedProblem Problem { get; init; } = new();
}
