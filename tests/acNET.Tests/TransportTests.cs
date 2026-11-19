using System.Net;
using AcNET.Bookmark;
using AcNET.Tests.Infrastructure;

namespace AcNET.Tests;

public class TransportTests
{
    const string UserJson = """{ "id": 1, "handle": "jyunni", "tier": 15, "rating": 1600 }""";

    [Fact]
    public async Task Sends_app_credentials_and_user_agent_on_every_request()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, UserJson);
        using SolvedAPI api = handler.CreateClient();

        await api.GetUserAsync("jyunni");

        var request = Assert.Single(handler.Requests);
        Assert.Equal("client-id", request.Headers["x-client-id"]);
        Assert.Equal("client-secret", request.Headers["x-client-secret"]);
        Assert.StartsWith("acNET/", request.Headers["user-agent"]);
        Assert.False(request.Headers.ContainsKey("authorization"));
        Assert.Equal("https://solved.ac/api/v4/users/jyunni", request.Uri.ToString());
    }

    [Fact]
    public async Task Sends_bearer_token_only_when_an_access_token_is_set()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, UserJson);
        using SolvedAPI api = handler.CreateClient(accessToken: "user-token");

        await api.GetUserAsync("jyunni");
        api.AccessToken = null;
        await api.GetUserAsync("jyunni");

        Assert.Equal("Bearer user-token", handler.Requests[0].Headers["authorization"]);
        Assert.False(handler.Requests[1].Headers.ContainsKey("authorization"));
    }

    [Fact]
    public async Task WithAccessToken_shares_the_http_client_and_uses_its_own_token()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, UserJson);
        using SolvedAPI api = handler.CreateClient(accessToken: "app-user", language: "en");

        using (SolvedAPI other = api.WithAccessToken("other-user"))
            await other.GetUserAsync("jyunni");
        await api.GetUserAsync("jyunni"); // 자식을 Dispose 해도 HttpClient 는 살아있어야 함

        Assert.Equal("Bearer other-user", handler.Requests[0].Headers["authorization"]);
        Assert.Contains("lang=en", handler.Requests[0].Uri.Query);
        Assert.Equal("Bearer app-user", handler.Requests[1].Headers["authorization"]);
    }

    [Fact]
    public async Task Url_encodes_path_segments_and_query_values()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, """{ "problems": [], "total": 0, "page": 1, "pageSize": 50 }""");
        using SolvedAPI api = handler.CreateClient();

        await api.GetSearchProblemAsync("tier:g5..g1 #dp & s@", 1, SolvedProblemSort.AverageTry, SolvedSortDirection.Descending);
        await api.GetUserAsync("a b/c?");

        Assert.Equal("?page=1&q=tier%3Ag5..g1%20%23dp%20%26%20s%40&sort=average_try&direction=desc", handler.Requests[0].Uri.Query);
        Assert.Equal("/api/v4/users/a%20b%2Fc%3F", handler.Requests[1].Uri.AbsolutePath);
    }

    [Fact]
    public async Task Adds_lang_only_to_endpoints_that_take_it()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, "{}");
        using SolvedAPI api = handler.CreateClient(language: "ja");

        await api.GetUserAsync("jyunni");
        await api.GetSiteStatsAsync();

        Assert.Equal("?lang=ja", handler.Requests[0].Uri.Query);
        Assert.Equal(string.Empty, handler.Requests[1].Uri.Query);
    }

    [Fact]
    public async Task Reads_rate_limit_headers_on_success()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, UserJson, r =>
        {
            r.Headers.Add("X-RateLimit-Limit", "600");
            r.Headers.Add("X-RateLimit-Remaining", "42");
        });
        using SolvedAPI api = handler.CreateClient();

        var result = await api.GetUserAsync("jyunni");

        Assert.True(result.IsSuccess);
        Assert.Equal(new SolvedRateLimit(600, 42), result.RateLimit);
        Assert.Equal("jyunni", result.GetResultOrThrow().Handle);
        Assert.Equal("Gold I", result.Result!.GetTierName);
    }

    [Fact]
    public async Task Omits_null_properties_from_request_bodies()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, """{ "slug": "a", "name": "b", "description": "", "color": "red", "visibility": "public", "problemCount": 0, "favoriteCount": 0, "solvedCount": null, "owner": { "id": 1, "handle": "a", "tier": null, "profileImageUrl": null }, "createdAt": "", "updatedAt": "" }""");
        using SolvedAPI api = handler.CreateClient(accessToken: "t");

        var result = await api.UpdateBookmarkFolderAsync("my_list", new SolvedBookmarkFolderUpdate { Name = "새 이름", Color = SolvedFolderColor.Teal });

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Patch, request.Method);
        Assert.Equal("application/json", request.ContentType);
        Assert.Equal("""{"name":"새 이름","color":"teal"}""", request.Body);
        Assert.Equal(SolvedFolderColor.Red, result.GetResultOrThrow().Color);
    }

    [Fact]
    public async Task No_content_responses_become_true()
    {
        StubHandler handler = new(_ => new HttpResponseMessage(HttpStatusCode.NoContent));
        using SolvedAPI api = handler.CreateClient(accessToken: "t");

        var result = await api.DeleteBookmarkFolderAsync("my_list");

        Assert.True(result.IsSuccess);
        Assert.True(result.Result);
        Assert.Equal(HttpMethod.Delete, handler.Requests[0].Method);
    }

    [Fact]
    public async Task Network_failures_are_returned_not_thrown()
    {
        StubHandler handler = new(_ => throw new HttpRequestException("connection refused"));
        using SolvedAPI api = handler.CreateClient();

        var result = await api.GetUserAsync("jyunni");

        Assert.False(result.IsSuccess);
        Assert.IsType<HttpRequestException>(result.Exception);
        Assert.Null(result.APIException);
        await Assert.ThrowsAsync<HttpRequestException>(async () => (await api.GetUserAsync("jyunni")).GetResultOrThrow());
    }

    [Fact]
    public async Task Caller_cancellation_is_thrown()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, UserJson);
        using SolvedAPI api = handler.CreateClient();
        using CancellationTokenSource cts = new();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => api.GetUserAsync("jyunni", cts.Token));
    }

    [Fact]
    public async Task Invalid_arguments_throw()
    {
        using SolvedAPI api = StubHandler.Json(HttpStatusCode.OK, "{}").CreateClient();

        await Assert.ThrowsAsync<ArgumentException>(() => api.GetUserAsync(""));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => api.GetAchievementItemAsync(SolvedItemKind.Unknown, "x"));
        Assert.Throws<ArgumentException>(() => new SolvedAPI(" ", "secret"));
    }

    [Fact]
    public async Task Unknown_enum_values_from_the_server_become_Unknown()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, """
            { "id": 1, "problemId": 1000, "subId": "", "level": 1, "userStatus": "something-new",
              "tags": [], "titles": [{ "language": "ko", "title": "A+B", "isOriginal": true, "latex": false, "html": false }] }
            """);
        using SolvedAPI api = handler.CreateClient();

        var problem = (await api.GetProblemAsync(1000)).GetResultOrThrow();

        Assert.Equal(SolvedUserStatus.Unknown, problem.UserStatus);
        Assert.Equal("A+B", problem.GetTitle("en"));
    }
}
