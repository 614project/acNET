using Newtonsoft.Json;

namespace acNET.User;

public class UserClassStat : Type.BaseBody
{
    public int total;
    public int totalSolved;
    public int essentials;
    public int essentialSolved;
    [JsonProperty("class")]
    public int @class;
    public string? decoration;
}