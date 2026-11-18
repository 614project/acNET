using AcNET.Problem;
using AcNET.Type;

namespace AcNET.Search;

/// <summary>
/// 문제 검색 결과 한 페이지입니다. (페이지 크기 50)
/// </summary>
public class SolvedProblemSearchResult : Jsonable
{
    /// <summary>
    /// 찾은 문제 목록입니다.
    /// </summary>
    public List<SolvedProblem> Problems { get; set; } = [];
    /// <summary>
    /// 찾은 문제 수입니다.
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 현재 페이지입니다.
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// 페이지 크기입니다.
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// 검색 쿼리에 대한 진단(오류, 경고)입니다.
    /// </summary>
    public List<SolvedSearchDiagnostic>? Diagnostics { get; set; }
    /// <summary>
    /// 무작위 정렬에 사용된 seed 입니다. 같은 seed 로 다음 페이지를 요청하면 순서가 유지됩니다.
    /// </summary>
    public string? Seed { get; set; }
    /// <summary>
    /// 목록을 순서대로 출력합니다. 각 항목마다 줄바꿈이 포함됩니다.
    /// </summary>
    public override string ToString() => string.Join('\n', Problems);
}

/// <summary>
/// 검색 쿼리에 대한 진단입니다.
/// </summary>
public class SolvedSearchDiagnostic : Jsonable
{
    /// <summary>
    /// 쿼리에서 문제가 시작되는 위치입니다.
    /// </summary>
    public int Start { get; set; }
    /// <summary>
    /// 쿼리에서 문제가 끝나는 위치입니다.
    /// </summary>
    public int End { get; set; }
    /// <summary>
    /// 심각도입니다. ("error", "warning")
    /// </summary>
    public string Severity { get; set; } = string.Empty;
    /// <summary>
    /// 진단 코드입니다. (예: "unknown-filter", "unknown-tag", "unclosed-quote")
    /// </summary>
    public string Code { get; set; } = string.Empty;
    /// <summary>
    /// 지역화 메시지 key 입니다.
    /// </summary>
    public string MessageKey { get; set; } = string.Empty;
    /// <summary>
    /// 메시지입니다.
    /// </summary>
    public string Message { get; set; } = string.Empty;
    /// <summary>
    /// 메시지에 들어가는 부가 정보입니다.
    /// </summary>
    public Dictionary<string, string>? Data { get; set; }
}

/// <summary>
/// 검색 필터에 넣을 수 있는 추천 값입니다.
/// </summary>
public class SolvedSuggestion : Jsonable
{
    /// <summary>
    /// 값입니다.
    /// </summary>
    public string Value { get; set; } = string.Empty;
    /// <summary>
    /// 부가 설명입니다.
    /// </summary>
    public string? Detail { get; set; }
    /// <summary>
    /// 사용자 추천일 경우 그 사용자의 티어입니다.
    /// </summary>
    public int? Tier { get; set; }
}
