using AcNET.Problem;
using AcNET.Search;
using static AcNET.SolvedQuery;

namespace AcNET;

public partial class SolvedAPI
{
    #region Problems
    /// <summary>
    /// 문제 정보를 가져옵니다. 제목, 태그, 저지 링크, 출처 대회, 요청한 사용자의 풀이 상태가 포함됩니다. (<c>GET /problems/{problemId}</c>)
    /// </summary>
    /// <remarks>사용자 토큰에 <c>solves</c> scope 가 있으면 <see cref="SolvedProblemRef.UserStatus"/> 가 채워집니다.</remarks>
    /// <param name="problemId">문제 번호</param>
    /// <param name="subId">하위 문제 ID. 일반 문제는 비워두세요.</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>문제 정보. 없는 문제면 404 에러</returns>
    public Task<SolvedResult<SolvedProblemDetail>> GetProblemAsync(int problemId, string? subId = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedProblemDetail>($"problems/{Segment(problemId)}", new SolvedQuery().Add("sub", NullIfEmpty(subId)), cancellationToken);

    /// <summary>
    /// 쿼리로 문제를 검색합니다. 페이지 크기는 50으로 고정입니다. (<c>GET /problems</c>)
    /// </summary>
    /// <remarks>
    /// 요청 비용이 3 입니다. 사용자 토큰 없이 검색하면 쿼리가 100자, 8단어로 제한되며,
    /// 넘으면 <see cref="SolvedErrorCode.SignInRequired"/> 에러가 납니다.
    /// </remarks>
    /// <param name="query">검색 쿼리 (예: "tier:g5..g1 #dp"). null 이면 모든 문제</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="sort">정렬 기준</param>
    /// <param name="direction">정렬 방향</param>
    /// <param name="seed">무작위 정렬의 seed. 같은 seed 면 같은 순서가 나옵니다.</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedProblemSearchResult>> GetSearchProblemAsync(
        string? query,
        int page = 1,
        SolvedProblemSort sort = SolvedProblemSort.Id,
        SolvedSortDirection direction = SolvedSortDirection.Ascending,
        string? seed = null,
        CancellationToken cancellationToken = default)
        => GetAsync<SolvedProblemSearchResult>(
            "problems",
            new SolvedQuery().Add("page", page).Add("q", query).Add("sort", sort).Add("direction", direction).Add("seed", seed),
            cancellationToken);

    /// <summary>
    /// 쿼리에 맞는 문제 하나를 무작위로 가져옵니다. (<c>GET /search/random-problem</c>)
    /// </summary>
    /// <param name="query">검색 쿼리</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>문제. 맞는 문제가 없으면 404 에러</returns>
    public Task<SolvedResult<SolvedProblem>> GetRandomProblemAsync(string? query, CancellationToken cancellationToken = default)
        => GetAsync<SolvedProblem>("search/random-problem", new SolvedQuery().Add("q", query), cancellationToken);

    /// <summary>
    /// 검색은 하지 않고 검색 쿼리의 문제점만 진단합니다. (<c>GET /search/analyze</c>)
    /// </summary>
    /// <param name="query">검색 쿼리</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>진단 목록. 문제가 없으면 빈 목록</returns>
    public Task<SolvedResult<List<SolvedSearchDiagnostic>>> GetSearchAnalysisAsync(string query, CancellationToken cancellationToken = default)
        => GetAsync<DiagnosticsEnvelope, List<SolvedSearchDiagnostic>>("search/analyze", new SolvedQuery().Add("q", query), static e => e.Diagnostics, cancellationToken);

    /// <summary>
    /// 검색 필터에 넣을 수 있는 값을 추천받습니다. (<c>GET /search/suggest</c>)
    /// </summary>
    /// <param name="topic">필터 이름 (예: "tag", "solved_by")</param>
    /// <param name="value">지금까지 입력한 값</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>최대 8개의 추천 값</returns>
    public Task<SolvedResult<List<SolvedSuggestion>>> GetSearchSuggestionAsync(string topic, string? value = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(topic);
        return GetAsync<SuggestionsEnvelope, List<SolvedSuggestion>>(
            "search/suggest",
            new SolvedQuery().Add("topic", topic).Add("value", value),
            static e => e.Suggestions,
            cancellationToken);
    }

    /// <summary>
    /// 문제의 난이도 기여 요약(그래프 점, 태그 분포)을 가져옵니다. (<c>GET /problems/{problemId}/votes/summary</c>)
    /// </summary>
    /// <remarks>로그인한 사용자가 푼 문제만 볼 수 있습니다. 그렇지 않으면 401 또는 403 에러가 납니다.</remarks>
    /// <param name="problemId">문제 번호</param>
    /// <param name="subId">하위 문제 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedVoteSummary>> GetProblemVoteSummaryAsync(int problemId, string? subId = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedVoteSummary>($"problems/{Segment(problemId)}/votes/summary", new SolvedQuery().Add("sub", NullIfEmpty(subId)), cancellationToken);

    /// <summary>
    /// 문제의 난이도 기여 목록을 가져옵니다. 페이지 크기는 30으로 고정입니다. (<c>GET /problems/{problemId}/votes</c>)
    /// </summary>
    /// <remarks>요청 비용이 2 입니다. 로그인한 사용자가 푼 문제만 볼 수 있습니다.</remarks>
    /// <param name="problemId">문제 번호</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="sort">정렬 기준</param>
    /// <param name="subId">하위 문제 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedVotePage>> GetProblemVotesAsync(int problemId, int page = 1, SolvedVoteSort sort = SolvedVoteSort.Recent, string? subId = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedVotePage>(
            $"problems/{Segment(problemId)}/votes",
            new SolvedQuery().Add("sub", NullIfEmpty(subId)).Add("page", page).Add("sort", sort),
            cancellationToken);

    /// <summary>
    /// 이 문제에 대한 요청한 사용자 본인의 기여를 가져옵니다. (<c>GET /problems/{problemId}/votes/me</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>votes</c>)</remarks>
    /// <param name="problemId">문제 번호</param>
    /// <param name="subId">하위 문제 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>기여. 기여하지 않았다면 Result 가 null 입니다. (성공)</returns>
    public Task<SolvedResult<SolvedVote?>> GetMyProblemVoteAsync(int problemId, string? subId = null, CancellationToken cancellationToken = default)
        => GetAsync<VoteEnvelope, SolvedVote?>(
            $"problems/{Segment(problemId)}/votes/me",
            new SolvedQuery().Add("sub", NullIfEmpty(subId)),
            static e => e.Vote,
            cancellationToken);

    /// <summary>
    /// 문제의 신고 목록을 최신순으로 가져옵니다. 페이지 크기는 10입니다. (<c>GET /problems/{problemId}/reports</c>)
    /// </summary>
    /// <param name="problemId">문제 번호</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="subId">하위 문제 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedProblemReportPage>> GetProblemReportsAsync(int problemId, int page = 1, string? subId = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedProblemReportPage>(
            $"problems/{Segment(problemId)}/reports",
            new SolvedQuery().Add("sub", NullIfEmpty(subId)).Add("page", page),
            cancellationToken);
    #endregion

    #region Tags
    /// <summary>
    /// 숨겨지지 않은 모든 태그를 가져옵니다. 자주 바뀌지 않으니 받아서 캐시해두는 것을 권장합니다. (<c>GET /tags</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedTag>>> GetTagListAsync(CancellationToken cancellationToken = default)
        => GetAsync<TagsEnvelope<SolvedTag>, List<SolvedTag>>("tags", null, static e => e.Tags, cancellationToken);

    /// <summary>
    /// 태그 하나의 정보를 가져옵니다. (<c>GET /tags/{key}</c>)
    /// </summary>
    /// <param name="key">태그의 key (예: "dp")</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>태그 정보. 없거나 메타/숨김 태그면 404 에러</returns>
    public Task<SolvedResult<SolvedTagDetail>> GetTagAsync(string key, CancellationToken cancellationToken = default)
        => GetAsync<SolvedTagDetail>($"tags/{Segment(key)}", null, cancellationToken);

    /// <summary>
    /// 요청한 사용자의 태그별 푼 문제 수와 시도한 문제 수를 가져옵니다. (<c>GET /me/tag-progress</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>solves</c>)</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedTagProgress>> GetMyTagProgressAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedTagProgress>("me/tag-progress", null, cancellationToken);
    #endregion

    static string? NullIfEmpty(string? value) => string.IsNullOrEmpty(value) ? null : value;

    sealed class DiagnosticsEnvelope
    {
        public List<SolvedSearchDiagnostic> Diagnostics { get; init; } = [];
    }

    sealed class SuggestionsEnvelope
    {
        public List<SolvedSuggestion> Suggestions { get; init; } = [];
    }

    sealed class VoteEnvelope
    {
        public SolvedVote? Vote { get; init; }
    }

    sealed class TagsEnvelope<T>
    {
        public List<T> Tags { get; init; } = [];
    }
}
