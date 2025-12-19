using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Image;

/// <summary>
/// 배경 사진의 정보
/// </summary>
public class SolvedBackground : Jsonable
{
    /// <summary>
    /// 배경의 ID입니다.
    /// </summary>
    [JsonProperty("backgroundId")]
    public string BackgroundId { get; set; } = string.Empty;
    /// <summary>
    /// 배경 사진으로 가는 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("backgroundImageUrl")]
    public string BackgroundImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// fallbackBackgroundImageUrl
    /// </summary>
    [JsonProperty("fallbackBackgroundImageUrl")]
    public string? FallbackBackgroundImageUrl { get; set; } = null;
    /// <summary>
    /// 배경 비디오로 가는 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("backgroundVideoUrl")]
    public string? BackgroundVideoUrl { get; set; } = null;
    /// <summary>
    /// 해당 배경을 획득한 사용자의 수입니다.
    /// </summary>
    [JsonProperty("unlockedUserCount")]
    public long UnlockedUserCount { get; set; }
    /// <summary>
    /// 배경의 이름입니다.
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;
    /// <summary>
    /// 배경의 설명입니다.
    /// </summary>
    [JsonProperty("displayDescription")]
    public string DisplayDescription { get; set; } = string.Empty;
    /// <summary>
    /// 해당 배경을 얻을 수 있는 조건입니다.
    /// </summary>
    [JsonProperty("conditions")]
    public string Conditions { get; set; } = string.Empty;
    /// <summary>
    /// 해당 배경을 얻을 수 있는 조건이 숨겨져 있는지 여부입니다.
    /// </summary>
    [JsonProperty("hiddenConditions")]
    public bool HiddenConditions { get; set; }
    /// <summary>
    /// 해당 배경이 일러스트인지 여부입니다.
    /// </summary>
    [JsonProperty("isIllust")]
    public bool IsIllust { get; set; }
    /// <summary>
    /// 해당 배경을 만든 사람들의 정보입니다.
    /// </summary>
    [JsonProperty("authors")]
    public List<SolvedAuthorsInfo> Authors { get; set; } = null!;
    /// <summary>
    /// 배경 이미지를 저장합니다.
    /// </summary>
    /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
    /// <returns>성공시 null, 실패시 Exception</returns>
    public async Task<Exception?> SaveBackgroundImage(string filename) => await SolvedOnceAPI.SaveFile(this.BackgroundImageUrl , filename);
    /// <summary>
    /// 배경 동영상을 저장합니다.
    /// </summary>
    /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
    /// <returns>성공시 null, 실패시 Exception</returns>
    public async Task<Exception?> SaveBackgroundVideo(string filename) => await SolvedOnceAPI.SaveFile(this.BackgroundVideoUrl ?? string.Empty , filename);
}

/// <summary>
/// 제작자의 정보입니다.
/// </summary>
public class SolvedAuthorsInfo : Jsonable
{
    /// <summary>
    /// 작가의 ID입니다.
    /// </summary>
    [JsonProperty("authorId")]
    public string AuthorId { get; set; } = string.Empty;
    /// <summary>
    /// 작가의 역할입니다.
    /// </summary>
    [JsonProperty("role")]
    public string Role { get; set; } = string.Empty;
    /// <summary>
    /// 작가의 홈페이지로 가는 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("authorUrl")]
    public string? AuthorUrl { get; set; } = null;
    /// <summary>
    /// 작가의 사용자 ID입니다.
    /// </summary>
    [JsonProperty("handle")]
    public string Handle { get; set; } = string.Empty;
    /// <summary>
    /// 작가의 X(트위터) ID입니다.
    /// </summary>
    [JsonProperty("twitter")]
    public string? Twitter { get; set; } = null;
    /// <summary>
    /// 작가의 인스타그램 ID입니다.
    /// </summary>
    [JsonProperty("instagram")]
    public string? Instagram { get; set; } = null;
    /// <summary>
    /// 작가의 이름입니다.
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 작가의 sovled.ac 아이디로 유저 정보를 가져옵니다.
    /// </summary>
    public SolvedResult<User.SolvedSocialUser> GetAuthor(SolvedAPI api) => api.GetUser(this.AuthorId);
    /// <summary>
    /// 일회용 SolvedAPI를 생성 후, 작가의 sovled.ac 아이디로 유저 정보를 가져옵니다.
    /// </summary>
    public SolvedResult<User.SolvedSocialUser> GetAuthor() => SolvedOnceAPI.Once(api => api.GetUser(this.AuthorId));
    /// <summary>
    /// 작가의 sovled.ac 아이디로 유저 정보를 비동기로 가져옵니다.
    /// </summary>
    /// <param name="api">SolvedAPI</param>
    /// <returns>실패시 null</returns>
    public async Task<SolvedResult<User.SolvedSocialUser>> GetAuthorAsync(SolvedAPI api) => await api.GetUserAsync(this.AuthorId);
}