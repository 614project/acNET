# acNET
[![](https://img.shields.io/nuget/v/acNET)](https://www.nuget.org/packages/acNET)

solved.ac API를 다루기 위한 C# 래퍼 라이브러리입니다.<br>
1.0 부터는 solved.ac 공식 API v4 ([API 문서](https://backstage.solved.ac/developer/docs))를 사용합니다.<br>

## 준비
solved.ac API v4 는 모든 요청에 애플리케이션 인증이 필요합니다.
[solved.ac Backstage](https://backstage.solved.ac/developer)에서 애플리케이션을 등록하고 **클라이언트 ID** 와 **클라이언트 시크릿**을 받아주세요.

> 클라이언트 시크릿이 노출되지 않도록 서버 등 신뢰할 수 있는 환경에서만 사용하세요.

## 예제
```cs
using AcNET;

using SolvedAPI api = new("CLIENT_ID", "CLIENT_SECRET");

var user = await api.GetUserAsync("jyunni");
if (user.IsSuccess)
    Console.WriteLine($"{user.Result!.Handle}: {user.Result.Rating} ({user.Result.GetTierName})");
else
    Console.WriteLine(user.Exception);

// 실패하면 예외를 던지게 할 수도 있습니다.
var problem = (await api.GetProblemAsync(1000)).GetResultOrThrow();
Console.WriteLine(problem.GetTitle("ko"));
```

### 에러 처리
모든 API 는 예외를 던지는 대신 `SolvedResult<T>` 를 돌려줍니다.
solved.ac 가 돌려준 에러(`{ "error": { "code", "message", "details" } }`)는 `SolvedAPIException` 으로 담깁니다.

```cs
var result = await api.GetSearchProblemAsync("tier:g5..g1 #dp");
if (result.APIException is { } e)
{
    Console.WriteLine(e.StatusCode);   // 401
    Console.WriteLine(e.ErrorCode);    // SIGN_IN_REQUIRED (SolvedErrorCode 참고)
    Console.WriteLine(e.ErrorMessage); // 서버가 보낸 설명
    Console.WriteLine(e.GetDetail("limit"));
    Console.WriteLine(e.RetryAfter);   // 429 일 때 기다려야 하는 시간
}
Console.WriteLine(result.RateLimit);   // X-RateLimit-Limit / Remaining
```

### 사용자 로그인 (OAuth 2.0 + PKCE)
사용자 대신 동작하는 API(설정, 북마크 수정 등)는 사용자 접근 토큰이 필요합니다.

```cs
using AcNET.OAuth;

// 1. 인가 페이지로 보내기 (pkce.CodeVerifier 와 state 는 세션에 보관)
SolvedPkce pkce = SolvedPkce.Create();
string state = SolvedPkce.CreateState();
Uri url = api.GetAuthorizeUrl("https://example.com/callback", new[] { SolvedScope.Solves, SolvedScope.OfflineAccess }, state, pkce);

// 2. 돌아온 code 를 토큰으로 교환
SolvedToken token = (await api.ExchangeCodeAsync(code, "https://example.com/callback", pkce)).GetResultOrThrow();

// 3. 토큰으로 요청 (여러 사용자를 다룬다면 WithAccessToken 을 사용하세요)
using SolvedAPI userApi = api.WithAccessToken(token.AccessToken);
var progress = await userApi.GetMyTagProgressAsync();

// 4. 1시간 뒤 만료되면 refresh token 으로 갱신 (refresh token 은 한 번만 쓸 수 있습니다)
token = (await api.RefreshTokenAsync(token.RefreshToken!)).GetResultOrThrow();
```

## 사용
.NET Standard 2.1 라이브러리입니다.
즉, .NET 5 (C# 9.0) 이상의 모든 프로젝트에서
최신 버전의 NuGet 을 추가하여 사용하시면 됩니다!

**Native AOT 와 트리밍**에서도 쓸 수 있습니다. 모든 Json 처리는 리플렉션 없이 System.Text.Json 소스 생성으로 이루어집니다.
CI 에서 `tests/acNET.AotProbe` 를 Native AOT 로 게시해서 트리밍/AOT 경고가 하나라도 나오면 실패하도록 확인합니다.

### 0.9 에서 옮겨오기
- `new SolvedAPI()` → `new SolvedAPI(clientId, clientSecret)` (쿠키 `solvedacToken` 대신 OAuth 토큰을 사용합니다)
- 동기 함수는 모두 삭제되었습니다. `...Async` 함수를 사용해주세요.
- 언어 지정(`Language`)은 헤더 대신 `lang` 쿼리로 보내집니다.
- organization 은 solved.ac 용어에 맞춰 affiliation 으로 바뀌었습니다. (`GetAffiliationRankingAsync`, `GetInAffiliationRankingAsync`)
- API 에 넘기는 날짜는 `DateTime` 입니다. (날짜 부분만 사용합니다)
- v4 에 없는 기능(코인/상점, 클래스 목록, 레벨 목록, 클래스/스트릭 랭킹, 아레나, 상위 100문제, 이벤트, 여러 문제 한 번에 조회)은 삭제되었습니다.
- 모델이 v4 응답에 맞게 바뀌었습니다. 예를 들어 사용자의 `OverRating` 은 없어졌고, 문제 제목은 `GetTitle("ko")` 로 가져옵니다.

## 디버깅
.NET 10 SDK 만 있으면 됩니다! (`global.json` 으로 지정되어 있습니다)

```sh
dotnet build
dotnet test tests/acNET.Tests
```

테스트는 `tests/acNET.Tests/Fixtures/solvedac-v4-openapi.json` 에 저장된 OpenAPI 문서로 가짜 서버를 만들어,
모든 엔드포인트의 요청(경로, 쿼리, 본문)과 응답 모델이 문서와 정확히 맞는지 확인합니다.
solved.ac 가 API 를 바꾸면 이 파일을 `https://backstage.solved.ac/api/v4/openapi` 에서 다시 받아 테스트를 돌려보세요.

`SOLVEDAC_CLIENT_ID`, `SOLVEDAC_CLIENT_SECRET` 환경 변수를 설정하면 실제 solved.ac 서버에도 요청하는 테스트(`LiveApiTests`)가 함께 실행됩니다.
앱 권한으로 부를 수 있는 모든 엔드포인트를 호출하고, 실제 응답이 문서와 다른 곳이 있으면 알려줍니다. 환경 변수가 없으면 건너뜁니다.
