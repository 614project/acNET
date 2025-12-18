using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.User;

/// <summary>
/// 클래스별 푼 문제 수
/// </summary>
public class SolvedUserClassStat : Jsonable
{
    /// <summary>
    /// 총 문제 수입니다.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
    /// <summary>
    /// 총 문제를 해결한 수입니다.
    /// </summary>
    [JsonProperty("totalSolved")]
    public int TotalSolved { get; set; }
    /// <summary>
    /// 에센셜 문제 수입니다.
    /// </summary>
    [JsonProperty("essentials")]
    public int Essentials { get; set; }
    /// <summary>
    /// 사용자가 푼 클래스 에센셜 문제 수입니다.
    /// </summary>
    [JsonProperty("essentialSolved")]
    public int EssentialSolved { get; set; }
    /// <summary>
    /// 클래스 숫자 입니다.
    /// </summary>
    [JsonProperty("class")]
    public int Class { get; set; }
    /// <summary>
    /// 사용자가 획득한 클래스 치장입니다.
    /// </summary>
    [JsonProperty("decoration")]
    public string? Decoration { get; set; }
}