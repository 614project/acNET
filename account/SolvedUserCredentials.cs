using AcNET.Emoticon;
using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Account;

/// <summary>
/// 크레데션 관련 본인 정보
/// </summary>
public class SolvedUserCredentials : Jsonable
{
    /// <summary>
    /// solved.ac 사용자 정보입니다.
    /// </summary>
    [JsonProperty("user")]
    public SolvedDetailedUser User { get; set; } = null!;
    /// <summary>
    /// 사용자 동의 여부입니다.
    /// </summary>
    [JsonProperty("aggredOn")]
    public SolvedAggred AggredOn { get; set; } = null!;
    /// <summary>
    /// 보유할 수 있는 모든 이모티콘에 대해 이모티콘 정보에 덧붙여 보유 여부를 함께 담은 목록입니다.
    /// </summary>
    [JsonProperty("emoticons")]
    public List<SolvedEmoticon> Emoticons { get; set; } = null!;
    /// <summary>
    /// 북마크
    /// </summary>
    [JsonProperty("bookmarks")]
    public object? Bookmarks { get; set; } = null;
    /// <summary>
    /// 받은 알림 수입니다.
    /// </summary>
    [JsonProperty("notificationCount")]
    public int NotificationCount { get; set; }
    /// <summary>
    /// 마지막으로 솔브드 상태가 변한 시각입니다.
    /// </summary>
    [JsonProperty("lastSolvedStateChangedAt")]
    public DateTime LastSolvedStateChangedAt { get; set; }

    /// <summary>
    /// 동의 목록
    /// </summary>
    public class SolvedAggred : Jsonable
    {
        /// <summary>
        /// 동의한 약관 버전입니다.
        /// </summary>
        [JsonProperty("tos")]
        public string Tos { get; set; } = string.Empty;
        /// <summary>
        /// 동의한 개인정보 처리방침 버전입니다.
        /// </summary>
        [JsonProperty("privacy")]
        public string Privacy { get; set; } = string.Empty;
    }
}

/// <summary>
/// 로그인을 통해 얻을수 있는 자세한 유저 정보
/// </summary>
public class SolvedDetailedUser : User.SolvedSocialUser
{
    /// <summary>
    /// 계정의 이메일 주소입니다.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// 사용자의 solved.ac 설정 정보입니다.
    /// </summary>
    [JsonProperty("settings")]
    public SolvedSettings Settings { get; set; } = null!;

    /// <summary>
    /// 사용자의 solved.ac 설정 정보입니다.
    /// </summary>
    public class SolvedSettings
    {
        /// <summary>
        /// 사이트 디자인 테마입니다.
        /// </summary>
        [JsonProperty("screenTheme")]
        public string ScreenTheme { get; set; } = string.Empty;
        /// <summary>
        /// 태그 이름을 나타낼 때 사용할 언어입니다.
        /// </summary>
        [JsonProperty("tagDisplayLanguage")]
        public string TagDisplayLanguage { get; set; } = string.Empty;
        /// <summary>
        /// 해결한 문제의 난이도 아이콘입니다.
        /// </summary>
        [JsonProperty("iconSchemeSolved")]
        public string IconSchemeSolved { get; set; } = string.Empty;
        /// <summary>
        /// 해결하지 못한 문제의 난이도 아이콘입니다.
        /// </summary>
        [JsonProperty("iconSchemeNotSolved")]
        public string IconSchemeNotSolved { get; set; } = string.Empty;
        /// <summary>
        /// 문제 목록의 기본 정렬 순서입니다.
        /// </summary>
        [JsonProperty("problemSortBy")]
        public string ProblemSortBy { get; set; } = string.Empty;
        /// <summary>
        /// 트윗에 핸들을 포함하는지 여부입니다.
        /// </summary>
        [JsonProperty("twitterPostHandle")]
        public string TwitterPostHandle { get; set; } = string.Empty;
        /// <summary>
        /// CLASS가 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        [JsonProperty("twitterPostOnClassIncrease")]
        public string TwitterPostOnClassIncrease { get; set; } = string.Empty;
        /// <summary>
        /// 문제를 처음 해결했을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        [JsonProperty("twitterPostOnProblemSolve")]
        public string TwitterPostOnProblemSolve { get; set; } = string.Empty;
        /// <summary>
        /// AC 레이팅이 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        [JsonProperty("twitterPostOnRatingIncrease")]
        public string TwitterPostOnRatingIncrease { get; set; } = string.Empty;
        /// <summary>
        /// 티어가 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        [JsonProperty("twitterPostOnTierIncrease")]
        public string TwitterPostOnTierIncrease { get; set; } = string.Empty;

        /// <summary>
        /// 트윗에 핸들을 포함하는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostHandleBoolean => this.TwitterPostHandle == "true";
        /// <summary>
        /// CLASS가 올랐을 때 트윗을 보내는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostOnClassIncreaseBoolean => this.TwitterPostOnClassIncrease == "true";
        /// <summary>
        /// 문제를 처음 해결했을 때 트윗을 보내는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostOnProblemSolveBoolean => this.TwitterPostOnProblemSolve == "true";
        /// <summary>
        /// AC 레이팅이 올랐을 때 트윗을 보내는지 여부입니다. (bool)
        /// </summary>
        public bool GetTwitterPostOnRatingIncreaseBoolean => this.TwitterPostOnRatingIncrease == "true";
        /// <summary>
        /// 티어가 올랐을 때 트윗을 보내는지 여부입니다.
        /// </summary>
        public bool GetTwitterPostOnTierIncreaseBoolean => this.TwitterPostOnTierIncrease == "true";
    }
}

/// <summary>
/// 사용자가 푼 문제에 대한 정보
/// </summary>
public class SolvedSolvedInformation
{
    /// <summary>
    /// 문제 ID입니다.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }
    /// <summary>
    /// 현재 문제 풀이 상태입니다. 알려진 값은 다음이 있습니다.
    /// </summary>
    [JsonProperty("tried")]
    public string Tried { get; set; } = string.Empty;
}

///// <summary>
///// 현재 로그인한 계정 정보
///// </summary>
//public class SolvedUserAccount : Jsonable
//{
//    /// <summary>
//    /// 계정의 사용자 정보입니다.
//    /// </summary>
//    [JsonProperty("user")]
//    public SolvedDetailedUser User { get; set; } = null!;
//    /// <summary>
//    /// 해당 계정의 사용자가 푼 문제 정보입니다.
//    /// </summary>
//    [JsonProperty("solved")]
//    public List<SolvedSolvedInformation> Solved { get; set; } = null!;
//}