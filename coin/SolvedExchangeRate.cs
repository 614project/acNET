using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Coin;

/// <summary>
/// 코인->별조각 환율
/// </summary>
public class SolvedExchangeRate : Jsonable
{
    /// <summary>
    /// 코인->별조각 환율입니다. 수수료 1%는 제외되어 있습니다.
    /// </summary>
    [JsonProperty("rate")]
    public int Rate { get; set; }
}
