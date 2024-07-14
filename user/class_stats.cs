using Newtonsoft.Json;

namespace acNET.User;

public class UserClassStat : Type.BaseBody
{
    /// <summary>
    /// 총 문제 수입니다.
    /// </summary>
    public int total{ get; set; }
    public int totalSolved{ get; set; }
    /// <summary>
    /// 에센셜 문제 수입니다.
    /// </summary>
    public int essentials{ get; set; }
    public int essentialSolved{ get; set; }
    /// <summary>
    /// 클래스 숫자 입니다.
    /// </summary>
    [JsonProperty("class")]
    public int @class{ get; set; }
    public string? decoration{ get; set; }
}