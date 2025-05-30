using System.Net;

namespace AcNET;

/// <summary>
/// SolvedAPI에서 요청과 관련된 문제를 담는 예외
/// </summary>
public class SolvedAPIException : Exception
{
    /// <summary>
    /// 요청 경로
    /// </summary>
    public string RequestUrl { get; } = string.Empty;
    /// <summary>
    /// HTTP 응답 코드
    /// </summary>
    public HttpStatusCode StatusCode { get; }
    /// <summary>
    /// SolvedAPI의 예외를 생성합니다.
    /// </summary>
    public SolvedAPIException(in string requestUrl, in HttpStatusCode statusCode) : base($"Request to '{requestUrl}' is failed! (status code: {(int)statusCode} ({statusCode}))")
    {
        RequestUrl = requestUrl;
        StatusCode = statusCode;
    }

    //internal SolvedAPIException(string baseUrl) : this() { BaseUrl = baseUrl; }
    //string BaseUrl { get; } = string.Empty;
}