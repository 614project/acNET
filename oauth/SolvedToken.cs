using System.Text.Json.Serialization;
using AcNET.Type;

namespace AcNET.OAuth;

/// <summary>
/// OAuth 토큰 응답입니다.
/// </summary>
/// <remarks>
/// access token 은 1시간 동안 유효합니다. refresh token 은 <see cref="SolvedScope.OfflineAccess"/> 를 받았을 때만 오며,
/// 90일 동안 유효하고 한 번 쓰면 사라집니다. (갱신할 때마다 새 refresh token 을 받습니다.)
/// 같은 refresh token 을 두 번 쓰면 복제된 것으로 보고 그 로그인의 모든 토큰이 무효가 되니,
/// 여러 곳에서 동시에 갱신하지 않도록 주의하세요.
/// </remarks>
public sealed class SolvedToken : Jsonable
{
    /// <summary>
    /// 접근 토큰입니다. <see cref="SolvedAPI.AccessToken"/> 에 넣어 사용합니다.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
    /// <summary>
    /// 토큰 종류입니다. 항상 "Bearer" 입니다.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = string.Empty;
    /// <summary>
    /// 접근 토큰의 유효 시간(초)입니다.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; init; }
    /// <summary>
    /// refresh token 입니다. <see cref="SolvedScope.OfflineAccess"/> 가 없으면 null 입니다.
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }
    /// <summary>
    /// 토큰이 가진 scope 들입니다. (공백으로 구분)
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; init; } = string.Empty;

    /// <summary>
    /// acNET 이 이 토큰 응답을 받은 시각입니다.
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset IssuedAt { get; internal set; } = DateTimeOffset.UtcNow;
    /// <summary>
    /// 접근 토큰이 만료되는 시각입니다. (받은 시각 기준으로 계산)
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset ExpiresAt => IssuedAt.AddSeconds(ExpiresIn);
    /// <summary>
    /// 토큰이 가진 scope 목록입니다.
    /// </summary>
    [JsonIgnore]
    public IReadOnlyList<string> Scopes => Scope.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    /// 토큰이 해당 scope 를 가지고 있는지 확인합니다.
    /// </summary>
    /// <param name="scope">확인할 scope (<see cref="SolvedScope"/>)</param>
    public bool HasScope(string scope) => Scopes.Contains(scope, StringComparer.Ordinal);

    /// <summary>
    /// 토큰 값이 로그에 남지 않도록 토큰을 가린 문자열을 돌려줍니다. 원본 Json 은 <see cref="Jsonable.ToJson"/> 을 사용하세요.
    /// </summary>
    public override string ToString()
        => $"SolvedToken {{ TokenType = {TokenType}, Scope = {Scope}, ExpiresAt = {ExpiresAt:O}, HasRefreshToken = {RefreshToken is not null} }}";
}
