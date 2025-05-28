using Newtonsoft.Json;

namespace AcNET.Problem;

/// <summary>
/// 수준별 문제
/// </summary>
public class SolvedLevel : Type.Jsonable
{
    /// <summary>
    /// 레벨값 마다 지정된 레벨 이름이 모인 문자열 배열
    /// </summary>
    public static readonly string[] Names = @"Unrated
Bronze V
Bronze IV
Bronze III
Bronze II
Bronze I
Silver V
Silver IV
Silver III
Silver II
Silver I
Gold V
Gold IV
Gold III
Gold II
Gold I
Platinum V
Platinum IV
Platinum III
Platinum II
Platinum I
Diamond V
Diamond IV
Diamond III
Diamond II
Diamond I
Ruby V
Ruby IV
Ruby III
Ruby II
Ruby I
Master".Replace("\r",string.Empty).Split('\n');
    /// <summary>
    /// 레벨값 마다 지정된 레벨 색깔이 모인 문자열 배열. (형식은 7자리 Hex 코드)
    /// </summary>
    public static readonly string[] Colors =
    {
        //unrated
        "#2D2D2D",
        //bronze
        "#9D4900",
        "#A54F00",
        "#AD5600",
        "#B55D0A",
        "#C67739",
        //Silver
        "#38546E",
        "#3D5A74",
        "#435F7A",
        "#496580",
        "#4E6A86",
        //gold
        "#D28500",
        "#DF8F00",
        "#EC9A00",
        "#F9A518",
        "#FFB028",
        //platinum
        "#00C78B",
        "#00D497",
        "#27E2A4",
        "#3EF0B1",
        "#51FDBD",
        //diamond
        "#009EE5",
        "#00A9F0",
        "#00B4FC",
        "#2BBFFF",
        "#41CAFF",
        //ruby
        "#E0004C",
        "#EA0053",
        "#F5005A",
        "#FF0062",
        "#FF3071",
        //master
        "#B491FF"
    };
    /// <summary>
    /// 레벨(티어)값입니다.
    /// </summary>
    [JsonProperty("level")]
    public long Level { get; set; }
    /// <summary>
    /// 이 문제 수준인 문제 수입니다.
    /// </summary>
    [JsonProperty("count")]
    public long Count { get; set; }
    /// <summary>
    /// 문제의 레벨(티어)값을 레벨 이름으로 가져옵니다.
    /// </summary>
    public string GetTierName => Names[this.Level];
    /// <summary>
    /// 레벨(티어)값에 알맞는 색깔의 Hex 코드를 가져옵니다. (7자리 #rrggbb 로 구성되어있습니다.)
    /// </summary>
    public string GetTierColor => Colors[this.Level];
}
