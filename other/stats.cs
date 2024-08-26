using acNET.Type;
namespace acNET.Site
{
    /// <summary>
    /// solved.ac 통계
    /// </summary>
    public class Stats : Jsonable
    {
        /// <summary>
        /// 여태까지 색인된 백준 문제 수입니다.
        /// </summary>
        public long problemCount{ get; set; }
        /// <summary>
        /// 여태까지 난이도가 기여된 백준 문제 수입니다.
        /// </summary>
        public long problemVotedCount{ get; set; }
        /// <summary>
        /// 여태까지 등록한 사용자 수입니다.
        /// </summary>
        public long userCount{ get; set; }
        /// <summary>
        /// 여태까지 난이도에 기여한 사용자 수입니다.
        /// </summary>
        public long contributorCount{ get; set; }
        /// <summary>
        /// 여태까지 이루어진 난이도 기여의 수입니다.
        /// </summary>
        public long contributionCount{ get; set; }
    }
}
