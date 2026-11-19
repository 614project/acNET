using AcNET.OAuth;

namespace AcNET;

public partial class SolvedAPI
{
    #region OAuth
    /// <summary>
    /// 사용자를 보낼 OAuth 인가 페이지 주소를 만듭니다. (Authorization Code + PKCE S256)
    /// </summary>
    /// <param name="redirectUri">돌아올 주소. Backstage 콘솔에 등록한 주소 중 하나와 정확히 같아야 합니다.</param>
    /// <param name="scopes">요청할 권한 목록 (<see cref="SolvedScope"/>). <see cref="SolvedScope.Profile"/> 은 항상 포함됩니다.</param>
    /// <param name="state">CSRF 방지용 값. 돌아왔을 때 같은지 확인하세요. (<see cref="SolvedPkce.CreateState"/>)</param>
    /// <param name="pkce">이번 인가에 사용할 PKCE 쌍 (<see cref="SolvedPkce.Create"/>). verifier 는 토큰 교환 때까지 보관하세요.</param>
    /// <returns>인가 페이지 주소</returns>
    public Uri GetAuthorizeUrl(string redirectUri, IEnumerable<string> scopes, string state, SolvedPkce pkce)
    {
        Guard.NotNullOrEmpty(redirectUri);
        Guard.NotNull(scopes);
        Guard.NotNullOrEmpty(state);
        Guard.NotNull(pkce);

        string query = new SolvedQuery()
            .Add("response_type", "code")
            .Add("client_id", options.ClientId)
            .Add("redirect_uri", redirectUri)
            .Add("scope", string.Join(' ', scopes.Prepend(SolvedScope.Profile).Distinct(StringComparer.Ordinal)))
            .Add("state", state)
            .Add("code_challenge", pkce.CodeChallenge)
            .Add("code_challenge_method", pkce.CodeChallengeMethod)
            .ToString();
        return new UriBuilder(options.AuthorizeEndpoint) { Query = query[1..] }.Uri;
    }

    /// <summary>
    /// 인가 코드를 토큰으로 교환합니다. (<c>POST /oauth/token</c>, grant_type=authorization_code)
    /// </summary>
    /// <remarks>
    /// 받은 토큰을 사용하려면 <see cref="AccessToken"/> 에 넣거나 <see cref="WithAccessToken"/> 을 사용하세요.
    /// 실패하면 <see cref="SolvedAPIException.ErrorCode"/> 가 <see cref="SolvedErrorCode.InvalidGrant"/> 등 RFC 6749 의 에러 코드가 됩니다.
    /// </remarks>
    /// <param name="code">인가 페이지에서 돌아온 code</param>
    /// <param name="redirectUri">인가 요청 때 보낸 것과 같은 redirect_uri</param>
    /// <param name="codeVerifier">인가 요청 때 사용한 PKCE 의 code verifier</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>토큰</returns>
    public Task<SolvedResult<SolvedToken>> ExchangeCodeAsync(string code, string redirectUri, string codeVerifier, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(code);
        Guard.NotNullOrEmpty(redirectUri);
        Guard.NotNullOrEmpty(codeVerifier);
        return TokenRequestAsync(new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["code"] = code,
            ["redirect_uri"] = redirectUri,
            ["code_verifier"] = codeVerifier,
        }, cancellationToken);
    }

    /// <summary>
    /// 인가 코드를 토큰으로 교환합니다. (<c>POST /oauth/token</c>, grant_type=authorization_code)
    /// </summary>
    /// <param name="code">인가 페이지에서 돌아온 code</param>
    /// <param name="redirectUri">인가 요청 때 보낸 것과 같은 redirect_uri</param>
    /// <param name="pkce">인가 요청 때 사용한 PKCE 쌍</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>토큰</returns>
    public Task<SolvedResult<SolvedToken>> ExchangeCodeAsync(string code, string redirectUri, SolvedPkce pkce, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(pkce);
        return ExchangeCodeAsync(code, redirectUri, pkce.CodeVerifier, cancellationToken);
    }

    /// <summary>
    /// refresh token 으로 새 토큰을 받습니다. (<c>POST /oauth/token</c>, grant_type=refresh_token)
    /// </summary>
    /// <remarks>
    /// refresh token 은 한 번만 쓸 수 있습니다. 응답의 새 refresh token 을 저장하세요.
    /// 같은 refresh token 을 두 번 쓰면 그 로그인의 모든 토큰이 무효가 됩니다.
    /// </remarks>
    /// <param name="refreshToken">refresh token</param>
    /// <param name="scopes">새 토큰의 권한을 좁히고 싶을 때 지정합니다. null 이면 기존 권한을 유지합니다.</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>새 토큰</returns>
    public Task<SolvedResult<SolvedToken>> RefreshTokenAsync(string refreshToken, IEnumerable<string>? scopes = null, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(refreshToken);
        Dictionary<string, string> form = new()
        {
            ["grant_type"] = "refresh_token",
            ["refresh_token"] = refreshToken,
        };
        if (scopes is not null)
            form["scope"] = string.Join(' ', scopes);
        return TokenRequestAsync(form, cancellationToken);
    }

    /// <summary>
    /// 토큰을 폐기해서 그 기기에서 사용자를 이 앱에서 로그아웃시킵니다. (<c>POST /oauth/revoke</c>, RFC 7009)
    /// </summary>
    /// <remarks>
    /// access token 과 refresh token 중 무엇을 보내도 같은 로그인의 모든 토큰이 폐기됩니다.
    /// 모르는 토큰이어도 성공으로 응답합니다.
    /// </remarks>
    /// <param name="token">access token 또는 refresh token</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>성공시 true</returns>
    public async Task<SolvedResult<bool>> RevokeTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(token);
        SolvedResult<object> result = await SendAsync<object, object>(
            HttpMethod.Post,
            "oauth/revoke",
            null,
            new FormUrlEncodedContent(new Dictionary<string, string> { ["token"] = token }),
            null,
            cancellationToken,
            sendUserToken: false).ConfigureAwait(false);
        return new(result.IsSuccess, result.Exception) { RateLimit = result.RateLimit };
    }

    Task<SolvedResult<SolvedToken>> TokenRequestAsync(Dictionary<string, string> form, CancellationToken cancellationToken)
        => SendAsync<SolvedToken, SolvedToken>(
            HttpMethod.Post,
            "oauth/token",
            null,
            new FormUrlEncodedContent(form),
            static token => token,
            cancellationToken,
            sendUserToken: false);
    #endregion
}
