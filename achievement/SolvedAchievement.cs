using AcNET.Problem;
using AcNET.Type;
using AcNET.User;

namespace AcNET.Achievement;

/// <summary>
/// 업적 아이템(뱃지 또는 배경)의 기본 정보입니다.
/// </summary>
public class SolvedItemRef : Jsonable
{
    /// <summary>
    /// 아이템 종류입니다.
    /// </summary>
    public SolvedItemKind Kind { get; set; }
    /// <summary>
    /// 아이템 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 아이템의 key 입니다. (<see cref="SolvedAPI.GetAchievementItemAsync"/>)
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 아이템 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 아이템 이미지 주소입니다.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// 분류입니다.
    /// </summary>
    public string Category { get; set; } = string.Empty;
    /// <summary>
    /// 아이템이 추가된 시각입니다.
    /// </summary>
    public string AddedAt { get; set; } = string.Empty;
}

/// <summary>
/// 최근 획득한 아이템입니다.
/// </summary>
public class SolvedEarnedItem : SolvedItemRef
{
    /// <summary>
    /// 획득한 시각입니다.
    /// </summary>
    public string AcquiredAt { get; set; } = string.Empty;
}

/// <summary>
/// 보유자 수가 포함된 아이템입니다.
/// </summary>
public class SolvedStandItem : SolvedItemRef
{
    /// <summary>
    /// 이 아이템을 가진 사용자 수입니다.
    /// </summary>
    public int HolderCount { get; set; }
}

/// <summary>
/// 보유 목록의 아이템입니다.
/// </summary>
public class SolvedCollectionItem : SolvedItemRef
{
    /// <summary>
    /// 장착 중인지 여부입니다.
    /// </summary>
    public bool Equipped { get; set; }
    /// <summary>
    /// 획득한 시각입니다.
    /// </summary>
    public string? AcquiredAt { get; set; }
    /// <summary>
    /// 이 아이템을 가진 사용자 수입니다.
    /// </summary>
    public int? HolderCount { get; set; }
}

/// <summary>
/// 사용자의 업적 요약입니다. (<see cref="SolvedAPI.GetUserAchievementSummaryAsync"/>)
/// </summary>
public class SolvedAchievementSummary : Jsonable
{
    /// <summary>
    /// 달성한 업적 수입니다.
    /// </summary>
    public int Earned { get; set; }
    /// <summary>
    /// 전체 업적 수입니다.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 지금은 달성할 수 없는 과거 업적 중 달성한 수입니다.
    /// </summary>
    public int Past { get; set; }
    /// <summary>
    /// 보유한 뱃지 수입니다.
    /// </summary>
    public int Badges { get; set; }
    /// <summary>
    /// 보유한 배경 수입니다.
    /// </summary>
    public int Backgrounds { get; set; }
    /// <summary>
    /// 최근 획득한 아이템들입니다.
    /// </summary>
    public List<SolvedEarnedItem> Recent { get; set; } = [];
}

/// <summary>
/// 프로필의 뱃지 진열대입니다. (<see cref="SolvedAPI.GetUserBadgeStandAsync"/>)
/// </summary>
public class SolvedBadgeStand : Jsonable
{
    /// <summary>
    /// 사용자가 직접 고른 뱃지인지 여부입니다. false 면 가장 희귀한 뱃지들입니다.
    /// </summary>
    public bool Chosen { get; set; }
    /// <summary>
    /// 진열된 뱃지들입니다.
    /// </summary>
    public List<SolvedStandItem> Badges { get; set; } = [];
}

/// <summary>
/// 보유한 뱃지 또는 배경 목록 한 페이지입니다.
/// </summary>
public class SolvedCollection : Jsonable
{
    /// <summary>
    /// 아이템 목록입니다.
    /// </summary>
    public List<SolvedCollectionItem> Items { get; set; } = [];
    /// <summary>
    /// 조건에 맞는 전체 아이템 수입니다.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 다음 페이지의 offset 입니다. 마지막 페이지면 null 입니다.
    /// </summary>
    public int? NextOffset { get; set; }
    /// <summary>
    /// 종류별 보유 수입니다.
    /// </summary>
    public SolvedCollectionCounts Counts { get; set; } = new();
    /// <summary>
    /// 분류 목록입니다.
    /// </summary>
    public List<string> Categories { get; set; } = [];
    /// <summary>
    /// 요청한 종류에서 장착 중인 아이템입니다.
    /// </summary>
    public SolvedCollectionItem? Equipped { get; set; }
    /// <summary>
    /// 종류별 장착 중인 아이템입니다.
    /// </summary>
    public SolvedEquippedItems EquippedByKind { get; set; } = new();
}

/// <summary>
/// 종류별 보유 수입니다.
/// </summary>
public class SolvedCollectionCounts : Jsonable
{
    /// <summary>
    /// 뱃지 수입니다.
    /// </summary>
    public int Badge { get; set; }
    /// <summary>
    /// 배경 수입니다.
    /// </summary>
    public int Background { get; set; }
}

/// <summary>
/// 종류별 장착 중인 아이템입니다.
/// </summary>
public class SolvedEquippedItems : Jsonable
{
    /// <summary>
    /// 장착 중인 뱃지입니다.
    /// </summary>
    public SolvedCollectionItem? Badge { get; set; }
    /// <summary>
    /// 장착 중인 배경입니다.
    /// </summary>
    public SolvedCollectionItem? Background { get; set; }
}

/// <summary>
/// 업적입니다.
/// </summary>
public class SolvedAchievement : Jsonable
{
    /// <summary>
    /// 업적 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 업적의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 업적 시리즈입니다.
    /// </summary>
    public string Series { get; set; } = string.Empty;
    /// <summary>
    /// 시리즈 안에서의 순서입니다.
    /// </summary>
    public int SeriesOrder { get; set; }
    /// <summary>
    /// 지금은 달성할 수 없는 과거 업적인지 여부입니다.
    /// </summary>
    public bool Historic { get; set; }
    /// <summary>
    /// 숨겨진 업적인지 여부입니다.
    /// </summary>
    public bool Hidden { get; set; }
    /// <summary>
    /// 업적 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 달성 조건입니다.
    /// </summary>
    public string? Condition { get; set; }
    /// <summary>
    /// 관련 문제를 찾는 검색 쿼리입니다.
    /// </summary>
    public string? SearchQuery { get; set; }
    /// <summary>
    /// 상점에서 살 수 있다면 그 상품의 key 입니다.
    /// </summary>
    public string? ShopKey { get; set; }
    /// <summary>
    /// 보상 아이템입니다.
    /// </summary>
    public List<SolvedItemRef> Rewards { get; set; } = [];
    /// <summary>
    /// 달성한 사용자 수입니다.
    /// </summary>
    public int HolderCount { get; set; }
    /// <summary>
    /// (요청한, 또는 조회한) 사용자가 달성한 시각입니다.
    /// </summary>
    public string? EarnedAt { get; set; }
    /// <summary>
    /// 진행도입니다. 진행도가 없는 업적은 null 입니다.
    /// </summary>
    public SolvedAchievementProgress? Progress { get; set; }
    /// <summary>
    /// 비교 대상 사용자의 달성 정보입니다. 비교하지 않으면 null 입니다.
    /// </summary>
    public SolvedAchievementVs? Vs { get; set; }
}

/// <summary>
/// 업적 진행도입니다.
/// </summary>
public class SolvedAchievementProgress : Jsonable
{
    /// <summary>
    /// 현재 값입니다.
    /// </summary>
    public int Current { get; set; }
    /// <summary>
    /// 목표 값입니다.
    /// </summary>
    public int Target { get; set; }
}

/// <summary>
/// 비교 대상 사용자의 업적 달성 정보입니다.
/// </summary>
public class SolvedAchievementVs : Jsonable
{
    /// <summary>
    /// 비교 대상 사용자가 달성한 시각입니다.
    /// </summary>
    public string? EarnedAt { get; set; }
}

/// <summary>
/// 전체 업적 목록과 달성 현황입니다.
/// </summary>
public class SolvedAchievementList : Jsonable
{
    /// <summary>
    /// 조회한 사용자입니다. 요청한 사용자 기준 목록(<see cref="SolvedAPI.GetAchievementListAsync"/>)에서는 null 입니다.
    /// </summary>
    public string? Handle { get; set; }
    /// <summary>
    /// 비교 대상 사용자입니다.
    /// </summary>
    public string? VsHandle { get; set; }
    /// <summary>
    /// 업적 목록입니다.
    /// </summary>
    public List<SolvedAchievement> Items { get; set; } = [];
    /// <summary>
    /// 전체 사용자 수입니다. (달성률 계산용)
    /// </summary>
    public int UserCount { get; set; }
}

/// <summary>
/// 문제를 풀면 달성하거나 진행되는 업적입니다.
/// </summary>
public class SolvedProblemAchievement : Jsonable
{
    /// <summary>
    /// 업적의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 업적 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 달성 조건입니다.
    /// </summary>
    public string? Condition { get; set; }
    /// <summary>
    /// 보상 아이템입니다.
    /// </summary>
    public List<SolvedItemRef> Rewards { get; set; } = [];
    /// <summary>
    /// 달성한 사용자 수입니다.
    /// </summary>
    public int HolderCount { get; set; }
    /// <summary>
    /// 요청한 사용자가 달성한 시각입니다.
    /// </summary>
    public string? EarnedAt { get; set; }
    /// <summary>
    /// 문제와의 관계입니다. ("unlocks": 풀면 달성, "counts": 풀면 진행)
    /// </summary>
    public string Relation { get; set; } = string.Empty;
    /// <summary>
    /// 문제 난이도에 따라 정해지는 업적인지 여부입니다.
    /// </summary>
    public bool ByLevel { get; set; }
}

/// <summary>
/// 뱃지 또는 배경과 그것을 주는 업적입니다. (<see cref="SolvedAPI.GetAchievementItemAsync"/>)
/// </summary>
public class SolvedAchievementItem : SolvedItemRef
{
    /// <summary>
    /// 모든 사용자가 기본으로 가진 아이템인지 여부입니다.
    /// </summary>
    public bool UnlockedByDefault { get; set; }
    /// <summary>
    /// 요청한 사용자가 가지고 있는지 여부입니다. 사용자 정보가 없으면 null 입니다.
    /// </summary>
    public bool? Owned { get; set; }
    /// <summary>
    /// 요청한 사용자가 장착 중인지 여부입니다. 사용자 정보가 없으면 null 입니다.
    /// </summary>
    public bool? Equipped { get; set; }
    /// <summary>
    /// 배경일 경우 배경 정보입니다.
    /// </summary>
    public SolvedBackgroundInfo? Background { get; set; }
    /// <summary>
    /// 제작자 정보입니다.
    /// </summary>
    public List<SolvedCredit> Credits { get; set; } = [];
    /// <summary>
    /// 가진 사용자 수입니다.
    /// </summary>
    public int UserCount { get; set; }
    /// <summary>
    /// 이 아이템을 주는 업적들입니다.
    /// </summary>
    public List<SolvedItemAchievement> Achievements { get; set; } = [];
}

/// <summary>
/// 아이템을 주는 업적입니다.
/// </summary>
public class SolvedItemAchievement : Jsonable
{
    /// <summary>
    /// 업적의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 업적 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 달성 조건입니다.
    /// </summary>
    public string? Condition { get; set; }
    /// <summary>
    /// 지금은 달성할 수 없는 과거 업적인지 여부입니다.
    /// </summary>
    public bool Historic { get; set; }
    /// <summary>
    /// 숨겨진 업적인지 여부입니다.
    /// </summary>
    public bool Hidden { get; set; }
    /// <summary>
    /// 달성한 사용자 수입니다.
    /// </summary>
    public int HolderCount { get; set; }
    /// <summary>
    /// 요청한 사용자가 달성한 시각입니다.
    /// </summary>
    public string? EarnedAt { get; set; }
    /// <summary>
    /// 관련 문제들입니다.
    /// </summary>
    public List<SolvedProblemSummary> Problems { get; set; } = [];
}

/// <summary>
/// 아이템 보유자 목록 한 페이지입니다. (<see cref="SolvedAPI.GetAchievementItemHoldersAsync"/>)
/// </summary>
public class SolvedItemHolders : Jsonable
{
    /// <summary>
    /// 모든 사용자가 기본으로 가진 아이템인지 여부입니다.
    /// </summary>
    public bool UnlockedByDefault { get; set; }
    /// <summary>
    /// 전체 보유자 수입니다.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 보유자 목록입니다.
    /// </summary>
    public List<SolvedItemHolder> Items { get; set; } = [];
}

/// <summary>
/// 아이템 보유자입니다.
/// </summary>
public class SolvedItemHolder : Jsonable
{
    /// <summary>
    /// 사용자입니다.
    /// </summary>
    public SolvedUserRef User { get; set; } = new();
    /// <summary>
    /// 획득한 시각입니다.
    /// </summary>
    public string AcquiredAt { get; set; } = string.Empty;
}

/// <summary>
/// 배경 정보입니다.
/// </summary>
public class SolvedBackgroundInfo : Jsonable
{
    /// <summary>
    /// 배경의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 배경 이미지 주소입니다.
    /// </summary>
    public string BackgroundImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// 대체 배경 이미지 주소입니다.
    /// </summary>
    public string? FallbackBackgroundImageUrl { get; set; }
    /// <summary>
    /// 배경 영상 주소입니다.
    /// </summary>
    public string? BackgroundVideoUrl { get; set; }
    /// <summary>
    /// 이미지를 불러오기 전에 보여줄 자리표시자입니다.
    /// </summary>
    public string? ThumbnailPlaceholder { get; set; }
    /// <summary>
    /// 일러스트 배경인지 여부입니다.
    /// </summary>
    public bool IsIllust { get; set; }
    /// <summary>
    /// 배경 이름입니다.
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// 제작자 정보입니다.
    /// </summary>
    public List<SolvedCredit> Authors { get; set; } = [];
    /// <summary>
    /// 배경 이미지를 저장합니다.
    /// </summary>
    /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>성공시 null, 실패시 Exception</returns>
    public Task<Exception?> SaveImageAsync(string filename, CancellationToken cancellationToken = default)
        => SolvedFile.SaveAsync(BackgroundImageUrl, filename, cancellationToken);
}

/// <summary>
/// 제작자 정보입니다.
/// </summary>
public class SolvedCredit : Jsonable
{
    /// <summary>
    /// 제작자의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 제작자 이름입니다.
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// 역할입니다.
    /// </summary>
    public string Role { get; set; } = string.Empty;
    /// <summary>
    /// 제작자 페이지 주소입니다.
    /// </summary>
    public string? AuthorUrl { get; set; }
    /// <summary>
    /// 제작자의 solved.ac 사용자명입니다.
    /// </summary>
    public string? Handle { get; set; }
    /// <summary>
    /// 제작자의 X(트위터) 계정입니다.
    /// </summary>
    public string? Twitter { get; set; }
    /// <summary>
    /// 제작자의 인스타그램 계정입니다.
    /// </summary>
    public string? Instagram { get; set; }
}
