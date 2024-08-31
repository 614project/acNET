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
    /// <summary>
    /// DateTime 형식으로 생년월일을 가져옵니다. 명시되지 않은 연도,월,일은 1을 기본값으로 가집니다. (예: '1년 6월 14일'일 경우 사용자가 연도를 입력하지 않았다는 뜻입니다.)
    /// </summary>
    public DateTime GetBirthDate => new(birthYear ?? 1 , birthMonth ?? 1 , birthDay ?? 1);
}
