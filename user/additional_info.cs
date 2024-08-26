using acNET.Type;

namespace acNET.User;

/// <summary>
/// solved.ac 사용자 부가 정보입니다.
/// </summary>
public class AdditionalInformation : Jsonable
{
    /// <summary>
    /// 사용자의 국가/지역 코드입니다.
    /// </summary>
    public string? countryCode { get; set; }
    /// <summary>
    /// 사용자의 성별입니다.
    /// </summary>
    public string? gender { get; set; }
    /// <summary>
    /// 사용자를 영어로 표기할 때 사용하는 대명사입니다.
    /// </summary>
    public string? pronouns { get; set; }
    /// <summary>
    /// 사용자의 생년입니다.
    /// </summary>
    public int? birthYear { get; set; }
    /// <summary>
    /// 사용자의 생월입니다.
    /// </summary>
    public int? birthMonth { get; set; }
    /// <summary>
    /// 사용자의 생일입니다.
    /// </summary>
    public int? birthDay { get; set; }
    /// <summary>
    /// 사용자의 영어 이름입니다.
    /// </summary>
    public string? name { get; set; }
    /// <summary>
    /// 사용자의 모국어 이름입니다.
    /// </summary>
    public string? nameNative { get; set; }
}
