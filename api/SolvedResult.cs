namespace AcNET;

/// <summary>
/// SolvedAPI의 처리 결과입니다.
/// </summary>
/// <typeparam name="T">Json 형식의 요청 결과를 역직렬화 할 클래스</typeparam>
/// <param name="Result">결과 (null일 경우 실패)</param>
/// <param name="Exception">예외 (null일 경우 성공)</param>
public record SolvedResult<T>(T? Result, Exception? Exception)
{
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
    /// 결과를 바로 가져옵니다. 만약 null일경우 예외를 일으킵니다.
    /// </summary>
    /// <returns>결과</returns>
    public T GetResultOrThrow() => this.Result ?? throw this.Exception ?? new NullReferenceException();
    /// <summary>
    /// Exception != null 이라면 예외를, 그렇지 않으면 Result를 .ToString() 한 결과를 가져옵니다.
    /// </summary>
    /// <returns>문자열</returns>
    public override string ToString() => Exception is not null ? Exception.ToString() : Result?.ToString() ?? string.Empty;
}
