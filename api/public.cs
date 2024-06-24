using acNET.Account;
using acNET.Badge;
using acNET.Coin;
using acNET.Image;
using acNET.Problem;
using acNET.Ranking;
using acNET.Search;
using acNET.User;

namespace acNET;


public partial class acAPI
{
    //background
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public Background? GetBackground(string backgroundId) => GET<Background>("background/show",out _, $"?backgroundId={backgroundId}");
    //badge
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public Badge.Badge? GetBadge(string badgeId) => GET<Badge.Badge>("badge/show" , out _ , $"?badgeId={badgeId}");
    //coins
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public ExchangeRate? GetExchangeRate() => GET<ExchangeRate>("coins/exchange_rate", out _ );
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ShopItem>? GetShopList() => GETLIST<ShopItem>("coins/shop/list");
    //problem
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ClassInfo>? GetClassList() => GETLIST<ClassInfo>("problem/class");
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public TaggedProblem? GetProblem(long problemId) => GET<TaggedProblem>("problem/show" , out _ , $"?problemId={problemId}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public List<TaggedProblem>? GetProblemList(params long[] problemIds) => GetProblemList(string.Join(',', problemIds));
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public List<TaggedProblem>? GetProblemList(string problemIds) => GETLIST<TaggedProblem>("problem/lookup" , $"?problemIds={problemIds}");
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<Level>? GetLevelList() => GETLIST<Level>("problem/level");
    //ranking
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetTierRanking(int page) => GET<UserRanking>("ranking/tier" , out _ ,$"?page={page}");
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetClassRanking(int page) => GET<UserRanking>("ranking/class" , out _ , $"?page={page}");
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetStreakRanking(int page) => GET<UserRanking>("ranking/streak" , out _ , $"?page={page}");
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetContributionRanking(int page) => GET<UserRanking>("ranking/contribution" , out _ , $"?page={page}");
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public OrganizationRanking? GetOrganizationRanking(int page) => GET<OrganizationRanking>("ranking/organization" , out _ , $"?page={page}");
    //search
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public SearchResult<RankedUser>? GetSearchUser(string query) => GET<SearchResult<RankedUser>>("search/user" , out _ , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public SearchResult<ProblemTag>? GetSearchTag(string query) => GET<SearchResult<ProblemTag>>("search/tag" , out _ , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public SearchResult<TaggedProblem>? GetSearchProblem(string query,bool descending_order=false) => GET<SearchResult<TaggedProblem>>("search/problem" , out _ , $"?direction={(descending_order?"desc":"asc")}&query={query}");
    //(내가 추가한거)
    /// <summary>
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public List<UserContributionStat>? GetUserContributionStats(string handle) => GETLIST<UserContributionStat>("user/contribution_stats", $"?handle={handle}");
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public List<UserClassStat>? GetUserClassStats(string handle) => GETLIST<UserClassStat>("user/class_stats", $"?handle={handle}");
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SearchResult<TagStat>? GetProblemTagStats(string handle) => GET<SearchResult<TagStat>>("user/problem_tag_stats" , out _ , $"?handle={handle}");
    //other
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public Site.Stats? GetSiteStats() => GET<Site.Stats>("site/stats" , out _);
    //user
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public List<Organization>? GetOrganizationsFromUser(string handle) => GETLIST<Organization>("user/organizations", $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public UserSolvedInfo? GetSolvedFromUser(string handle) => GET<UserSolvedInfo>("user/problem_stats" , out _ , $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public RankedUser? GetUser(string handle) => GET<RankedUser>("user/show" , out _ , $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="language">응답을 받을 언어입니다.</param>
    /// <returns>실패시 null</returns>
    public RankedUser? GetUser(string handle,solvedacLanguage language) => GET<RankedUser>("user/show" , out _ , $"?handle={handle}",_convert_language(language));
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public Top100? GetTop100FromUser(string handle) => GET<Top100>("user/top_100" , out _ , $"?handle={handle}");
}
