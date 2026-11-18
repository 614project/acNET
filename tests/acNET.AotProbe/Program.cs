using System.Net;
using System.Text;
using AcNET;
using AcNET.Bookmark;
using AcNET.OAuth;
using AcNET.Problem;

// 실제 서버 대신 정해둔 Json 을 돌려주는 가짜 서버로 acNET 의 주요 경로를 AOT 환경에서 실행해 봅니다.
int failures = 0;
void Check(bool ok, string what)
{
    if (!ok) { failures++; Console.WriteLine("FAILED: " + what); }
}

FakeServer server = new();
using SolvedAPI api = new(new SolvedAPIOptions("id", "secret") { AccessToken = "token", Language = "ko" }, new HttpClient(server));

var user = await api.GetUserAsync("jyunni");
Check(user.IsSuccess && user.Result!.Handle == "jyunni" && user.Result.Tier == 31 && user.Result.Affiliations[0].Type == SolvedAffiliationType.University, "user " + user.Exception);

var problem = await api.GetProblemAsync(1000);
Check(problem.IsSuccess && problem.Result!.UserStatus == SolvedUserStatus.Solved && problem.Result.GetTitle("ko") == "A+B" && problem.Result.Tags[0].Key == "math", "problem " + problem.Exception);

var tags = await api.GetTagListAsync();
Check(tags.IsSuccess && tags.Result![0].GetName("ko") == "수학", "tags " + tags.Exception);

var ranking = await api.GetUserRankingAsync();
Check(ranking.IsSuccess && ranking.Result!.Items[0].Rank == 1 && ranking.Result.Me is null, "ranking " + ranking.Exception);

var vote = await api.GetMyProblemVoteAsync(1000);
Check(vote.IsSuccess && vote.Result is null, "vote " + vote.Exception);

var folder = await api.CreateBookmarkFolderAsync(new SolvedBookmarkFolderCreate("my_list", "내 목록") { Color = SolvedFolderColor.Blue });
Check(folder.IsSuccess && folder.Result!.Visibility == SolvedFolderVisibility.Public, "folder " + folder.Exception);
Check(server.LastBody == """{"slug":"my_list","name":"내 목록","color":"blue"}""", "request body " + server.LastBody);

var added = await api.AddBookmarkProblemsAsync("my_list", [1000, 1001]);
Check(added.IsSuccess && server.LastBody == """{"problemIds":[1000,1001]}""", "problem ids body " + server.LastBody + " " + added.Exception);

var deleted = await api.DeleteBookmarkFolderAsync("my_list");
Check(deleted.IsSuccess && deleted.Result, "delete " + deleted.Exception);

var token = await api.ExchangeCodeAsync("code", "https://example.com/cb", SolvedPkce.Create());
Check(token.IsSuccess && token.Result!.HasScope(SolvedScope.Solves), "token " + token.Exception);

var error = await api.GetUserAsync("missing");
Check(error.APIException is { ErrorCode: "NOT_FOUND", StatusCode: HttpStatusCode.NotFound } e && e.GetDetail("handle") == "missing", "error " + error.Exception);

Check(user.Result!.ToJson().Contains("\"handle\":\"jyunni\""), "ToJson");
Check(SolvedConverter.ParsingJson<SolvedTag>("""{"key":"dp","names":{"ko":"다이나믹 프로그래밍"}}""")!.GetName("ko") == "다이나믹 프로그래밍", "ParsingJson");
Check(SolvedConverter.ParsingJsonList<SolvedTag>("""[{"key":"dp"},{"key":"math"}]""")!.Count == 2, "ParsingJsonList");
Check(SolvedConverter.ParsingJson<SolvedProblem>("""{"userStatus":"brand-new"}""")!.UserStatus == SolvedUserStatus.Unknown, "unknown enum");

Console.WriteLine(failures == 0 ? "AOT OK" : $"AOT FAILURES: {failures}");
return failures;

sealed class FakeServer : HttpMessageHandler
{
    public string? LastBody;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastBody = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
        string path = request.RequestUri!.AbsolutePath;
        (HttpStatusCode status, string? json) = (request.Method.Method, path) switch
        {
            ("GET", "/api/v4/users/missing") => (HttpStatusCode.NotFound, """{"error":{"code":"NOT_FOUND","message":"User not found.","details":{"handle":"missing"}}}"""),
            ("GET", "/api/v4/users/jyunni") => (HttpStatusCode.OK, """{"id":1,"handle":"jyunni","tier":31,"profileImageUrl":null,"bio":null,"rating":3000,"affiliations":[{"id":1,"affiliationId":2,"name":"U","type":"university","logoThumbnailUrl":null,"color":null}],"metadata":{"countryCode":"kr","pronouns":null,"name":null,"nameNative":null,"birthday":null}}"""),
            ("GET", "/api/v4/problems/1000") => (HttpStatusCode.OK, """{"id":1,"problemId":1000,"subId":"","level":1,"userStatus":"solved","titles":[{"language":"ko","title":"A+B","isOriginal":true,"latex":false,"html":false}],"tags":[{"id":1,"key":"math","isMeta":false}],"ojLinks":[]}"""),
            ("GET", "/api/v4/tags") => (HttpStatusCode.OK, """{"tags":[{"id":1,"key":"math","isMeta":false,"parents":[],"names":{"ko":"수학","en":"mathematics"},"aliases":[],"rated":true}]}"""),
            ("GET", "/api/v4/rankings/users") => (HttpStatusCode.OK, """{"me":null,"mePage":null,"total":1,"page":1,"pageSize":50,"items":[{"id":1,"handle":"a","tier":31,"profileImageUrl":null,"rank":1,"rating":3000,"solvedCount":1,"voteCount":0,"reputation":0,"class":10,"classPlus":true,"tagRating":null,"globalRank":null}]}"""),
            ("GET", "/api/v4/problems/1000/votes/me") => (HttpStatusCode.OK, """{"vote":null}"""),
            ("POST", "/api/v4/account/bookmarks") => (HttpStatusCode.Created, """{"slug":"my_list","name":"내 목록","description":"","color":"blue","visibility":"public","problemCount":0,"favoriteCount":0,"solvedCount":null,"owner":{"id":1,"handle":"a","tier":null,"profileImageUrl":null},"createdAt":"","updatedAt":""}"""),
            ("POST", "/api/v4/account/bookmarks/my_list/problems") => (HttpStatusCode.OK, """{"folder":{"slug":"my_list","name":"n","description":"","color":null,"visibility":"private","problemCount":2,"favoriteCount":0,"solvedCount":null,"owner":{"id":1,"handle":"a","tier":null,"profileImageUrl":null},"createdAt":"","updatedAt":""},"changed":2}"""),
            ("DELETE", "/api/v4/account/bookmarks/my_list") => (HttpStatusCode.NoContent, null),
            ("POST", "/api/v4/oauth/token") => (HttpStatusCode.OK, """{"access_token":"at","token_type":"Bearer","expires_in":3600,"scope":"profile solves"}"""),
            _ => throw new InvalidOperationException($"unexpected {request.Method} {path}"),
        };
        HttpResponseMessage response = new(status);
        if (json is not null)
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
        return response;
    }
}
