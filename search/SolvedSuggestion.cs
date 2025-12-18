using AcNET.Type;
using AcNET.User;
using Newtonsoft.Json;

namespace AcNET.Search;

/// <summary>
/// 자동 완성 및 상위 검색 결과
/// </summary>
public class SolvedSuggestion : Jsonable
{
    /// <summary>
    /// 고급 검색 관련 자동 완성입니다.
    /// </summary>
    [JsonProperty("autocomplete")]
    public SolvedAutoComplete[] Autocomplete { get; set; } = null!;
    /// <summary>
    /// 요약한 문제 정보입니다.
    /// </summary>
    [JsonProperty("problems")]
    public SolvedBriefProblem[] Problems { get; set; } = null!;
    /// <summary>
    /// 검색 결과로 나올 총 문제 수입니다
    /// </summary>
    [JsonProperty("problemCount")]
    public int ProblemCount { get; set; }
    /// <summary>
    /// 요약한 태그 정보 목록입니다.
    /// </summary>
    [JsonProperty("tags")]
    public SolvedBriefProblemTag[] Tags { get; set; } = null!;
    /// <summary>
    /// 검색 결과로 나올 총 태그 수입니다.
    /// </summary>
    [JsonProperty("tagCount")]
    public int TagCount { get; set; }
    /// <summary>
    /// 사용자 정보 목록입니다.
    /// </summary>
    [JsonProperty("users")]
    public SolvedSocialUser[] Users { get; set; } = null!;
    /// <summary>
    /// 검색 결과로 나올 총 사용자 수입니다.
    /// </summary>
    [JsonProperty("userCount")]
    public int UserCount { get; set; }
}

/// <summary>
/// 고급 검색 관련 자동 완성입니다.
/// </summary>
public class SolvedAutoComplete : Jsonable
{
    /// <summary>
    /// 자동 완성 제목입니다. 만약 href이 없을 경우 누르면 해당 값으로 자동 완성합니다.
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = string.Empty;
    /// <summary>
    /// 자동 완성 요소의 설명입니다.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// 요약한 문제 정보입니다.
/// </summary>
public class SolvedBriefProblem : Jsonable
{
    /// <summary>
    /// 문제 ID입니다.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }
    /// <summary>
    /// 문제 제목입니다.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// 문제 난이도입니다.
    /// </summary>
    [JsonProperty("level")]
    public int Level { get; set; }
    /// <summary>
    /// 푼 사람 수입니다.
    /// </summary>
    [JsonProperty("solved")]
    public int Solved { get; set; }
    /// <summary>
    /// 자동 완성 제목입니다. 만약 href이 없을 경우 누르면 해당 값으로 자동 완성합니다.
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = string.Empty;
    /// <summary>
    /// 자동 완성 요소의 설명입니다.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// 문제 주소입니다.
    /// </summary>
    [JsonProperty("href")]
    public string? Href { get; set; }
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
/// 요약한 태그 정보입니다.
/// </summary>
public class SolvedBriefProblemTag : Jsonable
{
    /// <summary>
    /// solved.ac에서 쓰는 태그 ID입니다.
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 태그 이름입니다. 사용자 언어에 따라 번역되지 않습니다.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 태그가 붙은 문제 수입니다.
    /// </summary>
    [JsonProperty("problemCount")]
    public string ProblemCount { get; set; } = string.Empty;
    /// <summary>
    /// 자동 완성 제목입니다. 만약 href이 없을 경우 누르면 해당 값으로 자동 완성합니다.
    /// </summary>
    [JsonProperty("caption")]
    public string Caption { get; set; } = string.Empty;
    /// <summary>
    /// 자동 완성 요소의 설명입니다.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("href")]
    public string? Href { get; set; }
}