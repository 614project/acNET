namespace acNET.User;

/// <summary>
/// 문제 수준별 기여한 문제 수가 담긴 목록
/// </summary>
public class UserContributionStat : Type.Jsonable
{
    /// <summary>
    /// solved.ac에 등록된 해당 수준 문제 수입니다.
    /// </summary>
    public int total{ get; set; }
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    public int solved{ get; set; }
    /// <summary>
    /// 사용자가 푼 표준 문제 수입니다.
    /// </summary>
    public int solvedStandards{ get; set; }
    /// <summary>
    /// 사용자가 기여한 문제 수입니다.
    /// </summary>
    public int contributed{ get; set; }
}