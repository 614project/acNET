using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Badge;

/// <summary>
/// 사용자가 사용할수 있는 뱃지입니다.
/// </summary>
public class SolvedBadge : Jsonable
{
    /// <summary>
    /// 뱃지의 ID입니다.
    /// </summary>
    [JsonProperty("badgeId")]
    public string BadgeId { get; set; } = string.Empty;
    /// <summary>
    /// 뱃지 사진으로 가는 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("badgeImageUrl")]
    public string BadgeImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// 뱃지의 이름입니다.
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;
    /// <summary>
    /// 뱃지의 설명입니다.
    /// </summary>
    [JsonProperty("displayDescription")]
    public string DisplayDescription { get; set; } = string.Empty;
    /// <summary>
    /// 뱃지 티어입니다.
    /// </summary>
    [JsonProperty("badgeTier")]
    public string BadgeTier { get; set; } = string.Empty;
    /// <summary>
    /// 뱃지 종류입니다.
    /// </summary>
    [JsonProperty("badgeCategory")]
    public string BadgeCategory { get; set; } = string.Empty;

    /// <summary>
    /// 뱃지 사진을 저장합니다.
    /// </summary>
    /// <param name="filename">파일명</param>
    /// <returns>성공시 null, 실패시 Exception</returns>
    public async Task<Exception?> SaveBadgeImage(string filename) => await SolvedOnceAPI.SaveFile(this.BadgeImageUrl , filename);
}