using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.User;

/// <summary>
/// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
/// </summary>
public class SolvedUserSolvedInfo : Jsonable
{
    /// <summary>
    /// 문제의 레벨값.
    /// </summary>
    [JsonProperty("level")]
    public long Level { get; set; }
    /// <summary>
    /// solved.ac에 등록된 해당 레벨의 문제 수입니다.
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