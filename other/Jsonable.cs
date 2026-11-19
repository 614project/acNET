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
    /// <remarks>
    /// AOT 를 지원하기 위해 소스 생성된 메타데이터를 사용하므로, acNET 에 들어있는 모델에서만 동작합니다.
    /// 이 클래스를 직접 상속한 형식은 이 함수를 재정의해야 합니다.
    /// </remarks>
    /// <returns>Json 문자열</returns>
    public virtual string ToJson() => JsonSerializer.Serialize(this, GetType(), SolvedJson.Display);

    /// <summary>
    /// ToJson 함수와 동일합니다.
    /// </summary>
    public override string ToString() => ToJson();
}
