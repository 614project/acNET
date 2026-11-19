using System.Text.Json.Serialization;

namespace AcNET;

/// <summary>
/// 정렬 방향입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedSortDirection>))]
public enum SolvedSortDirection
{
    /// <summary>오름차순</summary>
    [ApiValue("asc")] Ascending,
    /// <summary>내림차순</summary>
    [ApiValue("desc")] Descending,
}

/// <summary>
/// 문제에 대한 사용자의 풀이 상태입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedUserStatus>))]
public enum SolvedUserStatus
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>맞았습니다</summary>
    [ApiValue("solved")] Solved,
    /// <summary>부분 점수를 받았습니다</summary>
    [ApiValue("partial")] Partial,
    /// <summary>시도했지만 맞지 못했습니다</summary>
    [ApiValue("tried")] Tried,
}

/// <summary>
/// 업적 아이템의 종류입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedItemKind>))]
public enum SolvedItemKind
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>뱃지</summary>
    [ApiValue("badge")] Badge,
    /// <summary>배경</summary>
    [ApiValue("background")] Background,
}

/// <summary>
/// 소속(학교, 회사, 커뮤니티 등)의 종류입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedAffiliationType>))]
public enum SolvedAffiliationType
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>분류되지 않음 (API 값 "undefined")</summary>
    [ApiValue("undefined")] Undefined,
    /// <summary>대학교</summary>
    [ApiValue("university")] University,
    /// <summary>고등학교</summary>
    [ApiValue("high_school")] HighSchool,
    /// <summary>중학교</summary>
    [ApiValue("middle_school")] MiddleSchool,
    /// <summary>초등학교</summary>
    [ApiValue("elementary_school")] ElementarySchool,
    /// <summary>회사</summary>
    [ApiValue("company")] Company,
    /// <summary>커뮤니티</summary>
    [ApiValue("community")] Community,
}

/// <summary>
/// 정지의 종류입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedBanType>))]
public enum SolvedBanType
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>레이팅 정지</summary>
    [ApiValue("rating")] Rating,
    /// <summary>난이도 기여 정지</summary>
    [ApiValue("vote")] Vote,
}

/// <summary>
/// 북마크 폴더의 색입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedFolderColor>))]
public enum SolvedFolderColor
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>빨강</summary>
    [ApiValue("red")] Red,
    /// <summary>보라</summary>
    [ApiValue("violet")] Violet,
    /// <summary>파랑</summary>
    [ApiValue("blue")] Blue,
    /// <summary>청록</summary>
    [ApiValue("teal")] Teal,
    /// <summary>연두</summary>
    [ApiValue("lime")] Lime,
    /// <summary>노랑</summary>
    [ApiValue("yellow")] Yellow,
    /// <summary>주황</summary>
    [ApiValue("orange")] Orange,
}

/// <summary>
/// 북마크 폴더의 공개 범위입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedFolderVisibility>))]
public enum SolvedFolderVisibility
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>비공개</summary>
    [ApiValue("private")] Private,
    /// <summary>링크가 있는 사람만</summary>
    [ApiValue("unlisted")] Unlisted,
    /// <summary>공개</summary>
    [ApiValue("public")] Public,
}

/// <summary>
/// 활동 피드 이벤트의 종류입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedActivityType>))]
public enum SolvedActivityType
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>문제를 풀었습니다</summary>
    [ApiValue("solved")] Solved,
    /// <summary>푼 문제가 풀지 않은 것으로 바뀌었습니다</summary>
    [ApiValue("unsolved")] Unsolved,
    /// <summary>난이도에 기여했습니다</summary>
    [ApiValue("vote")] Vote,
    /// <summary>레이팅이 바뀌었습니다</summary>
    [ApiValue("rating")] Rating,
    /// <summary>티어가 바뀌었습니다</summary>
    [ApiValue("tier")] Tier,
    /// <summary>푼 문제 수 기념</summary>
    [ApiValue("solvedMilestone")] SolvedMilestone,
    /// <summary>기여 수 기념</summary>
    [ApiValue("voteMilestone")] VoteMilestone,
}

/// <summary>
/// 문제 목록(검색) 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedProblemSort>))]
public enum SolvedProblemSort
{
    /// <summary>문제 번호</summary>
    [ApiValue("id")] Id,
    /// <summary>난이도</summary>
    [ApiValue("level")] Level,
    /// <summary>맞은 사람 수</summary>
    [ApiValue("solved")] Solved,
    /// <summary>평균 시도 횟수</summary>
    [ApiValue("average_try")] AverageTry,
    /// <summary>무작위 (seed 로 순서를 고정할 수 있습니다)</summary>
    [ApiValue("random")] Random,
}

/// <summary>
/// 사용자 랭킹 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedUserRankingSort>))]
public enum SolvedUserRankingSort
{
    /// <summary>레이팅</summary>
    [ApiValue("rating")] Rating,
    /// <summary>푼 문제 수</summary>
    [ApiValue("solved")] Solved,
    /// <summary>기여</summary>
    [ApiValue("contribution")] Contribution,
}

/// <summary>
/// 보유 뱃지/배경 목록 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedCollectionSort>))]
public enum SolvedCollectionSort
{
    /// <summary>최근 획득</summary>
    [ApiValue("recent")] Recent,
    /// <summary>희귀도</summary>
    [ApiValue("rarity")] Rarity,
    /// <summary>이름</summary>
    [ApiValue("name")] Name,
}

/// <summary>
/// 뱃지/배경 보유자 목록 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedHolderSort>))]
public enum SolvedHolderSort
{
    /// <summary>최근 획득</summary>
    [ApiValue("recent")] Recent,
    /// <summary>먼저 획득</summary>
    [ApiValue("first")] First,
}

/// <summary>
/// 문제의 난이도 기여 목록 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedVoteSort>))]
public enum SolvedVoteSort
{
    /// <summary>최근</summary>
    [ApiValue("recent")] Recent,
    /// <summary>난이도</summary>
    [ApiValue("level")] Level,
    /// <summary>가중치</summary>
    [ApiValue("weight")] Weight,
}

/// <summary>
/// 사용자의 기여 목록 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedContributionSort>))]
public enum SolvedContributionSort
{
    /// <summary>최근</summary>
    [ApiValue("recent")] Recent,
    /// <summary>도움이 돼요 순</summary>
    [ApiValue("helpful")] Helpful,
}

/// <summary>
/// 사용자의 기여 목록 필터입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedContributionFilter>))]
public enum SolvedContributionFilter
{
    /// <summary>평판 점수를 받은 기여만</summary>
    [ApiValue("scoring")] Scoring,
    /// <summary>도움이 돼요를 받은 기여만</summary>
    [ApiValue("helpful")] Helpful,
    /// <summary>보증받은 기여만</summary>
    [ApiValue("vouched")] Vouched,
}

/// <summary>
/// 기록(레이팅, 평판) 조회 기간입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedHistoryRange>))]
public enum SolvedHistoryRange
{
    /// <summary>최근 90일</summary>
    [ApiValue("90d")] Days90,
    /// <summary>최근 1년</summary>
    [ApiValue("1y")] Year1,
    /// <summary>전체</summary>
    [ApiValue("all")] All,
}

/// <summary>
/// 공개 북마크 폴더 목록 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedBookmarkFolderSort>))]
public enum SolvedBookmarkFolderSort
{
    /// <summary>최근 수정</summary>
    [ApiValue("updated")] Updated,
    /// <summary>문제 수</summary>
    [ApiValue("problems")] Problems,
}

/// <summary>
/// 북마크 폴더 안의 문제 정렬 기준입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedBookmarkProblemSort>))]
public enum SolvedBookmarkProblemSort
{
    /// <summary>폴더에 정한 순서</summary>
    [ApiValue("order")] Order,
    /// <summary>난이도</summary>
    [ApiValue("level")] Level,
    /// <summary>맞은 사람 수</summary>
    [ApiValue("solved")] Solved,
    /// <summary>평균 시도 횟수</summary>
    [ApiValue("average_try")] AverageTry,
    /// <summary>문제 번호</summary>
    [ApiValue("id")] Id,
}

/// <summary>
/// solved.ac 의 정책 문서입니다.
/// </summary>
[JsonConverter(typeof(SolvedEnumConverter<SolvedPolicyKind>))]
public enum SolvedPolicyKind
{
    /// <summary>acNET 이 모르는 값</summary>
    Unknown = 0,
    /// <summary>이용약관</summary>
    [ApiValue("tos")] Tos,
    /// <summary>정기 결제 약관</summary>
    [ApiValue("tos_recurring")] TosRecurring,
    /// <summary>개인정보 처리방침</summary>
    [ApiValue("privacy")] Privacy,
    /// <summary>크롬 확장 프로그램 개인정보 처리방침</summary>
    [ApiValue("chrome_privacy")] ChromePrivacy,
    /// <summary>가이드라인</summary>
    [ApiValue("guideline")] Guideline,
    /// <summary>운영 정책</summary>
    [ApiValue("rules")] Rules,
    /// <summary>개발자 약관</summary>
    [ApiValue("developer_terms")] DeveloperTerms,
    /// <summary>파트너 약관</summary>
    [ApiValue("partner_terms")] PartnerTerms,
}
