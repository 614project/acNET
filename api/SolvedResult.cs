using System.Runtime.ExceptionServices;

namespace AcNET;

/// <summary>
/// SolvedAPI의 처리 결과입니다.
/// </summary>
/// <typeparam name="T">Json 형식의 요청 결과를 역직렬화 할 클래스</typeparam>
/// <param name="Result">결과 (실패시 기본값)</param>
/// <param name="Exception">예외 (null일 경우 성공)</param>
public readonly record struct SolvedResult<T>(T? Result, Exception? Exception)
{
    /// <summary>
    /// 요청이 성공했는지 여부입니다. (<see cref="Exception"/> 이 null 이면 성공)
    /// </summary>
    public bool IsSuccess => Exception is null;

    /// <summary>
    /// solved.ac 가 돌려준 표준 에러 응답입니다. 네트워크 오류 등 HTTP 응답이 없는 실패나 성공시에는 null 입니다.
    /// </summary>
    public SolvedAPIException? APIException => Exception as SolvedAPIException;

    /// <summary>
    /// 응답에 포함된 요청 한도 정보입니다. (X-RateLimit-Limit / X-RateLimit-Remaining)
    /// </summary>
    public SolvedRateLimit? RateLimit { get; init; }

    /// <summary>
    /// 결과(Result)를 바로 가져옵니다.
    /// </summary>
    /// <param name="me">요청 결과</param>
    public static implicit operator T?(SolvedResult<T> me) => me.Result;

    /// <summary>
    /// 예외(Exception)를 바로 가져옵니다.
    /// </summary>
    /// <param name="me">요청 결과</param>
    public static implicit operator Exception?(SolvedResult<T> me) => me.Exception;

    /// <summary>
    /// 결과를 바로 가져옵니다. 요청이 실패했다면 그 예외를 그대로 일으킵니다.
    /// </summary>
    /// <returns>결과</returns>
    public T GetResultOrThrow()
    {
        if (Exception is not null)
            ExceptionDispatchInfo.Capture(Exception).Throw();
        return Result!;
    }

    /// <summary>
    /// Exception != null 이라면 예외를, 그렇지 않으면 Result를 .ToString() 한 결과를 가져옵니다.
    /// </summary>
    /// <returns>문자열</returns>
    public override string ToString() => Exception is not null ? Exception.ToString() : Result?.ToString() ?? string.Empty;
}

/// <summary>
/// 응답 헤더로 받은 요청 한도(토큰 버킷) 정보입니다.
/// </summary>
/// <param name="Limit">버킷의 크기입니다. (X-RateLimit-Limit)</param>
/// <param name="Remaining">남은 단위입니다. (X-RateLimit-Remaining)</param>
public sealed record SolvedRateLimit(int Limit, int Remaining)
{
    internal static SolvedRateLimit? From(HttpResponseMessage response)
    {
        static int? Read(HttpResponseMessage response, string name)
            => response.Headers.TryGetValues(name, out var values) && int.TryParse(values.FirstOrDefault(), out int value) ? value : null;

        int? limit = Read(response, "X-RateLimit-Limit");
        int? remaining = Read(response, "X-RateLimit-Remaining");
        return limit is null && remaining is null ? null : new(limit ?? 0, remaining ?? 0);
    }
}
