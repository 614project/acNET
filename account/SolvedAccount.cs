using AcNET.Problem;
using AcNET.Type;
using AcNET.User;

namespace AcNET.Account;

/// <summary>
/// 본인의 수정 가능한 프로필 항목과 공개 여부입니다. (<see cref="SolvedAPI.GetAccountProfileAsync"/>)
/// </summary>
public class SolvedAccountProfile : Jsonable
{
    /// <summary>자기소개입니다.</summary>
    public string? Bio { get; init; }
    /// <summary>프로필 사진 주소입니다.</summary>
    public string? ProfileImageUrl { get; init; }
    /// <summary>국가 코드입니다.</summary>
    public string? CountryCode { get; init; }
    /// <summary>국가를 공개할지 여부입니다.</summary>
    public bool ShowCountry { get; init; }
    /// <summary>성별 코드입니다. 0, 1, 2, 9 중 하나입니다. (ISO/IEC 5218 과 같은 값으로 보입니다: 0 알 수 없음, 1 남성, 2 여성, 9 해당 없음)</summary>
    public int Gender { get; init; }
    /// <summary>직접 입력한 성별입니다.</summary>
    public string? CustomGender { get; init; }
    /// <summary>대명사입니다.</summary>
    public string? Pronouns { get; init; }
    /// <summary>대명사를 공개할지 여부입니다.</summary>
    public bool ShowPronouns { get; init; }
    /// <summary>생년입니다.</summary>
    public int? BirthYear { get; init; }
    /// <summary>생월입니다.</summary>
    public int? BirthMonth { get; init; }
    /// <summary>생일입니다.</summary>
    public int? BirthDay { get; init; }
    /// <summary>생일을 공개할지 여부입니다.</summary>
    public bool ShowBirthday { get; init; }
    /// <summary>생년을 공개할지 여부입니다.</summary>
    public bool ShowBirthYear { get; init; }
    /// <summary>이름입니다.</summary>
    public string? Name { get; init; }
    /// <summary>모국어로 쓴 이름입니다.</summary>
    public string? NameNative { get; init; }
    /// <summary>이름을 공개할지 여부입니다.</summary>
    public bool ShowName { get; init; }
    /// <summary>Discord 계정을 공개할지 여부입니다.</summary>
    public bool ShowDiscord { get; init; }
    /// <summary>ICPC 계정을 공개할지 여부입니다.</summary>
    public bool ShowIcpc { get; init; }
    /// <summary>장착한 배경의 ID 입니다.</summary>
    public long? BackgroundId { get; init; }
}

/// <summary>
/// 장착할 수 있는 배경입니다. (<see cref="SolvedAPI.GetAccountBackgroundsAsync"/>)
/// </summary>
public class SolvedOwnedBackground : Jsonable
{
    /// <summary>배경 ID 입니다.</summary>
    public long Id { get; init; }
    /// <summary>배경의 key 입니다.</summary>
    public string Key { get; init; } = string.Empty;
    /// <summary>배경 이미지 주소입니다.</summary>
    public string BackgroundImageUrl { get; init; } = string.Empty;
    /// <summary>대체 배경 이미지 주소입니다.</summary>
    public string? FallbackBackgroundImageUrl { get; init; }
    /// <summary>썸네일 주소입니다.</summary>
    public string? ThumbnailImageUrl { get; init; }
    /// <summary>이미지를 불러오기 전에 보여줄 자리표시자입니다.</summary>
    public string? ThumbnailPlaceholder { get; init; }
    /// <summary>일러스트 배경인지 여부입니다.</summary>
    public bool IsIllust { get; init; }
    /// <summary>분류입니다.</summary>
    public string Category { get; init; } = string.Empty;
    /// <summary>배경이 추가된 시각입니다.</summary>
    public string CreatedAt { get; init; } = string.Empty;
    /// <summary>획득한 시각입니다. 모두가 가진 배경이면 null 입니다.</summary>
    public string? AcquiredAt { get; init; }
    /// <summary>가진 사용자 수입니다.</summary>
    public int OwnedUserCount { get; init; }
}

/// <summary>
/// 아직 동의하지 않은 정책 버전입니다. (<see cref="SolvedAPI.GetAccountPendingConsentsAsync"/>)
/// </summary>
public class SolvedPendingConsent : Jsonable
{
    /// <summary>정책입니다. ("tos", "privacy")</summary>
    public string Slug { get; init; } = string.Empty;
    /// <summary>버전입니다.</summary>
    public string Version { get; init; } = string.Empty;
    /// <summary>버전 표시 이름입니다.</summary>
    public string Label { get; init; } = string.Empty;
}

/// <summary>
/// 계정의 경고와 정지 상태입니다. (<see cref="SolvedAPI.GetAccountStandingAsync"/>)
/// </summary>
public class SolvedAccountStanding : Jsonable
{
    /// <summary>경고 목록입니다.</summary>
    public List<SolvedStrike> Strikes { get; init; } = [];
    /// <summary>정지 목록입니다.</summary>
    public List<SolvedBan> Bans { get; init; } = [];
    /// <summary>이전 경고로 받은 정지 횟수입니다.</summary>
    public int PriorStrikeBans { get; init; }
    /// <summary>다음 정지의 기간(일)입니다.</summary>
    public int NextBanDays { get; init; }
}

/// <summary>
/// 경고입니다.
/// </summary>
public class SolvedStrike : Jsonable
{
    /// <summary>경고 ID 입니다.</summary>
    public long Id { get; init; }
    /// <summary>관련 문제입니다.</summary>
    public SolvedStrikeProblem? Problem { get; init; }
    /// <summary>경고 분류입니다. (예: "spam", "profanity", "troll-level")</summary>
    public string Category { get; init; } = string.Empty;
    /// <summary>경고를 받은 내용입니다.</summary>
    public string Text { get; init; } = string.Empty;
    /// <summary>조치입니다. ("hidden", "removed")</summary>
    public string Action { get; init; } = string.Empty;
    /// <summary>경고 시각입니다.</summary>
    public string CreatedAt { get; init; } = string.Empty;
    /// <summary>확인했는지 여부입니다.</summary>
    public bool Seen { get; init; }
    /// <summary>복구되었는지 여부입니다.</summary>
    public bool Restored { get; init; }
    /// <summary>이의 제기입니다.</summary>
    public SolvedAppeal? Appeal { get; init; }
    /// <summary>이의 제기할 수 있는지 여부입니다.</summary>
    public bool Appealable { get; init; }
    /// <summary>경고가 지워졌는지 여부입니다.</summary>
    public bool StrikeCleared { get; init; }
    /// <summary>경고가 만료되는 시각입니다.</summary>
    public string? StrikeExpiresAt { get; init; }
}

/// <summary>
/// 경고와 관련된 문제입니다.
/// </summary>
public class SolvedStrikeProblem : SolvedProblemRef
{
    /// <summary>문제 제목입니다.</summary>
    public string? Title { get; init; }
}

/// <summary>
/// 이의 제기입니다.
/// </summary>
public class SolvedAppeal : Jsonable
{
    /// <summary>이의 제기 내용입니다.</summary>
    public string Text { get; init; } = string.Empty;
    /// <summary>처리 상태입니다. ("pending", "granted", "denied")</summary>
    public string Status { get; init; } = string.Empty;
    /// <summary>이의 제기한 시각입니다.</summary>
    public string AppealedAt { get; init; } = string.Empty;
    /// <summary>운영진의 답변입니다.</summary>
    public string? Response { get; init; }
}

/// <summary>
/// 이의 제기할 수 있는 (또는 이의 제기한) 정지입니다. (<see cref="SolvedAPI.GetAccountBansAsync"/>)
/// </summary>
public class SolvedAppealableBan : SolvedBan
{
    /// <summary>정지 ID 입니다.</summary>
    public long Id { get; init; }
    /// <summary>이의 제기할 수 있는지 여부입니다.</summary>
    public bool Appealable { get; init; }
    /// <summary>이의 제기입니다.</summary>
    public SolvedAppeal? Appeal { get; init; }
}

/// <summary>
/// 보유한 아이템과 스트릭 테마, 휴가 상태입니다. (<see cref="SolvedAPI.GetAccountItemsAsync"/>)
/// </summary>
public class SolvedAccountItems : Jsonable
{
    /// <summary>보유한 아이템입니다.</summary>
    public List<SolvedHeldItem> Items { get; init; } = [];
    /// <summary>스트릭 테마입니다.</summary>
    public SolvedStreakThemes StreakThemes { get; init; } = new();
    /// <summary>진행 중인 스트릭 휴가입니다.</summary>
    public SolvedStreakVacation? Vacation { get; init; }
}

/// <summary>
/// 보유한 아이템입니다.
/// </summary>
public class SolvedHeldItem : Jsonable
{
    /// <summary>아이템의 key 입니다.</summary>
    public string Key { get; init; } = string.Empty;
    /// <summary>아이템 종류입니다. ("consumable", "equippable", "voucher", "keepsake")</summary>
    public string Kind { get; init; } = string.Empty;
    /// <summary>효과입니다.</summary>
    public string? Effect { get; init; }
    /// <summary>아이템 이름입니다.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>아이템 설명입니다.</summary>
    public string Description { get; init; } = string.Empty;
    /// <summary>아이템 이미지 주소입니다.</summary>
    public string ImageUrl { get; init; } = string.Empty;
    /// <summary>보유 개수입니다.</summary>
    public int Units { get; init; }
    /// <summary>최대 보유 개수입니다.</summary>
    public int MaxUnits { get; init; }
    /// <summary>지금 사용할 수 있는지 여부입니다.</summary>
    public bool Usable { get; init; }
    /// <summary>장착 중인지 여부입니다. 장착할 수 없는 아이템이면 null 입니다.</summary>
    public bool? Equipped { get; init; }
    /// <summary>모으는 아이템의 진행도입니다.</summary>
    public SolvedHeldItemProgress? Progress { get; init; }
    /// <summary>묶음별 정보입니다.</summary>
    public List<SolvedHeldItemStack> Stacks { get; init; } = [];
}

/// <summary>
/// 모으는 아이템의 진행도입니다.
/// </summary>
public class SolvedHeldItemProgress : Jsonable
{
    /// <summary>모은 개수입니다.</summary>
    public int Held { get; init; }
    /// <summary>전체 개수입니다.</summary>
    public int Total { get; init; }
}

/// <summary>
/// 아이템 묶음입니다.
/// </summary>
public class SolvedHeldItemStack : Jsonable
{
    /// <summary>개수입니다.</summary>
    public int Units { get; init; }
    /// <summary>만료 시각입니다.</summary>
    public string? ExpiresAt { get; init; }
    /// <summary>사용한 날짜입니다.</summary>
    public string? Day { get; init; }
    /// <summary>사용할 수 있는지 여부입니다.</summary>
    public bool Usable { get; init; }
    /// <summary>집계되었는지 여부입니다.</summary>
    public bool Counted { get; init; }
}

/// <summary>
/// 스트릭 테마입니다.
/// </summary>
public class SolvedStreakThemes : Jsonable
{
    /// <summary>장착 중인 테마입니다.</summary>
    public string Equipped { get; init; } = string.Empty;
    /// <summary>보유한 테마입니다.</summary>
    public List<SolvedOwnedStreakTheme> Owned { get; init; } = [];
}

/// <summary>
/// 보유한 스트릭 테마입니다.
/// </summary>
public class SolvedOwnedStreakTheme : Jsonable
{
    /// <summary>테마입니다.</summary>
    public string Theme { get; init; } = string.Empty;
    /// <summary>획득한 시각입니다.</summary>
    public string? AcquiredAt { get; init; }
}

/// <summary>
/// 별가루 지갑입니다. (<see cref="SolvedAPI.GetAccountWalletAsync"/>)
/// </summary>
public class SolvedWallet : Jsonable
{
    /// <summary>별가루 잔액입니다.</summary>
    public int Stardusts { get; init; }
    /// <summary>오늘 받은 별가루입니다.</summary>
    public SolvedWalletToday Today { get; init; } = new();
    /// <summary>행동별로 받는 별가루입니다.</summary>
    public SolvedWalletEarnings Earnings { get; init; } = new();
}

/// <summary>
/// 오늘 받은 별가루입니다.
/// </summary>
public class SolvedWalletToday : Jsonable
{
    /// <summary>날짜입니다.</summary>
    public string Day { get; init; } = string.Empty;
    /// <summary>오늘 받은 별가루입니다.</summary>
    public int Earned { get; init; }
    /// <summary>하루에 받을 수 있는 최대 별가루입니다.</summary>
    public int Cap { get; init; }
}

/// <summary>
/// 행동별로 받는 별가루입니다.
/// </summary>
public class SolvedWalletEarnings : Jsonable
{
    /// <summary>처음 푼 문제당 받는 별가루입니다.</summary>
    public int FirstSolve { get; init; }
    /// <summary>문제를 풀면 받는 별가루입니다.</summary>
    public int Solve { get; init; }
    /// <summary>기여하면 받는 별가루입니다.</summary>
    public int Contribute { get; init; }
    /// <summary>환영 보너스로 문제당 받는 별가루입니다.</summary>
    public int WelcomePerSolve { get; init; }
    /// <summary>환영 보너스로 받을 수 있는 최대 별가루입니다.</summary>
    public int WelcomeMax { get; init; }
}

/// <summary>
/// 한 달의 별가루 내역입니다. (<see cref="SolvedAPI.GetAccountWalletHistoryAsync"/>)
/// </summary>
public class SolvedWalletHistory : Jsonable
{
    /// <summary>월입니다. (yyyy-MM)</summary>
    public string Month { get; init; } = string.Empty;
    /// <summary>내역입니다. (최신순)</summary>
    public List<SolvedWalletMove> Moves { get; init; } = [];
}

/// <summary>
/// 별가루 내역 한 건입니다.
/// </summary>
public class SolvedWalletMove : Jsonable
{
    /// <summary>내역 ID 입니다.</summary>
    public long Id { get; init; }
    /// <summary>변동량입니다.</summary>
    public int Delta { get; init; }
    /// <summary>변동 후 잔액입니다.</summary>
    public int BalanceAfter { get; init; }
    /// <summary>사유입니다. (예: "solve", "first_solve", "contribute", "shop_purchase")</summary>
    public string Reason { get; init; } = string.Empty;
    /// <summary>시각입니다.</summary>
    public string At { get; init; } = string.Empty;
    /// <summary>집계된 날짜입니다.</summary>
    public string? Day { get; init; }
    /// <summary>관련 문제입니다.</summary>
    public SolvedProblemSummary? Problem { get; init; }
    /// <summary>표시 문구입니다.</summary>
    public string? Label { get; init; }
}

/// <summary>
/// 이모티콘입니다. (<see cref="SolvedAPI.GetMyEmoticonsAsync"/>)
/// </summary>
public class SolvedEmoticon : Jsonable
{
    /// <summary>이모티콘의 key 입니다.</summary>
    public string Key { get; init; } = string.Empty;
    /// <summary>이모티콘 이미지 주소입니다.</summary>
    public string Url { get; init; } = string.Empty;
    /// <summary>분류입니다.</summary>
    public string Category { get; init; } = string.Empty;
    /// <summary>이모티콘 이름입니다.</summary>
    public string? DisplayName { get; init; }
    /// <summary>요청한 사용자가 가지고 있는지 여부입니다.</summary>
    public bool Owned { get; init; }
}
