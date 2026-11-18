using AcNET.Type;

namespace AcNET.Account;

/// <summary>
/// 사용자의 사이트 설정입니다. 기본값이 적용된 상태로 옵니다. (<see cref="SolvedAPI.GetAccountSettingsAsync"/>)
/// </summary>
public class SolvedAccountSettings : Jsonable
{
    /// <summary>테마입니다. ("system", "light", "dark")</summary>
    public string Theme { get; init; } = string.Empty;
    /// <summary>티어 아이콘 테마입니다. ("default", "legacy")</summary>
    public string IconTheme { get; init; } = string.Empty;
    /// <summary>푼 문제의 티어 아이콘 표시 방식입니다. ("show", "warn", "hide")</summary>
    public string IconSchemeSolved { get; init; } = string.Empty;
    /// <summary>풀지 않은 문제의 티어 아이콘 표시 방식입니다. ("show", "warn", "hide")</summary>
    public string IconSchemeUnsolved { get; init; } = string.Empty;
    /// <summary>마우스를 올렸을 때 티어 아이콘 표시 방식입니다. ("show", "warn", "hide")</summary>
    public string IconSchemeHover { get; init; } = string.Empty;
    /// <summary>경고 기준 방식입니다. ("relative", "absolute")</summary>
    public string IconWarnMode { get; init; } = string.Empty;
    /// <summary>상대 경고 기준(내 티어와의 차이)입니다.</summary>
    public int IconWarnRelative { get; init; }
    /// <summary>절대 경고 기준(레벨)입니다.</summary>
    public int IconWarnAbsolute { get; init; }
    /// <summary>Unrated 문제를 항상 표시할지 여부입니다.</summary>
    public bool AlwaysShowUnrated { get; init; }
    /// <summary>난이도를 매길 수 없는 문제를 항상 표시할지 여부입니다.</summary>
    public bool AlwaysShowUnratable { get; init; }
    /// <summary>문제 번호 표시 방식입니다. ("judge", "solvedac")</summary>
    public string ProblemIdDisplay { get; init; } = string.Empty;
    /// <summary>저지 표시 순서입니다.</summary>
    public List<string> JudgeOrder { get; init; } = [];
    /// <summary>맞은 사람 수 표시 방식입니다. ("show", "solved-only", "hide")</summary>
    public string ShowSolvedUserCount { get; init; } = string.Empty;
    /// <summary>기여자 수 표시 방식입니다. ("show", "solved-only", "hide")</summary>
    public string ShowVotedUserCount { get; init; } = string.Empty;
    /// <summary>태그 버튼 표시 방식입니다. ("show", "solved-only", "hide")</summary>
    public string ShowTagButton { get; init; } = string.Empty;
    /// <summary>태그 표시 언어입니다. ("identifier", "ko", "en", "ja")</summary>
    public string TagDisplayLanguage { get; init; } = string.Empty;
    /// <summary>문제 목록 기본 정렬입니다. ("id", "level", "solved", "average_try", "random")</summary>
    public string ProblemSort { get; init; } = string.Empty;
    /// <summary>문제 목록 기본 정렬 방향입니다. ("asc", "desc")</summary>
    public string ProblemSortDirection { get; init; } = string.Empty;
    /// <summary>원문 제목을 표시할지 여부입니다.</summary>
    public bool ShowOriginalTitle { get; init; }
    /// <summary>원래 대회 이름을 표시할지 여부입니다.</summary>
    public bool ShowOriginalContest { get; init; }
    /// <summary>모두 푼 출처의 난이도 표시 방식입니다. ("show", "hide")</summary>
    public string SourceLevelAllSolved { get; init; } = string.Empty;
    /// <summary>일부 푼 출처의 난이도 표시 방식입니다. ("show", "partial-if-ascending", "partial", "hide")</summary>
    public string SourceLevelPartlySolved { get; init; } = string.Empty;
    /// <summary>풀지 않은 출처의 난이도 표시 방식입니다. ("show", "hide")</summary>
    public string SourceLevelUnsolved { get; init; } = string.Empty;
    /// <summary>마우스를 올렸을 때 출처 난이도 표시 방식입니다. ("show", "hide")</summary>
    public string SourceLevelHover { get; init; } = string.Empty;
    /// <summary>출처 난이도 표시 모양입니다. ("dots", "gradient", "none")</summary>
    public string SourceLevelDisplay { get; init; } = string.Empty;
    /// <summary>출처 정렬 기준입니다. ("default", "name", "size", "p25", "p50", "p75")</summary>
    public string SourceSort { get; init; } = string.Empty;
    /// <summary>출처 정렬 방향입니다. ("asc", "desc")</summary>
    public string SourceSortDirection { get; init; } = string.Empty;
    /// <summary>출처의 난이도 순서 표시 방식입니다. ("show", "partly-solved", "all-solved", "hide")</summary>
    public string SourceOrderType { get; init; } = string.Empty;
    /// <summary>마우스를 올렸을 때 출처의 난이도 순서 표시 방식입니다. ("show", "hide")</summary>
    public string SourceOrderHover { get; init; } = string.Empty;
}

/// <summary>
/// 바꿀 사이트 설정입니다. (<see cref="SolvedAPI.UpdateAccountSettingsAsync"/>)
/// null 인 항목은 보내지 않으며 바뀌지 않습니다. 각 값의 의미는 <see cref="SolvedAccountSettings"/> 를 참고하세요.
/// </summary>
public class SolvedAccountSettingsUpdate : Jsonable
{
    /// <summary>테마입니다. ("system", "light", "dark")</summary>
    public string? Theme { get; init; }
    /// <summary>티어 아이콘 테마입니다. ("default", "legacy")</summary>
    public string? IconTheme { get; init; }
    /// <summary>푼 문제의 티어 아이콘 표시 방식입니다. ("show", "warn", "hide")</summary>
    public string? IconSchemeSolved { get; init; }
    /// <summary>풀지 않은 문제의 티어 아이콘 표시 방식입니다. ("show", "warn", "hide")</summary>
    public string? IconSchemeUnsolved { get; init; }
    /// <summary>마우스를 올렸을 때 티어 아이콘 표시 방식입니다. ("show", "warn", "hide")</summary>
    public string? IconSchemeHover { get; init; }
    /// <summary>경고 기준 방식입니다. ("relative", "absolute")</summary>
    public string? IconWarnMode { get; init; }
    /// <summary>상대 경고 기준입니다.</summary>
    public int? IconWarnRelative { get; init; }
    /// <summary>절대 경고 기준입니다.</summary>
    public int? IconWarnAbsolute { get; init; }
    /// <summary>Unrated 문제를 항상 표시할지 여부입니다.</summary>
    public bool? AlwaysShowUnrated { get; init; }
    /// <summary>난이도를 매길 수 없는 문제를 항상 표시할지 여부입니다.</summary>
    public bool? AlwaysShowUnratable { get; init; }
    /// <summary>문제 번호 표시 방식입니다. ("judge", "solvedac")</summary>
    public string? ProblemIdDisplay { get; init; }
    /// <summary>저지 표시 순서입니다.</summary>
    public List<string>? JudgeOrder { get; init; }
    /// <summary>맞은 사람 수 표시 방식입니다. ("show", "solved-only", "hide")</summary>
    public string? ShowSolvedUserCount { get; init; }
    /// <summary>기여자 수 표시 방식입니다. ("show", "solved-only", "hide")</summary>
    public string? ShowVotedUserCount { get; init; }
    /// <summary>태그 버튼 표시 방식입니다. ("show", "solved-only", "hide")</summary>
    public string? ShowTagButton { get; init; }
    /// <summary>태그 표시 언어입니다. ("identifier", "ko", "en", "ja")</summary>
    public string? TagDisplayLanguage { get; init; }
    /// <summary>문제 목록 기본 정렬입니다. ("id", "level", "solved", "average_try", "random")</summary>
    public string? ProblemSort { get; init; }
    /// <summary>문제 목록 기본 정렬 방향입니다. ("asc", "desc")</summary>
    public string? ProblemSortDirection { get; init; }
    /// <summary>원문 제목을 표시할지 여부입니다.</summary>
    public bool? ShowOriginalTitle { get; init; }
    /// <summary>원래 대회 이름을 표시할지 여부입니다.</summary>
    public bool? ShowOriginalContest { get; init; }
    /// <summary>모두 푼 출처의 난이도 표시 방식입니다. ("show", "hide")</summary>
    public string? SourceLevelAllSolved { get; init; }
    /// <summary>일부 푼 출처의 난이도 표시 방식입니다. ("show", "partial-if-ascending", "partial", "hide")</summary>
    public string? SourceLevelPartlySolved { get; init; }
    /// <summary>풀지 않은 출처의 난이도 표시 방식입니다. ("show", "hide")</summary>
    public string? SourceLevelUnsolved { get; init; }
    /// <summary>마우스를 올렸을 때 출처 난이도 표시 방식입니다. ("show", "hide")</summary>
    public string? SourceLevelHover { get; init; }
    /// <summary>출처 난이도 표시 모양입니다. ("dots", "gradient", "none")</summary>
    public string? SourceLevelDisplay { get; init; }
    /// <summary>출처 정렬 기준입니다. ("default", "name", "size", "p25", "p50", "p75")</summary>
    public string? SourceSort { get; init; }
    /// <summary>출처 정렬 방향입니다. ("asc", "desc")</summary>
    public string? SourceSortDirection { get; init; }
    /// <summary>출처의 난이도 순서 표시 방식입니다. ("show", "partly-solved", "all-solved", "hide")</summary>
    public string? SourceOrderType { get; init; }
    /// <summary>마우스를 올렸을 때 출처의 난이도 순서 표시 방식입니다. ("show", "hide")</summary>
    public string? SourceOrderHover { get; init; }
}
