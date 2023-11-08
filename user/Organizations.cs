namespace acNET.User
{
    /// <summary>
    /// 사용자가 속한 조직 목록를 가져옵니다.
    /// </summary>
    public class Organizations : Type.BaseBody
    {
        /// <summary>
        /// 조직의 ID입니다.
        /// </summary>
        public int organizationId;
        /// <summary>
        /// 조직의 레이팅입니다.
        /// </summary>
        public int rating;
        /// <summary>
        /// 조직에 포함된 사용자의 수입니다.
        /// </summary>
        public int userCount;
        /// <summary>
        /// 조직의 총 난이도 기여 수입니다.
        /// </summary>
        public int voteCount;
        /// <summary>
        /// 조직의 총 푼 문제 수입니다.
        /// </summary>
        public int solvedCount;
        /// <summary>
        /// 조직의 이름입니다.
        /// </summary>
        public string name;
        /// <summary>
        /// 조직의 구분입니다.
        /// </summary>
        public string type;
        /// <summary>
        /// 조직의 색깔입니다.
        /// </summary>
        public string color;
    }
}
