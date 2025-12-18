using System.Net;
using AcNET.Account;
using AcNET.Coin;
using AcNET.Events;
using AcNET.Image;
using AcNET.Problem;
using AcNET.Ranking;
using AcNET.Search;
using AcNET.User;
using Newtonsoft.Json;


namespace AcNET;

/// <summary>
/// solved.ac API를 다룰수 있는 클라이언트입니다.
/// </summary>
public class SolvedAPI : IDisposable
{
    /// <summary>
    /// solved.ac API의 기본 주소.
    /// </summary>
    public const string DefaultBaseUrl = "https://solved.ac/api/v3/";
    /// <summary>
    /// SolvedAPI 생성.
    /// </summary>
    /// <param name="solvedacToken">solved.ac의 사용자 접근 토큰.  계정과 관련된 작업시에만 필요하며, 이외에는 null로 비워두어도 사용에 지장이 없습니다.</param>
    /// <param name="baseUrl">solved.ac api의 기본 주소입니다. null로 비워둘 경우 'solved.ac/api/v3' 를 사용합니다.</param>
    public SolvedAPI(string? solvedacToken = null, Uri? baseUrl = null)
    {
        baseUrl ??= new(DefaultBaseUrl);
        HttpClientHandler handler = new();
        if (solvedacToken != null)
        {
            handler.CookieContainer.Add(baseUrl , new Cookie("solvedacToken" , solvedacToken));
        }
        client = new(handler) {
            BaseAddress = baseUrl
        };
    }
    /// <summary>
    /// SolvedAPI 리소스를 정리합니다.
    /// </summary>
    public void Dispose()
    {
        client.Dispose();
    }
    /// <summary>
    /// 응답을 받을 언어를 지정합니다. ["ko","en"," ja"] 중에 한가지를 선택하거나 null로 비워두세요. (x-solvedac-language)
    /// </summary>
    public string? Language {
        set {
            client.DefaultRequestHeaders.Remove("x-solvedac-language");
            if (value == null)
                return;
            client.DefaultRequestHeaders.Add("x-solvedac-language" , value);
        }
    }
    #region AccountVerifyCredentials
    /// <summary>
    /// 현재 로그인한 계정 정보를 가져옵니다.
    /// </summary>
    /// <returns>크레데션 관련 본인 정보</returns>
    public SolvedResult<SolvedUserCredentials> GetAccountVerifyCredentials() => Get<SolvedUserCredentials>("account/verify_credentials");
    /// <summary>
    /// 현재 로그인한 계정 정보를 가져옵니다.
    /// </summary>
    /// <returns>크레데션 관련 본인 정보</returns>
    public async Task<SolvedResult<SolvedUserCredentials>> GetAccountVerifyCredentialsAsync() => await GetAsync<SolvedUserCredentials>("account/verify_credentials");
    #endregion
    #region Background
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedBackground> GetBackground(in string backgroundId) => Get<SolvedBackground>("background/show" , $"?backgroundId={backgroundId}");
    /// <summary>
    /// 배경의 정보를 가져옵니다.
    /// </summary>
    /// <param name="backgroundId">배경 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedBackground>> GetBackgroundAsync(string backgroundId) => await GetAsync<SolvedBackground>("background/show" , $"?backgroundId={backgroundId}");
    #endregion
    #region Badge
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<Badge.SolvedBadge> GetBadge(in string badgeId) => Get<Badge.SolvedBadge>("badge/show" , $"?badgeId={badgeId}");
    /// <summary>
    /// 뱃지의 정보를 가져옵니다.
    /// </summary>
    /// <param name="badgeId">뱃지 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<Badge.SolvedBadge>> GetBadgeAsync(string badgeId) => await GetAsync<Badge.SolvedBadge>("badge/show" , $"?badgeId={badgeId}");
    #endregion
    #region ExchangeRate
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedExchangeRate> GetExchangeRate() => Get<SolvedExchangeRate>("coins/exchange_rate");
    /// <summary>
    /// 현재 코인->별조각 환율을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedExchangeRate>> GetExchangeRateAsync() => await GetAsync<SolvedExchangeRate>("coins/exchange_rate");
    #endregion
    #region ShopList
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedShopItem>> GetShopList() => Get<List<SolvedShopItem>>("coins/shop/list");
    /// <summary>
    /// 코인샵에서 팔고 있는 상품 목록을 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedShopItem>>> GetShopListAsync() => await GetAsync<List<SolvedShopItem>>("coins/shop/list");
    #endregion
    #region ClassList
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedClassInfo>> GetClassList() => Get<List<SolvedClassInfo>>("problem/class");
    /// <summary>
    /// 문제 개수를 문제 CLASS별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedClassInfo>>> GetClassListAsync() => await GetAsync<List<SolvedClassInfo>>("problem/class");
    #endregion
    #region Problem
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedTaggedProblem> GetProblem(long problemId) => Get<SolvedTaggedProblem>("problem/show" , $"?problemId={problemId}");
    /// <summary>
    /// 해당하는 ID의 문제를 가져옵니다.
    /// </summary>
    /// <param name="problemId">문제 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedTaggedProblem>> GetProblemAsync(long problemId) => await GetAsync<SolvedTaggedProblem>("problem/show" , $"?problemId={problemId}");
    #endregion
    #region ProblemList
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedTaggedProblem>> GetProblemList(params long[] problemIds) => GetProblemList(string.Join(',' , problemIds));
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedTaggedProblem>> GetProblemList(in string problemIds) => Get<List<SolvedTaggedProblem>>("problem/lookup" , $"?problemIds={problemIds}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">쉼표로 구분한 문제 ID 목록 (공백이 없어야 됩니다.)</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedTaggedProblem>>> GetProblemListAsync(string problemIds) => await GetAsync<List<SolvedTaggedProblem>>("problem/lookup" , $"?problemIds={problemIds}");
    /// <summary>
    /// 해당하는 ID의 문제 목록을 가져옵니다.
    /// </summary>
    /// <param name="problemIds">문제 ID 배열</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedTaggedProblem>>> GetProblemListAsync(params long[] problemIds) => await GetProblemListAsync(string.Join(',' , problemIds));
    #endregion
    #region LevelList
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedLevel>> GetLevelList() => Get<List<SolvedLevel>>("problem/level");
    /// <summary>
    /// 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedLevel>>> GetLevelListAsync() => await GetAsync<List<SolvedLevel>>("problem/level");
    #endregion
    #region TierRanking
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedUserRanking> GetTierRanking(int page) => Get<SolvedUserRanking>("ranking/tier" , $"?page={page}");
    /// <summary>
    /// 사용자 레이팅에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedUserRanking>> GetTierRankingAsync(int page) => await GetAsync<SolvedUserRanking>("ranking/tier" , $"?page={page}");
    #endregion
    #region ClassRanking
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedUserRanking> GetClassRanking(int page) => Get<SolvedUserRanking>("ranking/class" , $"?page={page}");
    /// <summary>
    /// 사용자 CLASS에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedUserRanking>> GetClassRankingAsync(int page) => await GetAsync<SolvedUserRanking>("ranking/class" , $"?page={page}");
    #endregion
    #region StreakRanking
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedUserRanking> GetStreakRanking(int page) => Get<SolvedUserRanking>("ranking/streak" , $"?page={page}");
    /// <summary>
    /// 최장 스트릭에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedUserRanking>> GetStreakRankingAsync(int page) => await GetAsync<SolvedUserRanking>("ranking/streak" , $"?page={page}");
    #endregion
    #region ContributionRanking
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedUserRanking> GetContributionRanking(int page) => Get<SolvedUserRanking>("ranking/contribution" , $"?page={page}");
    /// <summary>
    /// 기여 횟수에 따른 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedUserRanking>> GetContributionRankingAsync(int page) => await GetAsync<SolvedUserRanking>("ranking/contribution" , $"?page={page}");
    #endregion
    #region OriganizationRanking
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedOrganizationRanking> GetOrganizationRanking(int page) => Get<SolvedOrganizationRanking>("ranking/organization" , $"?page={page}");
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedOrganizationRanking>> GetOrganizationRankingAsync(int page) => await GetAsync<SolvedOrganizationRanking>("ranking/organization" , $"?page={page}");
    #endregion
    #region InOriganizationRanking
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <param name="organizationId">단체 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedOrganizationRanking> GetInOrganizationRanking(int page , int organizationId) => Get<SolvedOrganizationRanking>("ranking/in_organization" , $"?organizationId={organizationId}&page={page}");
    /// <summary>
    /// 레이팅에 따른 조직 순위를 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <param name="organizationId">단체 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedOrganizationRanking>> GetInOrganizationRankingAsync(int page , int organizationId) => await GetAsync<SolvedOrganizationRanking>("ranking/in_organization" , $"?organizationId={organizationId}&page={page}");
    #endregion
    #region ArenaInOriganizationRanking
    /// <summary>
    /// 해당 단체에 속한 사용자 중에서 아레나 레이팅이 높은 사용자가 먼저 오도록 정렬한 목록을 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <param name="organizationId">단체 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedOrganizationRanking> GetArenaInOrganizationRanking(int page , int organizationId) => Get<SolvedOrganizationRanking>("ranking/arena_in_organization" , $"?organizationId={organizationId}&page={page}");
    /// <summary>
    /// 해당 단체에 속한 사용자 중에서 아레나 레이팅이 높은 사용자가 먼저 오도록 정렬한 목록을 가져옵니다.
    /// </summary>
    /// <param name="page">페이지 (자연수)</param>
    /// <param name="organizationId">단체 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedOrganizationRanking>> GetArenaInOrganizationRankingAsync(int page , int organizationId) => await GetAsync<SolvedOrganizationRanking>("ranking/arena_in_organization" , $"?organizationId={organizationId}&page={page}");
    #endregion
    #region SearchUser
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedSearchResult<SolvedSocialUser>> GetSearchUser(in string query) => Get<SolvedSearchResult<SolvedSocialUser>>("search/user" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 사용자를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedSearchResult<SolvedSocialUser>>> GetSearchUserAsync(string query) => await GetAsync<SolvedSearchResult<SolvedSocialUser>>("search/user" , $"?query={query}");
    #endregion
    #region SearchTag
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedSearchResult<SolvedProblemTag>> GetSearchTag(in string query) => Get<SolvedSearchResult<SolvedProblemTag>>("search/tag" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제 태그를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedSearchResult<SolvedProblemTag>>> GetSearchTagAsync(string query) => await GetAsync<SolvedSearchResult<SolvedProblemTag>>("search/tag" , $"?query={query}");
    #endregion
    #region SearchProblem
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedSearchResult<SolvedTaggedProblem>> GetSearchProblem(in string query , bool descending_order = false) => Get<SolvedSearchResult<SolvedTaggedProblem>>("search/problem" , $"?direction={(descending_order ? "desc" : "asc")}&query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 문제를 검색합니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <param name="descending_order">내림차순으로 정렬할지에 대한 여부입니다. 기본은 오름차순입니다.</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedSearchResult<SolvedTaggedProblem>>> GetSearchProblemAsync(string query , bool descending_order = false) => await GetAsync<SolvedSearchResult<SolvedTaggedProblem>>("search/problem" , $"?direction={(descending_order ? "desc" : "asc")}&query={query}");
    #endregion
    #region UserContributionStats
    /// <summary>
    /// 해당 핸들의 사용자가 기여한 문제 수를 문제 수준별로 나누어 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedUserContributionStat>> GetUserContributionStats(in string handle) => Get<List<SolvedUserContributionStat>>("user/contribution_stats" , $"?handle={handle}");
    /// <summary>
    /// 해당 핸들의 사용자가 기여한 문제 수를 문제 수준별로 나누어 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedUserContributionStat>>> GetUserContributionStatsAsync(string handle) => await GetAsync<List<SolvedUserContributionStat>>("user/contribution_stats" , $"?handle={handle}");
    #endregion
    #region UserClassStats
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedUserClassStat>> GetUserClassStats(in string handle) => Get<List<SolvedUserClassStat>>("user/class_stats" , $"?handle={handle}");
    /// <summary>
    /// 유저의 클래스별 진행도를 가져옵니다. (추측)
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedUserClassStat>>> GetUserClassStatsAsync(string handle) => await GetAsync<List<SolvedUserClassStat>>("user/class_stats" , $"?handle={handle}");
    #endregion
    #region ProblemTagStats
    /// <summary>
    /// 해당 핸들의 사용자가 푼 문제 수를 태그별로 나누어 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedSearchResult<SolvedTagStat>> GetProblemTagStats(in string handle) => Get<SolvedSearchResult<SolvedTagStat>>("user/problem_tag_stats" , $"?handle={handle}");
    /// <summary>
    /// 해당 핸들의 사용자가 푼 문제 수를 태그별로 나누어 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedSearchResult<SolvedTagStat>>> GetProblemTagStatsAsync(string handle) => await GetAsync<SolvedSearchResult<SolvedTagStat>>("user/problem_tag_stats" , $"?handle={handle}");
    #endregion
    #region SiteStats
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public SolvedResult<Site.SolvedStats> GetSiteStats() => Get<Site.SolvedStats>("site/stats");
    /// <summary>
    /// solved.ac 통계를 가져옵니다.
    /// </summary>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<Site.SolvedStats>> GetSiteStatsAsync() => await GetAsync<Site.SolvedStats>("site/stats");
    #endregion
    #region OriganizationFromUser
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<List<SolvedOrganization>> GetOrganizationsFromUser(in string handle) => Get<List<SolvedOrganization>>("user/organizations" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<List<SolvedOrganization>>> GetOrganizationsFromUserAsync(string handle) => await GetAsync<List<SolvedOrganization>>("user/organizations" , $"?handle={handle}");
    #endregion
    #region SolvedFromUser
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedUserSolvedInfo> GetSolvedFromUser(in string handle) => Get<SolvedUserSolvedInfo>("user/problem_stats" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedUserSolvedInfo>> GetSolvedFromUserAsync(string handle) => await GetAsync<SolvedUserSolvedInfo>("user/problem_stats" , $"?handle={handle}");
    #endregion
    #region User
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedSocialUser> GetUser(in string handle) => Get<SolvedSocialUser>("user/show" , $"?handle={handle}");
    /// <summary>
    /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedSocialUser>> GetUserAsync(string handle) => await GetAsync<SolvedSocialUser>("user/show" , $"?handle={handle}");
    #endregion
    #region Top100FromUser
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedTop100> GetTop100FromUser(in string handle) => Get<SolvedTop100>("user/top_100" , $"?handle={handle}");
    /// <summary>
    /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
    /// </summary>
    /// <param name="handle">사용자 ID</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<SolvedTop100>> GetTop100FromUserAsync(string handle) => await GetAsync<SolvedTop100>("user/top_100" , $"?handle={handle}");
    #endregion
    #region PostFromTitle
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public SolvedResult<SolvedPost> GetPostFromTitle(in string postId) => Get<SolvedPost>("post/show" , $"?postId={postId}");
    /// <summary>
    /// 해당 제목의 게시글을 가져옵니다.
    /// </summary>
    /// <param name="postId">요청할 게시글의 제목</param>
    /// <returns>게시글을 가져옵니다. 실패시 null</returns>
    public async Task<SolvedResult<SolvedPost>> GetPostFromTitleAsync(string postId) => await GetAsync<SolvedPost>("post/show" , $"?postId={postId}");
    #endregion
    #region AdditionalInfo
    /// <summary>
    /// 해당 핸들을 가진 사용자의 부가 정보를 가져옵니다.
    /// </summary>
    /// <param name="handle">요청할 사용자명</param>
    /// <returns>부가 정보를 가져옵니다. 실패시 null</returns>
    public SolvedResult<SolvedAdditionalInformation> GetUserAdditionalInfo(in string handle) => Get<SolvedAdditionalInformation>("user/additional_info" , $"?handle={handle}");
    /// <summary>
    /// 해당 핸들을 가진 사용자의 부가 정보를 가져옵니다.
    /// </summary>
    /// <param name="handle">요청할 사용자명</param>
    /// <returns>부가 정보를 가져옵니다. 실패시 null</returns>
    public async Task<SolvedResult<SolvedAdditionalInformation>> GetUserAdditionalInfoAsync(string handle) => await GetAsync<SolvedAdditionalInformation>("user/additional_info" , $"?handle={handle}");
    #endregion
    #region SearchAutoComplete
    /// <summary>
    /// 주어진 쿼리에 따라 검색할 때 도움이 되도록 자동 완성 및 상위 검색 결과를 반환합니다. 자동 완성 결과는 언어에 의존적입니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>자동 완성 및 상위 검색 결과를 반환합니다.</returns>
    public SolvedResult<SolvedSuggestion> GetSearchAutoComplete(in string query) => Get<SolvedSuggestion>("search/suggestion" , $"?query={query}");
    /// <summary>
    /// 주어진 쿼리에 따라 검색할 때 도움이 되도록 자동 완성 및 상위 검색 결과를 반환합니다. 자동 완성 결과는 언어에 의존적입니다.
    /// </summary>
    /// <param name="query">쿼리 문자열</param>
    /// <returns>자동 완성 및 상위 검색 결과를 반환합니다.</returns>
    public async Task<SolvedResult<SolvedSuggestion>> GetSearchAutoCompleteAsync(string query) => await GetAsync<SolvedSuggestion>("search/suggestion" , $"?query={query}");
    #endregion
    #region Produce101Status
    public SolvedResult<SolvedProduce101Event> GetProduce101Status() => Get<SolvedProduce101Event>("event/250401/status");
    public async Task<SolvedResult<SolvedProduce101Event>> GetProduce101StatusAsync() => await GetAsync<SolvedProduce101Event>("event/250401/status");
    #endregion
    #region HanbyeolForce
    public SolvedResult<SolvedHanbyeolForceEvent> GetHanbyeolForceStatus() => Get<SolvedHanbyeolForceEvent>("event/220401/status");
    public async Task<SolvedResult<SolvedHanbyeolForceEvent>> GetHanbyeolForceAsync() => await GetAsync<SolvedHanbyeolForceEvent>("event/220401/status");
    #endregion
    #region PeperoDay2021
    public SolvedResult<SolvedPeperoDay2021Event> GetPeperoDay2021Status() => Get<SolvedPeperoDay2021Event>("event/211111/status");
    public async Task<SolvedResult<SolvedPeperoDay2021Event>> GetPeperoDay2021Async() => await GetAsync<SolvedPeperoDay2021Event>("event/211111/status");
    #endregion
    #region DecorateSummerPostcard
    public SolvedResult<SolvedDecorateSummerPostcardEvent> GetDecorateSummerPostcardStatus() => Get<SolvedDecorateSummerPostcardEvent>("event/220626/status");
    public async Task<SolvedResult<SolvedDecorateSummerPostcardEvent>> GetDecorateSummerPostcardAsync() => await GetAsync<SolvedDecorateSummerPostcardEvent>("event/220626/status");
    #endregion

    readonly HttpClient client;
    async Task<SolvedResult<T>> GetAsync<T>(string url, string? option = null)
    {
        try
        {
            string requestUrl = url + (option ?? string.Empty);
            using var response = await client.GetAsync(requestUrl);
            if ((int)response.StatusCode >= 400)
            {
                return new(default , new SolvedAPIException(client.BaseAddress + requestUrl, response.StatusCode));
            }
            return new(
                JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()),
                null
            );
        } catch (Exception ex)
        {
            return new(default , ex);
        }
    }
    SolvedResult<T> Get<T>(in string url, in string? option = null)
    {
        return GetAsync<T>(url , option).GetAwaiter().GetResult();
    }
}
