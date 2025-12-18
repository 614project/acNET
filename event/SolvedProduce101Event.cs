using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Events;

/// <summary>
/// 2025년 4월 1일에 열린 solved.ac의 PRODUCE 10¹ 이벤트에 대한 정보입니다.
/// </summary>
public class SolvedProduce101Event : Jsonable
{
    /// <summary>
    /// 전체 투표 현황
    /// </summary>
    public class TotalVotes
    {
        /// <summary>
        /// 전체 투표 수
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
        /// <summary>
        /// 순서대로 골드, 다이아몬드, 루비, 마스터, 박한별, 봄, 브론즈, 실버, 은하, 플래티넘에 대한 득표 수입니다.
        /// </summary>
        [JsonProperty("votes")]
        public List<int> Votes { get; set; } = null!;
    }
    /// <summary>
    /// 투표권 흭득/사용 현황
    /// </summary>
    public class VoteStatus
    {
        /// <summary>
        /// 획득한 투표권 수
        /// </summary>
        [JsonProperty("earned")]
        public int Earned { get; set; }
        /// <summary>
        /// 사용한 투표권 수
        /// </summary>
        [JsonProperty("used")]
        public int Used { get; set; }
        /// <summary>
        /// 순서대로 골드, 다이아몬드, 루비, 마스터, 박한별, 봄, 브론즈, 실버, 은하, 플래티넘에 대한 투표 수입니다.
        /// </summary>
        [JsonProperty("votes")]
        public List<int> Votes { get; set; } = null!;
    }
    /// <summary>
    /// 이벤트 기간
    /// </summary>
    public class EventSchedule
    {
        /// <summary>
        /// 시작일
        /// </summary>
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        /// <summary>
        /// 종료일
        /// </summary>
        [JsonProperty("end")]
        public DateTime End { get; set; }
    }

    /// <summary>
    /// 전체 투표 현황
    /// </summary>
    [JsonProperty("status")]
    TotalVotes Status { get; set; } = null!;
    /// <summary>
    /// 나의 투표 현황
    /// </summary>
    [JsonProperty("myStatus")]
    VoteStatus MyStatus { get; set; } = null!;
    /// <summary>
    /// 이벤트 기간
    /// </summary>
    [JsonProperty("schedule")]
    EventSchedule Schedule { get; set; } = null!;
}
