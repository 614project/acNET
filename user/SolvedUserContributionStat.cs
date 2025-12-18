using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.User;

/// <summary>
/// 문제 수준별 기여한 문제 수가 담긴 목록
/// </summary>
public class SolvedUserContributionStat : Jsonable
{
    /// <summary>
    /// solved.ac에 등록된 해당 수준 문제 수입니다.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    [JsonProperty("solved")]
    public int Solved { get; set; }
    /// <summary>
    /// 사용자가 푼 표준 문제 수입니다.
    /// </summary>
    [JsonProperty("solvedStandards")]
    public int SolvedStandards { get; set; }
    /// <summary>
    /// 사용자가 기여한 문제 수입니다.
    /// </summary>
    [JsonProperty("contributed")]
    public int Contributed { get; set; }
}