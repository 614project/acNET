namespace AcNET.OAuth;

/// <summary>
/// solved.ac OAuth 에서 요청할 수 있는 권한(scope) 입니다.
/// </summary>
/// <remarks>
/// 각 API 메서드의 문서에 필요한 scope 가 적혀 있습니다. <see cref="Profile"/> 은 항상 포함됩니다.
/// </remarks>
public static class SolvedScope
{
    /// <summary>
    /// 사용자 식별 (항상 포함됨). <c>GET /me</c>, 동의 대기 정책 조회
    /// </summary>
    public const string Profile = "profile";
    /// <summary>
    /// 본인의 수정 가능한 프로필 항목 읽기
    /// </summary>
    public const string ProfilePersonal = "profile.personal";
    /// <summary>
    /// 사용자 설정 읽기
    /// </summary>
    public const string Settings = "settings";
    /// <summary>
    /// 사용자 설정 변경
    /// </summary>
    public const string SettingsWrite = "settings.write";
    /// <summary>
    /// 뱃지, 배경, 아이템, 이모티콘 등 보유 목록 읽기
    /// </summary>
    public const string Collection = "collection";
    /// <summary>
    /// 경고 및 정지 상태 읽기
    /// </summary>
    public const string Standing = "standing";
    /// <summary>
    /// 문제 풀이 상태 읽기 (문제의 userStatus, 태그별 진행도)
    /// </summary>
    public const string Solves = "solves";
    /// <summary>
    /// 별가루 잔액과 내역 읽기
    /// </summary>
    public const string Wallet = "wallet";
    /// <summary>
    /// 라이벌, 역라이벌 랭킹 읽기
    /// </summary>
    public const string Rivals = "rivals";
    /// <summary>
    /// 난이도 기여 읽기
    /// </summary>
    public const string Votes = "votes";
    /// <summary>
    /// 북마크 폴더 읽기 (비공개 폴더 포함)
    /// </summary>
    public const string Bookmarks = "bookmarks";
    /// <summary>
    /// 북마크 폴더 만들기, 수정, 삭제, 즐겨찾기
    /// </summary>
    public const string BookmarksWrite = "bookmarks.write";
    /// <summary>
    /// refresh token 발급 (90일, 1회용)
    /// </summary>
    public const string OfflineAccess = "offline.access";
}
