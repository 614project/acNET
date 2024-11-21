using acNET.Type;
using System.Security.AccessControl;

namespace acNET.Image
{
    /// <summary>
    /// 배경 사진의 정보
    /// </summary>
    public class Background : Jsonable
    {
        /// <summary>
        /// 배경의 ID입니다.
        /// </summary>
        public string backgroundId { get; set; } = string.Empty;
        /// <summary>
        /// 배경 사진으로 가는 하이퍼링크입니다.
        /// </summary>
        public string backgroundImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// fallbackBackgroundImageUrl
        /// </summary>
        public string? fallbackBackgroundImageUrl { get; set; } = null;
        /// <summary>
        /// 배경 비디오로 가는 하이퍼링크입니다.
        /// </summary>
        public string? backgroundVideoUrl { get; set; } = null;
        /// <summary>
        /// 해당 배경을 획득한 사용자의 수입니다.
        /// </summary>
        public long unlockedUserCount { get; set; }
        /// <summary>
        /// 배경의 이름입니다.
        /// </summary>
        public string displayName { get; set; } = string.Empty;
        /// <summary>
        /// 배경의 설명입니다.
        /// </summary>
        public string displayDescription { get; set; } = string.Empty;
        /// <summary>
        /// 해당 배경을 얻을 수 있는 조건입니다.
        /// </summary>
        public string conditions { get; set; } = string.Empty;
        /// <summary>
        /// 해당 배경을 얻을 수 있는 조건이 숨겨져 있는지 여부입니다.
        /// </summary>
        public bool hiddenConditions { get; set; }
        /// <summary>
        /// 해당 배경이 일러스트인지 여부입니다.
        /// </summary>
        public bool isIllust { get; set; }
        /// <summary>
        /// 해당 배경을 만든 사람들의 정보입니다.
        /// </summary>
        public List<AuthorsInfo> authors { get; set; } = null!;
        /// <summary>
        /// 배경 이미지를 저장합니다.
        /// </summary>
        /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
        /// <returns>성공시 null, 실패시 Exception</returns>
        public async Task<Exception?> SaveBackgroundImage(string filename) => await acAPI.SaveFile(this.backgroundImageUrl , filename);
        /// <summary>
        /// 배경 동영상을 저장합니다.
        /// </summary>
        /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
        /// <returns>성공시 null, 실패시 Exception</returns>
        public async Task<Exception?> SaveBackgroundVideo(string filename) => await acAPI.SaveFile(this.backgroundVideoUrl ?? string.Empty , filename);
    }

    /// <summary>
    /// 제작자의 정보입니다.
    /// </summary>
    public class AuthorsInfo : Jsonable
    {
        /// <summary>
        /// 작가의 ID입니다.
        /// </summary>
        public string authorId { get; set; } = string.Empty;
        /// <summary>
        /// 작가의 역할입니다.
        /// </summary>
        public string role { get; set; } = string.Empty;
        /// <summary>
        /// 작가의 홈페이지로 가는 하이퍼링크입니다.
        /// </summary>
        public string? authorUrl { get; set; } = null;
        /// <summary>
        /// 작가의 사용자 ID입니다.
        /// </summary>
        public string handle { get; set; } = string.Empty;
        /// <summary>
        /// 작가의 X(트위터) ID입니다.
        /// </summary>
        public string? twitter { get; set; } = null;
        /// <summary>
        /// 작가의 인스타그램 ID입니다.
        /// </summary>
        public string? instagram { get; set; } = null;
        /// <summary>
        /// 작가의 이름입니다.
        /// </summary>
        public string displayName { get; set; } = string.Empty;

        /// <summary>
        /// 작가의 sovled.ac 아이디로 유저 정보를 가져옵니다.
        /// </summary>
        /// <param name="api">acNET.acAPI</param>
        /// <returns>실패시 null</returns>
        public acAPI.acResult<User.RankedUser> GetAuthor(acAPI api) => api.GetUser(this.authorId);
        /// <summary>
        /// 작가의 sovled.ac 아이디로 유저 정보를 가져옵니다.(비동기)
        /// </summary>
        /// <param name="api">acNET.acAPI</param>
        /// <returns>실패시 null</returns>
        public async Task<acAPI.acResult<User.RankedUser>> GetAuthorAsync(acAPI api) => await api.GetUserAsync(this.authorId);
    }
}
