using acNET.Type;

namespace acNET.Search;

/// <summary>
/// 검색 결과입니다.
/// </summary>
public class SearchResult<T> : BaseBody where T : BaseBody
{
    /// <summary>
    /// 찾은 목록 수입니다.
    /// </summary>
    public long count { get; set; }
    /// <summary>
    /// 찾은 목록입니다.
    /// </summary>
    public List<T> items { get; set; } = null!;
    /// <summary>
    /// 목록을 순서대로 출력합니다. 각 항목마다 줄바꿈이 포함됩니다.
    /// </summary>
    public override string ToString()
    {
        return string.Join('\n',items);
    }
}
