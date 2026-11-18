using System.Text.Json;
using AcNET.Problem;
using AcNET.Type;

namespace AcNET.User;

/// <summary>
/// 스트릭 그래프용 날짜별 활동입니다. (<see cref="SolvedAPI.GetUserActivityDaysAsync"/>)
/// </summary>
public class SolvedActivityDays : Jsonable
{
    /// <summary>조회 시작일입니다. (yyyy-MM-dd)</summary>
    public string From { get; set; } = string.Empty;
    /// <summary>조회 종료일입니다. (yyyy-MM-dd)</summary>
    public string To { get; set; } = string.Empty;
    /// <summary>활동이 있는 날만 담긴 목록입니다. 빈 날은 직접 채워야 합니다.</summary>
    public List<SolvedActivityDay> Days { get; set; } = [];
    /// <summary>기간 중 하루 최대 푼 문제 수입니다.</summary>
    public int MaxSolvedCount { get; set; }
    /// <summary>현재 스트릭입니다.</summary>
    public int CurrentStreak { get; set; }
    /// <summary>최대 스트릭입니다.</summary>
    public int MaxStreak { get; set; }
    /// <summary>스트릭 그래프 테마입니다.</summary>
    public string? Theme { get; set; }
    /// <summary>처음 활동한 날입니다.</summary>
    public string? FirstActiveDay { get; set; }
    /// <summary>진행 중인 스트릭 휴가입니다.</summary>
    public SolvedStreakVacation? Vacation { get; set; }
    /// <summary>기간 중의 스트릭 휴가들입니다.</summary>
    public List<SolvedVacationRange> Vacations { get; set; } = [];
    /// <summary>오늘 스트릭이 채워졌는지 여부입니다.</summary>
    public bool CountedToday { get; set; }
    /// <summary>어제 스트릭 프리즈가 사용되었는지 여부입니다.</summary>
    public bool FrozenYesterday { get; set; }
}

/// <summary>
/// 하루의 활동입니다.
/// </summary>
public class SolvedActivityDay : Jsonable
{
    /// <summary>날짜입니다. (yyyy-MM-dd)</summary>
    public string Day { get; set; } = string.Empty;
    /// <summary>푼 문제 수입니다.</summary>
    public int SolvedCount { get; set; }
    /// <summary>푼 문제 중 가장 높은 레벨입니다.</summary>
    public int MaxSolvedLevel { get; set; }
    /// <summary>레이팅 변화량입니다.</summary>
    public int RatingDelta { get; set; }
    /// <summary>스트릭 프리즈를 사용했는지 여부입니다.</summary>
    public bool FreezeUsed { get; set; }
    /// <summary>스트릭을 복구했는지 여부입니다.</summary>
    public bool StreakRepaired { get; set; }
    /// <summary>날짜를 C# DateTime 으로 가져옵니다.</summary>
    public DateTime? GetDay => SolvedConverter.ToDay(Day);
}

/// <summary>
/// 스트릭 휴가 기간입니다.
/// </summary>
public class SolvedVacationRange : Jsonable
{
    /// <summary>시작일입니다.</summary>
    public string FromDay { get; set; } = string.Empty;
    /// <summary>종료일입니다. 진행 중이면 null 입니다.</summary>
    public string? UntilDay { get; set; }
}

/// <summary>
/// 활동 피드 한 페이지입니다. (<see cref="SolvedAPI.GetUserActivityFeedAsync"/>)
/// </summary>
public class SolvedActivityFeed : Jsonable
{
    /// <summary>이벤트 목록입니다. (최신순)</summary>
    public List<SolvedActivityEvent> Events { get; set; } = [];
    /// <summary>다음 페이지를 가져올 때 before 에 넣을 값입니다. 마지막이면 null 입니다.</summary>
    public string? NextCursor { get; set; }
}

/// <summary>
/// 활동 피드의 이벤트입니다.
/// </summary>
public class SolvedActivityEvent : Jsonable
{
    /// <summary>이벤트 ID 입니다.</summary>
    public long Id { get; set; }
    /// <summary>이벤트 종류입니다.</summary>
    public SolvedActivityType Type { get; set; }
    /// <summary>일어난 시각입니다.</summary>
    public string OccurredAt { get; set; } = string.Empty;
    /// <summary>관련 문제입니다.</summary>
    public SolvedProblemSummary? Problem { get; set; }
    /// <summary>사유입니다.</summary>
    public string? Reason { get; set; }
    /// <summary>기념 이벤트의 수치입니다.</summary>
    public int? Milestone { get; set; }
    /// <summary>이벤트 종류별 부가 정보입니다. (형식이 정해져 있지 않습니다)</summary>
    public JsonElement? Payload { get; set; }
}

/// <summary>
/// 사용자의 기여 목록 한 페이지입니다. (<see cref="SolvedAPI.GetUserContributionsAsync"/>)
/// </summary>
public class SolvedContributionPage : Jsonable
{
    /// <summary>기여 목록입니다.</summary>
    public List<SolvedContribution> Items { get; set; } = [];
    /// <summary>전체 기여 수입니다.</summary>
    public int Total { get; set; }
    /// <summary>숨겨진 기여 수입니다.</summary>
    public int HiddenCount { get; set; }
    /// <summary>현재 페이지입니다.</summary>
    public int Page { get; set; }
    /// <summary>페이지 크기입니다.</summary>
    public int PageSize { get; set; }
    /// <summary>정렬 기준입니다. ("recent", "helpful")</summary>
    public string Sort { get; set; } = string.Empty;
}

/// <summary>
/// 기여와 그 기여로 받은 평판입니다.
/// </summary>
public class SolvedContribution : Jsonable
{
    /// <summary>기여 ID 입니다.</summary>
    public long VoteId { get; set; }
    /// <summary>기여한 문제입니다.</summary>
    public SolvedProblemSummary Problem { get; set; } = new();
    /// <summary>기여한 시각입니다.</summary>
    public string VotedAt { get; set; } = string.Empty;
    /// <summary>받은 평판 점수입니다.</summary>
    public int Points { get; set; }
    /// <summary>문제에 몇 번째로 기여했는지입니다.</summary>
    public int? Place { get; set; }
    /// <summary>기여 내용입니다.</summary>
    public SolvedVote Vote { get; set; } = new();
}

/// <summary>
/// 사용자의 평판입니다. (<see cref="SolvedAPI.GetUserReputationAsync"/>)
/// </summary>
public class SolvedReputation : Jsonable
{
    /// <summary>평판입니다.</summary>
    public int Reputation { get; set; }
    /// <summary>점수를 받은 기여 수입니다.</summary>
    public int ScoringCount { get; set; }
    /// <summary>도움이 돼요를 받은 기여 수입니다.</summary>
    public int HelpfulCount { get; set; }
    /// <summary>보증받은 기여 수입니다.</summary>
    public int VouchedCount { get; set; }
    /// <summary>평판 기록입니다.</summary>
    public List<SolvedDailyValue> History { get; set; } = [];
    /// <summary>오늘 받은 평판입니다. (본인에게만 의미 있음)</summary>
    public SolvedReputationToday Today { get; set; } = new();
}

/// <summary>
/// 오늘 받은 평판입니다.
/// </summary>
public class SolvedReputationToday : Jsonable
{
    /// <summary>오늘의 기여 수와 최대치입니다.</summary>
    public SolvedReputationContributions Contributions { get; set; } = new();
    /// <summary>오늘 도움이 돼요로 받은 평판과 최대치입니다.</summary>
    public SolvedReputationHelpful Helpful { get; set; } = new();
}

/// <summary>
/// 오늘의 기여 수와 최대치입니다.
/// </summary>
public class SolvedReputationContributions : Jsonable
{
    /// <summary>오늘 점수를 받은 기여 수입니다.</summary>
    public int Count { get; set; }
    /// <summary>하루에 점수를 받을 수 있는 최대 기여 수입니다.</summary>
    public int Max { get; set; }
}

/// <summary>
/// 오늘 도움이 돼요로 받은 평판과 최대치입니다.
/// </summary>
public class SolvedReputationHelpful : Jsonable
{
    /// <summary>오늘 받은 평판입니다.</summary>
    public int Earned { get; set; }
    /// <summary>하루 최대치입니다.</summary>
    public int Cap { get; set; }
}

/// <summary>
/// 날짜별 값입니다. (레이팅, 평판 기록)
/// </summary>
public class SolvedDailyValue : Jsonable
{
    /// <summary>날짜입니다. (yyyy-MM-dd)</summary>
    public string Day { get; set; } = string.Empty;
    /// <summary>그 날이 끝났을 때의 값입니다.</summary>
    public int Value { get; set; }
    /// <summary>날짜를 C# DateTime 으로 가져옵니다.</summary>
    public DateTime? GetDay => SolvedConverter.ToDay(Day);
}

/// <summary>
/// 평판 변동 기록입니다. (<see cref="SolvedAPI.GetUserReputationLogAsync"/>)
/// </summary>
public class SolvedReputationLog : Jsonable
{
    /// <summary>날짜별 변동입니다. (최신순)</summary>
    public List<SolvedReputationLogDay> Days { get; set; } = [];
    /// <summary>처음 평판이 변동된 날입니다.</summary>
    public string? FirstDay { get; set; }
}

/// <summary>
/// 하루의 평판 변동입니다.
/// </summary>
public class SolvedReputationLogDay : Jsonable
{
    /// <summary>날짜입니다.</summary>
    public string Day { get; set; } = string.Empty;
    /// <summary>변동량입니다.</summary>
    public int Change { get; set; }
    /// <summary>변동 후 평판입니다.</summary>
    public int After { get; set; }
    /// <summary>하루 최대치에 걸려 받지 못한 평판입니다.</summary>
    public int Capped { get; set; }
    /// <summary>변동 항목입니다.</summary>
    public List<SolvedReputationLogItem> Items { get; set; } = [];
    /// <summary>나머지 항목을 합친 값입니다.</summary>
    public SolvedReputationLogOthers? Others { get; set; }
}

/// <summary>
/// 평판 변동 항목입니다.
/// </summary>
public class SolvedReputationLogItem : Jsonable
{
    /// <summary>종류입니다. ("contribution", "helpful", "unhelpful", "markedUnhelpful", "takedown")</summary>
    public string Kind { get; set; } = string.Empty;
    /// <summary>관련 문제입니다.</summary>
    public SolvedProblemSummary? Problem { get; set; }
    /// <summary>횟수입니다.</summary>
    public int Count { get; set; }
    /// <summary>점수입니다.</summary>
    public int Points { get; set; }
}

/// <summary>
/// 나머지 평판 변동 항목을 합친 값입니다.
/// </summary>
public class SolvedReputationLogOthers : Jsonable
{
    /// <summary>횟수입니다.</summary>
    public int Count { get; set; }
    /// <summary>점수입니다.</summary>
    public int Points { get; set; }
}
