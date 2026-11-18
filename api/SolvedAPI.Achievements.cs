using AcNET.Achievement;
using static AcNET.SolvedQuery;

namespace AcNET;

public partial class SolvedAPI
{
    #region Achievements
    /// <summary>
    /// 사용자의 업적 달성 수와 최근 획득한 아이템을 가져옵니다. (<c>GET /users/{handle}/achievements/summary</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAchievementSummary>> GetUserAchievementSummaryAsync(string handle, CancellationToken cancellationToken = default)
        => GetAsync<SolvedAchievementSummary>($"users/{Segment(handle)}/achievements/summary", Lang(), cancellationToken);

    /// <summary>
    /// 사용자 프로필의 뱃지 진열대를 가져옵니다. (<c>GET /users/{handle}/badge-stand</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedBadgeStand>> GetUserBadgeStandAsync(string handle, CancellationToken cancellationToken = default)
        => GetAsync<SolvedBadgeStand>($"users/{Segment(handle)}/badge-stand", Lang(), cancellationToken);

    /// <summary>
    /// 사용자가 가진 뱃지 또는 배경 목록을 가져옵니다. (<c>GET /users/{handle}/collection</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="kind">아이템 종류</param>
    /// <param name="sort">정렬 기준</param>
    /// <param name="category">분류 필터</param>
    /// <param name="query">이름 검색어</param>
    /// <param name="offset">시작 위치. 다음 페이지는 <see cref="SolvedCollection.NextOffset"/> 을 넣으세요.</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedCollection>> GetUserCollectionAsync(
        string handle,
        SolvedItemKind kind = SolvedItemKind.Badge,
        SolvedCollectionSort sort = SolvedCollectionSort.Recent,
        string? category = null,
        string? query = null,
        int offset = 0,
        CancellationToken cancellationToken = default)
        => GetAsync<SolvedCollection>($"users/{Segment(handle)}/collection", CollectionQuery(kind, sort, category, query, offset), cancellationToken);

    /// <summary>
    /// 모든 업적과 사용자의 달성 현황을 가져옵니다. 다른 사용자와 비교할 수도 있습니다. (<c>GET /users/{handle}/achievements</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="vsHandle">비교할 사용자명</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAchievementList>> GetUserAchievementsAsync(string handle, string? vsHandle = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedAchievementList>($"users/{Segment(handle)}/achievements", Lang().Add("vs", vsHandle), cancellationToken);

    /// <summary>
    /// 모든 업적과 요청한 사용자의 달성 현황을 가져옵니다. (<c>GET /achievements</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAchievementList>> GetAchievementListAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedAchievementList>("achievements", Lang(), cancellationToken);

    /// <summary>
    /// 문제를 풀면 달성하거나 진행되는 업적을 가져옵니다. (<c>GET /achievements/for-problem/{problemId}</c>)
    /// </summary>
    /// <param name="problemId">문제 번호</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedProblemAchievement>>> GetProblemAchievementsAsync(int problemId, CancellationToken cancellationToken = default)
        => GetAsync<ItemsEnvelope<SolvedProblemAchievement>, List<SolvedProblemAchievement>>(
            $"achievements/for-problem/{Segment(problemId)}",
            Lang(),
            static e => e.Items,
            cancellationToken);

    /// <summary>
    /// 뱃지 또는 배경과 그것을 주는 업적을 가져옵니다. (<c>GET /achievements/items/{kind}/{key}</c>)
    /// </summary>
    /// <param name="kind">아이템 종류</param>
    /// <param name="key">아이템의 key</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAchievementItem>> GetAchievementItemAsync(SolvedItemKind kind, string key, CancellationToken cancellationToken = default)
        => GetAsync<SolvedAchievementItem>($"achievements/items/{kind.ToApiValue()}/{Segment(key)}", Lang(), cancellationToken);

    /// <summary>
    /// 뱃지 정보를 가져옵니다. <see cref="GetAchievementItemAsync"/> 의 kind 가 Badge 인 경우와 같습니다.
    /// </summary>
    /// <param name="badgeKey">뱃지의 key</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAchievementItem>> GetBadgeAsync(string badgeKey, CancellationToken cancellationToken = default)
        => GetAchievementItemAsync(SolvedItemKind.Badge, badgeKey, cancellationToken);

    /// <summary>
    /// 배경 정보를 가져옵니다. <see cref="GetAchievementItemAsync"/> 의 kind 가 Background 인 경우와 같습니다.
    /// </summary>
    /// <param name="backgroundKey">배경의 key</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedAchievementItem>> GetBackgroundAsync(string backgroundKey, CancellationToken cancellationToken = default)
        => GetAchievementItemAsync(SolvedItemKind.Background, backgroundKey, cancellationToken);

    /// <summary>
    /// 뱃지 또는 배경을 가진 사용자 목록을 가져옵니다. (<c>GET /achievements/items/{kind}/{key}/holders</c>)
    /// </summary>
    /// <param name="kind">아이템 종류</param>
    /// <param name="key">아이템의 key</param>
    /// <param name="page">페이지 (1~200)</param>
    /// <param name="sort">정렬 기준</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedItemHolders>> GetAchievementItemHoldersAsync(SolvedItemKind kind, string key, int page = 1, SolvedHolderSort sort = SolvedHolderSort.Recent, CancellationToken cancellationToken = default)
        => GetAsync<SolvedItemHolders>(
            $"achievements/items/{kind.ToApiValue()}/{Segment(key)}/holders",
            new SolvedQuery().Add("sort", sort).Add("page", page),
            cancellationToken);
    #endregion

    SolvedQuery CollectionQuery(SolvedItemKind kind, SolvedCollectionSort sort, string? category, string? query, int offset)
        => Lang().Add("kind", kind).Add("sort", sort).Add("category", category).Add("q", query).Add("offset", offset);

    sealed class ItemsEnvelope<T>
    {
        public List<T> Items { get; init; } = [];
    }
}
