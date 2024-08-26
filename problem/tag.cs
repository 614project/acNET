using acNET.Type;

namespace acNET.Problem;

/// <summary>
/// 문제 태그
/// </summary>
public class ProblemTag : Jsonable
{
    /// <summary>
    /// 태그의 ID입니다.
    /// </summary>
    public string key { get; set; } = string.Empty;
    /// <summary>
    /// (알수없음.)
    /// </summary>
    public bool isMeta { get; set; }
    /// <summary>
    /// 백준에서 사용되는 이 태그의 ID입니다.
    /// </summary>
    public long bojTagId { get; set; }
    /// <summary>
    /// 이 태그를 포함하는 문제의 수입니다.
    /// </summary>
    public long problemCount { get; set; }
    /// <summary>
    /// 언어별 태그의 이름 목록입니다.
    /// </summary>
    public List<DisplayName> displayNames { get; set; } = null!;
    /// <summary>
    /// 태그의 별칭입니다.
    /// </summary>
    public List<Alias> aliases { get; set; } = null!;
}

/// <summary>
/// 문제 태그의 진행도 입니다.
/// </summary>
public class TagStat : Jsonable
{
    /// <summary>
    /// 태그에 대한 정보입니다.
    /// </summary>
    public ProblemTag tag { get; set; } = null!;
    /// <summary>
    /// solved.ac에 등록된 해당 태그의 문제 수입니다.
    /// </summary>
    public long total { get; set; }
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    public long solved { get; set; }
    /// <summary>
    /// 사용자가 부분 성공한 문제 수입니다.
    /// </summary>
    public long partial { get; set; }
    /// <summary>
    /// 사용자가 시도해본 문제 수입니다.
    /// </summary>
    public long tried { get; set; }
}