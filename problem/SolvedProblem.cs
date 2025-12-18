using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Problem;

/// <summary>
/// 문제 정보입니다.
/// </summary>
public class SolvedProblem : Jsonable
{
    /// <summary>
    /// 문제 ID입니다.
    /// </summary>
    [JsonProperty("problemId")]
    public int ProblemId { get; set; }
    /// <summary>
    /// 한국어 문제 제목입니다. HTML 엔티티나 LaTeX 수식을 포함할 수 있습니다.
    /// </summary>
    [JsonProperty("titleKo")]
    public string TitleKo { get; set; } = string.Empty;
    /// <summary>
    /// 언어별 문제 제목입니다.
    /// </summary>
    [JsonProperty("titles")]
    public List<SolvedProblemTitle> Titles { get; set; } = null!;
    /// <summary>
    /// 채점 가능 여부입니다.
    /// </summary>
    [JsonProperty("isSolvable")]
    public bool IsSolvable { get; set; }
    /// <summary>
    /// 부분 점수 혹은 서브태스크 문제 여부입니다.
    /// </summary>
    [JsonProperty("isPartial")]
    public bool IsPartial { get; set; }
    /// <summary>
    /// 맞은 사람 수입니다.
    /// </summary>
    [JsonProperty("acceptedUserCount")]
    public int AcceptedUserCount { get; set; }
    /// <summary>
    /// 문제의 레벨(티어)값
    /// </summary>
    [JsonProperty("level")]
    public int Level { get; set; }
    /// <summary>
    /// 난이도 기여자의 수입니다.
    /// </summary>
    [JsonProperty("votedUserCount")]
    public int VotedUserCount { get; set; }
    /// <summary>
    /// 새싹 문제인지 여부입니다.
    /// </summary>
    [JsonProperty("sprout")]
    public bool Sprout { get; set; }
    /// <summary>
    /// 레이팅을 주지 않는지 여부입니다.
    /// </summary>
    [JsonProperty("givesNoRating")]
    public bool GivesNoRating { get; set; }
    /// <summary>
    /// 난이도 기여 제한 여부입니다.
    /// </summary>
    [JsonProperty("isLevelLocked")]
    public bool IsLevelLocked { get; set; }
    /// <summary>
    /// 평균 시도 횟수입니다.
    /// </summary>
    [JsonProperty("averageTries")]
    public long AverageTries { get; set; }
    /// <summary>
    /// 공식 문제 여부입니다.
    /// </summary>
    [JsonProperty("official")]
    public bool Official { get; set; }
    /// <summary>
    /// 문제 레벨에 맞는 티어 이름을 가져옵니다.
    /// </summary>
    public string? GetTierName => SolvedConverter.LevelName(Level);
    /// <summary>
    /// 문제 레벨에 맞는 티어 색깔을 가져옵니다. (7자리 Hex 코드입니다.)
    /// </summary>
    public string GetTierColor => SolvedConverter.LevelColor(Level);
}

/// <summary>
/// 태그가 포함된 문제 정보입니다.
/// </summary>
public class SolvedTaggedProblem : SolvedProblem
{
    /// <summary>
    /// 태그 목록입니다.
    /// </summary>
    [JsonProperty("tags")]
    public List<SolvedProblemTag> Tags { get; set; } = null!;
}

/// <summary>
/// 문제의 제목에 대한 정보
/// </summary>
public class SolvedProblemTitle
{
    /// <summary>
    /// 언어입니다.
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// 표시되는 언어의 이름입니다.
    /// </summary>
    [JsonProperty("languageDisplayName")]
    public string LanguageDisplayName { get; set; } = string.Empty;
    /// <summary>
    /// 문제의 제목입니다.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// 원본과 동일한지 여부입니다.
    /// </summary>
    [JsonProperty("isOriginal")]
    public bool IsOriginal { get; set; }
}

/// <summary>
/// 표시된 이름
/// </summary>
public class SolvedDisplayName
{
    /// <summary>
    /// 언어입니다.
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; set; } = string.Empty;
    /// <summary>
    /// 태그의 이름입니다.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 태그의 짧은 이름입니다.
    /// </summary>
    [JsonProperty("short")]
    public string ShortName { get; set; } = string.Empty;
}

/// <summary>
/// 별칭
/// </summary>
public class SolvedAlias
{
    /// <summary>
    /// 별칭입니다.
    /// </summary>
    [JsonProperty("alias")]
    public string Alias { get; set; } = string.Empty;
}