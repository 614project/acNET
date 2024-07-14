using acNET.Type;
using Newtonsoft.Json;

namespace acNET.Problem
{
    /// <summary>
    /// 문제 정보입니다.
    /// </summary>
    public class Problem : BaseBody
    {
        /// <summary>
        /// 문제 ID입니다.
        /// </summary>
        public long problemId { get; set; }
        /// <summary>
        /// 한국어 문제 제목입니다. HTML 엔티티나 LaTeX 수식을 포함할 수 있습니다.
        /// </summary>
        public string titleKo { get; set; }
        /// <summary>
        /// 언어별 문제 제목입니다.
        /// </summary>
        public List<ProblemTitle> titles { get; set; }
        /// <summary>
        /// 채점 가능 여부입니다.
        /// </summary>
        public bool isSolvable { get; set; }
        /// <summary>
        /// 부분 점수 혹은 서브태스크 문제 여부입니다.
        /// </summary>
        public bool isPartial { get; set; }
        /// <summary>
        /// 맞은 사람 수입니다.
        /// </summary>
        public long acceptedUserCount { get; set; }
        /// <summary>
        /// 문제의 레벨(티어)값
        /// </summary>
        public long level { get; set; }
        /// <summary>
        /// 난이도 기여자의 수입니다.
        /// </summary>
        public long votedUserCount { get; set; }
        /// <summary>
        /// 새싹 문제인지 여부입니다.
        /// </summary>
        public bool sprout { get; set; }
        /// <summary>
        /// 레이팅을 주지 않는지 여부입니다.
        /// </summary>
        public bool givesNoRating { get; set; }
        /// <summary>
        /// 난이도 기여 제한 여부입니다.
        /// </summary>
        public bool isLevelLocked { get; set; }
        /// <summary>
        /// 평균 시도 횟수입니다.
        /// </summary>
        public long averageTries { get; set; }
        /// <summary>
        /// 공식 문제 여부입니다.
        /// </summary>
        public bool official { get; set; }
        /// <summary>
        /// 문제 레벨에 맞는 티어 이름을 가져옵니다.
        /// </summary>
        public string? GetLevelName => Converter.LevelName(level);
        /// <summary>
        /// 문제 레벨에 맞는 티어 색깔을 가져옵니다. (7자리 Hex 코드입니다.)
        /// </summary>
        public string GetLevelColor => Converter.LevelColor(level);
    }

    /// <summary>
    /// 태그가 포함된 문제 정보입니다.
    /// </summary>
    public class TaggedProblem : Problem
    {
        /// <summary>
        /// 태그 목록입니다.
        /// </summary>
        public List<ProblemTag> tags { get; set; }
    }

    /// <summary>
    /// 문제의 제목에 대한 정보
    /// </summary>
    public class ProblemTitle
    {
        /// <summary>
        /// 언어입니다.
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 표시되는 언어의 이름입니다.
        /// </summary>
        public string languageDisplayName { get; set; }
        /// <summary>
        /// 문제의 제목입니다.
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 원본과 동일한지 여부입니다.
        /// </summary>
        public bool isOriginal { get; set; }
    }

    /// <summary>
    /// 표시된 이름
    /// </summary>
    public class DisplayName
    {
        /// <summary>
        /// 언어입니다.
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 태그의 이름입니다.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 태그의 짧은 이름입니다.
        /// </summary>
        [JsonProperty("short")]
        public string @short; //임시방편
    }

    /// <summary>
    /// 별칭
    /// </summary>
    public class Alias
    {
        /// <summary>
        /// 별칭입니다.
        /// </summary>
        public string alias { get; set; }
    }
}