using AcNET.Ranking;
using AcNET.User;
using static AcNET.SolvedQuery;

namespace AcNET;

public partial class SolvedAPI
{
    #region Rankings
    /// <summary>
    /// 전체 사용자 랭킹을 가져옵니다. (<c>GET /rankings/users</c>)
    /// </summary>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="sort">정렬 기준 (레이팅, 푼 문제 수, 기여)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedUserRanking>> GetUserRankingAsync(int page = 1, SolvedUserRankingSort sort = SolvedUserRankingSort.Rating, CancellationToken cancellationToken = default)
        => GetAsync<SolvedUserRanking>("rankings/users", new SolvedQuery().Add("page", page).Add("sort", sort), cancellationToken);

    /// <summary>
    /// 레이팅 순 사용자 랭킹을 가져옵니다. <see cref="GetUserRankingAsync"/> 의 sort 가 Rating 인 경우와 같습니다.
    /// </summary>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedUserRanking>> GetTierRankingAsync(int page = 1, CancellationToken cancellationToken = default)
        => GetUserRankingAsync(page, SolvedUserRankingSort.Rating, cancellationToken);

    /// <summary>
    /// 기여 순 사용자 랭킹을 가져옵니다. <see cref="GetUserRankingAsync"/> 의 sort 가 Contribution 인 경우와 같습니다.
    /// </summary>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedUserRanking>> GetContributionRankingAsync(int page = 1, CancellationToken cancellationToken = default)
        => GetUserRankingAsync(page, SolvedUserRankingSort.Contribution, cancellationToken);

    /// <summary>
    /// 사용자명 접두사로 사용자를 찾고 전체 랭킹에서의 순위를 함께 가져옵니다. (<c>GET /rankings/users/search</c>)
    /// </summary>
    /// <param name="query">사용자명 접두사 (2자 미만이면 결과 없음)</param>
    /// <param name="sort">순위 기준</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>최대 10명</returns>
    public Task<SolvedResult<SolvedUserSearch>> GetSearchUserAsync(string query, SolvedUserRankingSort sort = SolvedUserRankingSort.Rating, CancellationToken cancellationToken = default)
        => GetAsync<SolvedUserSearch>("rankings/users/search", new SolvedQuery().Add("q", query).Add("sort", sort), cancellationToken);

    /// <summary>
    /// 요청한 사용자와 그 라이벌들의 레이팅 랭킹을 가져옵니다. (<c>GET /rankings/rivals</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>rivals</c>)</remarks>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedRivalRanking>> GetRivalRankingAsync(int page = 1, CancellationToken cancellationToken = default)
        => GetAsync<SolvedRivalRanking>("rankings/rivals", new SolvedQuery().Add("page", page), cancellationToken);

    /// <summary>
    /// 요청한 사용자와, 요청한 사용자를 라이벌로 등록한 사용자들의 레이팅 랭킹을 가져옵니다. (<c>GET /rankings/reverse-rivals</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>rivals</c>)</remarks>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedRivalRanking>> GetReverseRivalRankingAsync(int page = 1, CancellationToken cancellationToken = default)
        => GetAsync<SolvedRivalRanking>("rankings/reverse-rivals", new SolvedQuery().Add("page", page), cancellationToken);

    /// <summary>
    /// 랭킹이 있는 지역(국가) 목록을 사용자가 많은 순서로 가져옵니다. (<c>GET /rankings/regions</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedRegion>>> GetRegionListAsync(CancellationToken cancellationToken = default)
        => GetAsync<RegionsEnvelope, List<SolvedRegion>>("rankings/regions", null, static e => e.Regions, cancellationToken);

    /// <summary>
    /// 한 지역(국가) 안의 사용자 랭킹을 가져옵니다. (<c>GET /rankings/regions/{countryCode}</c>)
    /// </summary>
    /// <param name="countryCode">국가 코드 (ISO 3166-1 alpha-2, 예: "kr")</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedUserRanking>> GetRegionRankingAsync(string countryCode, int page = 1, CancellationToken cancellationToken = default)
        => GetAsync<SolvedUserRanking>($"rankings/regions/{Segment(countryCode)}", new SolvedQuery().Add("page", page), cancellationToken);

    /// <summary>
    /// 한 태그 안의 사용자 랭킹을 가져옵니다. (<c>GET /rankings/tags/{key}</c>)
    /// </summary>
    /// <param name="tagKey">태그의 key (<see cref="GetRankedTagListAsync"/>)</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedUserRanking>> GetTagRankingAsync(string tagKey, int page = 1, CancellationToken cancellationToken = default)
        => GetAsync<SolvedUserRanking>($"rankings/tags/{Segment(tagKey)}", new SolvedQuery().Add("page", page), cancellationToken);

    /// <summary>
    /// 랭킹이 있는 태그 목록을 가져옵니다. (<c>GET /rankings/tags</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedRankedTag>>> GetRankedTagListAsync(CancellationToken cancellationToken = default)
        => GetAsync<TagsEnvelope<SolvedRankedTag>, List<SolvedRankedTag>>("rankings/tags", Lang(), static e => e.Tags, cancellationToken);
    #endregion

    #region Affiliations
    /// <summary>
    /// 소속(학교, 회사, 커뮤니티)의 상세 정보를 가져옵니다. (<c>GET /affiliations/{id}</c>)
    /// </summary>
    /// <param name="affiliationId">소속 ID (<see cref="SolvedAffiliationRef.AffiliationId"/>)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAffiliation>> GetAffiliationAsync(long affiliationId, CancellationToken cancellationToken = default)
        => GetAsync<SolvedAffiliation>($"affiliations/{Segment(affiliationId)}", Lang(), cancellationToken);

    /// <summary>
    /// 한 소속 안의 사용자 랭킹을 가져옵니다. (<c>GET /rankings/affiliations/{affiliationId}</c>)
    /// </summary>
    /// <param name="affiliationId">소속 ID</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedUserRanking>> GetInAffiliationRankingAsync(long affiliationId, int page = 1, CancellationToken cancellationToken = default)
        => GetAsync<SolvedUserRanking>($"rankings/affiliations/{Segment(affiliationId)}", new SolvedQuery().Add("page", page), cancellationToken);

    /// <summary>
    /// 사용자가 있는 소속 목록을 사용자가 많은 순서로 가져옵니다. (<c>GET /rankings/affiliations</c>)
    /// </summary>
    /// <param name="query">소속 이름 검색어</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedRankableAffiliation>>> GetAffiliationListAsync(string? query = null, CancellationToken cancellationToken = default)
        => GetAsync<AffiliationsEnvelope, List<SolvedRankableAffiliation>>(
            "rankings/affiliations",
            Lang().Add("q", query),
            static e => e.Affiliations,
            cancellationToken);

    /// <summary>
    /// 소속끼리의 레이팅 랭킹을 가져옵니다. (<c>GET /rankings/affiliation-board</c>)
    /// </summary>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="type">소속 종류 필터. null 이면 전체</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAffiliationRanking>> GetAffiliationRankingAsync(int page = 1, SolvedAffiliationType? type = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedAffiliationRanking>("rankings/affiliation-board", Lang().Add("page", page).Add("type", type), cancellationToken);
    #endregion

    internal sealed class RegionsEnvelope
    {
        public List<SolvedRegion> Regions { get; set; } = [];
    }

    internal sealed class AffiliationsEnvelope
    {
        public List<SolvedRankableAffiliation> Affiliations { get; set; } = [];
    }
}
