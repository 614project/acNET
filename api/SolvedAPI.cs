using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace AcNET;

/// <summary>
/// solved.ac API (v4) 를 다룰 수 있는 클라이언트입니다.
/// </summary>
/// <remarks>
/// <para>
/// 모든 요청에는 solved.ac Backstage(https://backstage.solved.ac/developer)에서 등록한 애플리케이션의
/// 클라이언트 ID 와 시크릿이 필요합니다. 사용자 대신 동작하는 API 는 OAuth 로 받은 사용자 접근 토큰(<see cref="AccessToken"/>)도 필요합니다.
/// </para>
/// <para>
/// 모든 API 는 비동기이며 예외를 던지는 대신 <see cref="SolvedResult{T}"/> 로 결과를 돌려줍니다.
/// (단, 인자가 잘못되었거나 <see cref="CancellationToken"/> 으로 취소한 경우에는 예외가 발생합니다.)
/// </para>
/// <para>
/// 한 인스턴스를 여러 스레드에서 함께 사용해도 됩니다. 매 요청마다 새로 만들지 말고 재사용하세요.
/// </para>
/// </remarks>
public partial class SolvedAPI : IDisposable
{
    /// <summary>
    /// solved.ac API의 기본 주소.
    /// </summary>
    public const string DefaultBaseUrl = "https://solved.ac/api/v4/";
    /// <summary>
    /// solved.ac OAuth 인가 페이지의 기본 주소.
    /// </summary>
    public const string DefaultAuthorizeUrl = "https://solved.ac/oauth/authorize";

    readonly HttpClient client;
    readonly bool ownsClient;
    readonly SolvedAPIOptions options;

    /// <summary>
    /// SolvedAPI 생성.
    /// </summary>
    /// <param name="clientId">애플리케이션의 클라이언트 ID (x-client-id)</param>
    /// <param name="clientSecret">애플리케이션의 클라이언트 시크릿 (x-client-secret)</param>
    /// <param name="accessToken">사용자 접근 토큰. 사용자 권한이 필요한 작업에만 필요하며, 이외에는 null로 비워두어도 됩니다.</param>
    public SolvedAPI(string clientId, string clientSecret, string? accessToken = null)
        : this(new SolvedAPIOptions { ClientId = clientId, ClientSecret = clientSecret, AccessToken = accessToken })
    {
    }

    /// <summary>
    /// SolvedAPI 생성.
    /// </summary>
    /// <param name="options">설정</param>
    /// <param name="httpClient">
    /// 사용할 HttpClient (IHttpClientFactory 등). null 이면 내부에서 만들고 <see cref="Dispose"/> 할 때 함께 정리합니다.
    /// 직접 넘긴 HttpClient 는 정리하지 않습니다.
    /// </param>
    public SolvedAPI(SolvedAPIOptions options, HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrWhiteSpace(options.ClientId, nameof(options.ClientId));
        ArgumentException.ThrowIfNullOrWhiteSpace(options.ClientSecret, nameof(options.ClientSecret));

        this.options = options;
        AccessToken = options.AccessToken;
        Language = options.Language;

        if (httpClient is null)
        {
            client = new HttpClient(new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                AutomaticDecompression = DecompressionMethods.All,
            });
            ownsClient = true;
        }
        else
        {
            client = httpClient;
            ownsClient = false;
        }
    }

    SolvedAPI(SolvedAPI parent, string? accessToken)
    {
        options = parent.options;
        client = parent.client;
        ownsClient = false;
        AccessToken = accessToken;
        Language = parent.Language;
        JsonOptions = parent.JsonOptions;
    }

    /// <summary>
    /// 애플리케이션의 클라이언트 ID 입니다.
    /// </summary>
    public string ClientId => options.ClientId;

    /// <summary>
    /// 요청에 함께 보낼 사용자 접근 토큰입니다. (Authorization: Bearer) null 이면 앱 권한으로만 요청합니다.
    /// </summary>
    /// <remarks>
    /// 서버에서 여러 사용자를 대신해 요청한다면 이 속성을 바꾸는 대신 <see cref="WithAccessToken"/> 을 사용하세요.
    /// </remarks>
    public string? AccessToken { get; set; }

    /// <summary>
    /// 응답을 받을 언어를 지정합니다. ("ko", "en", "ja" 등) 언어를 받는 엔드포인트에 <c>lang</c> 쿼리로 붙습니다.
    /// null 이면 서버 기본값(대부분 한국어)을 따릅니다.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// HttpClient 와 앱 설정을 공유하면서 다른 사용자 토큰으로 요청하는 클라이언트를 만듭니다.
    /// </summary>
    /// <remarks>
    /// 만들어진 클라이언트는 원래 클라이언트의 HttpClient 를 빌려 쓰므로, 원래 클라이언트보다 오래 사용하면 안 됩니다.
    /// 만들어진 클라이언트를 Dispose 해도 HttpClient 는 정리되지 않습니다.
    /// </remarks>
    /// <param name="accessToken">사용자 접근 토큰. null 이면 앱 권한으로만 요청합니다.</param>
    /// <returns>새 클라이언트</returns>
    public SolvedAPI WithAccessToken(string? accessToken) => new(this, accessToken);

    /// <summary>
    /// SolvedAPI 리소스를 정리합니다.
    /// </summary>
    public void Dispose()
    {
        if (ownsClient)
            client.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 응답을 읽을 때 쓰는 Json 설정입니다. (테스트에서 엄격한 설정으로 바꾸기 위해 internal 로 열어둡니다.)
    /// </summary>
    internal JsonSerializerOptions JsonOptions { get; set; } = SolvedJson.Read;

    #region Transport
    HttpRequestMessage CreateRequest(HttpMethod method, string path, SolvedQuery? query, HttpContent? content, bool sendUserToken)
    {
        HttpRequestMessage request = new(method, new Uri(options.BaseAddress, path + query))
        {
            Content = content,
        };
        request.Headers.Add("x-client-id", options.ClientId);
        request.Headers.Add("x-client-secret", options.ClientSecret);
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("acNET", SolvedLibrary.Version));
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (sendUserToken && !string.IsNullOrEmpty(AccessToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        return request;
    }

    async Task<SolvedResult<TResult>> SendAsync<TWire, TResult>(
        HttpMethod method,
        string path,
        SolvedQuery? query,
        HttpContent? content,
        Func<TWire, TResult>? map,
        CancellationToken cancellationToken,
        bool sendUserToken = true)
    {
        using HttpRequestMessage request = CreateRequest(method, path, query, content, sendUserToken);
        try
        {
            using HttpResponseMessage response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
            SolvedRateLimit? rateLimit = SolvedRateLimit.From(response);

            if (!response.IsSuccessStatusCode)
            {
                SolvedAPIException exception = await SolvedAPIException
                    .FromResponseAsync(request, response, cancellationToken)
                    .ConfigureAwait(false);
                return new(default, exception) { RateLimit = rateLimit };
            }

            // 본문이 없는 응답 (204 등)
            if (map is null)
                return new(default, null) { RateLimit = rateLimit };

            TWire? wire = await response.Content
                .ReadFromJsonAsync<TWire>(JsonOptions, cancellationToken)
                .ConfigureAwait(false);
            if (wire is null)
                return new(default, new JsonException($"'{request.Method} {request.RequestUri}' 의 응답 본문이 비어있습니다.")) { RateLimit = rateLimit };
            return new(map(wire), null) { RateLimit = rateLimit };
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            return new(default, ex);
        }
    }

    Task<SolvedResult<T>> GetAsync<T>(string path, SolvedQuery? query, CancellationToken cancellationToken)
        => SendAsync<T, T>(HttpMethod.Get, path, query, null, static x => x, cancellationToken);

    Task<SolvedResult<TResult>> GetAsync<TWire, TResult>(string path, SolvedQuery? query, Func<TWire, TResult> map, CancellationToken cancellationToken)
        => SendAsync(HttpMethod.Get, path, query, null, map, cancellationToken);

    Task<SolvedResult<T>> SendJsonAsync<T>(HttpMethod method, string path, object? body, CancellationToken cancellationToken)
        => SendAsync<T, T>(method, path, null, JsonBody(body), static x => x, cancellationToken);

    Task<SolvedResult<TResult>> SendJsonAsync<TWire, TResult>(HttpMethod method, string path, object? body, Func<TWire, TResult> map, CancellationToken cancellationToken)
        => SendAsync(method, path, null, JsonBody(body), map, cancellationToken);

    async Task<SolvedResult<bool>> SendNoContentAsync(HttpMethod method, string path, object? body, CancellationToken cancellationToken)
    {
        SolvedResult<object> result = await SendAsync<object, object>(method, path, null, JsonBody(body), null, cancellationToken).ConfigureAwait(false);
        return new(result.IsSuccess, result.Exception) { RateLimit = result.RateLimit };
    }

    static HttpContent? JsonBody(object? body)
        => body is null ? null : JsonContent.Create(body, body.GetType(), options: SolvedJson.Write);
    #endregion
}
