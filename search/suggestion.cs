using acNET.Type;
using acNET.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acNET.Search;

/// <summary>
/// 자동 완성 및 상위 검색 결과
/// </summary>
public class Suggestion : Jsonable
{
    /// <summary>
    /// 고급 검색 관련 자동 완성입니다.
    /// </summary>
    public AutoComplete[] autocomplete { get; set; }
    /// <summary>
    /// 요악한 문제 정보입니다.
    /// </summary>
    public BriefProblem[] problems { get; set; }
    /// <summary>
    /// 검색 결과로 나올 총 문제 수입니다
    /// </summary>
    public int problemCount { get; set; }
    /// <summary>
    /// 요약한 태그 정보 목록입니다.
    /// </summary>
    public BriefProblemTag[] tags { get; set; }
    /// <summary>
    /// 검색 결과로 나올 총 태그 수입니다.
    /// </summary>
    public int tagCount { get; set; }
    /// <summary>
    /// 사용자 정보 목록입니다.
    /// </summary>
    public RankedUser[] users { get; set; }
    /// <summary>
    /// 검색 결과로 나올 총 사용자 수입니다.
    /// </summary>
    public int userCount { get; set; }
}

/// <summary>
/// 고급 검색 관련 자동 완성입니다.
/// </summary>
public class AutoComplete : Jsonable
{
    /// <summary>
    /// 자동 완성 제목입니다. 만약 href이 없을 경우 누르면 해당 값으로 자동 완성합니다.
    /// </summary>
    public string caption { get; set; }
    /// <summary>
    /// 자동 완성 요소의 설명입니다.
    /// </summary>
    public string description { get; set; }
}

/// <summary>
/// 요악한 문제 정보입니다.
/// </summary>
public class BriefProblem : Jsonable
{
    /// <summary>
    /// 문제 ID입니다.
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 문제 제목입니다.
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 문제 난이도입니다.
    /// </summary>
    public int level { get; set; }
    /// <summary>
    /// 푼 사람 수입니다.
    /// </summary>
    public int solved { get; set; }
    /// <summary>
    /// 자동 완성 제목입니다. 만약 href이 없을 경우 누르면 해당 값으로 자동 완성합니다.
    /// </summary>
    public string caption { get; set; }
    /// <summary>
    /// 자동 완성 요소의 설명입니다.
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 문제 주소입니다.
    /// </summary>
    public string? href { get; set; }
    /// <summary>
    /// 문제 레벨에 맞는 티어 이름을 가져옵니다.
    /// </summary>
    public string? GetTierName => Converter.LevelName(level);
    /// <summary>
    /// 문제 레벨에 맞는 티어 색깔을 가져옵니다. (7자리 Hex 코드입니다.)
    /// </summary>
    public string GetTierColor => Converter.LevelColor(level);
}

public class BriefProblemTag : Jsonable
{
    /// <summary>
    /// solved.ac에서 쓰는 태그 ID입니다.
    /// </summary>
    public string key { get; set; }
    /// <summary>
    /// 태그 이름입니다. 사용자 언어에 따라 번역되지 않습니다.
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 태그가 붙은 문제 수입니다.
    /// </summary>
    public string problemCount { get; set; }
    /// <summary>
    /// 자동 완성 제목입니다. 만약 href이 없을 경우 누르면 해당 값으로 자동 완성합니다.
    /// </summary>
    public string caption { get; set; }
    /// <summary>
    /// 자동 완성 요소의 설명입니다.
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 하이퍼링크입니다.
    /// </summary>
    public string? href { get; set; }
}