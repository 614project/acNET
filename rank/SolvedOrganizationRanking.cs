using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Ranking;

/// <summary>
/// 조직들의 순위 정보
/// </summary>
public class SolvedOrganizationRanking : Jsonable
{
    /// <summary>
    /// 순위가 배정된 사용자의 수입니다.
    /// </summary>
    [JsonProperty("count")]
    public long Count { get; set; }
    /// <summary>
    /// 티어 순위로 정렬된 사용자 목록입니다.
    /// </summary>
    [JsonProperty("items")]
    public List<User.SolvedOrganization> Items { get; set; } = null!;
}
