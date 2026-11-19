using System.Net;
using System.Text.Json;
using AcNET.Tests.Infrastructure;

namespace AcNET.Tests;

public class ErrorTests
{
    [Fact]
    public async Task Parses_the_standard_error_body()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.Unauthorized, """
            { "error": { "code": "SIGN_IN_REQUIRED", "message": "Query is too long.", "details": { "limit": 100, "kind": "length", "strict": true } } }
            """);
        using SolvedAPI api = handler.CreateClient();

        var result = await api.GetSearchProblemAsync(new string('a', 200));

        SolvedAPIException ex = Assert.IsType<SolvedAPIException>(result.Exception);
        Assert.Same(ex, result.APIException);
        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
        Assert.Equal(SolvedErrorCode.SignInRequired, ex.ErrorCode);
        Assert.Equal("Query is too long.", ex.ErrorMessage);
        Assert.Equal(JsonValueKind.Number, ex.ErrorDetails["limit"].ValueKind);
        Assert.Equal("100", ex.GetDetail("limit"));
        Assert.Equal("length", ex.GetDetail("kind"));
        Assert.Equal("true", ex.GetDetail("strict"));
        Assert.Null(ex.GetDetail("missing"));
        Assert.Null(ex.ResponseBody);
        Assert.Equal(HttpMethod.Get, ex.RequestMethod);
        Assert.StartsWith("https://solved.ac/api/v4/problems?", ex.RequestUrl);
        Assert.Contains("[SIGN_IN_REQUIRED] Query is too long.", ex.Message);
        Assert.Contains("401", ex.Message);
    }

    [Fact]
    public async Task Parses_errors_without_details()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.Forbidden, """{ "error": { "code": "INSUFFICIENT_SCOPE", "message": "Needs solves." } }""");
        using SolvedAPI api = handler.CreateClient(accessToken: "t");

        var result = await api.GetMyTagProgressAsync();

        Assert.Equal(SolvedErrorCode.InsufficientScope, result.APIException!.ErrorCode);
        Assert.Empty(result.APIException.ErrorDetails);
    }

    [Fact]
    public async Task Parses_oauth_errors()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.BadRequest, """{ "error": "invalid_grant", "error_description": "The code has expired." }""");
        using SolvedAPI api = handler.CreateClient();

        var result = await api.ExchangeCodeAsync("code", "https://example.com/cb", "verifier");

        Assert.Equal(SolvedErrorCode.InvalidGrant, result.APIException!.ErrorCode);
        Assert.Equal("The code has expired.", result.APIException.ErrorMessage);
        Assert.Null(result.APIException.ResponseBody);
    }

    [Fact]
    public async Task Non_json_errors_keep_the_body()
    {
        StubHandler handler = new(_ => new HttpResponseMessage(HttpStatusCode.Forbidden)
        {
            Content = new StringContent("<html><title>Attention Required! | Cloudflare</title></html>"),
        });
        using SolvedAPI api = handler.CreateClient();

        var result = await api.GetUserAsync("jyunni");

        Assert.Equal(SolvedErrorCode.Unknown, result.APIException!.ErrorCode);
        Assert.Null(result.APIException.ErrorMessage);
        Assert.Contains("Cloudflare", result.APIException.ResponseBody);
    }

    [Fact]
    public async Task Rate_limited_responses_expose_retry_after()
    {
        StubHandler handler = StubHandler.Json((HttpStatusCode)429, """{ "error": { "code": "RATE_LIMITED", "message": "Slow down." } }""", r =>
        {
            r.Headers.RetryAfter = new System.Net.Http.Headers.RetryConditionHeaderValue(TimeSpan.FromSeconds(5));
            r.Headers.Add("X-RateLimit-Limit", "600");
            r.Headers.Add("X-RateLimit-Remaining", "0");
        });
        using SolvedAPI api = handler.CreateClient();

        var result = await api.GetUserAsync("jyunni");

        Assert.True(result.APIException!.IsRateLimited);
        Assert.Equal(TimeSpan.FromSeconds(5), result.APIException.RetryAfter);
        Assert.Equal(new SolvedRateLimit(600, 0), result.APIException.RateLimit);
        Assert.Equal(new SolvedRateLimit(600, 0), result.RateLimit);
    }

    [Fact]
    public async Task GetResultOrThrow_rethrows_the_api_exception()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.NotFound, """{ "error": { "code": "NOT_FOUND", "message": "User not found." } }""");
        using SolvedAPI api = handler.CreateClient();

        var result = await api.GetUserAsync("nobody");

        Assert.False(result.IsSuccess);
        Assert.Null(result.Result);
        var ex = Assert.Throws<SolvedAPIException>(() => result.GetResultOrThrow());
        Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        Assert.Equal("NOT_FOUND", ex.ErrorCode);
    }
}
