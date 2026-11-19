using System.Net;
using System.Text.Json;
using AcNET.Achievement;
using AcNET.Tests.Infrastructure;
using Xunit.Abstractions;

namespace AcNET.Tests;

/// <summary>
/// 환경 변수 SOLVEDAC_CLIENT_ID / SOLVEDAC_CLIENT_SECRET 가 있을 때만 실행되는 실제 solved.ac API 테스트입니다.
/// </summary>
public sealed class LiveFactAttribute : FactAttribute
{
    public LiveFactAttribute()
    {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SOLVEDAC_CLIENT_ID"))
            || string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SOLVEDAC_CLIENT_SECRET")))
            Skip = "SOLVEDAC_CLIENT_ID / SOLVEDAC_CLIENT_SECRET 환경 변수가 없어 실제 API 테스트를 건너뜁니다.";
    }
}

/// <summary>
/// 앱 권한(사용자 토큰 없이)으로 부를 수 있는 모든 엔드포인트를 실제 서버에 요청합니다.
/// 실제로 쓰는 경로(소스 생성, 모르는 속성 무시)로는 모두 성공해야 하고,
/// 엄격한 경로(모르는 속성 금지, nullable 검사)에서 걸리는 차이는 문서와 실제 응답이 다른 곳으로 보고합니다.
/// </summary>
public class LiveApiTests(ITestOutputHelper output)
{
    readonly List<string> failures = [];
    readonly List<string> differences = [];
    SolvedAPI api = null!;
    SolvedAPI strict = null!;

    [LiveFact]
    public async Task Every_public_endpoint_works_against_the_real_server()
    {
        string id = Environment.GetEnvironmentVariable("SOLVEDAC_CLIENT_ID")!;
        string secret = Environment.GetEnvironmentVariable("SOLVEDAC_CLIENT_SECRET")!;
        using SolvedAPI production = new(id, secret);
        using SolvedAPI strictClient = new(id, secret);
        strictClient.ReadTypeInfo = SpecServer.StrictOptions.GetTypeInfo;
        api = production;
        strict = strictClient;

        const string handle = "jyunni";
        DateTime today = DateTime.UtcNow.Date;

        // 사이트
        await Run("health", a => a.GetHealthAsync());
        await Run("site/stats", a => a.GetSiteStatsAsync());
        await Run("judges", a => a.GetJudgesAsync());

        // 사용자
        var user = await Run("users/{handle}", a => a.GetUserAsync(handle));
        await Run("users/{handle} (en)", a => { a.Language = "en"; return a.GetUserAsync(handle); });
        api.Language = strict.Language = null;
        await Run("activity/days", a => a.GetUserActivityDaysAsync(handle, today.AddDays(-60), today));
        var feed = await Run("activity/feed", a => a.GetUserActivityFeedAsync(handle, limit: 20));
        if (feed?.NextCursor is string cursor)
            await Run("activity/feed (before)", a => a.GetUserActivityFeedAsync(handle, before: cursor, limit: 5));
        await Run("contributions", a => a.GetUserContributionsAsync(handle, limit: 20));
        await Run("reputation", a => a.GetUserReputationAsync(handle, SolvedHistoryRange.All));
        await Run("reputation/log", a => a.GetUserReputationLogAsync(handle));
        await Run("rating/history", a => a.GetUserRatingHistoryAsync(handle, SolvedHistoryRange.All));

        // 태그
        var tags = await Run("tags", a => a.GetTagListAsync());
        await Run("tags/{key}", a => a.GetTagAsync("dp"));
        var rankedTags = await Run("rankings/tags", a => a.GetRankedTagListAsync());
        string? rankedTag = rankedTags?.FirstOrDefault()?.Key;
        if (rankedTag is not null)
        {
            await Run("rankings/tags/{key}", a => a.GetTagRankingAsync(rankedTag));
            await Run("users/{handle}/tags/{key}/histogram", a => a.GetUserTagHistogramAsync(handle, rankedTag));
        }

        // 문제
        await Run("problems/{id}", a => a.GetProblemAsync(1000));
        await Run("problems (search)", a => a.GetSearchProblemAsync("tier:g5..g1 #dp", 1, SolvedProblemSort.Level, SolvedSortDirection.Descending));
        await Run("problems (random sort)", a => a.GetSearchProblemAsync("#math", 1, SolvedProblemSort.Random));
        await Run("problems (bad query)", a => a.GetSearchProblemAsync("tier:zz #nonexistent-tag-xyz"));
        await Run("search/random-problem", a => a.GetRandomProblemAsync("tier:b5"));
        await Run("search/analyze", a => a.GetSearchAnalysisAsync("tier:zz (unclosed"));
        await Run("search/suggest", a => a.GetSearchSuggestionAsync("tag", "d"));
        await Run("problems/{id}/reports", a => a.GetProblemReportsAsync(1000));
        await Run("achievements/for-problem/{id}", a => a.GetProblemAchievementsAsync(1000));

        // 랭킹, 소속
        var ranking = await Run("rankings/users", a => a.GetUserRankingAsync());
        await Run("rankings/users (solved)", a => a.GetUserRankingAsync(2, SolvedUserRankingSort.Solved));
        await Run("rankings/users (contribution)", a => a.GetContributionRankingAsync());
        await Run("rankings/users/search", a => a.GetSearchUserAsync("jyun"));
        var regions = await Run("rankings/regions", a => a.GetRegionListAsync());
        await Run("rankings/regions/{cc}", a => a.GetRegionRankingAsync(regions?.FirstOrDefault()?.CountryCode ?? "kr"));
        var affiliations = await Run("rankings/affiliations", a => a.GetAffiliationListAsync());
        long? affiliationId = affiliations?.FirstOrDefault()?.AffiliationId;
        if (affiliationId is long aid)
        {
            await Run("affiliations/{id}", a => a.GetAffiliationAsync(aid));
            await Run("rankings/affiliations/{id}", a => a.GetInAffiliationRankingAsync(aid));
        }
        await Run("rankings/affiliation-board", a => a.GetAffiliationRankingAsync());
        await Run("rankings/affiliation-board (university)", a => a.GetAffiliationRankingAsync(1, SolvedAffiliationType.University));

        // 업적
        await Run("achievements/summary", a => a.GetUserAchievementSummaryAsync(handle));
        var stand = await Run("badge-stand", a => a.GetUserBadgeStandAsync(handle));
        var badges = await Run("collection (badge)", a => a.GetUserCollectionAsync(handle));
        var backgrounds = await Run("collection (background)", a => a.GetUserCollectionAsync(handle, SolvedItemKind.Background, SolvedCollectionSort.Rarity));
        string? rival = ranking?.Items.FirstOrDefault(u => u.Handle != handle)?.Handle;
        await Run("users/{handle}/achievements", a => a.GetUserAchievementsAsync(handle, rival));
        await Run("achievements", a => a.GetAchievementListAsync());
        string? badgeKey = stand?.Badges.FirstOrDefault()?.Key ?? badges?.Items.FirstOrDefault()?.Key;
        if (badgeKey is not null)
        {
            await Run("items/badge/{key}", a => a.GetBadgeAsync(badgeKey));
            await Run("items/badge/{key}/holders", a => a.GetAchievementItemHoldersAsync(SolvedItemKind.Badge, badgeKey, 1, SolvedHolderSort.First));
        }
        string? backgroundKey = user?.Background?.Key ?? backgrounds?.Items.FirstOrDefault()?.Key;
        if (backgroundKey is not null)
            await Run("items/background/{key}", a => a.GetBackgroundAsync(backgroundKey));

        // 출처
        var groups = await Run("sources", a => a.GetSourceGroupListAsync());
        long? groupId = groups?.FirstOrDefault()?.Id;
        if (groupId is long gid)
        {
            var group = await Run("sources/groups/{id}", a => a.GetSourceGroupAsync(gid));
            long? contestId = group?.Contests.FirstOrDefault()?.Id;
            if (contestId is null && group?.Groups.FirstOrDefault()?.Id is long childId)
                contestId = (await Run("sources/groups/{id} (child)", a => a.GetSourceGroupAsync(childId)))?.Contests.FirstOrDefault()?.Id;
            if (contestId is long cid)
                await Run("sources/contests/{id}", a => a.GetSourceContestAsync(cid));
        }
        await RunAllowing404("sources/oj/boj/groups/{native}", a => a.FindSourceGroupIdAsync("boj", "1"));
        await RunAllowing404("sources/oj/boj/contests/{native}", a => a.FindSourceContestIdAsync("boj", "1"));

        // 북마크
        var folders = await Run("bookmarks", a => a.GetBookmarkFoldersAsync());
        var folder = folders?.Folders.FirstOrDefault();
        if (folder is not null)
        {
            await Run("users/{handle}/bookmarks", a => a.GetUserBookmarkFoldersAsync(folder.Owner.Handle));
            await Run("users/{handle}/bookmarks/{slug}", a => a.GetUserBookmarkFolderAsync(folder.Owner.Handle, folder.Slug));
        }

        // 공지, 도움말, 정책
        var notices = await Run("posts/notice", a => a.GetNoticeListAsync());
        string? noticeSlug = notices?.Items.FirstOrDefault()?.Path.Split('/').Last();
        if (noticeSlug is not null)
            await Run("posts/notice/{slug}", a => a.GetNoticeAsync(noticeSlug));
        var tree = await Run("posts/help", a => a.GetHelpTreeAsync());
        string? helpPath = Flatten(tree ?? []).FirstOrDefault(n => n.HasPage)?.Path;
        if (helpPath is not null)
            await Run("posts/help/page", a => a.GetHelpPageAsync(helpPath));
        if (helpPath is not null)
            await RunAllowing404("help/{slug}", a => a.GetHelpArticleAsync(helpPath.Split('/').Last()));
        var policy = await Run("policies/{slug}", a => a.GetPolicyAsync(SolvedPolicyKind.Tos));
        string? label = policy?.Versions.FirstOrDefault()?.Label;
        if (label is not null)
            await Run("policies/{slug}/{ref}", a => a.GetPolicyAsync(SolvedPolicyKind.Tos, label));
        await Run("policies/developer_terms", a => a.GetPolicyAsync(SolvedPolicyKind.DeveloperTerms));

        // 에러 응답 형식
        await ExpectError("users/{handle} (없는 사용자)", a => a.GetUserAsync("no-such-user-acnet-xyz"), HttpStatusCode.NotFound, SolvedErrorCode.NotFound);
        await ExpectError("problems/{id}/votes (로그인 필요)", a => a.GetProblemVotesAsync(1000), HttpStatusCode.Unauthorized, SolvedErrorCode.Unauthenticated);
        await ExpectError("me (사용자 토큰 필요)", a => a.GetMeAsync(), HttpStatusCode.Unauthorized, SolvedErrorCode.UserTokenRequired);
        await ExpectError("account/settings (사용자 토큰 필요)", a => a.GetAccountSettingsAsync(), HttpStatusCode.Unauthorized, SolvedErrorCode.UserTokenRequired);
        await ExpectError("oauth/token (잘못된 코드)", a => a.ExchangeCodeAsync("invalid-code", "https://example.com/cb", OAuth.SolvedPkce.Create()), HttpStatusCode.BadRequest, SolvedErrorCode.InvalidGrant);

        output.WriteLine("");
        output.WriteLine($"== 문서와 다른 실제 응답 ({differences.Count}) ==");
        foreach (string d in differences)
            output.WriteLine(d);
        output.WriteLine($"== 실패 ({failures.Count}) ==");
        foreach (string f in failures)
            output.WriteLine(f);
        Assert.Empty(failures);
    }

    async Task<T?> Run<T>(string name, Func<SolvedAPI, Task<SolvedResult<T>>> call)
    {
        var result = await call(api);
        if (!result.IsSuccess)
        {
            failures.Add($"{name}: {result.Exception!.GetType().Name}: {result.Exception.Message}");
            output.WriteLine($"FAIL {name}");
            return default;
        }
        var strictResult = await call(strict);
        if (!strictResult.IsSuccess)
            differences.Add($"{name}: {Describe(strictResult.Exception!)}");
        output.WriteLine($"{(strictResult.IsSuccess ? "ok  " : "diff")} {name} (남은 요청 한도: {result.RateLimit?.Remaining})");
        return result.Result;
    }

    async Task RunAllowing404<T>(string name, Func<SolvedAPI, Task<SolvedResult<T>>> call)
    {
        var result = await call(api);
        if (result.APIException is { StatusCode: HttpStatusCode.NotFound } e)
        {
            output.WriteLine($"404  {name} [{e.ErrorCode}] {e.ErrorMessage}");
            return;
        }
        await Run(name, call);
    }

    async Task ExpectError<T>(string name, Func<SolvedAPI, Task<SolvedResult<T>>> call, HttpStatusCode status, string? code = null)
    {
        var result = await call(api);
        var e = result.APIException;
        bool ok = e is not null && e.StatusCode == status && e.ErrorCode != SolvedErrorCode.Unknown && e.ResponseBody is null && (code is null || e.ErrorCode == code);
        if (!ok)
            failures.Add($"{name}: 기대한 에러가 아님 ({result.Exception?.Message ?? "성공"})");
        output.WriteLine($"{(ok ? "err " : "FAIL")} {name} → {(int?)e?.StatusCode} [{e?.ErrorCode}] {e?.ErrorMessage}");
    }

    static string Describe(Exception ex) => ex is JsonException json ? $"{json.Message}" : $"{ex.GetType().Name}: {ex.Message}";

    static IEnumerable<Post.SolvedHelpTreeNode> Flatten(IEnumerable<Post.SolvedHelpTreeNode> nodes)
        => nodes.SelectMany(n => Flatten(n.Children).Prepend(n));
}
