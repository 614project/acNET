using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.User;

/// <summary>
/// 사용자가 속한 조직 목록를 가져옵니다.
/// </summary>
public class SolvedOrganization : Jsonable
{
    /// <summary>
    /// 조직의 ID입니다.
    /// </summary>
    [JsonProperty("organizationId")]
    public int OrganizationId { get; set; }
    /// <summary>
    /// 조직의 레이팅입니다.
    /// </summary>
    [JsonProperty("rating")]
    public int Rating { get; set; }
    /// <summary>
    /// 조직에 포함된 사용자의 수입니다.
    /// </summary>
    [JsonProperty("userCount")]
    public int UserCount { get; set; }
    /// <summary>
    /// 조직의 총 난이도 기여 수입니다.
    /// </summary>
    [JsonProperty("voteCount")]
    public int VoteCount { get; set; }
    /// <summary>
    /// 조직의 총 푼 문제 수입니다.
    /// </summary>
    [JsonProperty("solvedCount")]
    public int SolvedCount { get; set; }
    /// <summary>
    /// 조직의 이름입니다.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// 조직의 구분입니다.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;
    /// <summary>
    /// 조직의 색깔입니다.
    /// </summary>
    [JsonProperty("color")]
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// 조직 타입을 Enum 형식으로 가져옵니다.
    /// </summary>
    public SolvedOrganizationType GetEnumType {
        get => this.Type switch {
            "community" => SolvedOrganizationType.Community,
            "university" => SolvedOrganizationType.University,
            "company" => SolvedOrganizationType.Company,
            "high_school" => SolvedOrganizationType.High_school,
            _ => SolvedOrganizationType.Unknown
        };
    }
}

/// <summary>
/// 조직 타입을 Enum으로 정리한것.
/// </summary>
public enum SolvedOrganizationType 
{
    /// <summary>
    /// 알수없음 (오류)
    /// </summary>
    Unknown = 0,
    Community,
    University,
    Company,
    High_school,
}
