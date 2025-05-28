using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.User;

/// <summary>
/// solved.ac 사용자 부가 정보입니다.
/// </summary>
public class SolvedAdditionalInformation : Jsonable
{
    /// <summary>
    /// 사용자의 국가/지역 코드입니다.
    /// </summary>
    [JsonProperty("countryCode")]
    public string? CountryCode { get; set; }
    /// <summary>
    /// 사용자의 성별입니다.
    /// </summary>
    [JsonProperty("gender")]
    public string? Gender { get; set; }
    /// <summary>
    /// 사용자를 영어로 표기할 때 사용하는 대명사입니다.
    /// </summary>
    [JsonProperty("pronouns")]
    public string? Pronouns { get; set; }
    /// <summary>
    /// 사용자의 생년입니다.
    /// </summary>
    [JsonProperty("birthYear")]
    public int? BirthYear { get; set; }
    /// <summary>
    /// 사용자의 생월입니다.
    /// </summary>
    [JsonProperty("birthMonth")]
    public int? BirthMonth { get; set; }
    /// <summary>
    /// 사용자의 생일입니다.
    /// </summary>
    [JsonProperty("birthDay")]
    public int? BirthDay { get; set; }
    /// <summary>
    /// 사용자의 영어 이름입니다.
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }
    /// <summary>
    /// 사용자의 모국어 이름입니다.
    /// </summary>
    [JsonProperty("nameNative")]
    public string? NameNative { get; set; }
    /// <summary>
    /// DateTime 형식으로 생년월일을 가져옵니다. 명시되지 않은 연도,월,일은 1을 기본값으로 가집니다. (예: '1년 6월 14일'일 경우 사용자가 연도를 입력하지 않았다는 뜻입니다.)
    /// </summary>
    public DateTime GetBirthDate => new(BirthYear ?? 1 , BirthMonth ?? 1 , BirthDay ?? 1);
}
