namespace acNET.User;

/// <summary>
/// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
/// </summary>
public class UserSolvedInfo : Type.Jsonable
{
    /// <summary>
    /// 문제의 레벨값.
    /// </summary>
    public long level{ get; set; }
    /// <summary>
    /// solved.ac에 등록된 해당 레벨의 문제 수입니다.
    /// </summary>
    public long total{ get; set; }
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    public long solved{ get; set; }
    /// <summary>
    /// 사용자가 부분 성공한 문제 수입니다.
    /// </summary>
    public long partial{ get; set; }
    /// <summary>
    /// 사용자가 시도해본 문제 수입니다.
    /// </summary>
    public long tried{ get; set; }
}
