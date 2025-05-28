using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Problem;

/// <summary>
/// 문제 태그
/// </summary>
public class SolvedProblemTag : Jsonable
{
    /// <summary>
    /// 태그의 ID입니다.
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// (알수없음.)
    /// </summary>
    [JsonProperty("isMeta")]
    public bool IsMeta { get; set; }
    /// <summary>
    /// 백준에서 사용되는 이 태그의 ID입니다.
    /// </summary>
    [JsonProperty("bojTagId")]
    public long BojTagId { get; set; }
    /// <summary>
    /// 이 태그를 포함하는 문제의 수입니다.
    /// </summary>
    [JsonProperty("problemCount")]
    public long ProblemCount { get; set; }
    /// <summary>
    /// 언어별 태그의 이름 목록입니다.
    /// </summary>
    [JsonProperty("displayNames")]
    public List<SolvedDisplayName> DisplayNames { get; set; } = null!;
    /// <summary>
    /// 태그의 별칭입니다.
    /// </summary>
    [JsonProperty("aliases")]
    public List<SolvedAlias> Aliases { get; set; } = null!;
}

/// <summary>
/// 문제 태그의 진행도 입니다.
/// </summary>
public class SolvedTagStat : Jsonable
{
    /// <summary>
    /// 태그에 대한 정보입니다.
    /// </summary>
    [JsonProperty("tag")]
    public SolvedProblemTag Tag { get; set; } = null!;
    /// <summary>
    /// solved.ac에 등록된 해당 태그의 문제 수입니다.
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    [JsonProperty("solved")]
    public long Solved { get; set; }
    /// <summary>
    /// 사용자가 부분 성공한 문제 수입니다.
    /// </summary>
    [JsonProperty("partial")]
    public long Partial { get; set; }
    /// <summary>
    /// 사용자가 시도해본 문제 수입니다.
    /// </summary>
    [JsonProperty("tried")]
    public long Tried { get; set; }
}