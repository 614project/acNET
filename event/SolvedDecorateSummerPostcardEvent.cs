using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Events;

/// <summary>
/// 2022년 6월 26일에 열린 여름 엽서 꾸미기 이벤트
/// </summary>
public class SolvedDecorateSummerPostcardEvent : Jsonable
{
    [JsonProperty("highestRecord")]
    public RecordStatus HighestRecord { get; set; } = null;

    [JsonProperty("equipStatus")]
    public Equips EquipStatus { get; set; } = null;

    [JsonProperty("items")]
    public List<ProblemItem> Items { get; set; } = null;

    [JsonProperty("result")]
    public ResultStat Result { get; set; } = null;

    public class ProblemItem
    {
        [JsonProperty("problemId")]
        public int ProblemId { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("isInLevel")]
        public string IsInLevel { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; } = null;

        [JsonProperty("shape")]
        public int Shape { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("acceptedUserCount")]
        public int AcceptedUserCount { get; set; }

        [JsonProperty("equipped")]
        public bool Equipped { get; set; }
    }

    public class Equips
    {
        [JsonProperty("equipItems")]
        public List<ProblemItem> EquipItems { get; set; } = null;
    }

    public class RecordStatus
    {
        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("equipStatus")]
        public Equips EquipStatus { get; set; } = null;
    }

    public class ResultStat
    {
        [JsonProperty("isWinner")]
        public bool IsWinner { get; set; }

        [JsonProperty("wonStardusts")]
        public int WonStardusts { get; set; }
    }
}