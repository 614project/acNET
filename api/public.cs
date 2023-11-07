using acNET.Image;
using acNET.User;

namespace acNET
{
    public partial class acAPI
    {
        /// <summary>
        /// 사용자의 정보를 가져옵니다. 만약 로그인한 경우, 라이벌 여부도 가져옵니다.
        /// </summary>
        /// <param name="handle">사용자 ID</param>
        /// <returns>실패시 null</returns>
        public User.User? GetUser(string handle) => GET<User.User>("user/show", $"?handle={handle}");
        /// <summary>
        /// 사용자가 속한 조직 목록를 가져옵니다.
        /// </summary>
        /// <param name="handle">사용자 ID</param>
        /// <returns>실패시 null</returns>
        public Group? GetGroupFromUser(string handle) => GET<Group>("user/organizations", $"?handle={handle}");
        /// <summary>
        /// 사용자가 푼 문제 개수를 문제 수준별로 가져옵니다.
        /// </summary>
        /// <param name="handle">사용자 ID</param>
        /// <returns>실패시 null</returns>
        public UserSolvedInfo? GetSolvedFromUser(string handle) => GET<UserSolvedInfo>("user/problem_stats", $"?handle={handle}");
        /// <summary>
        /// 배경의 정보를 가져옵니다.
        /// </summary>
        /// <param name="backgroundId">배경 ID</param>
        /// <returns>실패시 null</returns>
        public Background? GetBackground(string backgroundId) => GET<Background>("background/show", $"?backgroundId={backgroundId}");
        /// <summary>
        /// 사용자가 푼 문제 중 상위 100문제를 가져옵니다.
        /// </summary>
        /// <param name="">사용자 ID</param>
        /// <returns>실패시 null</returns>
        public Top100? GetTop100FromUser(string handle) => GET<Top100>("user/top_100", $"?handle={handle}");
        /// <summary>
        /// solved.ac 통계를 가져옵니다.
        /// </summary>
        /// <returns>실패시 null</returns>
        public SiteStats? GetSiteStats() => GET<SiteStats>("site/stats");
    }
}
