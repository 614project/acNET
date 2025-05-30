using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Events;

/// <summary>
/// 2021년 11월 11일에 열린 빼빼로 데이 이벤트
/// </summary>
public class SolvedPeperoDay2021Event : Jsonable
{
    public class Cost
    {
        [JsonProperty("from")]
        public string From { get; set; } = string.Empty;

        [JsonProperty("to")]
        public string To { get; set; } = string.Empty;

        [JsonProperty("rate")]
        public int Rate { get; set; }
    }

    [JsonProperty("base")]
    public int Base { get; set; }

    [JsonProperty("cream")]
    public int Cream { get; set; }

    [JsonProperty("choco")]
    public int Choco { get; set; }

    [JsonProperty("mint")]
    public int Mint { get; set; }

    [JsonProperty("ruby")]
    public int Ruby { get; set; }

    [JsonProperty("cookieCream")]
    public int CookieCream { get; set; }

    [JsonProperty("cookieChoco")]
    public int CookieChoco { get; set; }

    [JsonProperty("cookieMint")]
    public int CookieMint { get; set; }

    [JsonProperty("cookieRuby")]
    public int CookieRuby { get; set; }

    [JsonProperty("stardusts")]
    public int Stardusts { get; set; }

    [JsonProperty("gifticon")]
    public string Gifticon { get; set; } = string.Empty;

    [JsonProperty("cookie")]
    public int Cookie { get; set; }

    [JsonProperty("cookieRank")]
    public int CookieRank { get; set; }

    [JsonProperty("cookiePercentile")]
    public int CookiePercentile { get; set; }

    [JsonProperty("costs")]
    public List<Cost> Costs { get; set; } = null;
}