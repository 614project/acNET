using System.Net;
using System.Web;
using AcNET.OAuth;
using AcNET.Tests.Infrastructure;

namespace AcNET.Tests;

public class OAuthTests
{
    const string TokenJson = """{ "access_token": "at", "token_type": "Bearer", "expires_in": 3600, "refresh_token": "rt", "scope": "profile solves offline.access" }""";

    [Fact]
    public void Pkce_matches_the_rfc_7636_test_vector()
    {
        SolvedPkce pkce = SolvedPkce.FromVerifier("dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk");
        Assert.Equal("E9Melhoa2OwvFrEMTJguCHaoeK1t8URWbuGJSstw-cM", pkce.CodeChallenge);
        Assert.Equal("S256", pkce.CodeChallengeMethod);
    }

    [Fact]
    public void Created_pkce_and_state_are_random_and_valid()
    {
        SolvedPkce a = SolvedPkce.Create(), b = SolvedPkce.Create();
        Assert.Equal(43, a.CodeVerifier.Length);
        Assert.NotEqual(a.CodeVerifier, b.CodeVerifier);
        Assert.Equal(a.CodeChallenge, SolvedPkce.FromVerifier(a.CodeVerifier).CodeChallenge);
        Assert.NotEqual(SolvedPkce.CreateState(), SolvedPkce.CreateState());
        Assert.Throws<ArgumentException>(() => SolvedPkce.FromVerifier("too-short"));
        Assert.Throws<ArgumentException>(() => SolvedPkce.FromVerifier(new string('a', 42) + "!"));
    }

    [Fact]
    public void Builds_the_authorize_url()
    {
        using SolvedAPI api = new("my-client", "secret");
        SolvedPkce pkce = SolvedPkce.FromVerifier("dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk");

        Uri url = api.GetAuthorizeUrl("https://example.com/callback?x=1", [SolvedScope.Solves, SolvedScope.Profile, SolvedScope.OfflineAccess], "state-123", pkce);

        Assert.Equal("https://solved.ac/oauth/authorize", url.GetLeftPart(UriPartial.Path));
        var query = HttpUtility.ParseQueryString(url.Query);
        Assert.Equal("code", query["response_type"]);
        Assert.Equal("my-client", query["client_id"]);
        Assert.Equal("https://example.com/callback?x=1", query["redirect_uri"]);
        Assert.Equal("profile solves offline.access", query["scope"]);
        Assert.Equal("state-123", query["state"]);
        Assert.Equal("E9Melhoa2OwvFrEMTJguCHaoeK1t8URWbuGJSstw-cM", query["code_challenge"]);
        Assert.Equal("S256", query["code_challenge_method"]);
    }

    [Fact]
    public async Task Exchanges_a_code_with_app_credentials_only()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, TokenJson);
        using SolvedAPI api = handler.CreateClient(accessToken: "should-not-be-sent");

        DateTimeOffset before = DateTimeOffset.UtcNow;
        SolvedToken token = (await api.ExchangeCodeAsync("the-code", "https://example.com/cb", "the-verifier")).GetResultOrThrow();

        var request = Assert.Single(handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("https://solved.ac/api/v4/oauth/token", request.Uri.ToString());
        Assert.Equal("application/x-www-form-urlencoded", request.ContentType);
        Assert.False(request.Headers.ContainsKey("authorization"));
        Assert.Equal("client-id", request.Headers["x-client-id"]);
        var form = HttpUtility.ParseQueryString(request.Body!);
        Assert.Equal("authorization_code", form["grant_type"]);
        Assert.Equal("the-code", form["code"]);
        Assert.Equal("https://example.com/cb", form["redirect_uri"]);
        Assert.Equal("the-verifier", form["code_verifier"]);

        Assert.Equal("at", token.AccessToken);
        Assert.Equal("rt", token.RefreshToken);
        Assert.Equal(["profile", "solves", "offline.access"], token.Scopes);
        Assert.True(token.HasScope(SolvedScope.Solves));
        Assert.False(token.HasScope(SolvedScope.Wallet));
        Assert.InRange(token.ExpiresAt, before.AddSeconds(3599), DateTimeOffset.UtcNow.AddSeconds(3601));
        Assert.DoesNotContain("at", token.ToString().Replace("ExpiresAt", "").Replace("HasRefreshToken", ""));
        Assert.Contains("\"access_token\":\"at\"", token.ToJson());
    }

    [Fact]
    public async Task Refreshes_with_optional_narrower_scope()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, TokenJson);
        using SolvedAPI api = handler.CreateClient();

        await api.RefreshTokenAsync("old-refresh");
        await api.RefreshTokenAsync("old-refresh", [SolvedScope.Profile]);

        var first = HttpUtility.ParseQueryString(handler.Requests[0].Body!);
        Assert.Equal("refresh_token", first["grant_type"]);
        Assert.Equal("old-refresh", first["refresh_token"]);
        Assert.Null(first["scope"]);
        Assert.Equal("profile", HttpUtility.ParseQueryString(handler.Requests[1].Body!)["scope"]);
    }

    [Fact]
    public async Task Revokes_a_token()
    {
        StubHandler handler = StubHandler.Json(HttpStatusCode.OK, "{}");
        using SolvedAPI api = handler.CreateClient(accessToken: "at");

        var result = await api.RevokeTokenAsync("at");

        Assert.True(result.GetResultOrThrow());
        Assert.Equal("https://solved.ac/api/v4/oauth/revoke", handler.Requests[0].Uri.ToString());
        Assert.Equal("token=at", handler.Requests[0].Body);
        Assert.False(handler.Requests[0].Headers.ContainsKey("authorization"));
    }
}
