using AcNET.Type;
using AcNET.User;

namespace AcNET.Ranking;

/// <summary>
/// 랭킹에 표시되는 사용자입니다.
/// </summary>
public class SolvedRankedUser : SolvedUserRef
{
    /// <summary>
    /// 이 랭킹에서의 순위입니다.
    /// </summary>
    public int Rank { get; set; }
    /// <summary>
    /// 레이팅입니다.
    /// </summary>
    public int Rating { get; set; }
    /// <summary>
    /// 푼 문제 수입니다.
    /// </summary>
    public int SolvedCount { get; set; }
    /// <summary>
    /// 기여 수입니다.
    /// </summary>
    public int VoteCount { get; set; }
    /// <summary>
    /// 평판입니다.
    /// </summary>
    public int Reputation { get; set; }
    /// <summary>
    /// 클래스입니다.
    /// </summary>
    public int Class { get; set; }
    /// <summary>
    /// Class+ 여부입니다.
    /// </summary>
    public bool ClassPlus { get; set; }
    /// <summary>
    /// 태그 랭킹에서의 태그 레이팅입니다. 태그 랭킹이 아니면 null 입니다.
    /// </summary>
    public int? TagRating { get; set; }
    /// <summary>
    /// 전체 랭킹에서의 순위입니다. (지역, 소속, 태그 랭킹에서 함께 표시)
    /// </summary>
    public int? GlobalRank { get; set; }
}

/// <summary>
/// 라이벌 랭킹에 표시되는 사용자입니다.
/// </summary>
public class SolvedRivalRankedUser : SolvedRankedUser
{
    /// <summary>
    /// 요청한 사용자의 라이벌인지 여부입니다.
    /// </summary>
    public bool IsRival { get; set; }
    /// <summary>
    /// 요청한 사용자를 라이벌로 등록했는지 여부입니다.
    /// </summary>
    public bool IsReverseRival { get; set; }
}

/// <summary>
/// 사용자 검색 결과의 한 사용자입니다.
/// </summary>
public class SolvedUserSearchItem : SolvedRankedUser
{
    /// <summary>
    /// 전체 랭킹에서 이 사용자가 있는 페이지입니다.
    /// </summary>
    public int Page { get; set; }
}

/// <summary>
/// 사용자명 접두사로 검색한 결과입니다. (<see cref="SolvedAPI.GetSearchUserAsync"/>)
/// </summary>
public class SolvedUserSearch : Jsonable
{
    /// <summary>
    /// 사용자명이 정확히 일치하는 사용자입니다. 없으면 null 입니다.
    /// </summary>
    public SolvedUserSearchItem? Exact { get; set; }
    /// <summary>
    /// 접두사가 일치하는 사용자들입니다. (최대 10명, 2자 미만이면 비어있음)
    /// </summary>
    public List<SolvedUserSearchItem> Items { get; set; } = [];
}

/// <summary>
/// 사용자 랭킹 한 페이지입니다.
/// </summary>
/// <typeparam name="TUser">랭킹에 표시되는 사용자의 형식</typeparam>
public class SolvedUserRanking<TUser> : Jsonable where TUser : SolvedRankedUser
{
    /// <summary>
    /// 요청한 사용자 본인의 순위 정보입니다. 사용자 토큰이 없거나 랭킹에 없으면 null 입니다.
    /// </summary>
    public SolvedRankedUser? Me { get; set; }
    /// <summary>
    /// 요청한 사용자가 있는 페이지입니다.
    /// </summary>
    public int? MePage { get; set; }
    /// <summary>
    /// 랭킹에 있는 전체 사용자 수입니다.
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
    /// 이 페이지의 사용자들입니다.
    /// </summary>
    public List<TUser> Items { get; set; } = [];
    /// <summary>
    /// 목록을 순서대로 출력합니다. 각 항목마다 줄바꿈이 포함됩니다.
    /// </summary>
    public override string ToString() => string.Join('\n', Items);
}

/// <summary>
/// 사용자 랭킹 한 페이지입니다. (전체, 지역, 소속, 태그 랭킹)
/// </summary>
public class SolvedUserRanking : SolvedUserRanking<SolvedRankedUser>
{
}

/// <summary>
/// 라이벌(또는 역라이벌) 랭킹 한 페이지입니다.
/// </summary>
public class SolvedRivalRanking : SolvedUserRanking<SolvedRivalRankedUser>
{
    /// <summary>
    /// 요청한 사용자의 라이벌 수입니다.
    /// </summary>
    public int RivalCount { get; set; }
    /// <summary>
    /// 요청한 사용자의 역라이벌 수입니다.
    /// </summary>
    public int ReverseRivalCount { get; set; }
}

/// <summary>
/// 랭킹이 있는 지역입니다.
/// </summary>
public class SolvedRegion : Jsonable
{
    /// <summary>
    /// 국가 코드입니다. (ISO 3166-1 alpha-2)
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;
    /// <summary>
    /// 지역 사용자 수입니다.
    /// </summary>
    public int UserCount { get; set; }
}

/// <summary>
/// 랭킹이 있는 태그입니다.
/// </summary>
public class SolvedRankedTag : Jsonable
{
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 태그 레이팅 슬롯 번호입니다.
    /// </summary>
    public int RatingTagIndex { get; set; }
    /// <summary>
    /// 요청한 언어로 표시되는 태그 이름입니다.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;
}

/// <summary>
/// 소속 랭킹에 표시되는 소속입니다.
/// </summary>
public class SolvedRankedAffiliation : SolvedAffiliationRef
{
    /// <summary>
    /// 순위입니다. (종류 필터가 있으면 그 종류 안에서의 순위)
    /// </summary>
    public int Rank { get; set; }
    /// <summary>
    /// 전체 소속 중 순위입니다.
    /// </summary>
    public int? GlobalRank { get; set; }
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
    /// 이 소속이 있는 페이지입니다. 요청한 사용자의 소속(<see cref="SolvedAffiliationRanking.Mine"/>)에서만 채워집니다.
    /// </summary>
    public int? Page { get; set; }
}

/// <summary>
/// 소속 랭킹 한 페이지입니다. (<see cref="SolvedAPI.GetAffiliationRankingAsync"/>)
/// </summary>
public class SolvedAffiliationRanking : Jsonable
{
    /// <summary>
    /// 요청한 사용자의 소속들입니다.
    /// </summary>
    public List<SolvedRankedAffiliation> Mine { get; set; } = [];
    /// <summary>
    /// 랭킹에 있는 전체 소속 수입니다.
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
    /// 이 페이지의 소속들입니다.
    /// </summary>
    public List<SolvedRankedAffiliation> Items { get; set; } = [];
}
