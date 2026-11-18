using System.Text.Json;

namespace AcNET.Type;

/// <summary>
/// solved.ac API 로 주고받는 모든 모델의 기본 클래스입니다.
/// </summary>
public abstract class Jsonable
{
    /// <summary>
    /// 해당 객체를 Json 형식의 문자열로 변환합니다.
    /// </summary>
    /// <returns>Json 문자열</returns>
    public virtual string ToJson() => JsonSerializer.Serialize(this, GetType(), SolvedJson.Display);

    /// <summary>
    /// ToJson 함수와 동일합니다.
    /// </summary>
    public override string ToString() => ToJson();
}
