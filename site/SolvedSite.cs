using AcNET.Type;

namespace AcNET.Site;

/// <summary>
/// 사이트 전체 통계입니다. 최대 10분 전 값입니다. (<see cref="SolvedAPI.GetSiteStatsAsync"/>)
/// </summary>
public class SolvedStats : Jsonable
{
    /// <summary>난이도가 매겨진 문제 수입니다.</summary>
    public int ProblemsWithLevel { get; set; }
    /// <summary>사용자 수입니다.</summary>
    public int Users { get; set; }
    /// <summary>기여자 수입니다.</summary>
    public int Contributors { get; set; }
    /// <summary>기여 수입니다.</summary>
    public int Contributions { get; set; }
    /// <summary>티어별 사용자 수입니다. (티어로 색인)</summary>
    public List<int> UsersByTier { get; set; } = [];
}

/// <summary>
/// solved.ac 가 읽는 저지입니다. (<see cref="SolvedAPI.GetJudgesAsync"/>)
/// </summary>
public class SolvedJudge : Jsonable
{
    /// <summary>저지 ID 입니다. (예: "boj")</summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>저지 이름입니다.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>저지의 짧은 이름입니다.</summary>
    public string? ShortName { get; set; }
    /// <summary>로고 주소입니다.</summary>
    public string? LogoUrl { get; set; }
    /// <summary>홈페이지 주소입니다.</summary>
    public string HomepageUrl { get; set; } = string.Empty;
    /// <summary>문제 번호 앞에 붙는 접두사입니다.</summary>
    public string Prefix { get; set; } = string.Empty;
    /// <summary>문제 번호를 0으로 채우는 자릿수입니다.</summary>
    public int IdDigits { get; set; }
    /// <summary>검색에서 쓰는 별칭입니다.</summary>
    public List<string> Aliases { get; set; } = [];
}

/// <summary>
/// 서비스 상태입니다. (<see cref="SolvedAPI.GetHealthAsync"/>)
/// </summary>
public class SolvedHealth : Jsonable
{
    /// <summary>상태입니다. ("ok", "degraded", "down")</summary>
    public string Status { get; set; } = string.Empty;
    /// <summary>확인한 시각입니다.</summary>
    public string Timestamp { get; set; } = string.Empty;
}
