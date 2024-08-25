using acNET.Coin;
using acNET.Image;
using acNET.Problem;
using acNET.Ranking;
using acNET.Search;
using acNET.User;

namespace acNET;


public partial class acAPI
{
    /// <summary>
    /// 비동기 처리 결과입니다.
    /// </summary>
    /// <typeparam name="T">반환할 데이터 형식</typeparam>
    /// <param name="result">결과 (null 일경우 실패)</param>
    /// <param name="exception">예외 (null일경우 성공)</param>
    public record AsyncResult<T>(T? result,Exception? exception);
    /// <summary>
    ///API 처리 결과입니다. (동기)
    /// </summary>
    /// <typeparam name="T">반환할 데이터 형식</typeparam>
    /// <param name="Result">결과 (null일경우 실패)</param>
    /// <param name="Exception">예외 (null일경우 성공)</param>
    public record acAPIResult<T>(T? Result,Exception? Exception);
    #region Background
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<Background> GetBackground(string backgroundId) => Get<Background>("background/show" , $"?backgroundId={backgroundId}");
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<Background>> GetBackgroundAsync(string backgroundId) => await GetAsync<Background>("background/show" , $"?backgroundId={backgroundId}");
    #endregion
    #region Badge
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<Badge.Badge> GetBadge(string badgeId) => Get<Badge.Badge>("badge/show" , $"?badgeId={badgeId}");
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<Badge.Badge>> GetBadgeAsync(string badgeId) => await GetAsync<Badge.Badge>("badge/show" , $"?badgeId={badgeId}");
    #endregion
    #region ExchangeRate
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acAPIResult<ExchangeRate> GetExchangeRate() => Get<ExchangeRate>("coins/exchange_rate");
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<ExchangeRate>> GetExchangeRateAsync() => await GetAsync<ExchangeRate>("coins/exchange_rate");
    #endregion
    #region ShopList
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<ShopItem>> GetShopList() => Get<List<ShopItem>>("coins/shop/list");
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<ShopItem>>> GetShopListAsync() => await GetAsync<List<ShopItem>>("coins/shop/list");
    #endregion
    #region ClassList
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<ClassInfo>> GetClassList() => Get<List<ClassInfo>>("problem/class");
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<ClassInfo>>> GetClassListAsync() => await GetAsync<List<ClassInfo>>("problem/class");
    #endregion
    #region Problem
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<TaggedProblem> GetProblem(long problemId) => Get<TaggedProblem>("problem/show" , $"?problemId={problemId}");
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<TaggedProblem>> GetProblemAsync(long problemId) => await GetAsync<TaggedProblem>("problem/show" , $"?problemId={problemId}");
    #endregion
    #region ProblemList
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<TaggedProblem>> GetProblemList(params long[] problemIds) => GetProblemList(string.Join(',' , problemIds));
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<TaggedProblem>> GetProblemList(string problemIds) => Get<List<TaggedProblem>>("problem/lookup" , $"?problemIds={problemIds}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<TaggedProblem>>> GetProblemListAsync(string problemIds) => await GetAsync<List<TaggedProblem>>("problem/lookup" , $"?problemIds={problemIds}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<TaggedProblem>>> GetProblemListAsync(params long[] problemIds) => await GetProblemListAsync(string.Join(',' , problemIds));
    #endregion
    #region LevelList
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<Level>> GetLevelList() => Get<List<Level>>("problem/level");
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<Level>>> GetLevelListAsync() => await GetAsync<List<Level>>("problem/level");
    #endregion
    #region TierRanking
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<UserRanking> GetTierRanking(int page) => Get<UserRanking>("ranking/tier" , $"?page={page}");
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<UserRanking>> GetTierRankingAsync(int page) => await GetAsync<UserRanking>("ranking/tier" , $"?page={page}");
    #endregion
    #region ClassRanking
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<UserRanking> GetClassRanking(int page) => Get<UserRanking>("ranking/class"  , $"?page={page}");
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<UserRanking>> GetClassRankingAsync(int page) => await GetAsync<UserRanking>("ranking/class" , $"?page={page}");
    #endregion
    #region StreakRanking
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<UserRanking> GetStreakRanking(int page) => Get<UserRanking>("ranking/streak" , $"?page={page}");
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<UserRanking>> GetStreakRankingAsync(int page) => await GetAsync<UserRanking>("ranking/streak" , $"?page={page}");
    #endregion
    #region ContributionRanking
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<UserRanking> GetContributionRanking(int page) => Get<UserRanking>("ranking/contribution" , $"?page={page}");
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<UserRanking>> GetContributionRankingAsync(int page) => await GetAsync<UserRanking>("ranking/contribution" , $"?page={page}");
    #endregion
    #region OriganizationRanking
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<OrganizationRanking>> GetOrganizationRankingAsync(int page) => await GetAsync<OrganizationRanking>("ranking/organization" , $"?page={page}");
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<OrganizationRanking> GetOrganizationRanking(int page) => Get<OrganizationRanking>("ranking/organization" , $"?page={page}");
    #endregion
    #region SearchUser
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<SearchResult<RankedUser>> GetSearchUser(string query) => Get<SearchResult<RankedUser>>("search/user" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<SearchResult<RankedUser>>> GetSearchUserAsync(string query) => await GetAsync<SearchResult<RankedUser>>("search/user" , $"?query={query}");
    #endregion
    #region SearchTag
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<SearchResult<ProblemTag>> GetSearchTag(string query) => Get<SearchResult<ProblemTag>>("search/tag" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<SearchResult<ProblemTag>>> GetSearchTagAsync(string query) => await GetAsync<SearchResult<ProblemTag>>("search/tag" , $"?query={query}");
    #endregion
    #region SearchProblem
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<SearchResult<TaggedProblem>> GetSearchProblem(string query , bool descending_order = false) => Get<SearchResult<TaggedProblem>>("search/problem" , $"?direction={(descending_order ? "desc" : "asc")}&query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<SearchResult<TaggedProblem>>> GetSearchProblemAsync(string query , bool descending_order = false) => await GetAsync<SearchResult<TaggedProblem>>("search/problem" , $"?direction={(descending_order ? "desc" : "asc")}&query={query}");
    #endregion
    #region UserContributionStats
    /// <summary>
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<UserContributionStat>> GetUserContributionStats(string handle) => Get<List<UserContributionStat>>("user/contribution_stats" , $"?handle={handle}");
    /// <summary>
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<UserContributionStat>>> GetUserContributionStatsAsync(string handle) => await GetAsync<List<UserContributionStat>>("user/contribution_stats" , $"?handle={handle}");
    #endregion
    #region UserClassStats
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<UserClassStat>> GetUserClassStats(string handle) => Get<List<UserClassStat>>("user/class_stats" , $"?handle={handle}");
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<UserClassStat>>> GetUserClassStatsAsync(string handle) => await GetAsync<List<UserClassStat>>("user/class_stats" , $"?handle={handle}");
    #endregion
    #region ProblemTagStats
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<SearchResult<TagStat>> GetProblemTagStats(string handle) => Get<SearchResult<TagStat>>("user/problem_tag_stats" , $"?handle={handle}");
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<SearchResult<TagStat>>> GetProblemTagStatsAsync(string handle) => await GetAsync<SearchResult<TagStat>>("user/problem_tag_stats" , $"?handle={handle}");
    #endregion
    #region SiteStats
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acAPIResult<Site.Stats> GetSiteStats() => Get<Site.Stats>("site/stats");
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<Site.Stats>> GetSiteStatsAsync() => await GetAsync<Site.Stats>("site/stats");
    #endregion
    #region OriganizationFromUser
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<List<Organization>> GetOrganizationsFromUser(string handle) => Get<List<Organization>>("user/organizations" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<List<Organization>>> GetOrganizationsFromUserAsync(string handle) => await GetAsync<List<Organization>>("user/organizations", $"?handle={handle}");
    #endregion
    #region SolvedFromUser
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<UserSolvedInfo> GetSolvedFromUser(string handle) => Get<UserSolvedInfo>("user/problem_stats" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<UserSolvedInfo>> GetSolvedFromUserAsync(string handle) => await GetAsync<UserSolvedInfo>("user/problem_stats" , $"?handle={handle}");
    #endregion
    #region User
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<RankedUser> GetUser(string handle) => Get<RankedUser>("user/show" , $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<RankedUser>> GetUserAsync(string handle) => await GetAsync<RankedUser>("user/show" , $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="language">응답을 받을 언어입니다.</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<RankedUser> GetUser(string handle , solvedacLanguage language) => Get<RankedUser>("user/show" , $"?handle={handle}" , _convert_language(language));
    #endregion
    #region Top100FromUser
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acAPIResult<Top100> GetTop100FromUser(string handle) => Get<Top100>("user/top_100" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<AsyncResult<Top100>> GetTop100FromUserAsync(string handle) => await GetAsync<Top100>("user/top_100" , $"?handle={handle}");
    #endregion
    #region PostFromTitle
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public acAPIResult<Post> GetPostFromTitle(string postId) => Get<Post>("post/show" , $"?postId={postId}");
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public async Task<AsyncResult<Post>>GetPostFromTitleAsync(string postId) => await GetAsync<Post>("post/show" , $"?postId={postId}");
    #endregion
}
