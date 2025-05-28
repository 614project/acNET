using Newtonsoft.Json;

namespace AcNET.Type;

/// <summary>
/// acAPI 로 가져올수 있는 기본 클래스
/// </summary>
public abstract class Jsonable
{
    /// <summary>
    /// 해당 클래스를 Json 형식의 문자열로 변환합니다.
    /// </summary>
    /// <returns>Json 문자열</returns>
    public virtual string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
    /// <summary>
    /// ToJson 함수와 동일합니다.
    /// </summary>
    public override string ToString()
    {
        return this.ToJson();
    }
}