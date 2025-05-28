using AcNET.Type;
using Newtonsoft.Json;
namespace AcNET.Site;

/// <summary>
/// solved.ac 통계
/// </summary>
public class SolvedStats : Jsonable
{
    /// <summary>
    /// 여태까지 색인된 백준 문제 수입니다.
    /// </summary>
    [JsonProperty("problemCount")]
    public long ProblemCount { get; set; }
    /// <summary>
    /// 여태까지 난이도가 기여된 백준 문제 수입니다.
    /// </summary>
    [JsonProperty("problemVotedCount")]
    public long ProblemVotedCount { get; set; }
    /// <summary>
    /// 여태까지 등록한 사용자 수입니다.
    /// </summary>
    [JsonProperty("userCount")]
    public long UserCount { get; set; }
    /// <summary>
    /// 여태까지 난이도에 기여한 사용자 수입니다.
    /// </summary>
    [JsonProperty("contributorCount")]
    public long ContributorCount { get; set; }
    /// <summary>
    /// 여태까지 이루어진 난이도 기여의 수입니다.
    /// </summary>
    [JsonProperty("contributionCount")]
    public long ContributionCount { get; set; }
}
