namespace acNET.User;

/// <summary>
/// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
/// </summary>
public class UserSolvedInfo : Type.BaseBody
{
    /// <summary>
    /// 문제의 레벨값.
    /// </summary>
    public long level;
    /// <summary>
    /// solved.ac에 등록된 해당 레벨의 문제 수입니다.
    /// </summary>
    public long total;
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    public long solved;
    /// <summary>
    /// 사용자가 부분 성공한 문제 수입니다.
    /// </summary>
    public long partial;
    /// <summary>
    /// 사용자가 시도해본 문제 수입니다.
    /// </summary>
    public long tried;
}
