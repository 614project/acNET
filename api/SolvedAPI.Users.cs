using System.Globalization;
using AcNET.User;
using static AcNET.SolvedQuery;

namespace AcNET;

public partial class SolvedAPI
{
    #region Users
    /// <summary>
    /// 사용자 토큰의 주인을 가져옵니다. (<c>GET /me</c>)
    /// </summary>
    /// <remarks>사용자 토큰 필요 (scope: <c>profile</c>). 이전 버전의 GetAccountVerifyCredentialsAsync 를 대신합니다.</remarks>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>사용자 ID 와 사용자명</returns>
    public Task<SolvedResult<SolvedMe>> GetMeAsync(CancellationToken cancellationToken = default)
        => GetAsync<SolvedMe>("me", null, cancellationToken);

    /// <summary>
    /// 해당 핸들을 가진 사용자의 공개 프로필을 가져옵니다. (<c>GET /users/{handle}</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>사용자 정보. 없는 사용자면 404 에러</returns>
    public Task<SolvedResult<SolvedUser>> GetUserAsync(string handle, CancellationToken cancellationToken = default)
        => GetAsync<SolvedUser>($"users/{Segment(handle)}", Lang(), cancellationToken);

    /// <summary>
    /// 한 태그 안에서 사용자가 푼 문제의 레벨별 개수를 가져옵니다. (<c>GET /users/{handle}/tags/{key}/histogram</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="tagKey">태그의 key (랭킹이 있는 태그만)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedTagHistogram>> GetUserTagHistogramAsync(string handle, string tagKey, CancellationToken cancellationToken = default)
        => GetAsync<SolvedTagHistogram>($"users/{Segment(handle)}/tags/{Segment(tagKey)}/histogram", null, cancellationToken);

    /// <summary>
    /// 스트릭 그래프용 날짜별 활동을 가져옵니다. (<c>GET /users/{handle}/activity/days</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="from">시작일</param>
    /// <param name="to">종료일 (기간이 너무 길면 400 에러)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedActivityDays>> GetUserActivityDaysAsync(string handle, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        => GetAsync<SolvedActivityDays>(
            $"users/{Segment(handle)}/activity/days",
            new SolvedQuery().Add("from", Day(from)).Add("to", Day(to)),
            cancellationToken);

    /// <summary>
    /// 사용자의 활동 피드를 최신순으로 가져옵니다. (<c>GET /users/{handle}/activity/feed</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="before">이전 응답의 <see cref="SolvedActivityFeed.NextCursor"/>. null 이면 처음부터</param>
    /// <param name="from">시작일</param>
    /// <param name="to">종료일</param>
    /// <param name="limit">가져올 개수 (1~500)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedActivityFeed>> GetUserActivityFeedAsync(string handle, string? before = null, DateTime? from = null, DateTime? to = null, int? limit = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedActivityFeed>(
            $"users/{Segment(handle)}/activity/feed",
            new SolvedQuery().Add("before", before).Add("from", Day(from)).Add("to", Day(to)).Add("limit", limit),
            cancellationToken);

    /// <summary>
    /// 사용자의 난이도 기여와 그로 받은 평판을 가져옵니다. (<c>GET /users/{handle}/contributions</c>)
    /// </summary>
    /// <remarks>사용자 토큰에 <c>votes</c> scope 가 있으면 요청한 사용자 기준 정보(평가 여부 등)가 채워집니다.</remarks>
    /// <param name="handle">사용자명</param>
    /// <param name="page">페이지 (1부터)</param>
    /// <param name="limit">페이지 크기 (1~20)</param>
    /// <param name="sort">정렬 기준</param>
    /// <param name="only">필터</param>
    /// <param name="commented">true 면 코멘트가 있는 기여만, false 면 없는 기여만</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedContributionPage>> GetUserContributionsAsync(
        string handle,
        int? page = null,
        int? limit = null,
        SolvedContributionSort? sort = null,
        SolvedContributionFilter? only = null,
        bool? commented = null,
        CancellationToken cancellationToken = default)
        => GetAsync<SolvedContributionPage>(
            $"users/{Segment(handle)}/contributions",
            new SolvedQuery()
                .Add("page", page)
                .Add("limit", limit)
                .Add("sort", sort)
                .Add("only", only)
                .Add("commented", commented switch { true => "true", false => "false", null => null }),
            cancellationToken);

    /// <summary>
    /// 사용자의 평판과 그 기록을 가져옵니다. (<c>GET /users/{handle}/reputation</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="range">기록 기간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedReputation>> GetUserReputationAsync(string handle, SolvedHistoryRange? range = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedReputation>($"users/{Segment(handle)}/reputation", new SolvedQuery().Add("range", range), cancellationToken);

    /// <summary>
    /// 한 달 동안 사용자의 평판을 바꾼 내역을 날짜별로 가져옵니다. (<c>GET /users/{handle}/reputation/log</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="month">조회할 달 (연도와 월만 사용). null 이면 이번 달</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedReputationLog>> GetUserReputationLogAsync(string handle, DateTime? month = null, CancellationToken cancellationToken = default)
        => GetAsync<SolvedReputationLog>($"users/{Segment(handle)}/reputation/log", new SolvedQuery().Add("month", Month(month)), cancellationToken);

    /// <summary>
    /// 사용자의 레이팅이 바뀐 날마다 그 날이 끝났을 때의 레이팅을 가져옵니다. (<c>GET /users/{handle}/rating/history</c>)
    /// </summary>
    /// <param name="handle">사용자명</param>
    /// <param name="range">기록 기간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>오래된 순서의 기록</returns>
    public Task<SolvedResult<List<SolvedDailyValue>>> GetUserRatingHistoryAsync(string handle, SolvedHistoryRange? range = null, CancellationToken cancellationToken = default)
        => GetAsync<HistoryEnvelope, List<SolvedDailyValue>>(
            $"users/{Segment(handle)}/rating/history",
            new SolvedQuery().Add("range", range),
            static e => e.History,
            cancellationToken);
    #endregion

    /// <summary>
    /// <see cref="Language"/> 가 있으면 <c>lang</c> 쿼리를 붙입니다.
    /// </summary>
    SolvedQuery Lang() => new SolvedQuery().Add("lang", Language);

    static string? Day(DateTime? day) => day?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    static string? Month(DateTime? month) => month?.ToString("yyyy-MM", CultureInfo.InvariantCulture);

    sealed class HistoryEnvelope
    {
        public List<SolvedDailyValue> History { get; init; } = [];
    }
}
