using acNET.Type;

namespace acNET.Ranking
{
    /// <summary>
    /// 유저들의 순위 정보
    /// </summary>
    public class UserRanking : BaseBody
    {
        /// <summary>
        /// 순위가 배정된 사용자의 수입니다.
        /// </summary>
        public long count{ get; set; }
        /// <summary>
        /// 티어 순위로 정렬된 사용자 목록입니다.
        /// </summary>
        public List<User.RankedUser> items { get; set; } = null!;
    }

    /// <summary>
    /// 조직들의 순위 정보
    /// </summary>
    public class OrganizationRanking : BaseBody
    {
        /// <summary>
        /// 순위가 배정된 사용자의 수입니다.
        /// </summary>
        public long count{ get; set; }
        /// <summary>
        /// 티어 순위로 정렬된 사용자 목록입니다.
        /// </summary>
        public List<User.Organization> items { get; set; } = null!;
    }
}
