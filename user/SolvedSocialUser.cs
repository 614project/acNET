using AcNET.Image;
using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.User;

/// <summary>
/// 로그인 없이 가져올수 있는 사용자의 정보
/// </summary>
public class SolvedSocialUser : Jsonable
{
    /// <summary>
    /// 사용자명입니다.
    /// </summary>
    [JsonProperty("handle")]
    public string Handle { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 자기소개입니다.
    /// </summary>
    [JsonProperty("bio")]
    public string Bio { get; set; } = string.Empty;
    /// <summary>
    /// 사용자가 지금 사용 중인 뱃지의 아이디입니다.
    /// </summary>
    [JsonProperty("badgeId")]
    public string? BadgeId { get; set; }
    /// <summary>
    /// 사용자가 지금 사용 중인 배경의 아이디입니다.
    /// </summary>
    [JsonProperty("backgroundId")]
    public string BackgroundId { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 프로필 사진으로 가는 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("profileImageUrl")]
    public string? ProfileImageUrl { get; set; }
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    [JsonProperty("solvedCount")]
    public int SolvedCount { get; set; }
    /// <summary>
    /// 사용자가 난이도 기여를 한 횟수입니다.
    /// </summary>
    [JsonProperty("voteCount")]
    public int VoteCount { get; set; }
    /// <summary>
    /// 사용자의 티어(레벨)값입니다.
    /// </summary>
    [JsonProperty("tier")]
    public int Tier { get; set; }
    /// <summary>
    /// 사용자의 레이팅입니다.
    /// </summary>
    [JsonProperty("rating")]
    public int Rating { get; set; }
    /// <summary>
    /// 푼 문제의 난이도 합으로 계산한 사용자의 레이팅입니다.
    /// </summary>
    [JsonProperty("ratingByProblemsSum")]
    public int RatingByProblemsSum { get; set; }
    /// <summary>
    /// 취득한 클래스에 따른 사용자의 레이팅입니다.
    /// </summary>
    [JsonProperty("ratingByClass")]
    public int RatingByClass { get; set; }
    /// <summary>
    /// 푼 문제 수로 계산한 사용자의 레이팅입니다.
    /// </summary>
    [JsonProperty("ratingBySolvedCount")]
    public int RatingBySolvedCount { get; set; }
    /// <summary>
    /// 문제 난이도에 기여한 횟수로 계산한 사용자의 레이팅입니다.
    /// </summary>
    [JsonProperty("ratingByVoteCount")]
    public int RatingByVoteCount { get; set; }
    /// <summary>
    /// 사용자가 취득한 Class입니다.
    /// </summary>
    [JsonProperty("class")]
    public int Class { get; set; }
    /// <summary>
    /// 클래스 치장입니다.
    /// </summary>
    [JsonProperty("classDecoration")]
    public string ClassDecoration { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 라이벌 수입니다.
    /// </summary>
    [JsonProperty("rivalCount")]
    public int RivalCount { get; set; }
    /// <summary>
    /// 사용자의 역라이벌 수입니다.
    /// </summary>
    [JsonProperty("reverseRivalCount")]
    public int ReverseRivalCount { get; set; }
    /// <summary>
    /// 유지한 최대 스트릭의 길이입니다. (일 단위)
    /// </summary>
    [JsonProperty("maxStreak")]
    public int MaxStreak { get; set; }
    /// <summary>
    /// 사용자가 가지고 있는 코인의 수 × 100입니다. (예: 코인 0.15를 가지고 있을경우 이 값은 15가 됩니다.)
    /// </summary>
    [JsonProperty("coins")]
    public int Coins { get; set; }
    /// <summary>
    /// 사용자가 가지고 있는 별가루의 수입니다.
    /// </summary>
    [JsonProperty("stardusts")]
    public int Stardusts { get; set; }
    /// <summary>
    /// 사용자가 가입한 날짜입니다.
    /// </summary>
    [JsonProperty("joinedAt")]
    public string JoinedAt { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 정지 종료 날짜입니다.
    /// </summary>
    [JsonProperty("bannedUntil")]
    public string BannedUntil { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 PRO 종료 날짜입니다.
    /// </summary>
    [JsonProperty("proUntil")]
    public string ProUntil { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 순위입니다.
    /// </summary>
    [JsonProperty("rank")]
    public int Rank { get; set; }
    /// <summary>
    /// 라이벌 여부입니다.
    /// </summary>
    [JsonProperty("isRival")]
    public bool IsRival { get; set; }
    /// <summary>
    /// 역라이벌 여부입니다.
    /// </summary>
    [JsonProperty("isReverseRival")]
    public bool IsReverseRival { get; set; }
    /// <summary>
    /// 현재 아레나 티어입니다.
    /// </summary>
    [JsonProperty("arenaTier")]
    public int ArenaTier { get; set; }
    /// <summary>
    /// 현재 아레나 레이팅입니다.
    /// </summary>
    [JsonProperty("arenaRating")]
    public int ArenaRating { get; set; }
    /// <summary>
    /// 역대 받은 아레나 티어 중 최고점일 때의 아레나 티어입니다.
    /// </summary>
    [JsonProperty("arenaMaxTier")]
    public int ArenaMaxTier { get; set; }
    /// <summary>
    /// 역대 받은 아레나 레이팅 중 최고점일 때의 아레나 레이팅입니다.
    /// </summary>
    [JsonProperty("arenaMaxRating")]
    public int ArenaMaxRating { get; set; }
    /// <summary>
    /// 참여한 아레나 라운드 수입니다.
    /// </summary>
    [JsonProperty("arenaCompetedRoundCount")]
    public int ArenaCompetedRoundCount { get; set; }
    /// <summary>
    /// 로그인한 사용자가 해당 사용자를 차단했는지 여부입니다.
    /// </summary>
    [JsonProperty("blocked")]
    public bool Blocked { get; set; }
    /// <summary>
    /// 로그인한 사용자가 해당 사용자에게 차단당했는지 여부입니다.
    /// </summary>
    [JsonProperty("reverseBlocked")]
    public bool ReverseBlocked { get; set; }
    /// <summary>
    /// 정확한 가격의 보유 코인을 가져옵니다.
    /// </summary>
    public double GetExactCoin => Coins * 0.01;
    /// <summary>
    /// 가입한 날짜를 C# DateTime으로 가져옵니다.
    /// </summary>
    public DateTime? GetJoinedAt => SolvedConverter.ToTime(this.JoinedAt);
    /// <summary>
    /// 정지 종료 날짜를 C# DateTime으로 가져옵니다.
    /// </summary>
    public DateTime? GetBannedUntil => SolvedConverter.ToTime(this.BannedUntil);
    /// <summary>
    /// PRO 종료 날짜를 C# DateTime으로 가져옵니다.
    /// </summary>
    public DateTime? GetProUntil => SolvedConverter.ToTime(this.ProUntil);
    /// <summary>
    /// 사용자의 티어(레벨)값을 티어 이름으로 가져옵니다.
    /// </summary>
    public string? GetTierName => SolvedConverter.LevelName(this.Tier);
    /// <summary>
    /// 문제 레벨에 맞는 티어 색깔을 가져옵니다. (7자리 Hex 코드입니다.)
    /// </summary>
    public string? GetTierColor => SolvedConverter.LevelColor(this.Tier);
    /// <summary>
    /// 사용자의 아레나 티어(레벨)값을 아레나 티어 이름으로 가져옵니다.
    /// </summary>
    public string? GetArenaTierName => SolvedConverter.ArenaName(this.ArenaTier);
    /// <summary>
    /// 유저의 배경 정보를 가져옵니다.
    /// </summary>
    /// <param name="api">acNET.acAPI</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<SolvedBackground> GetBackground(SolvedAPI api) => api.GetBackground(this.BackgroundId);
    /// <summary>
    /// 유저의 뱃지 정보를 가져옵니다.
    /// </summary>
    /// <param name="api">acNET.acAPI</param>
    /// <returns>실패시 null</returns>
    public SolvedResult<Badge.SolvedBadge> GetBadge(SolvedAPI api) => this.BadgeId is null ? new(null , null) : api.GetBadge(this.BadgeId);
    /// <summary>
    /// 사용자의 프로필 이미지를 저장합니다.
    /// </summary>
    /// <param name="filename">저장될 파일 경로 (파일명 포함)</param>
    /// <returns>성공시 null, 실패시 Exception</returns>
    public async Task<Exception?> SaveBadgeImage(string filename) => await SolvedOnceAPI.SaveFile(this.ProfileImageUrl ?? string.Empty , filename);
}