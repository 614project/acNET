using acNET.Coin;
using acNET.Image;
using acNET.Problem;
using acNET.Ranking;
using acNET.Search;
using acNET.User;
using System.Security.Cryptography;

namespace acNET;


public partial class acAPI
{
    #region Background
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public Background? GetBackground(string backgroundId) => GETwithoutERROR<Background>("background/show",$"?backgroundId={backgroundId}");
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public Background? GetBackground(string backgroundId,out acAPIError? error) => GET<Background>("background/show" , out error, $"?backgroundId={backgroundId}");
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<(Background?,Exception?)> GetBackgroundAsync(string backgroundId) => await GetAsync<Background>("background/show" , $"?backgroundId={backgroundId}");
    #endregion
    #region Badge
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
    public async Task<(Badge.Badge?,Exception?)> GetBadgeAsync(string badgeId) => await GetAsync<Badge.Badge>("badge/show" , $"?badgeId={badgeId}");
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public Badge.Badge? GetBadge(string badgeId,out acAPIError? error) => GET<Badge.Badge>("badge/show" ,out error, $"?badgeId={badgeId}");
    #endregion
    #region ExchangeRate
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public ExchangeRate? GetExchangeRate() => GETwithoutERROR<ExchangeRate>("coins/exchange_rate");
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<(ExchangeRate?,Exception?)> GetExchangeRateAsync() => await GetAsync<ExchangeRate>("coins/exchange_rate");
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public ExchangeRate? GetExchangeRate(out acAPIError? error) => GET<ExchangeRate>("coins/exchange_rate",out error);
    #endregion
    #region ShopList
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
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<(List<ShopItem>?,Exception?)> GetShopListAsync() => await GetAsync<List<ShopItem>>("coins/shop/list");
    #endregion
    #region ClassList
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ClassInfo>? GetClassList() => GETListWithoutError<ClassInfo>("problem/class");
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<(List<ClassInfo>?,Exception?)> GetClassListAsync() => await GetAsync<List<ClassInfo>>("problem/class");
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public List<ClassInfo>? GetClassList(out acAPIError? error) => GETLIST<ClassInfo>("problem/class", out error);
    #endregion
    #region Problem
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
    public async Task<(TaggedProblem?,Exception?)> GetProblemAsync(long problemId) => await GetAsync<TaggedProblem>("problem/show" , $"?problemId={problemId}");
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public TaggedProblem? GetProblem(long problemId,out acAPIError? error) => GET<TaggedProblem>("problem/show" ,out error, $"?problemId={problemId}");
    #endregion
    #region ProblemList
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
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public async Task<(List<TaggedProblem>?,Exception?)> GetProblemListAsync(string problemIds) => await GetAsync<List<TaggedProblem>>("problem/lookup" , $"?problemIds={problemIds}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public async Task<(List<TaggedProblem>?,Exception?)> GetProblemListAsync(params long[] problemIds) => await GetProblemListAsync(string.Join(',' , problemIds));
    #endregion
    #region LevelList
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
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<(List<Level>?,Exception?)> GetLevelListAsync() => await GetAsync<List<Level>>("problem/level");
    #endregion
    #region TierRanking
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
    public async Task<(UserRanking?,Exception?)> GetTierRankingAsync(int page) => await GetAsync<UserRanking>("ranking/tier" , $"?page={page}");
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetTierRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/tier" ,out error, $"?page={page}");
    #endregion
    #region ClassRanking
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
    public async Task<(UserRanking?,Exception?)> GetClassRankingAsync(int page) => await GetAsync<UserRanking>("ranking/class" , $"?page={page}");
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetClassRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/class" ,out error, $"?page={page}");
    #endregion
    #region StreakRanking
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
    public async Task<(UserRanking?,Exception?)> GetStreakRankingAsync(int page) => await GetAsync<UserRanking>("ranking/streak" , $"?page={page}");
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetStreakRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/streak" ,out error, $"?page={page}");
    #endregion
    #region ContributionRanking
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
    public async Task<(UserRanking?,Exception?)> GetContributionRankingAsync(int page) => await GetAsync<UserRanking>("ranking/contribution" , $"?page={page}");
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    public UserRanking? GetContributionRanking(int page,out acAPIError? error) => GET<UserRanking>("ranking/contribution" ,out error, $"?page={page}");
    #endregion
    #region OriganizationRanking
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<(OrganizationRanking?,Exception?)> GetOrganizationRankingAsync(int page) => await GetAsync<OrganizationRanking>("ranking/organization" , $"?page={page}");
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
    #endregion
    #region SearchUser
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
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<(SearchResult<RankedUser>?,Exception?)> GetSearchUserAsync(string query) => await GetAsync<SearchResult<RankedUser>>("search/user" , $"?query={query}");
    #endregion
    #region SearchTag
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
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<(SearchResult<ProblemTag>?,Exception?)> GetSearchTagAsync(string query) => await GetAsync<SearchResult<ProblemTag>>("search/tag" , $"?query={query}");
    #endregion
    #region SearchProblem
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
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public async Task<(SearchResult<TaggedProblem>?,Exception?)> GetSearchProblemAsync(string query , bool descending_order = false) => await GetAsync<SearchResult<TaggedProblem>>("search/problem" , $"?direction={(descending_order ? "desc" : "asc")}&query={query}");
    #endregion
    #region UserContributionStats
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
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<(List<UserContributionStat>?,Exception?)> GetUserContributionStatsAsync(string handle) => await GetAsync<List<UserContributionStat>>("user/contribution_stats" , $"?handle={handle}");
    #endregion
    #region UserClassStats
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
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<(List<UserClassStat>?,Exception?)> GetUserClassStatsAsync(string handle) => await GetAsync<List<UserClassStat>>("user/class_stats" , $"?handle={handle}");
    #endregion
    #region ProblemTagStats
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
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<(SearchResult<TagStat>?,Exception?)> GetProblemTagStatsAsync(string handle) => await GetAsync<SearchResult<TagStat>>("user/problem_tag_stats" , $"?handle={handle}");
    #endregion
    #region SiteStats
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public Site.Stats? GetSiteStats(out acAPIError? error) => GET<Site.Stats>("site/stats",out error);
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public Site.Stats? GetSiteStats() => GETwithoutERROR<Site.Stats>("site/stats" );
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<(Site.Stats?,Exception?)> GetSiteStatsAsync() => await GetAsync<Site.Stats>("site/stats");
    #endregion
    #region OriganizationFromUser
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
    public List<Organization>? GetOrganizationsFromUser(string handle) => GETListWithoutError<Organization>("user/organizations", $"?handle={handle}");    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<(List<Organization>?,Exception?)> GetOrganizationsFromUserAsync(string handle) => await GetAsync<List<Organization>>("user/organizations", $"?handle={handle}");
    #endregion
    #region SolvedFromUser
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public UserSolvedInfo? GetSolvedFromUser(string handle) => GETwithoutERROR<UserSolvedInfo>("user/problem_stats" ,  $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<(UserSolvedInfo?,Exception?)> GetSolvedFromUserAsync(string handle) => await GetAsync<UserSolvedInfo>("user/problem_stats" , $"?handle={handle}");
    #endregion
    #region User
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
    public async Task<(RankedUser?,Exception?)> GetUserAsync(string handle) => await GetAsync<RankedUser>("user/show" , $"?handle={handle}");
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
    #endregion
    #region Top100FromUser
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
    /// <returns>실패시 null</returns>
    public async Task<(Top100?,Exception?)> GetTop100FromUserAsync(string handle) => await GetAsync<Top100>("user/top_100" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="error">요청에 성공했으나 응답에 실패한 경우의 예외. 만약 null 일경우 C# 런타임 에러</param>
    /// <returns>실패시 null</returns>
    public Top100? GetTop100FromUser(string handle, out acAPIError? error) => GET<Top100>("user/top_100", out error, $"?handle={handle}");
    #endregion
    #region PostFromTitle
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public Post? GetPostFromTitle(string postId) => GETwithoutERROR<Post>("post/show" , $"?postId={postId}");
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public Post? GetPostFromTitle(string postId,out acAPIError? error) => GET<Post>("post/show" ,out error, $"?postId={postId}");
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public async Task<(Post?,Exception?)>GetPostFromTitleAsync(string postId) => await GetAsync<Post>("post/show" , $"?postId={postId}");
    #endregion
}
