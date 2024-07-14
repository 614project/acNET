namespace acNET.User;

public class UserContributionStat : Type.BaseBody
{
    public int total{ get; set; }
    public int solved{ get; set; }
    public int solvedStandards{ get; set; }
    public int contributed{ get; set; }
}