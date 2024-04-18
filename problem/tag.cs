using acNET.Type;

namespace acNET.Problem;

/// <summary>
/// 문제 태그
/// </summary>
public class ProblemTag : BaseBody
{
    /// <summary>
    /// 태그의 ID입니다.
    /// </summary>
    public string key;
    /// <summary>
    /// (알수없음.)
    /// </summary>
    public bool isMeta;
    /// <summary>
    /// 백준에서 사용되는 이 태그의 ID입니다.
    /// </summary>
    public long bojTagId;
    /// <summary>
    /// 이 태그를 포함하는 문제의 수입니다.
    /// </summary>
    public long problemCount;
    /// <summary>
    /// 언어별 태그의 이름 목록입니다.
    /// </summary>
    public List<DisplayName> displayNames;
    /// <summary>
    /// 태그의 별칭입니다.
    /// </summary>
    public List<Alias> aliases;
}

/// <summary>
/// 문제 태그의 진행도 입니다.
/// </summary>
public class TagStat : BaseBody
{
    /// <summary>
    /// 태그에 대한 정보입니다.
    /// </summary>
    public ProblemTag tag;
    /// <summary>
    /// solved.ac에 등록된 해당 태그의 문제 수입니다.
    /// </summary>
    public string total;
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    public string solved;
    /// <summary>
    /// 사용자가 부분 성공한 문제 수입니다.
    /// </summary>
    public string partial;
    /// <summary>
    /// 사용자가 시도해본 문제 수입니다.
    /// </summary>
    public string tried;
}

/// <summary>
/// 모든 태그 별 진행도 입니다.
/// </summary>
public class ProblemTagStats : BaseBody
{
    /// <summary>
    /// solved.ac에 등록된 모든 태그의 개수입니다.
    /// </summary>
    public int count;
    /// <summary>
    /// 태그 별 진행도 목록입니다.
    /// </summary>
    public List<TagStat> items;
}