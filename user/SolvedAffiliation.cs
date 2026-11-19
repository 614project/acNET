using AcNET.Type;

namespace AcNET.User;

/// <summary>
/// 다른 응답에 포함되는 소속(학교, 회사, 커뮤니티) 정보입니다.
/// </summary>
public class SolvedAffiliationRef : Jsonable
{
    /// <summary>
    /// 소속 ID 입니다. 소속 상세(<see cref="SolvedAPI.GetAffiliationAsync"/>)에 사용합니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 소속 랭킹 ID 입니다. 소속 안의 사용자 랭킹(<see cref="SolvedAPI.GetInAffiliationRankingAsync"/>)에 사용합니다.
    /// <see cref="Id"/> 와 값이 다르니 주의하세요.
    /// </summary>
    public long AffiliationId { get; set; }
    /// <summary>
    /// 소속 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 소속 종류입니다.
    /// </summary>
    public SolvedAffiliationType Type { get; set; }
    /// <summary>
    /// 로고 썸네일 주소입니다.
    /// </summary>
    public string? LogoThumbnailUrl { get; set; }
    /// <summary>
    /// 소속 색입니다.
    /// </summary>
    public string? Color { get; set; }
}

/// <summary>
/// 랭킹이 있는 소속입니다. (<see cref="SolvedAPI.GetAffiliationListAsync"/>)
/// </summary>
public class SolvedRankableAffiliation : SolvedAffiliationRef
{
    /// <summary>
    /// 소속 사용자 수입니다.
    /// </summary>
    public int UserCount { get; set; }
}

/// <summary>
/// 소속 상세 정보입니다. (<see cref="SolvedAPI.GetAffiliationAsync"/>)
/// </summary>
public class SolvedAffiliation : Jsonable
{
    /// <summary>
    /// 소속 ID 입니다. 소속 상세(<see cref="SolvedAPI.GetAffiliationAsync"/>)에 사용합니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 소속 랭킹 ID 입니다. 소속 안의 사용자 랭킹(<see cref="SolvedAPI.GetInAffiliationRankingAsync"/>)에 사용합니다.
    /// <see cref="Id"/> 와 값이 다르니 주의하세요.
    /// </summary>
    public long AffiliationId { get; set; }
    /// <summary>
    /// 소속 이름입니다.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 소속 종류입니다.
    /// </summary>
    public SolvedAffiliationType Type { get; set; }
    /// <summary>
    /// 국가 코드입니다.
    /// </summary>
    public string? CountryCode { get; set; }
    /// <summary>
    /// 로고 주소입니다.
    /// </summary>
    public string? LogoImageUrl { get; set; }
    /// <summary>
    /// 로고 썸네일 주소입니다.
    /// </summary>
    public string? LogoThumbnailUrl { get; set; }
    /// <summary>
    /// 소속 색입니다.
    /// </summary>
    public string? Color { get; set; }
    /// <summary>
    /// 웹사이트 주소입니다.
    /// </summary>
    public string? Website { get; set; }
    /// <summary>
    /// 소속 레이팅입니다.
    /// </summary>
    public int Rating { get; set; }
    /// <summary>
    /// 소속 사용자 수입니다.
    /// </summary>
    public int UserCount { get; set; }
    /// <summary>
    /// 소속 사용자들이 푼 문제 수입니다.
    /// </summary>
    public int SolvedCount { get; set; }
    /// <summary>
    /// 소속 사용자들의 기여 수입니다.
    /// </summary>
    public int VoteCount { get; set; }
    /// <summary>
    /// 요청한 언어로 표시되는 이름입니다.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;
    /// <summary>
    /// 전체 소속 중 순위입니다.
    /// </summary>
    public int Rank { get; set; }
    /// <summary>
    /// 순위가 매겨진 전체 소속 수입니다.
    /// </summary>
    public int RankedOf { get; set; }
    /// <summary>
    /// 같은 종류 소속 중 순위입니다.
    /// </summary>
    public int TypeRank { get; set; }
    /// <summary>
    /// 순위가 매겨진 같은 종류 소속 수입니다.
    /// </summary>
    public int TypeRankedOf { get; set; }
    /// <summary>
    /// 티어별 사용자 수입니다. (티어로 색인)
    /// </summary>
    public List<int> TierCounts { get; set; } = [];
    /// <summary>
    /// 푼 문제의 난이도 합으로 계산한 레이팅입니다.
    /// </summary>
    public int RatingByProblemsSum { get; set; }
    /// <summary>
    /// 클래스로 계산한 레이팅입니다.
    /// </summary>
    public int RatingByClass { get; set; }
    /// <summary>
    /// 푼 문제 수로 계산한 레이팅입니다.
    /// </summary>
    public int RatingBySolvedCount { get; set; }
    /// <summary>
    /// 레이팅 계산에 들어가는 문제의 레벨별 개수입니다.
    /// </summary>
    public List<int> RatingProblemsHistogram { get; set; } = [];
    /// <summary>
    /// 소속의 클래스입니다.
    /// </summary>
    public int RatingClass { get; set; }
    /// <summary>
    /// 소속의 클래스가 Class+ 인지 여부입니다.
    /// </summary>
    public bool RatingClassPlus { get; set; }
}
