using AcNET.Source;
using static AcNET.SolvedQuery;

namespace AcNET;

public partial class SolvedAPI
{
    #region Sources
    /// <summary>
    /// 최상위 출처 그룹 목록을 가져옵니다. (<c>GET /sources</c>)
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<List<SolvedSourceGroup>>> GetSourceGroupListAsync(CancellationToken cancellationToken = default)
        => GetAsync<GroupsEnvelope, List<SolvedSourceGroup>>("sources", null, static e => e.Groups, cancellationToken);

    /// <summary>
    /// 출처 그룹의 경로, 하위 그룹, 대회 목록을 가져옵니다. (<c>GET /sources/groups/{id}</c>)
    /// </summary>
    /// <param name="groupId">그룹 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedSourceGroupPage>> GetSourceGroupAsync(long groupId, CancellationToken cancellationToken = default)
        => GetAsync<SolvedSourceGroupPage>($"sources/groups/{Segment(groupId)}", null, cancellationToken);

    /// <summary>
    /// 대회와 그 문제들을 대회 순서대로 가져옵니다. (<c>GET /sources/contests/{id}</c>)
    /// </summary>
    /// <param name="contestId">대회 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public Task<SolvedResult<SolvedSourceContestPage>> GetSourceContestAsync(long contestId, CancellationToken cancellationToken = default)
        => GetAsync<SolvedSourceContestPage>($"sources/contests/{Segment(contestId)}", null, cancellationToken);

    /// <summary>
    /// 저지의 그룹 ID 에 해당하는 solved.ac 출처 그룹 ID 를 찾습니다. (<c>GET /sources/oj/{ojId}/groups/{nativeId}</c>)
    /// </summary>
    /// <param name="ojId">저지 ID (예: "boj")</param>
    /// <param name="nativeId">그 저지의 그룹 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>solved.ac 출처 그룹 ID. 없으면 404 에러</returns>
    public Task<SolvedResult<long>> FindSourceGroupIdAsync(string ojId, string nativeId, CancellationToken cancellationToken = default)
        => GetAsync<IdEnvelope, long>($"sources/oj/{Segment(ojId)}/groups/{Segment(nativeId)}", null, static e => e.Id, cancellationToken);

    /// <summary>
    /// 저지의 대회 ID 에 해당하는 solved.ac 대회 ID 를 찾습니다. (<c>GET /sources/oj/{ojId}/contests/{nativeId}</c>)
    /// </summary>
    /// <param name="ojId">저지 ID (예: "boj")</param>
    /// <param name="nativeId">그 저지의 대회 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>solved.ac 대회 ID. 없으면 404 에러</returns>
    public Task<SolvedResult<long>> FindSourceContestIdAsync(string ojId, string nativeId, CancellationToken cancellationToken = default)
        => GetAsync<IdEnvelope, long>($"sources/oj/{Segment(ojId)}/contests/{Segment(nativeId)}", null, static e => e.Id, cancellationToken);
    #endregion

    internal sealed class GroupsEnvelope
    {
        public List<SolvedSourceGroup> Groups { get; set; } = [];
    }

    internal sealed class IdEnvelope
    {
        public long Id { get; set; }
    }
}
