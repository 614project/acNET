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
        public long problemId;
        /// <summary>
        /// 한국어 문제 제목입니다. HTML 엔티티나 LaTeX 수식을 포함할 수 있습니다.
        /// </summary>
        public string titleKo;
        /// <summary>
        /// 언어별 문제 제목입니다.
        /// </summary>
        public List<ProblemTitle> titles;
        /// <summary>
        /// 채점 가능 여부입니다.
        /// </summary>
        public bool isSolvable;
        /// <summary>
        /// 부분 점수 혹은 서브태스크 문제 여부입니다.
        /// </summary>
        public bool isPartial;
        /// <summary>
        /// 맞은 사람 수입니다.
        /// </summary>
        public long acceptedUserCount;
        /// <summary>
        /// 문제의 레벨(티어)값
        /// </summary>
        public long level;
        /// <summary>
        /// 난이도 기여자의 수입니다.
        /// </summary>
        public long votedUserCount;
        /// <summary>
        /// 새싹 문제인지 여부입니다.
        /// </summary>
        public bool sprout;
        /// <summary>
        /// 레이팅을 주지 않는지 여부입니다.
        /// </summary>
        public bool givesNoRating;
        /// <summary>
        /// 난이도 기여 제한 여부입니다.
        /// </summary>
        public bool isLevelLocked;
        /// <summary>
        /// 평균 시도 횟수입니다.
        /// </summary>
        public long averageTries;
        /// <summary>
        /// 공식 문제 여부입니다.
        /// </summary>
        public bool official;
        /// <summary>
        /// 문제 레벨에 맞는 티어 이름을 가져옵니다.
        /// </summary>
        public string? GetLevelName => Converter.LevelName(level);
    }

    /// <summary>
    /// 태그가 포함된 문제 정보입니다.
    /// </summary>
    public class TaggedProblem : Problem
    {
        /// <summary>
        /// 태그 목록입니다.
        /// </summary>
        public List<ProblemTag> tags; 
        /// <summary>
        /// 레벨(티어)값을 레벨 이름으로 가져옵니다.
        /// </summary>
        public new string? GetLevelName => Converter.LevelName(this.level);
    }

    /// <summary>
    /// 문제의 제목에 대한 정보
    /// </summary>
    public class ProblemTitle
    {
        /// <summary>
        /// 언어입니다.
        /// </summary>
        public string language;
        /// <summary>
        /// 표시되는 언어의 이름입니다.
        /// </summary>
        public string languageDisplayName;
        /// <summary>
        /// 문제의 제목입니다.
        /// </summary>
        public string title;
        /// <summary>
        /// 원본과 동일한지 여부입니다.
        /// </summary>
        public bool isOriginal;
    }

    /// <summary>
    /// 표시된 이름
    /// </summary>
    public class DisplayName
    {
        /// <summary>
        /// 언어입니다.
        /// </summary>
        public string language;
        /// <summary>
        /// 태그의 이름입니다.
        /// </summary>
        public string name;
        /// <summary>
        /// 태그의 짧은 이름입니다.
        /// </summary>
        [JsonProperty("short")]
        public string @short;
    }

    /// <summary>
    /// 별칭
    /// </summary>
    public class Alias
    {
        /// <summary>
        /// 별칭입니다.
        /// </summary>
        public string alias;
    }
}