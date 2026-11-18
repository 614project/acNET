using AcNET.Post;
using AcNET.Site;
using static AcNET.SolvedQuery;

namespace AcNET;

public partial class SolvedAPI
{
    #region Site
    /// <summary>
    /// solved.ac 서비스 상태를 확인합니다. (<c>GET /health</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedHealth>> GetHealthAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedHealth>("health", null, cancellationToken);

    /// <summary>
    /// 사이트 전체 통계를 가져옵니다. 최대 10분 전 값입니다. (<c>GET /site/stats</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedStats>> GetSiteStatsAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedStats>("site/stats", null, cancellationToken);

    /// <summary>
    /// solved.ac 가 읽는 저지들과 각 저지가 문제를 부르는 방식을 가져옵니다. (<c>GET /judges</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>기본 표시 순서대로의 저지 목록</returns>
    public Task<SolvedResult<List<SolvedJudge>>> GetJudgesAsync(CancellationToken cancellationToken = default)
        => GetAsync<JudgesEnvelope, List<SolvedJudge>>("judges", null, static e => e.Judges, cancellationToken);
    #endregion

    #region Posts
    /// <summary>
    /// 공지사항 목록을 가져옵니다. 고정 글이 먼저, 그 다음 최신순입니다. (<c>GET /posts/notice</c>)
    /// </summary>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="query">검색어</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedNoticePage>> GetNoticeListAsync(int page = 1, string? query = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedNoticePage>("posts/notice", Lang().Add("page", page).Add("q", query), cancellationToken);

    /// <summary>
    /// 공지사항을 가져옵니다. (<c>GET /posts/notice/{slug}</c>)
    /// </summary>
    /// <param name="slug">공지사항의 slug</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedPost>> GetNoticeAsync(string slug, CancellationToken cancellationToken = default)
        => GetAsync<SolvedPost>($"posts/notice/{Segment(slug)}", Lang(), cancellationToken);

    /// <summary>
    /// 도움말 센터의 문서들을 트리로 가져옵니다. (<c>GET /posts/help</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedHelpTreeNode>>> GetHelpTreeAsync(CancellationToken cancellationToken = default)
        => GetAsync<ItemsEnvelope<SolvedHelpTreeNode>, List<SolvedHelpTreeNode>>("posts/help", Lang(), static e => e.Items, cancellationToken);

    /// <summary>
    /// 도움말 센터의 문서를 가져옵니다. (<c>GET /posts/help/page</c>)
    /// </summary>
    /// <param name="path">문서 경로 (<see cref="SolvedHelpTreeNode.Path"/>)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedPost>> GetHelpPageAsync(string path, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(path);
        return GetAsync<SolvedPost>("posts/help/page", Lang().Add("path", path), cancellationToken);
    }

    /// <summary>
    /// 도움말 글을 가져옵니다. (<c>GET /help/{slug}</c>)
    /// </summary>
    /// <param name="slug">글의 slug</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedHelpArticle>> GetHelpArticleAsync(string slug, CancellationToken cancellationToken = default)
        => GetAsync<SolvedHelpArticle>($"help/{Segment(slug)}", Lang(), cancellationToken);

    /// <summary>
    /// 정책 문서를 가져옵니다. (<c>GET /policies/{slug}</c>, <c>GET /policies/{slug}/{ref}</c>)
    /// </summary>
    /// <param name="kind">정책</param>
    /// <param name="version">
    /// 가져올 버전. 버전 이름(예: "v3", "beta")이나 시행일(예: "20240714")을 넣습니다.
    /// null 이면 지금 시행 중인 버전(없으면 가장 가까운 예정 버전)을 가져옵니다.
    /// </param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedPolicy>> GetPolicyAsync(SolvedPolicyKind kind, string? version = null, CancellationToken cancellationToken = default)
    {
        string path = $"policies/{kind.ToApiValue()}";
        if (version is not null)
            path += "/" + Segment(version);
        return GetAsync<SolvedPolicy>(path, Lang(), cancellationToken);
    }
    #endregion

    sealed class JudgesEnvelope
    {
        public List<SolvedJudge> Judges { get; init; } = [];
    }
}
