using acNET.Coin;
using acNET.Image;
using acNET.Problem;
using acNET.Ranking;
using acNET.Search;
using acNET.User;
using RestSharp;

namespace acNET;


public partial class acAPI
{
    /// <summary>
    ///API 처리 결과입니다. (동기)
    /// </summary>
    /// <typeparam name="T">반환할 데이터 형식</typeparam>
    /// <param name="Result">결과 (null일경우 실패)</param>
    /// <param name="Exception">예외 (null일경우 성공)</param>
    public record acResult<T>(T? Result,Exception? Exception)
    {
        /// <summary>
        /// Result만 바로 가져옵니다.
        /// </summary>
        /// <param name="me">요청 결과</param>
        public static implicit operator T?(acResult<T> me) => me.Result;
        /// <summary>
        /// Exception만 바로 가져옵니다.
        /// </summary>
        /// <param name="me">요청 결과</param>
        public static implicit operator Exception?(acResult<T> me) => me.Exception;
        /// <summary>
        /// 결과를 바로 가져옵니다. 만약 null일경우 예외를 일으킵니다.
        /// </summary>
        /// <returns>결과</returns>
        public T GetResultOrThrow() => this.Result ?? throw this.Exception ?? new NullReferenceException();
        /// <summary>
        /// Exception != null 이라면 예외를, 그렇지 않으면 Result를 .ToString() 한 결과를 가져옵니다.
        /// </summary>
        /// <returns>문자열</returns>
        public override string ToString() => Exception is not null ? Exception.ToString() : Result?.ToString() ?? string.Empty;
    }
    #region Background
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<Background> GetBackground(in string backgroundId) => Get<Background>("background/show" , $"?backgroundId={backgroundId}");
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<Background>> GetBackgroundAsync(string backgroundId) => await AsyncGetRequest<Background>(new RestRequest("background/show").AddQueryParameter("backgroundId",backgroundId));
    #endregion
    #region Badge
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<Badge.Badge> GetBadge(in string badgeId) => Get<Badge.Badge>("badge/show" , $"?badgeId={badgeId}");
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<Badge.Badge>> GetBadgeAsync(string badgeId) => await AsyncGetRequest<Badge.Badge>(new RestSharp.RestRequest("badge/show").AddQueryParameter("badgeId", badgeId));
    #endregion
    #region ExchangeRate
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acResult<ExchangeRate> GetExchangeRate() => Get<ExchangeRate>("coins/exchange_rate");
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<acResult<ExchangeRate>> GetExchangeRateAsync() => await AsyncGetRequest<ExchangeRate>(new RestRequest("coins/exchange_rate"));
    #endregion
    #region ShopList
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acResult<List<ShopItem>> GetShopList() => Get<List<ShopItem>>("coins/shop/list");
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<ShopItem>>> GetShopListAsync() => await AsyncGetRequest<List<ShopItem>>(new RestRequest("coins/shop/list"));
    #endregion
    #region ClassList
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acResult<List<ClassInfo>> GetClassList() => Get<List<ClassInfo>>("problem/class");
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<ClassInfo>>> GetClassListAsync() => await AsyncGetRequest<List<ClassInfo>>(new RestRequest("problem/class"));
    #endregion
    #region Problem
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<TaggedProblem> GetProblem(long problemId) => Get<TaggedProblem>("problem/show" , $"?problemId={problemId}");
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<TaggedProblem>> GetProblemAsync(long problemId) => await AsyncGetRequest<TaggedProblem>(new RestRequest("problem/show").AddQueryParameter("problemId",problemId));
    #endregion
    #region ProblemList
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public acResult<List<TaggedProblem>> GetProblemList(params long[] problemIds) => GetProblemList(string.Join(',' , problemIds));
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public acResult<List<TaggedProblem>> GetProblemList(in string problemIds) => Get<List<TaggedProblem>>("problem/lookup" , $"?problemIds={problemIds}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<TaggedProblem>>> GetProblemListAsync(string problemIds) => await AsyncGetRequest<List<TaggedProblem>>(new RestRequest("problem/lookup").AddQueryParameter("problemIds" , problemIds));
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<TaggedProblem>>> GetProblemListAsync(params long[] problemIds) => await GetProblemListAsync(string.Join(',' , problemIds));
    #endregion
    #region LevelList
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acResult<List<Level>> GetLevelList() => Get<List<Level>>("problem/level");
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<Level>>> GetLevelListAsync() => await AsyncGetRequest<List<Level>>(new RestRequest("problem/level"));
    #endregion
    #region TierRanking
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acResult<UserRanking> GetTierRanking(int page) => Get<UserRanking>("ranking/tier" , $"?page={page}");
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<UserRanking>> GetTierRankingAsync(int page) => await AsyncGetRequest<UserRanking>(new RestRequest("ranking/tier").AddQueryParameter("page",page));
    #endregion
    #region ClassRanking
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acResult<UserRanking> GetClassRanking(int page) => Get<UserRanking>("ranking/class"  , $"?page={page}");
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<UserRanking>> GetClassRankingAsync(int page) => await AsyncGetRequest<UserRanking>(new RestRequest("ranking/class").AddQueryParameter("page" , page));
    #endregion
    #region StreakRanking
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acResult<UserRanking> GetStreakRanking(int page) => Get<UserRanking>("ranking/streak" , $"?page={page}");
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<UserRanking>> GetStreakRankingAsync(int page) => await AsyncGetRequest<UserRanking>(new RestRequest("ranking/streak").AddQueryParameter("page" , page));
    #endregion
    #region ContributionRanking
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acResult<UserRanking> GetContributionRanking(int page) => Get<UserRanking>("ranking/contribution" , $"?page={page}");
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<UserRanking>> GetContributionRankingAsync(int page) => await AsyncGetRequest<UserRanking>(new RestRequest("ranking/contribution").AddQueryParameter("page" , page));
    #endregion
    #region OriganizationRanking
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<OrganizationRanking>> GetOrganizationRankingAsync(int page) => await AsyncGetRequest<OrganizationRanking>(new RestRequest("ranking/organization").AddQueryParameter("page" , page));
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public acResult<OrganizationRanking> GetOrganizationRanking(int page) => Get<OrganizationRanking>("ranking/organization" , $"?page={page}");
    #endregion
    #region SearchUser
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public acResult<SearchResult<RankedUser>> GetSearchUser(in string query) => Get<SearchResult<RankedUser>>("search/user" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<SearchResult<RankedUser>>> GetSearchUserAsync(string query) => await AsyncGetRequest<SearchResult<RankedUser>>(new RestRequest("search/user").AddQueryParameter("query" , query));
    #endregion
    #region SearchTag
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public acResult<SearchResult<ProblemTag>> GetSearchTag(in string query) => Get<SearchResult<ProblemTag>>("search/tag" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<SearchResult<ProblemTag>>> GetSearchTagAsync(string query) => await AsyncGetRequest<SearchResult<ProblemTag>>(new RestRequest("search/tag").AddQueryParameter("query" , query));
    #endregion
    #region SearchProblem
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public acResult<SearchResult<TaggedProblem>> GetSearchProblem(in string query , bool descending_order = false) => Get<SearchResult<TaggedProblem>>("search/problem" , $"?direction={(descending_order ? "desc" : "asc")}&query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<SearchResult<TaggedProblem>>> GetSearchProblemAsync(string query , bool descending_order = false) => await AsyncGetRequest<SearchResult<TaggedProblem>>(new RestRequest("search/problem").AddQueryParameter("direction",descending_order ? "desc" : "asc").AddQueryParameter("query",query));
    #endregion
    #region UserContributionStats
    /// <summary>
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<List<UserContributionStat>> GetUserContributionStats(in string handle) => Get<List<UserContributionStat>>("user/contribution_stats" , $"?handle={handle}");
    /// <summary>
    /// 유저의 티어별 기여도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<UserContributionStat>>> GetUserContributionStatsAsync(string handle) => await AsyncGetRequest<List<UserContributionStat>>(new RestRequest("user/contribution_stats").AddQueryParameter("handle" , handle));
    #endregion
    #region UserClassStats
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<List<UserClassStat>> GetUserClassStats(in string handle) => Get<List<UserClassStat>>("user/class_stats" , $"?handle={handle}");
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<UserClassStat>>> GetUserClassStatsAsync(string handle) => await AsyncGetRequest<List<UserClassStat>>(new RestRequest("user/class_stats").AddQueryParameter("handle" , handle));
    #endregion
    #region ProblemTagStats
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<SearchResult<TagStat>> GetProblemTagStats(in string handle) => Get<SearchResult<TagStat>>("user/problem_tag_stats" , $"?handle={handle}");
    /// <summary>
    /// 유저의 태그별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<SearchResult<TagStat>>> GetProblemTagStatsAsync(string handle) => await AsyncGetRequest<SearchResult<TagStat>>(new RestRequest("user/problem_tag_stats").AddQueryParameter("handle" , handle));
    #endregion
    #region SiteStats
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public acResult<Site.Stats> GetSiteStats() => Get<Site.Stats>("site/stats");
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<acResult<Site.Stats>> GetSiteStatsAsync() => await AsyncGetRequest<Site.Stats>(new RestRequest("site/stats"));
    #endregion
    #region OriganizationFromUser
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<List<Organization>> GetOrganizationsFromUser(in string handle) => Get<List<Organization>>("user/organizations" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<List<Organization>>> GetOrganizationsFromUserAsync(string handle) => await AsyncGetRequest<List<Organization>>(new RestRequest("user/organizations").AddQueryParameter("handle" , handle));
    #endregion
    #region SolvedFromUser
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<UserSolvedInfo> GetSolvedFromUser(in string handle) => Get<UserSolvedInfo>("user/problem_stats" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<UserSolvedInfo>> GetSolvedFromUserAsync(string handle) => await AsyncGetRequest<UserSolvedInfo>(new RestRequest("user/problem_stats").AddQueryParameter("handle" , handle));
    #endregion
    #region User
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<RankedUser> GetUser(in string handle) => Get<RankedUser>("user/show" , $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<RankedUser>> GetUserAsync(string handle) => await AsyncGetRequest<RankedUser>(new RestRequest("user/show").AddQueryParameter("handle" , handle));
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <param name="language">응답을 받을 언어입니다.</param>
    /// <returns>실패시 null</returns>
    public acResult<RankedUser> GetUser(in string handle , solvedacLanguage language) => Get<RankedUser>("user/show" , $"?handle={handle}" , _convert_language(language));
    #endregion
    #region Top100FromUser
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public acResult<Top100> GetTop100FromUser(in string handle) => Get<Top100>("user/top_100" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<acResult<Top100>> GetTop100FromUserAsync(string handle) => await AsyncGetRequest<Top100>(new RestRequest("user/top_100").AddQueryParameter("handle" , handle));
    #endregion
    #region PostFromTitle
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public acResult<Post> GetPostFromTitle(in string postId) => Get<Post>("post/show" , $"?postId={postId}");
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public async Task<acResult<Post>>GetPostFromTitleAsync(string postId) => await AsyncGetRequest<Post>(new RestRequest("post/show").AddQueryParameter("postId" , postId));
    #endregion
    #region AdditionalInfo
    /// <summary>
    /// 해당 핸들을 가진 사용자의 부가 정보를 가져옵니다.
    /// </summary>
    /// <param name="handle">요청할 사용자명</param>
    /// <returns>부가 정보를 가져옵니다. 실패시 null</returns>
    public acResult<AdditionalInformation> GetUserAdditionalInfo(in string handle) => Get<AdditionalInformation>("user/additional_info" , $"?handle={handle}");
    /// <summary>
    /// 해당 핸들을 가진 사용자의 부가 정보를 가져옵니다.
    /// </summary>
    /// <param name="handle">요청할 사용자명</param>
    /// <returns>부가 정보를 가져옵니다. 실패시 null</returns>
    public async Task<acResult<AdditionalInformation>> GetUserAdditionalInfoAsync(string handle) => await AsyncGetRequest<AdditionalInformation>(new RestRequest("user/additional_info").AddQueryParameter("handle" , handle));
    #endregion
    #region SearchAutoComplete
    /// <summary>
    /// 주어진 쿼리에 따라 검색할 때 도움이 되도록 자동 완성 및 상위 검색 결과를 반환합니다. 자동 완성 결과는 언어에 의존적입니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>자동 완성 및 상위 검색 결과를 반환합니다.</returns>
    public acResult<Suggestion> GetSearchAutoComplete(in string query) => Get<Suggestion>("search/suggestion" , $"?query={query}");

    /// <summary>
    /// 주어진 쿼리에 따라 검색할 때 도움이 되도록 자동 완성 및 상위 검색 결과를 반환합니다. 자동 완성 결과는 언어에 의존적입니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>자동 완성 및 상위 검색 결과를 반환합니다.</returns>
    public async Task<acResult<Suggestion>> GetSearchAutoCompleteAsync(string query) => await AsyncGetRequest<Suggestion>(new RestRequest("search/suggestion").AddQueryParameter("query" , query));
    #endregion
}
