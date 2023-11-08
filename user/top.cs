using acNET.Type;

namespace acNET.User
{
    /// <summary>
    /// 상위 100문제
    /// </summary>
    public class Top100 : BaseBody
    {
        /// <summary>
        /// 문제 수입니다.
        /// </summary>
        public long count;
        /// <summary>
        /// 문제 목록입니다.
        /// </summary>
        public List<Problem.TaggedProblem> items;
    }
}
