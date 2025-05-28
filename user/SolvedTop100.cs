using Newtonsoft.Json;
using AcNET.Type;

namespace AcNET.User;

/// <summary>
/// 상위 100문제
/// </summary>
public class SolvedTop100 : Jsonable
{
    /// <summary>
    /// 문제 수입니다.
    /// </summary>
    [JsonProperty("count")]
    public long Count { get; set; }
    /// <summary>
    /// 문제 목록입니다.
    /// </summary>
    [JsonProperty("items")]
    public List<Problem.SolvedTaggedProblem> Items { get; set; } = null!;
}
