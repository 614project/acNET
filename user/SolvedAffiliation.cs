using AcNET.Type;

namespace AcNET.User;

/// <summary>
/// 다른 응답에 포함되는 소속(학교, 회사, 커뮤니티) 정보입니다.
/// </summary>
public class SolvedAffiliationRef : Jsonable
{
    /// <summary>
    /// 소속 항목 ID 입니다.
    /// </summary>
    public long Id { get; init; }
    /// <summary>
    /// 소속 ID 입니다. 소속 상세와 소속 랭킹에 사용합니다.
    /// </summary>
    public long AffiliationId { get; init; }
    /// <summary>
    /// 소속 이름입니다.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>
    /// 소속 종류입니다.
    /// </summary>
    public SolvedAffiliationType Type { get; init; }
    /// <summary>
    /// 로고 썸네일 주소입니다.
    /// </summary>
    public string? LogoThumbnailUrl { get; init; }
    /// <summary>
    /// 소속 색입니다.
    /// </summary>
    public string? Color { get; init; }
}

/// <summary>
/// 랭킹이 있는 소속입니다. (<see cref="SolvedAPI.GetAffiliationListAsync"/>)
/// </summary>
public class SolvedRankableAffiliation : SolvedAffiliationRef
{
    /// <summary>
    /// 소속 사용자 수입니다.
    /// </summary>
    public int UserCount { get; init; }
}

/// <summary>
/// 소속 상세 정보입니다. (<see cref="SolvedAPI.GetAffiliationAsync"/>)
/// </summary>
public class SolvedAffiliation : Jsonable
{
    /// <summary>
    /// 소속 항목 ID 입니다.
    /// </summary>
    public long Id { get; init; }
    /// <summary>
    /// 소속 ID 입니다.
    /// </summary>
    public long AffiliationId { get; init; }
    /// <summary>
    /// 소속 이름입니다.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>
    /// 소속 종류입니다.
    /// </summary>
    public SolvedAffiliationType Type { get; init; }
    /// <summary>
    /// 국가 코드입니다.
    /// </summary>
    public string? CountryCode { get; init; }
    /// <summary>
    /// 로고 주소입니다.
    /// </summary>
    public string? LogoImageUrl { get; init; }
    /// <summary>
    /// 로고 썸네일 주소입니다.
    /// </summary>
    public string? LogoThumbnailUrl { get; init; }
    /// <summary>
    /// 소속 색입니다.
    /// </summary>
    public string? Color { get; init; }
    /// <summary>
    /// 웹사이트 주소입니다.
    /// </summary>
    public string? Website { get; init; }
    /// <summary>
    /// 소속 레이팅입니다.
    /// </summary>
    public int Rating { get; init; }
    /// <summary>
    /// 소속 사용자 수입니다.
    /// </summary>
    public int UserCount { get; init; }
    /// <summary>
    /// 소속 사용자들이 푼 문제 수입니다.
    /// </summary>
    public int SolvedCount { get; init; }
    /// <summary>
    /// 소속 사용자들의 기여 수입니다.
    /// </summary>
    public int VoteCount { get; init; }
    /// <summary>
    /// 요청한 언어로 표시되는 이름입니다.
    /// </summary>
    public string DisplayName { get; init; } = string.Empty;
    /// <summary>
    /// 전체 소속 중 순위입니다.
    /// </summary>
    public int Rank { get; init; }
    /// <summary>
    /// 순위가 매겨진 전체 소속 수입니다.
    /// </summary>
    public int RankedOf { get; init; }
    /// <summary>
    /// 같은 종류 소속 중 순위입니다.
    /// </summary>
    public int TypeRank { get; init; }
    /// <summary>
    /// 순위가 매겨진 같은 종류 소속 수입니다.
    /// </summary>
    public int TypeRankedOf { get; init; }
    /// <summary>
    /// 티어별 사용자 수입니다. (티어로 색인)
    /// </summary>
    public List<int> TierCounts { get; init; } = [];
    /// <summary>
    /// 푼 문제의 난이도 합으로 계산한 레이팅입니다.
    /// </summary>
    public int RatingByProblemsSum { get; init; }
    /// <summary>
    /// 클래스로 계산한 레이팅입니다.
    /// </summary>
    public int RatingByClass { get; init; }
    /// <summary>
    /// 푼 문제 수로 계산한 레이팅입니다.
    /// </summary>
    public int RatingBySolvedCount { get; init; }
    /// <summary>
    /// 레이팅 계산에 들어가는 문제의 레벨별 개수입니다.
    /// </summary>
    public List<int> RatingProblemsHistogram { get; init; } = [];
    /// <summary>
    /// 소속의 클래스입니다.
    /// </summary>
    public int RatingClass { get; init; }
    /// <summary>
    /// 소속의 클래스가 Class+ 인지 여부입니다.
    /// </summary>
    public bool RatingClassPlus { get; init; }
}
