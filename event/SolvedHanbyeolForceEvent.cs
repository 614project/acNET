using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Events;

/// <summary>
/// 2022년 4월 1일에 열린 한별포스 이벤트
/// </summary>
public class SolvedHanbyeolForceEvent : Jsonable
{
    public class ProbsStat
    {
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("succeed")]
        public double Succeed { get; set; }

        [JsonProperty("asIs")]
        public double AsIs { get; set; }

        [JsonProperty("fail")]
        public double Fail { get; set; }

        [JsonProperty("destroy")]
        public double Destroy { get; set; }
    }

    [JsonProperty("stars")]
    public int Stars { get; set; }

    [JsonProperty("itemLocked")]
    public bool ItemLocked { get; set; }

    [JsonProperty("hanbyeolCatch")]
    public bool HanbyeolCatch { get; set; }

    [JsonProperty("probs")]
    public ProbsStat Probs { get; set; } = null;
}
