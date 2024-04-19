using acNET.Type;

namespace acNET.Image
{
    /// <summary>
    /// 배경 사진의 정보
    /// </summary>
    public class Background : BaseBody
    {
        /// <summary>
        /// 배경의 ID입니다.
        /// </summary>
        public string backgroundId;
        /// <summary>
        /// 배경 사진으로 가는 하이퍼링크입니다.
        /// </summary>
        public string backgroundImageUrl;
        /// <summary>
        /// fallbackBackgroundImageUrl
        /// </summary>
        public string? fallbackBackgroundImageUrl;
        /// <summary>
        /// 배경 비디오로 가는 하이퍼링크입니다.
        /// </summary>
        public string? backgroundVideoUrl;
        /// <summary>
        /// 해당 배경을 획득한 사용자의 수입니다.
        /// </summary>
        public long unlockedUserCount;
        /// <summary>
        /// 배경의 이름입니다.
        /// </summary>
        public string displayName;
        /// <summary>
        /// 배경의 설명입니다.
        /// </summary>
        public string displayDescription;
        /// <summary>
        /// 해당 배경을 얻을 수 있는 조건입니다.
        /// </summary>
        public string conditions;
        /// <summary>
        /// 해당 배경을 얻을 수 있는 조건이 숨겨져 있는지 여부입니다.
        /// </summary>
        public bool hiddenConditions;
        /// <summary>
        /// 해당 배경이 일러스트인지 여부입니다.
        /// </summary>
        public bool isIllust;
        /// <summary>
        /// 해당 배경을 만든 사람들의 정보입니다.
        /// </summary>
        public List<AuthorsInfo> authors;

        /// <summary>
        /// 배경 이미지를 저장합니다.
        /// </summary>
        /// <param name="filename">파일명 (확장자 제외)</param>
        /// <returns>실패시 null, 성공시 저장된 파일명 (확장자 포함)</returns>
        public string? SaveBackgroundImage(string filename)
        {
            if(acAPI.SaveImage(this.backgroundImageUrl, filename, out var f)) return f;
            return null;
        }
    }

    /// <summary>
    /// 제작자의 정보입니다.
    /// </summary>
    public class AuthorsInfo : BaseBody
    {
        /// <summary>
        /// 작가의 ID입니다.
        /// </summary>
        public string authorId;
        /// <summary>
        /// 작가의 역할입니다.
        /// </summary>
        public string role;
        /// <summary>
        /// 작가의 홈페이지로 가는 하이퍼링크입니다.
        /// </summary>
        public string? authorUrl;
        /// <summary>
        /// 작가의 사용자 ID입니다.
        /// </summary>
        public string handle;
        /// <summary>
        /// 작가의 X(트위터) ID입니다.
        /// </summary>
        public string? twitter;
        /// <summary>
        /// 작가의 인스타그램 ID입니다.
        /// </summary>
        public string? instagram;
        /// <summary>
        /// 작가의 이름입니다.
        /// </summary>
        public string displayName;

        /// <summary>
        /// 작가의 sovled.ac 아이디로 유저 정보를 가져옵니다.
        /// </summary>
        /// <param name="api">acNET.acAPI</param>
        /// <returns>실패시 null</returns>
        public User.RankedUser? GetAuthor(acAPI api) => api.GetUser(this.authorId);
    }
}
