using acNET.Image;
using Newtonsoft.Json;

namespace acNET.User;

/// <summary>
/// 로그인 없이 가져올수 있는 사용자의 정보
/// </summary>
public class RankedUser : Type.BaseBody
{
    /// <summary>
    /// 사용자명입니다.
    /// </summary>
    public string handle;
    /// <summary>
    /// 사용자의 자기소개입니다.
    /// </summary>
    public string bio;
    /// <summary>
    /// 사용자가 지금 사용 중인 뱃지의 아이디입니다.
    /// </summary>
    public string? badgeId;
    /// <summary>
    /// 사용자가 지금 사용 중인 배경의 아이디입니다.
    /// </summary>
    public string backgroundId;
    /// <summary>
    /// 사용자의 프로필 사진으로 가는 하이퍼링크입니다.
    /// </summary>
    public string? profileImageUrl;
    /// <summary>
    /// 사용자가 푼 문제 수입니다.
    /// </summary>
    public long solvedCount;
    /// <summary>
    /// 사용자가 난이도 기여를 한 횟수입니다.
    /// </summary>
    public long voteCount;
    /// <summary>
    /// 사용자의 티어(레벨)값입니다.
    /// </summary>
    public long tier;
    /// <summary>
    /// 사용자의 레이팅입니다.
    /// </summary>
    public long rating;
    /// <summary>
    /// 푼 문제의 난이도 합으로 계산한 사용자의 레이팅입니다.
    /// </summary>
    public long ratingByProblemsSum;
    /// <summary>
    /// 취득한 클래스에 따른 사용자의 레이팅입니다.
    /// </summary>
    public long ratingByClass;
    /// <summary>
    /// 푼 문제 수로 계산한 사용자의 레이팅입니다.
    /// </summary>
    public long ratingBySolvedCount;
    /// <summary>
    /// 문제 난이도에 기여한 횟수로 계산한 사용자의 레이팅입니다.
    /// </summary>
    public long ratingByVoteCount;
    /// <summary>
    /// 사용자가 취득한 Class입니다.
    /// </summary>
    [JsonProperty("class")]
    public long @class;
    /// <summary>
    /// 클래스 치장입니다.
    /// </summary>
    public string classDecoration;
    /// <summary>
    /// 사용자의 라이벌 수입니다.
    /// </summary>
    public long rivalCount;
    /// <summary>
    /// 사용자의 역라이벌 수입니다.
    /// </summary>
    public long reverseRivalCount;
    /// <summary>
    /// 유지한 최대 스트릭의 길이입니다. (일 단위)
    /// </summary>
    public long maxStreak;
    /// <summary>
    /// 사용자가 가지고 있는 코인의 수 × 100입니다. (예: 코인 0.15를 가지고 있을경우 이 값은 15가 됩니다.)
    /// </summary>
    public long coins;
    /// <summary>
    /// 사용자가 가지고 있는 별가루의 수입니다.
    /// </summary>
    public long stardusts;
    /// <summary>
    /// 사용자가 가입한 날짜입니다.
    /// </summary>
    public string joinedAt;
    /// <summary>
    /// 사용자의 정지 종료 날짜입니다.
    /// </summary>
    public string bannedUntil;
    /// <summary>
    /// 사용자의 PRO 종료 날짜입니다.
    /// </summary>
    public string proUntil;
    /// <summary>
    /// 사용자의 순위입니다.
    /// </summary>
    public long rank;
    /// <summary>
    /// 라이벌 여부입니다.
    /// </summary>
    public bool isRival;
    /// <summary>
    /// 역라이벌 여부입니다.
    /// </summary>
    public bool isReverseRival;
    /// <summary>
    /// 현재 아레나 티어입니다.
    /// </summary>
    public long arenaTier;
    /// <summary>
    /// 현재 아레나 레이팅입니다.
    /// </summary>
    public long arenaRating;
    /// <summary>
    /// 역대 받은 아레나 티어 중 최고점일 때의 아레나 티어입니다.
    /// </summary>
    public long arenaMaxTier;
    /// <summary>
    /// 역대 받은 아레나 레이팅 중 최고점일 때의 아레나 레이팅입니다.
    /// </summary>
    public long arenaMaxRating;
    /// <summary>
    /// 참여한 아레나 라운드 수입니다.
    /// </summary>
    public long arenaCompetedRoundCount;
    public bool blocked;
    public bool reverseBlocked;
    /// <summary>
    /// 가입한 날짜를 C# DateTime으로 가져옵니다.
    /// </summary>
    public DateTime? GetJoinedAt => Converter.Time(this.joinedAt);
    /// <summary>
    /// 정지 종료 날짜를 C# DateTime으로 가져옵니다.
    /// </summary>
    public DateTime? GetBannedUntil => Converter.Time(this.bannedUntil);
    /// <summary>
    /// PRO 종료 날짜를 C# DateTime으로 가져옵니다.
    /// </summary>
    public DateTime? GetProUntil => Converter.Time(this.proUntil);
    /// <summary>
    /// 사용자의 티어(레벨)값을 티어 이름으로 가져옵니다.
    /// </summary>
    public string? GetTierName => Converter.LevelName(this.tier);
    /// <summary>
    /// 사용자의 아레나 티어(레벨)값을 아레나 티어 이름으로 가져옵니다.
    /// </summary>
    public string? GetArenaTierName => Converter.ArenaName(this.arenaTier);
    /// <summary>
    /// 유저의 배경 정보를 가져옵니다.
    /// </summary>
    /// <param name="api">acNET.acAPI</param>
    /// <returns>실패시 null</returns>
    public Background? GetBackground(acAPI api) => api.GetBackground(this.backgroundId);
    /// <summary>
    /// 유저의 뱃지 정보를 가져옵니다.
    /// </summary>
    /// <param name="api">acNET.acAPI</param>
    /// <returns>실패시 null</returns>
    public Badge.Badge? GetBadge(acAPI api) => api.GetBadge(this.badgeId);
    /// <summary>
    /// 사용자의 프로필 이미지를 저장합니다.
    /// </summary>
    /// <param name="filename">파일명 (확장자 필요없음)</param>
    /// <returns>실패시 null, 성공시 저장된 파일명 (확장자 포함)</returns>
    public string? SaveProfileImage(string filename)
    {
        if (this.profileImageUrl is null) return null;
        if(!acAPI.SaveImage(this.profileImageUrl, filename, out var fn)) return null;
        return fn;
    }
}
