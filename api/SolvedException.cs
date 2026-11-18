using System.Net;
using System.Text.Json;

namespace AcNET;

/// <summary>
/// solved.ac API 가 돌려준 에러 응답을 담는 예외입니다.
/// </summary>
/// <remarks>
/// solved.ac API v4 는 에러를 아래 형식으로 돌려줍니다.
/// <code>{ "error": { "code": "...", "message": "...", "details": { ... } } }</code>
/// OAuth 엔드포인트(<c>/oauth/token</c>, <c>/oauth/revoke</c>)는 RFC 6749 형식
/// (<c>{ "error": "...", "error_description": "..." }</c>)을 쓰며, 이 경우에도 같은 속성으로 읽을 수 있습니다.
/// </remarks>
public class SolvedAPIException : Exception
{
    /// <summary>
    /// 요청 메서드
    /// </summary>
    public HttpMethod RequestMethod { get; }
    /// <summary>
    /// 요청 경로
    /// </summary>
    public string RequestUrl { get; }
    /// <summary>
    /// HTTP 응답 코드
    /// </summary>
    public HttpStatusCode StatusCode { get; }
    /// <summary>
    /// 에러 코드입니다. (<c>error.code</c>, OAuth 는 <c>error</c>)
    /// 응답이 표준 형식이 아니면 <see cref="SolvedErrorCode.Unknown"/> 입니다. 자주 쓰는 값은 <see cref="SolvedErrorCode"/> 를 참고하세요.
    /// </summary>
    public string ErrorCode { get; }
    /// <summary>
    /// 서버가 보낸 에러 설명입니다. (<c>error.message</c>, OAuth 는 <c>error_description</c>)
    /// </summary>
    public string? ErrorMessage { get; }
    /// <summary>
    /// 에러의 부가 정보입니다. (<c>error.details</c>) 값은 문자열, 숫자, 불리언 중 하나입니다.
    /// 예: 검색 쿼리 제한에 걸리면 <c>limit</c> 이 들어있습니다.
    /// </summary>
    public IReadOnlyDictionary<string, JsonElement> ErrorDetails { get; }
    /// <summary>
    /// 다시 요청해도 되기까지 기다려야 하는 시간입니다. (429 응답의 Retry-After)
    /// </summary>
    public TimeSpan? RetryAfter { get; }
    /// <summary>
    /// 응답에 포함된 요청 한도 정보입니다.
    /// </summary>
    public SolvedRateLimit? RateLimit { get; }
    /// <summary>
    /// 응답이 표준 에러 형식이 아닐 때의 원본 본문입니다. (최대 2000자) 표준 형식이면 null 입니다.
    /// </summary>
    public string? ResponseBody { get; }
    /// <summary>
    /// 요청 한도를 넘어서 거절되었는지 여부입니다. (HTTP 429)
    /// </summary>
    public bool IsRateLimited => (int)StatusCode == 429;

    /// <summary>
    /// SolvedAPI의 예외를 생성합니다.
    /// </summary>
    public SolvedAPIException(
        HttpMethod requestMethod,
        string requestUrl,
        HttpStatusCode statusCode,
        string errorCode,
        string? errorMessage = null,
        IReadOnlyDictionary<string, JsonElement>? errorDetails = null,
        TimeSpan? retryAfter = null,
        SolvedRateLimit? rateLimit = null,
        string? responseBody = null)
        : base(BuildMessage(requestMethod, requestUrl, statusCode, errorCode, errorMessage))
    {
        RequestMethod = requestMethod;
        RequestUrl = requestUrl;
        StatusCode = statusCode;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        ErrorDetails = errorDetails ?? new Dictionary<string, JsonElement>();
        RetryAfter = retryAfter;
        RateLimit = rateLimit;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// 에러 부가 정보(<see cref="ErrorDetails"/>)에서 문자열 값을 가져옵니다. 숫자나 불리언은 문자열로 바꿔 돌려줍니다.
    /// </summary>
    /// <param name="key">키</param>
    /// <returns>없으면 null</returns>
    public string? GetDetail(string key)
    {
        if (!ErrorDetails.TryGetValue(key, out JsonElement value))
            return null;
        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.GetRawText();
    }

    static string BuildMessage(HttpMethod method, string url, HttpStatusCode statusCode, string code, string? message)
        => $"[{code}] {(string.IsNullOrEmpty(message) ? "Request failed." : message)} ({(int)statusCode} {statusCode}: {method} {url})";

    internal static async Task<SolvedAPIException> FromResponseAsync(HttpRequestMessage request, HttpResponseMessage response, CancellationToken cancellationToken)
    {
        string body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        string code = SolvedErrorCode.Unknown;
        string? message = null;
        Dictionary<string, JsonElement>? details = null;
        bool standard = false;

        try
        {
            using JsonDocument document = JsonDocument.Parse(body);
            if (document.RootElement.ValueKind == JsonValueKind.Object
                && document.RootElement.TryGetProperty("error", out JsonElement error))
            {
                if (error.ValueKind == JsonValueKind.Object)
                {
                    // { "error": { "code", "message", "details" } }
                    if (error.TryGetProperty("code", out JsonElement c) && c.ValueKind == JsonValueKind.String)
                    {
                        code = c.GetString()!;
                        standard = true;
                    }
                    if (error.TryGetProperty("message", out JsonElement m) && m.ValueKind == JsonValueKind.String)
                        message = m.GetString();
                    if (error.TryGetProperty("details", out JsonElement d) && d.ValueKind == JsonValueKind.Object)
                    {
                        details = [];
                        foreach (JsonProperty property in d.EnumerateObject())
                            details[property.Name] = property.Value.Clone();
                    }
                }
                else if (error.ValueKind == JsonValueKind.String)
                {
                    // RFC 6749 §5.2: { "error", "error_description" }
                    code = error.GetString()!;
                    standard = true;
                    if (document.RootElement.TryGetProperty("error_description", out JsonElement m) && m.ValueKind == JsonValueKind.String)
                        message = m.GetString();
                }
            }
        }
        catch (JsonException)
        {
            // HTML 등 Json 이 아닌 응답 (예: 프록시, Cloudflare 차단 페이지)
        }

        TimeSpan? retryAfter = response.Headers.RetryAfter switch
        {
            { Delta: TimeSpan delta } => delta,
            { Date: DateTimeOffset date } => TimeSpan.FromTicks(Math.Max(0, (date - DateTimeOffset.UtcNow).Ticks)),
            _ => null,
        };

        return new SolvedAPIException(
            request.Method,
            request.RequestUri?.ToString() ?? string.Empty,
            response.StatusCode,
            code,
            message,
            details,
            retryAfter,
            SolvedRateLimit.From(response),
            standard ? null : (body.Length > 2000 ? body[..2000] : body));
    }
}

/// <summary>
/// solved.ac API 가 돌려주는 대표적인 에러 코드입니다. (<see cref="SolvedAPIException.ErrorCode"/>)
/// </summary>
public static class SolvedErrorCode
{
    /// <summary>
    /// 응답이 표준 에러 형식이 아니어서 코드를 알 수 없습니다. (acNET 이 붙이는 값)
    /// </summary>
    public const string Unknown = "UNKNOWN";
    /// <summary>
    /// 사용자 토큰이 필요한 요청인데 토큰이 없습니다. (401)
    /// </summary>
    public const string UserTokenRequired = "USER_TOKEN_REQUIRED";
    /// <summary>
    /// 사용자 토큰에 필요한 scope 가 없습니다. (403)
    /// </summary>
    public const string InsufficientScope = "INSUFFICIENT_SCOPE";
    /// <summary>
    /// 로그인한 사용자만 할 수 있는 요청입니다. 앱 단독 문제 검색이 쿼리 길이(100자) 나 단어 수(8개) 제한을 넘은 경우에도 옵니다.
    /// 이때 <see cref="SolvedAPIException.ErrorDetails"/> 의 <c>limit</c> 에 넘은 제한이 들어있습니다. (401)
    /// </summary>
    public const string SignInRequired = "SIGN_IN_REQUIRED";
    /// <summary>
    /// 북마크 폴더 순서를 바꿀 때, 보낸 목록이 현재 폴더 목록과 정확히 일치하지 않습니다. (409)
    /// </summary>
    public const string StaleOrder = "STALE_ORDER";
    /// <summary>
    /// OAuth: 요청에 필요한 값이 없거나 잘못되었습니다. (400)
    /// </summary>
    public const string InvalidRequest = "invalid_request";
    /// <summary>
    /// OAuth: 인가 코드나 refresh token 이 유효하지 않습니다. (400)
    /// </summary>
    public const string InvalidGrant = "invalid_grant";
    /// <summary>
    /// OAuth: 요청한 scope 가 유효하지 않습니다. (400)
    /// </summary>
    public const string InvalidScope = "invalid_scope";
    /// <summary>
    /// OAuth: 지원하지 않는 grant_type 입니다. (400)
    /// </summary>
    public const string UnsupportedGrantType = "unsupported_grant_type";
    /// <summary>
    /// OAuth: 클라이언트 인증(client id / secret)에 실패했습니다. (401)
    /// </summary>
    public const string InvalidClient = "invalid_client";
}
