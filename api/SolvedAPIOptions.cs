namespace AcNET;

/// <summary>
/// <see cref="SolvedAPI"/> 를 만들 때 쓰는 설정입니다.
/// </summary>
public sealed class SolvedAPIOptions
{
    /// <summary>
    /// 설정을 만듭니다.
    /// </summary>
    /// <param name="clientId">애플리케이션의 클라이언트 ID (x-client-id)</param>
    /// <param name="clientSecret">애플리케이션의 클라이언트 시크릿 (x-client-secret)</param>
    public SolvedAPIOptions(string clientId, string clientSecret)
    {
        ClientId = Guard.NotNullOrWhiteSpace(clientId);
        ClientSecret = Guard.NotNullOrWhiteSpace(clientSecret);
    }

    /// <summary>
    /// solved.ac Backstage(https://backstage.solved.ac/developer)에서 등록한 애플리케이션의 클라이언트 ID 입니다. (x-client-id)
    /// </summary>
    public string ClientId { get; }

    /// <summary>
    /// 애플리케이션의 클라이언트 시크릿입니다. (x-client-secret)
    /// 시크릿이 노출되지 않도록 서버 등 신뢰할 수 있는 환경에서만 사용하세요.
    /// </summary>
    public string ClientSecret { get; }

    /// <summary>
    /// 처음부터 사용할 사용자 접근 토큰(OAuth access token)입니다. 사용자 권한이 필요없다면 비워두세요.
    /// </summary>
    public string? AccessToken { get; init; }

    /// <summary>
    /// 응답 언어입니다. ("ko", "en", "ja" 등) 언어를 받는 엔드포인트에 <c>lang</c> 쿼리로 붙습니다. null 이면 서버 기본값을 따릅니다.
    /// </summary>
    public string? Language { get; init; }

    /// <summary>
    /// solved.ac API 의 기본 주소입니다. 기본값은 <see cref="SolvedAPI.DefaultBaseUrl"/> 입니다.
    /// </summary>
    public Uri BaseAddress
    {
        get => baseAddress;
        init => baseAddress = EnsureTrailingSlash(value ?? throw new ArgumentNullException(nameof(value)));
    }
    Uri baseAddress = new(SolvedAPI.DefaultBaseUrl);

    /// <summary>
    /// OAuth 인가 페이지 주소입니다. 기본값은 <see cref="SolvedAPI.DefaultAuthorizeUrl"/> 입니다.
    /// </summary>
    public Uri AuthorizeEndpoint { get; init; } = new(SolvedAPI.DefaultAuthorizeUrl);

    static Uri EnsureTrailingSlash(Uri uri)
        => uri.AbsoluteUri.EndsWith('/') ? uri : new Uri(uri.AbsoluteUri + "/");
}
