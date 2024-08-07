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
    public Background? GetBackground(string backgroundId) => GETwithoutERROR<Background>("background/show",$"?backgroundId={backgroundId}");
    //background
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public Background? GetBackground(string backgroundId,out acAPIError? error) => GET<Background>("background/show" , out error, $"?backgroundId={backgroundId}");
    //badge
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public Badge.Badge? GetBadge(string badgeId) => GETwithoutERROR<Badge.Badge>("badge/show" , $"?badgeId={badgeId}");
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public Badge.Badge? GetBadge(string badgeId,out acAPIError? error) => GET<Badge.Badge>("badge/show" ,out error, $"?badgeId={badgeId}");
    //coins
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public ExchangeRate? GetExchangeRate() => GETwithoutERROR<ExchangeRate>("coins/exchange_rate");
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public ExchangeRate? GetExchangeRate(out acAPIError? error) => GET<ExchangeRate>("coins/exchange_rate",out error);
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ShopItem>? GetShopList(out acAPIError? error) => GETLIST<ShopItem>("coins/shop/list",out error);
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ShopItem>? GetShopList() => GETListWithoutError<ShopItem>("coins/shop/list");
    //problem
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ClassInfo>? GetClassList() => GETListWithoutError<ClassInfo>("problem/class");
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ClassInfo>? GetClassList(out acAPIError? error) => GETLIST<ClassInfo>("problem/class", out error);
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public TaggedProblem? GetProblem(long problemId) => GETwithoutERROR<TaggedProblem>("problem/show" ,  $"?problemId={problemId}");
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public TaggedProblem? GetProblem(long problemId,out acAPIError? error) => GET<TaggedProblem>("problem/show" ,out error, $"?problemId={problemId}");
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
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public List<TaggedProblem>? GetProblemList(string problemIds,out acAPIError? error) => GETLIST<TaggedProblem>("problem/lookup" ,out error, $"?problemIds={problemIds}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public List<TaggedProblem>? GetProblemList(string problemIds) => GETListWithoutError<TaggedProblem>("problem/lookup", $"?problemIds={problemIds}");
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<Level>? GetLevelList(out acAPIError? error) => GETLIST<Level>("problem/level", out error);
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<Level>? GetLevelList() => GETListWithoutError<Level>("problem/level");
    //ranking
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetTierRanking(int page) => GETwithoutERROR<UserRanking>("ranking/tier" ,$"?page={page}");
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetTierRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/tier" ,out error, $"?page={page}");
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetClassRanking(int page) => GETwithoutERROR<UserRanking>("ranking/class"  , $"?page={page}");
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetClassRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/class" ,out error, $"?page={page}");
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetStreakRanking(int page) => GETwithoutERROR<UserRanking>("ranking/streak"  , $"?page={page}");
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetStreakRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/streak" ,out error, $"?page={page}");
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public UserRanking? GetContributionRanking(int page) => GETwithoutERROR<UserRanking>("ranking/contribution" , $"?page={page}");
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetContributionRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/contribution" ,out error, $"?page={page}");
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public OrganizationRanking? GetOrganizationRanking(int page,out acAPIError? error) => GET<OrganizationRanking>("ranking/organization",out error, $"?page={page}");
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public OrganizationRanking? GetOrganizationRanking(int page) => GETwithoutERROR<OrganizationRanking>("ranking/organization"  , $"?page={page}");
    //search
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public SearchResult<RankedUser>? GetSearchUser(string query) => GETwithoutERROR<SearchResult<RankedUser>>("search/user"  , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public SearchResult<RankedUser>? GetSearchUser(string query , out acAPIError? error) => GET<SearchResult<RankedUser>>("search/user" ,out error, $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public SearchResult<ProblemTag>? GetSearchTag(string query) => GETwithoutERROR<SearchResult<ProblemTag>>("search/tag" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public SearchResult<ProblemTag>? GetSearchTag(string query , out acAPIError? error) => GET<SearchResult<ProblemTag>>("search/tag" ,out error, $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public SearchResult<TaggedProblem>? GetSearchProblem(string query,out acAPIError? error, bool descending_order = false) => GET<SearchResult<TaggedProblem>>("search/problem",out error, $"?direction={(descending_order ? "desc" : "asc")}&query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public SearchResult<TaggedProblem>? GetSearchProblem(string query,bool descending_order=false) => GETwithoutERROR<SearchResult<TaggedProblem>>("search/problem" ,  $"?direction={(descending_order?"desc":"asc")}&query={query}");
    //(내가 추가한거)
    /// <summary>
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public List<UserContributionStat>? GetUserContributionStats(string handle, out acAPIError? error) => GETLIST<UserContributionStat>("user/contribution_stats", out error, $"?handle={handle}");
    /// <summary>
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public List<UserContributionStat>? GetUserContributionStats(string handle) => GETListWithoutError<UserContributionStat>("user/contribution_stats", $"?handle={handle}");
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public List<UserClassStat>? GetUserClassStats(string handle, out acAPIError? error) => GETLIST<UserClassStat>("user/class_stats", out error, $"?handle={handle}");
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public List<UserClassStat>? GetUserClassStats(string handle) => GETListWithoutError<UserClassStat>("user/class_stats", $"?handle={handle}");
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public SearchResult<TagStat>? GetProblemTagStats(string handle, out acAPIError? error) => GET<SearchResult<TagStat>>("user/problem_tag_stats", out error, $"?handle={handle}");
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SearchResult<TagStat>? GetProblemTagStats(string handle) => GETwithoutERROR<SearchResult<TagStat>>("user/problem_tag_stats"  , $"?handle={handle}");
    //other
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public Site.Stats? GetSiteStats() => GETwithoutERROR<Site.Stats>("site/stats" );
    //user
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public List<Organization>? GetOrganizationsFromUser(string handle, out acAPIError? error) => GETLIST<Organization>("user/organizations", out error, $"?handle={handle}");
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public List<Organization>? GetOrganizationsFromUser(string handle) => GETListWithoutError<Organization>("user/organizations", $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public UserSolvedInfo? GetSolvedFromUser(string handle) => GETwithoutERROR<UserSolvedInfo>("user/problem_stats" ,  $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public RankedUser? GetUser(string handle) => GETwithoutERROR<RankedUser>("user/show" ,  $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public RankedUser? GetUser(string handle , out acAPIError? error) => GET<RankedUser>("user/show" ,out error, $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="language">응답을 받을 언어입니다.</param>
    /// <returns>실패시 null</returns>
    public RankedUser? GetUser(string handle,solvedacLanguage language) => GETwithoutERROR<RankedUser>("user/show" , $"?handle={handle}",_convert_language(language));
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public Top100? GetTop100FromUser(string handle) => GETwithoutERROR<Top100>("user/top_100" ,  $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public Top100? GetTop100FromUser(string handle, out acAPIError? error) => GET<Top100>("user/top_100", out error, $"?handle={handle}");
}
