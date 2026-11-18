using AcNET.Achievement;
using AcNET.Type;

namespace AcNET.User;

/// <summary>
/// 다른 응답에 포함되는 가장 작은 사용자 정보입니다.
/// </summary>
public class SolvedUserRef : Jsonable
{
    /// <summary>
    /// 사용자 ID 입니다. 바뀌지 않습니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 사용자명입니다. 바뀔 수 있습니다.
    /// </summary>
    public string Handle { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 티어(레벨)값입니다. (0: Unrated, 1: Bronze V, ..., 31: Master)
    /// </summary>
    public int? Tier { get; set; }
    /// <summary>
    /// 사용자의 프로필 사진 주소입니다.
    /// </summary>
    public string? ProfileImageUrl { get; set; }
    /// <summary>
    /// 사용자의 티어(레벨)값을 티어 이름으로 가져옵니다.
    /// </summary>
    public string? GetTierName => SolvedConverter.LevelName(Tier);
    /// <summary>
    /// 티어에 맞는 색깔을 가져옵니다. (7자리 Hex 코드입니다.)
    /// </summary>
    public string GetTierColor => SolvedConverter.LevelColor(Tier);
    /// <summary>
    /// 사용자의 프로필 이미지를 저장합니다.
    /// </summary>
    /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>성공시 null, 실패시 Exception</returns>
    public Task<Exception?> SaveProfileImageAsync(string filename, CancellationToken cancellationToken = default)
        => SolvedFile.SaveAsync(ProfileImageUrl, filename, cancellationToken);
}

/// <summary>
/// 사용자의 공개 프로필입니다. (<see cref="SolvedAPI.GetUserAsync"/>)
/// </summary>
public class SolvedUser : SolvedUserRef
{
    /// <summary>
    /// 사용자의 자기소개입니다.
    /// </summary>
    public string? Bio { get; set; }
    /// <summary>
    /// 사용자의 레이팅입니다.
    /// </summary>
    public int Rating { get; set; }
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    public int SolvedCount { get; set; }
    /// <summary>
    /// 사용자가 난이도 기여를 한 횟수입니다.
    /// </summary>
    public int VoteCount { get; set; }
    /// <summary>
    /// 사용자의 평판입니다.
    /// </summary>
    public int Reputation { get; set; }
    /// <summary>
    /// 유지한 최대 스트릭의 길이입니다. (일 단위)
    /// </summary>
    public int MaxStreak { get; set; }
    /// <summary>
    /// 현재 스트릭의 길이입니다. (일 단위)
    /// </summary>
    public int CurrentStreak { get; set; }
    /// <summary>
    /// 진행 중인 스트릭 휴가입니다. 없으면 null 입니다.
    /// </summary>
    public SolvedStreakVacation? StreakVacation { get; set; }
    /// <summary>
    /// 오늘 스트릭이 채워졌는지 여부입니다.
    /// </summary>
    public bool StreakCountedToday { get; set; }
    /// <summary>
    /// 어제 스트릭 프리즈가 사용되었는지 여부입니다.
    /// </summary>
    public bool StreakFrozenYesterday { get; set; }
    /// <summary>
    /// 레이팅을 주지 않는 문제를 푼 수입니다.
    /// </summary>
    public int NoRatingSolvedCount { get; set; }
    /// <summary>
    /// 스트릭 그래프 테마입니다.
    /// </summary>
    public string? StreakTheme { get; set; }
    /// <summary>
    /// 사용자가 장착한 배경입니다.
    /// </summary>
    public SolvedBackgroundInfo? Background { get; set; }
    /// <summary>
    /// 요청한 사용자의 라이벌인지 여부입니다.
    /// </summary>
    public bool IsRival { get; set; }
    /// <summary>
    /// 요청한 사용자를 라이벌로 등록했는지(역라이벌) 여부입니다.
    /// </summary>
    public bool IsReverseRival { get; set; }
    /// <summary>
    /// 연결된 저지 계정들입니다.
    /// </summary>
    public List<SolvedOjAccount> OjAccounts { get; set; } = [];
    /// <summary>
    /// 연결된 외부 계정들입니다. (Discord, ICPC)
    /// </summary>
    public List<SolvedLinkedAccount> LinkedAccounts { get; set; } = [];
    /// <summary>
    /// 사용자의 소속들입니다.
    /// </summary>
    public List<SolvedAffiliationRef> Affiliations { get; set; } = [];
    /// <summary>
    /// 사용자가 취득한 Class입니다. (0~10)
    /// </summary>
    public int Class { get; set; }
    /// <summary>
    /// 클래스의 모든 문제를 풀었는지(Class+) 여부입니다.
    /// </summary>
    public bool ClassPlus { get; set; }
    /// <summary>
    /// 사용자가 장착한 뱃지입니다.
    /// </summary>
    public SolvedUserBadge? Badge { get; set; }
    /// <summary>
    /// 푼 문제의 난이도 합으로 계산한 레이팅입니다.
    /// </summary>
    public int RatingByProblemsSum { get; set; }
    /// <summary>
    /// 취득한 클래스에 따른 레이팅입니다.
    /// </summary>
    public int RatingByClass { get; set; }
    /// <summary>
    /// 푼 문제 수로 계산한 레이팅입니다.
    /// </summary>
    public int RatingBySolvedCount { get; set; }
    /// <summary>
    /// 레이팅 계산에 들어가는 문제 중 가장 낮은 난이도입니다.
    /// </summary>
    public int RatingProblemsCutoff { get; set; }
    /// <summary>
    /// 레이팅 계산에 들어가는 문제의 레벨별 개수입니다. (레벨로 색인)
    /// </summary>
    public List<int> RatingProblemsHistogram { get; set; } = [];
    /// <summary>
    /// 사용자에게 걸린 정지입니다.
    /// </summary>
    public List<SolvedBan> Bans { get; set; } = [];
    /// <summary>
    /// 태그별 레이팅입니다.
    /// </summary>
    public List<SolvedTagRating> TagRatings { get; set; } = [];
    /// <summary>
    /// 사용자가 공개한 부가 정보입니다.
    /// </summary>
    public SolvedUserMetadata Metadata { get; set; } = new();
}

/// <summary>
/// 사용자 토큰의 주인입니다. (<see cref="SolvedAPI.GetMeAsync"/>)
/// </summary>
public class SolvedMe : Jsonable
{
    /// <summary>
    /// 사용자 ID 입니다. 바뀌지 않습니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 사용자명입니다. 바뀔 수 있습니다.
    /// </summary>
    public string Handle { get; set; } = string.Empty;
}

/// <summary>
/// 스트릭 휴가입니다.
/// </summary>
public class SolvedStreakVacation : Jsonable
{
    /// <summary>
    /// 휴가 시작일입니다. (yyyy-MM-dd)
    /// </summary>
    public string FromDay { get; set; } = string.Empty;
    /// <summary>
    /// 휴가가 시작된 경로입니다. ("item", "launch")
    /// </summary>
    public string Source { get; set; } = string.Empty;
}

/// <summary>
/// 연결된 저지 계정입니다.
/// </summary>
public class SolvedOjAccount : Jsonable
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
    /// 저지에서의 사용자명입니다.
    /// </summary>
    public string Handle { get; set; } = string.Empty;
    /// <summary>
    /// 저지의 프로필 주소입니다.
    /// </summary>
    public string? ProfileUrl { get; set; }
    /// <summary>
    /// 저지 로고 주소입니다.
    /// </summary>
    public string? LogoUrl { get; set; }
    /// <summary>
    /// 인증된 계정인지 여부입니다.
    /// </summary>
    public bool Verified { get; set; }
}

/// <summary>
/// 연결된 외부 계정입니다.
/// </summary>
public class SolvedLinkedAccount : Jsonable
{
    /// <summary>
    /// 서비스입니다. ("discord", "icpc")
    /// </summary>
    public string Provider { get; set; } = string.Empty;
    /// <summary>
    /// 계정 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 계정 주소입니다.
    /// </summary>
    public string Url { get; set; } = string.Empty;
}

/// <summary>
/// 사용자가 장착한 뱃지입니다.
/// </summary>
public class SolvedUserBadge : Jsonable
{
    /// <summary>
    /// 뱃지의 key 입니다. (<see cref="SolvedAPI.GetAchievementItemAsync"/>)
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 뱃지 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 뱃지 이미지 주소입니다.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
}

/// <summary>
/// 정지 정보입니다.
/// </summary>
public class SolvedBan : Jsonable
{
    /// <summary>
    /// 정지 종류입니다.
    /// </summary>
    public SolvedBanType BanType { get; set; }
    /// <summary>
    /// 정지 사유 코드입니다. (예: "rating_copied_code", "vote_troll_level", "other")
    /// </summary>
    public string? BanReason { get; set; }
    /// <summary>
    /// 정지 사유입니다.
    /// </summary>
    public string Reason { get; set; } = string.Empty;
    /// <summary>
    /// 정지가 끝나는 시각입니다.
    /// </summary>
    public string Until { get; set; } = string.Empty;
    /// <summary>
    /// 정지가 끝나는 시각을 C# DateTimeOffset 으로 가져옵니다.
    /// </summary>
    public DateTimeOffset? GetUntil => SolvedConverter.ToTime(Until);
}

/// <summary>
/// 태그별 레이팅입니다.
/// </summary>
public class SolvedTagRating : Jsonable
{
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 태그 레이팅입니다.
    /// </summary>
    public int Rating { get; set; }
    /// <summary>
    /// 이 태그에서 푼 문제 수입니다.
    /// </summary>
    public int SolvedCount { get; set; }
    /// <summary>
    /// 푼 문제의 난이도 합으로 계산한 레이팅입니다.
    /// </summary>
    public int RatingByProblemsSum { get; set; }
    /// <summary>
    /// 푼 문제 수로 계산한 레이팅입니다.
    /// </summary>
    public int RatingBySolvedCount { get; set; }
    /// <summary>
    /// 레이팅 계산에 들어가는 문제 중 가장 낮은 난이도입니다.
    /// </summary>
    public int RatingProblemsCutoff { get; set; }
}

/// <summary>
/// 사용자가 공개한 부가 정보입니다.
/// </summary>
public class SolvedUserMetadata : Jsonable
{
    /// <summary>
    /// 국가 코드입니다. (ISO 3166-1 alpha-2)
    /// </summary>
    public string? CountryCode { get; set; }
    /// <summary>
    /// 대명사입니다.
    /// </summary>
    public string? Pronouns { get; set; }
    /// <summary>
    /// 이름입니다.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 모국어로 쓴 이름입니다.
    /// </summary>
    public string? NameNative { get; set; }
    /// <summary>
    /// 생일입니다. 공개한 부분만 채워집니다.
    /// </summary>
    public SolvedBirthday? Birthday { get; set; }
}

/// <summary>
/// 생일입니다.
/// </summary>
public class SolvedBirthday : Jsonable
{
    /// <summary>
    /// 연도입니다.
    /// </summary>
    public int? Year { get; set; }
    /// <summary>
    /// 월입니다.
    /// </summary>
    public int? Month { get; set; }
    /// <summary>
    /// 일입니다.
    /// </summary>
    public int? Day { get; set; }
}

/// <summary>
/// 한 태그 안에서 사용자가 푼 문제의 레벨별 개수입니다.
/// </summary>
public class SolvedTagHistogram : Jsonable
{
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 레벨별 푼 문제 수입니다. (31칸, 레벨로 색인)
    /// </summary>
    public List<int> Histogram { get; set; } = [];
}
