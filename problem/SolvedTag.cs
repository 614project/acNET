using AcNET.Type;

namespace AcNET.Problem;

/// <summary>
/// 태그 정보입니다. (<see cref="SolvedAPI.GetTagListAsync"/>)
/// </summary>
public class SolvedTag : Jsonable
{
    /// <summary>
    /// 태그 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 태그의 key 입니다. (예: "dp")
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 메타 태그인지 여부입니다.
    /// </summary>
    public bool IsMeta { get; set; }
    /// <summary>
    /// 바로 위 부모 태그들의 key 입니다.
    /// </summary>
    public List<string> Parents { get; set; } = [];
    /// <summary>
    /// 언어별 태그 이름입니다. (언어 → 이름, 예: "ko" → "다이나믹 프로그래밍")
    /// </summary>
    public Dictionary<string, string> Names { get; set; } = [];
    /// <summary>
    /// 태그의 별칭입니다.
    /// </summary>
    public List<string> Aliases { get; set; } = [];
    /// <summary>
    /// 태그 레이팅이 있는 태그인지 여부입니다.
    /// </summary>
    public bool Rated { get; set; }

    /// <summary>
    /// 해당 언어의 태그 이름을 가져옵니다. 없으면 영어, 그것도 없으면 key 를 가져옵니다.
    /// </summary>
    /// <param name="language">언어 (예: "ko")</param>
    public string GetName(string language) => SolvedTagLink.PickName(Names, language, Key);
}

/// <summary>
/// 태그 상세 정보입니다. (<see cref="SolvedAPI.GetTagAsync"/>)
/// </summary>
public class SolvedTagDetail : Jsonable
{
    /// <summary>
    /// 태그 ID 입니다.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 언어별 태그 이름입니다. (언어 → 이름)
    /// </summary>
    public Dictionary<string, string> Names { get; set; } = [];
    /// <summary>
    /// 태그의 별칭입니다.
    /// </summary>
    public List<string> Aliases { get; set; } = [];
    /// <summary>
    /// 부모 태그들입니다.
    /// </summary>
    public List<SolvedTagLink> Parents { get; set; } = [];
    /// <summary>
    /// 자식 태그들입니다.
    /// </summary>
    public List<SolvedTagLink> Children { get; set; } = [];
    /// <summary>
    /// 태그 레이팅이 있는 태그인지 여부입니다.
    /// </summary>
    public bool Rated { get; set; }
    /// <summary>
    /// 이 태그가 붙은 문제 수입니다.
    /// </summary>
    public int ProblemCount { get; set; }
    /// <summary>
    /// 그 중 요청한 사용자가 푼 문제 수입니다. 사용자 정보가 없으면 null 입니다.
    /// </summary>
    public int? SolvedCount { get; set; }
    /// <summary>
    /// 레벨별 문제 수입니다.
    /// </summary>
    public List<SolvedLevel> Levels { get; set; } = [];

    /// <summary>
    /// 해당 언어의 태그 이름을 가져옵니다. 없으면 영어, 그것도 없으면 key 를 가져옵니다.
    /// </summary>
    /// <param name="language">언어 (예: "ko")</param>
    public string GetName(string language) => SolvedTagLink.PickName(Names, language, Key);
}

/// <summary>
/// 부모, 자식 관계로 연결된 태그입니다.
/// </summary>
public class SolvedTagLink : Jsonable
{
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 언어별 태그 이름입니다. (언어 → 이름)
    /// </summary>
    public Dictionary<string, string> Names { get; set; } = [];

    /// <summary>
    /// 해당 언어의 태그 이름을 가져옵니다. 없으면 영어, 그것도 없으면 key 를 가져옵니다.
    /// </summary>
    /// <param name="language">언어 (예: "ko")</param>
    public string GetName(string language) => PickName(Names, language, Key);

    internal static string PickName(Dictionary<string, string> names, string language, string key)
        => names.TryGetValue(language, out string? name) || names.TryGetValue("en", out name) ? name : key;
}

/// <summary>
/// 요청한 사용자의 태그별 진행도입니다. (<see cref="SolvedAPI.GetMyTagProgressAsync"/>)
/// </summary>
public class SolvedTagProgress : Jsonable
{
    /// <summary>
    /// 태그별 전체 문제 수입니다.
    /// </summary>
    public List<SolvedTagProblemCount> Counts { get; set; } = [];
    /// <summary>
    /// 태그별 푼 문제 수와 시도한 문제 수입니다.
    /// </summary>
    public List<SolvedTagSolveCount> Items { get; set; } = [];
    /// <summary>
    /// 태그별 레이팅입니다.
    /// </summary>
    public List<SolvedTagRatingValue> Ratings { get; set; } = [];
}

/// <summary>
/// 태그의 문제 수입니다.
/// </summary>
public class SolvedTagProblemCount : Jsonable
{
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 문제 수입니다.
    /// </summary>
    public int ProblemCount { get; set; }
}

/// <summary>
/// 태그에서 푼 문제 수와 시도한 문제 수입니다.
/// </summary>
public class SolvedTagSolveCount : Jsonable
{
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 푼 문제 수입니다.
    /// </summary>
    public int Solved { get; set; }
    /// <summary>
    /// 시도했지만 풀지 못한 문제 수입니다.
    /// </summary>
    public int Tried { get; set; }
}

/// <summary>
/// 태그의 레이팅입니다.
/// </summary>
public class SolvedTagRatingValue : Jsonable
{
    /// <summary>
    /// 태그의 key 입니다.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>
    /// 레이팅입니다.
    /// </summary>
    public int Rating { get; set; }
}
