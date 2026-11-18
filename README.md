# acNET
[![](https://img.shields.io/nuget/v/acNET)](https://www.nuget.org/packages/acNET)

[한국어(Korean)](./README-KR.md)

## Introduce
A C# wrapper library for interacting with the solved.ac API.<br>
Since 1.0 it targets the official solved.ac API v4 ([API documentation](https://backstage.solved.ac/developer/docs)).<br>

## Before you start
Every solved.ac API v4 request must be authenticated as an application.
Register an application on [solved.ac Backstage](https://backstage.solved.ac/developer) to get a **client ID** and **client secret**.

> Keep the client secret on a server or another trusted environment.

## Example
```cs
using AcNET;

using SolvedAPI api = new("CLIENT_ID", "CLIENT_SECRET");

var user = await api.GetUserAsync("jyunni");
if (user.IsSuccess)
    Console.WriteLine($"{user.Result!.Handle}: {user.Result.Rating} ({user.Result.GetTierName})");
else
    Console.WriteLine(user.Exception);

// Or throw on failure.
var problem = (await api.GetProblemAsync(1000)).GetResultOrThrow();
Console.WriteLine(problem.GetTitle("en"));
```

### Errors
Every API returns a `SolvedResult<T>` instead of throwing.
Errors returned by solved.ac (`{ "error": { "code", "message", "details" } }`) are exposed as `SolvedAPIException`.

```cs
var result = await api.GetSearchProblemAsync("tier:g5..g1 #dp");
if (result.APIException is { } e)
{
    Console.WriteLine(e.StatusCode);   // 401
    Console.WriteLine(e.ErrorCode);    // SIGN_IN_REQUIRED (see SolvedErrorCode)
    Console.WriteLine(e.ErrorMessage); // message from the server
    Console.WriteLine(e.GetDetail("limit"));
    Console.WriteLine(e.RetryAfter);   // how long to wait on 429
}
Console.WriteLine(result.RateLimit);   // X-RateLimit-Limit / Remaining
```

### Signing users in (OAuth 2.0 + PKCE)
APIs that act for a user (settings, bookmark edits, ...) need a user access token.

```cs
using AcNET.OAuth;

// 1. Send the user to the authorize page (keep pkce.CodeVerifier and state in the session)
SolvedPkce pkce = SolvedPkce.Create();
string state = SolvedPkce.CreateState();
Uri url = api.GetAuthorizeUrl("https://example.com/callback", new[] { SolvedScope.Solves, SolvedScope.OfflineAccess }, state, pkce);

// 2. Exchange the returned code for tokens
SolvedToken token = (await api.ExchangeCodeAsync(code, "https://example.com/callback", pkce)).GetResultOrThrow();

// 3. Call APIs as the user (use WithAccessToken when serving many users)
using SolvedAPI userApi = api.WithAccessToken(token.AccessToken);
var progress = await userApi.GetMyTagProgressAsync();

// 4. Refresh after an hour (a refresh token can be used only once)
token = (await api.RefreshTokenAsync(token.RefreshToken!)).GetResultOrThrow();
```

## Usage
This is a .NET Standard 2.1 library.
That means you can use it in any project targeting .NET 5 (C# 9.0) or higher
by simply adding the latest version from NuGet!

It also works with **Native AOT and trimming**: all JSON goes through System.Text.Json source generation, without reflection.
CI publishes `tests/acNET.AotProbe` with Native AOT and fails on any trim/AOT warning.

### Migrating from 0.9
- `new SolvedAPI()` → `new SolvedAPI(clientId, clientSecret)` (OAuth tokens replace the `solvedacToken` cookie)
- All synchronous methods were removed. Use the `...Async` methods.
- `Language` is sent as the `lang` query parameter instead of a header.
- "organization" is now "affiliation", following solved.ac (`GetAffiliationRankingAsync`, `GetInAffiliationRankingAsync`).
- Dates passed to APIs are `DateTime` (only the date part is used).
- Features that v4 does not have were removed (coins/shop, class list, level list, class/streak rankings, arena, top 100, events, multi-problem lookup).
- Models follow the v4 responses. For example the user's `OverRating` is gone, and problem titles come from `GetTitle("ko")`.

## Debugging
All you need is the .NET 10 SDK! (pinned by `global.json`)

```sh
dotnet build
dotnet test tests/acNET.Tests
```

The tests build a fake server from the OpenAPI document saved at `tests/acNET.Tests/Fixtures/solvedac-v4-openapi.json`
and check that every endpoint's request (path, query, body) and response model match it exactly.
When solved.ac changes the API, download the file again from `https://backstage.solved.ac/api/v4/openapi` and rerun the tests.
