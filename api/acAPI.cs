using RestSharp;

namespace acNET;

/// <summary>
/// solved.ac API 를 다룰수 있는 객체입니다.
/// </summary>
public partial class acAPI : IDisposable
{
    RestClient _client;

    /// <summary>
    /// solved.ac api 기본 주소.
    /// </summary>
    public const string DefaultUrl = "https://solved.ac/api/v3/";

    /// <summary>
    /// acAPI 생성.
    /// </summary>
    /// <param name="url">solved.ac API 링크</param>
    public acAPI(string url = DefaultUrl)
    {
        this._client = new(url);
    }

    /// <summary>
    /// acNET.API 사용 도중 발생한 오류가 저장되는 큐입니다.
    /// </summary>
    public readonly Queue<Exception> Errors = new();

    /// <summary>
    /// acAPI 리소스를 정리합니다.
    /// </summary>
    public void Dispose()
    {
        this._client.Dispose();
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// acAPI가 자체적으로 일으킨 의도적인 예외 (C# 런타임 에러가 아닙니다.)
/// </summary>
public class acAPIError : Exception
{
    /// <summary>
    /// acAPI 알수없는 예외
    /// </summary>
    public acAPIError() { this.Code = -1; }
    /// <summary>
    /// acAPI 사유가 있는 예외
    /// </summary>
    /// <param name="message">예외 사유</param>
    /// <param name="code">오류 코드</param>
    public acAPIError(string message,short code=-1) : base(message) { this.Code = code; }
    /// <summary>
    /// acAPI 내 일어난 예외
    /// </summary>
    /// <param name="message">예외 사유</param>
    /// <param name="inner">발생한 예외</param>
    public acAPIError(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// acAPI 내 일어난 예외
    /// </summary>
    internal protected acAPIError(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    /// <summary>
    /// acAPI 예외를 생성합니다.
    /// </summary>
    /// <param name="message">사유</param>
    /// <param name="code">오류 코드</param>
    /// <returns>예외 객체</returns>
    public static acAPIError Create(string message,short code=-1)
    {
        acAPIError error = new acAPIError(message,code);
        return error;
    }

    /// <summary>
    /// 오류 코드입니다. (0~599: API 응답 코드, -1: 알수 없음, 이외: 위키 참고)
    /// </summary>
    public short Code { get; init; }
}
