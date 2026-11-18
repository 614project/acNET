using System.Net;
using System.Text;

namespace AcNET.Tests.Infrastructure;

/// <summary>
/// 정해둔 응답을 돌려주고 받은 요청을 기록하는 가짜 HttpMessageHandler 입니다.
/// </summary>
public sealed class StubHandler(Func<HttpRequestMessage, HttpResponseMessage> respond) : HttpMessageHandler
{
    public List<RecordedRequest> Requests { get; } = [];

    public static StubHandler Json(HttpStatusCode status, string json, Action<HttpResponseMessage>? configure = null)
        => new(_ =>
        {
            HttpResponseMessage response = new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
            configure?.Invoke(response);
            return response;
        });

    public SolvedAPI CreateClient(string? accessToken = null, string? language = null)
        => new(new SolvedAPIOptions { ClientId = "client-id", ClientSecret = "client-secret", AccessToken = accessToken, Language = language }, new HttpClient(this));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? body = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
        Requests.Add(new RecordedRequest(request.Method, request.RequestUri!, request.Headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => string.Join(",", h.Value)), request.Content?.Headers.ContentType?.MediaType, body));
        return respond(request);
    }

    public sealed record RecordedRequest(HttpMethod Method, Uri Uri, Dictionary<string, string> Headers, string? ContentType, string? Body);
}
