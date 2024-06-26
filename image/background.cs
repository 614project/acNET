﻿using acNET.Type;
using RestSharp;

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
        public string backgroundId = string.Empty;
        /// <summary>
        /// 배경 사진으로 가는 하이퍼링크입니다.
        /// </summary>
        public string backgroundImageUrl = string.Empty;
        /// <summary>
        /// fallbackBackgroundImageUrl
        /// </summary>
        public string? fallbackBackgroundImageUrl = null;
        /// <summary>
        /// 배경 비디오로 가는 하이퍼링크입니다.
        /// </summary>
        public string? backgroundVideoUrl = null;
        /// <summary>
        /// 해당 배경을 획득한 사용자의 수입니다.
        /// </summary>
        public long unlockedUserCount;
        /// <summary>
        /// 배경의 이름입니다.
        /// </summary>
        public string displayName = string.Empty;
        /// <summary>
        /// 배경의 설명입니다.
        /// </summary>
        public string displayDescription = string.Empty;
        /// <summary>
        /// 해당 배경을 얻을 수 있는 조건입니다.
        /// </summary>
        public string conditions = string.Empty;
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
        /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
        /// <returns>실패시 false, 성공시 true</returns>
        public bool SaveBackgroundImage(string filename) => acAPI.SaveFile(this.backgroundImageUrl, filename);
        /// <summary>
        /// 배경 동영상을 저장합니다.
        /// </summary>
        /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
        /// <returns>실패시 false, 성공시 true</returns>
        public bool SaveBackgroundVideo(string filename) => this.backgroundVideoUrl is null ? false : acAPI.SaveFile(this.backgroundVideoUrl , filename);
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
