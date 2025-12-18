using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Problem;

/// <summary>
/// 클래스 별 정보
/// </summary>
public class SolvedClassInfo : Jsonable
{
    /// <summary>
    /// 클래스 값입니다. (1 이상, 10 이하의 값이 들어있습니다)
    /// </summary>
    [JsonProperty("class")]
    public long Class{ get; set; }
    /// <summary>
    /// 이 CLASS에 속한 에센셜이 아닌 문제 수입니다.
    /// </summary>
    [JsonProperty("total")]
    public long Total{ get; set; }
    /// <summary>
    /// 이 CLASS에 속한 에센셜 문제 수입니다.
    /// </summary>
    [JsonProperty("essentials")]
    public int Essentials{ get; set; }
    /// <summary>
    /// 이 CLASS를 취득하기 위한 최소 문제 수입니다.
    /// </summary>
    [JsonProperty("criteria")]
    public int Criteria{ get; set; }
}
