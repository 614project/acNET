using Newtonsoft.Json;

namespace acNET.User;

public class UserClassStat : Type.BaseBody
{
    /// <summary>
    /// 총 문제 수입니다.
    /// </summary>
    public int total;
    public int totalSolved;
    /// <summary>
    /// 에센셜 문제 수입니다.
    /// </summary>
    public int essentials;
    public int essentialSolved;
    /// <summary>
    /// 클래스 숫자 입니다.
    /// </summary>
    [JsonProperty("class")]
    public int @class;
    public string? decoration;
}