using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Search;

/// <summary>
/// 검색 결과입니다.
/// </summary>
public class SolvedSearchResult<T> : Jsonable where T : Jsonable
{
    /// <summary>
    /// 찾은 목록 수입니다.
    /// </summary>
    [JsonProperty("count")]
    public long Count { get; set; }
    /// <summary>
    /// 찾은 목록입니다.
    /// </summary>
    [JsonProperty("items")]
    public List<T> Items { get; set; } = null!;
    /// <summary>
    /// 목록을 순서대로 출력합니다. 각 항목마다 줄바꿈이 포함됩니다.
    /// </summary>
    public override string ToString() => string.Join('\n' , Items);
}
