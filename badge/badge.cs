using acNET.Type;

namespace acNET.Badge
{
    /// <summary>
    /// 사용자가 사용할수 있는 뱃지입니다.
    /// </summary>
    public class Badge : BaseBody
    {
        /// <summary>
        /// 뱃지의 ID입니다.
        /// </summary>
        public string badgeId;
        /// <summary>
        /// 뱃지 사진으로 가는 하이퍼링크입니다.
        /// </summary>
        public string badgeImageUrl;
        /// <summary>
        /// 뱃지의 이름입니다.
        /// </summary>
        public string displayName;
        /// <summary>
        /// 뱃지의 설명입니다.
        /// </summary>
        public string displayDescription;
        /// <summary>
        /// 뱃지 티어입니다.
        /// </summary>
        public string badgeTier;
        /// <summary>
        /// 뱃지 종류입니다.
        /// </summary>
        public string badgeCategory;

        /// <summary>
        /// 뱃지 사진을 저장합니다.
        /// </summary>
        /// <param name="filename">파일명</param>
        /// <returns>확장자가 포함된 파일명</returns>
        public string? SaveBadgeImage(string filename) {
            acAPI.SaveImage(this.badgeImageUrl, filename, out string f);
            return f;
        }
    }
}
