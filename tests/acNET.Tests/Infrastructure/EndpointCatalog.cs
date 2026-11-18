using AcNET.Account;
using AcNET.Bookmark;
using AcNET.OAuth;

namespace AcNET.Tests.Infrastructure;

/// <summary>
/// SolvedAPI 의 모든 공개 API 를 한 번씩 부르는 목록입니다.
/// 기본값이 아닌 인자를 넣어서 쿼리 이름과 열거형 값이 문서와 맞는지도 함께 확인합니다.
/// </summary>
public static class EndpointCatalog
{
    public sealed record Entry(string Label, string Method, Func<SolvedAPI, Task<Exception?>> Call);

    static readonly DateOnly Jan1 = new(2025, 1, 1);
    static readonly DateOnly Dec31 = new(2025, 12, 31);

    public static IReadOnlyList<Entry> All { get; } =
    [
        // Users
        E(nameof(SolvedAPI.GetMeAsync), api => api.GetMeAsync()),
        E(nameof(SolvedAPI.GetUserAsync), api => api.GetUserAsync("jyunni")),
        E(nameof(SolvedAPI.GetUserTagHistogramAsync), api => api.GetUserTagHistogramAsync("jyunni", "dp")),
        E(nameof(SolvedAPI.GetUserActivityDaysAsync), api => api.GetUserActivityDaysAsync("jyunni", Jan1, Dec31)),
        E(nameof(SolvedAPI.GetUserActivityFeedAsync), api => api.GetUserActivityFeedAsync("jyunni", "cursor", Jan1, Dec31, 50)),
        E(nameof(SolvedAPI.GetUserContributionsAsync), api => api.GetUserContributionsAsync("jyunni", 2, 20, SolvedContributionSort.Helpful, SolvedContributionFilter.Vouched, true)),
        E(nameof(SolvedAPI.GetUserReputationAsync), api => api.GetUserReputationAsync("jyunni", SolvedHistoryRange.Year1)),
        E(nameof(SolvedAPI.GetUserReputationLogAsync), api => api.GetUserReputationLogAsync("jyunni", new DateOnly(2025, 3, 15))),
        E(nameof(SolvedAPI.GetUserRatingHistoryAsync), api => api.GetUserRatingHistoryAsync("jyunni", SolvedHistoryRange.Days90)),

        // Problems
        E(nameof(SolvedAPI.GetProblemAsync), api => api.GetProblemAsync(1000)),
        E(nameof(SolvedAPI.GetProblemAsync), api => api.GetProblemAsync(1000, "a"), "GetProblemAsync(sub)"),
        E(nameof(SolvedAPI.GetSearchProblemAsync), api => api.GetSearchProblemAsync("tier:g5..g1 #dp", 2, SolvedProblemSort.AverageTry, SolvedSortDirection.Descending, "seed")),
        E(nameof(SolvedAPI.GetRandomProblemAsync), api => api.GetRandomProblemAsync("#dp")),
        E(nameof(SolvedAPI.GetSearchAnalysisAsync), api => api.GetSearchAnalysisAsync("tier:g5")),
        E(nameof(SolvedAPI.GetSearchSuggestionAsync), api => api.GetSearchSuggestionAsync("tag", "d")),
        E(nameof(SolvedAPI.GetProblemVoteSummaryAsync), api => api.GetProblemVoteSummaryAsync(1000)),
        E(nameof(SolvedAPI.GetProblemVotesAsync), api => api.GetProblemVotesAsync(1000, 2, SolvedVoteSort.Weight)),
        E(nameof(SolvedAPI.GetMyProblemVoteAsync), api => api.GetMyProblemVoteAsync(1000)),
        E(nameof(SolvedAPI.GetProblemReportsAsync), api => api.GetProblemReportsAsync(1000, 2)),
        E(nameof(SolvedAPI.GetTagListAsync), api => api.GetTagListAsync()),
        E(nameof(SolvedAPI.GetTagAsync), api => api.GetTagAsync("dp")),
        E(nameof(SolvedAPI.GetMyTagProgressAsync), api => api.GetMyTagProgressAsync()),

        // Rankings
        E(nameof(SolvedAPI.GetUserRankingAsync), api => api.GetUserRankingAsync(2, SolvedUserRankingSort.Solved)),
        E(nameof(SolvedAPI.GetTierRankingAsync), api => api.GetTierRankingAsync()),
        E(nameof(SolvedAPI.GetContributionRankingAsync), api => api.GetContributionRankingAsync()),
        E(nameof(SolvedAPI.GetSearchUserAsync), api => api.GetSearchUserAsync("jy", SolvedUserRankingSort.Contribution)),
        E(nameof(SolvedAPI.GetRivalRankingAsync), api => api.GetRivalRankingAsync()),
        E(nameof(SolvedAPI.GetReverseRivalRankingAsync), api => api.GetReverseRivalRankingAsync()),
        E(nameof(SolvedAPI.GetRegionListAsync), api => api.GetRegionListAsync()),
        E(nameof(SolvedAPI.GetRegionRankingAsync), api => api.GetRegionRankingAsync("kr", 2)),
        E(nameof(SolvedAPI.GetTagRankingAsync), api => api.GetTagRankingAsync("dp", 2)),
        E(nameof(SolvedAPI.GetRankedTagListAsync), api => api.GetRankedTagListAsync()),
        E(nameof(SolvedAPI.GetAffiliationAsync), api => api.GetAffiliationAsync(1)),
        E(nameof(SolvedAPI.GetInAffiliationRankingAsync), api => api.GetInAffiliationRankingAsync(1, 2)),
        E(nameof(SolvedAPI.GetAffiliationListAsync), api => api.GetAffiliationListAsync("seoul")),
        E(nameof(SolvedAPI.GetAffiliationRankingAsync), api => api.GetAffiliationRankingAsync(2, SolvedAffiliationType.HighSchool)),

        // Achievements
        E(nameof(SolvedAPI.GetUserAchievementSummaryAsync), api => api.GetUserAchievementSummaryAsync("jyunni")),
        E(nameof(SolvedAPI.GetUserBadgeStandAsync), api => api.GetUserBadgeStandAsync("jyunni")),
        E(nameof(SolvedAPI.GetUserCollectionAsync), api => api.GetUserCollectionAsync("jyunni", SolvedItemKind.Background, SolvedCollectionSort.Rarity, "event", "summer", 24)),
        E(nameof(SolvedAPI.GetUserAchievementsAsync), api => api.GetUserAchievementsAsync("jyunni", "other")),
        E(nameof(SolvedAPI.GetAchievementListAsync), api => api.GetAchievementListAsync()),
        E(nameof(SolvedAPI.GetProblemAchievementsAsync), api => api.GetProblemAchievementsAsync(1000)),
        E(nameof(SolvedAPI.GetAchievementItemAsync), api => api.GetAchievementItemAsync(SolvedItemKind.Background, "summer")),
        E(nameof(SolvedAPI.GetBadgeAsync), api => api.GetBadgeAsync("ac")),
        E(nameof(SolvedAPI.GetBackgroundAsync), api => api.GetBackgroundAsync("summer")),
        E(nameof(SolvedAPI.GetAchievementItemHoldersAsync), api => api.GetAchievementItemHoldersAsync(SolvedItemKind.Badge, "ac", 2, SolvedHolderSort.First)),

        // Account
        E(nameof(SolvedAPI.GetAccountSettingsAsync), api => api.GetAccountSettingsAsync()),
        E(nameof(SolvedAPI.UpdateAccountSettingsAsync), api => api.UpdateAccountSettingsAsync(new SolvedAccountSettingsUpdate { Theme = "dark", IconWarnRelative = 2, JudgeOrder = ["boj"], ShowOriginalTitle = false })),
        E(nameof(SolvedAPI.GetAccountProfileAsync), api => api.GetAccountProfileAsync()),
        E(nameof(SolvedAPI.GetAccountBackgroundsAsync), api => api.GetAccountBackgroundsAsync()),
        E(nameof(SolvedAPI.GetAccountPendingConsentsAsync), api => api.GetAccountPendingConsentsAsync()),
        E(nameof(SolvedAPI.GetAccountStandingAsync), api => api.GetAccountStandingAsync()),
        E(nameof(SolvedAPI.GetAccountBansAsync), api => api.GetAccountBansAsync()),
        E(nameof(SolvedAPI.GetAccountCollectionAsync), api => api.GetAccountCollectionAsync(SolvedItemKind.Badge, SolvedCollectionSort.Name, null, "a", 0)),
        E(nameof(SolvedAPI.GetAccountItemsAsync), api => api.GetAccountItemsAsync()),
        E(nameof(SolvedAPI.GetAccountWalletAsync), api => api.GetAccountWalletAsync()),
        E(nameof(SolvedAPI.GetAccountWalletHistoryAsync), api => api.GetAccountWalletHistoryAsync(new DateOnly(2025, 11, 1))),
        E(nameof(SolvedAPI.GetMyEmoticonsAsync), api => api.GetMyEmoticonsAsync()),

        // Bookmarks
        E(nameof(SolvedAPI.GetBookmarkFoldersAsync), api => api.GetBookmarkFoldersAsync(2, SolvedBookmarkFolderSort.Problems, "dp")),
        E(nameof(SolvedAPI.GetUserBookmarkFoldersAsync), api => api.GetUserBookmarkFoldersAsync("jyunni", 2)),
        E(nameof(SolvedAPI.GetUserBookmarkFolderAsync), api => api.GetUserBookmarkFolderAsync("jyunni", "my_list", 2, SolvedBookmarkProblemSort.AverageTry, SolvedSortDirection.Descending, "#dp")),
        E(nameof(SolvedAPI.GetAccountBookmarkFoldersAsync), api => api.GetAccountBookmarkFoldersAsync(1000)),
        E(nameof(SolvedAPI.CreateBookmarkFolderAsync), api => api.CreateBookmarkFolderAsync(new SolvedBookmarkFolderCreate { Slug = "my_list", Name = "내 목록", Description = "설명", Color = SolvedFolderColor.Violet, Visibility = SolvedFolderVisibility.Unlisted })),
        E(nameof(SolvedAPI.UpdateBookmarkFolderAsync), api => api.UpdateBookmarkFolderAsync("my_list", new SolvedBookmarkFolderUpdate { Name = "새 이름", Color = SolvedFolderColor.Orange, Visibility = SolvedFolderVisibility.Private })),
        E(nameof(SolvedAPI.DeleteBookmarkFolderAsync), api => api.DeleteBookmarkFolderAsync("my_list")),
        E(nameof(SolvedAPI.AddBookmarkProblemsAsync), api => api.AddBookmarkProblemsAsync("my_list", [1000, 1001])),
        E(nameof(SolvedAPI.RemoveBookmarkProblemsAsync), api => api.RemoveBookmarkProblemsAsync("my_list", [1000])),
        E(nameof(SolvedAPI.UpdateBookmarkProblemAsync), api => api.UpdateBookmarkProblemAsync("my_list", 1000, new SolvedBookmarkProblemUpdate { Note = "메모", After = 1001 })),
        E(nameof(SolvedAPI.ReorderBookmarkFoldersAsync), api => api.ReorderBookmarkFoldersAsync(["b", "a"])),
        E(nameof(SolvedAPI.GetBookmarkFavoritesAsync), api => api.GetBookmarkFavoritesAsync(2)),
        E(nameof(SolvedAPI.AddBookmarkFavoriteAsync), api => api.AddBookmarkFavoriteAsync("jyunni", "my_list")),
        E(nameof(SolvedAPI.RemoveBookmarkFavoriteAsync), api => api.RemoveBookmarkFavoriteAsync("jyunni", "my_list")),

        // Sources
        E(nameof(SolvedAPI.GetSourceGroupListAsync), api => api.GetSourceGroupListAsync()),
        E(nameof(SolvedAPI.GetSourceGroupAsync), api => api.GetSourceGroupAsync(1)),
        E(nameof(SolvedAPI.GetSourceContestAsync), api => api.GetSourceContestAsync(1)),
        E(nameof(SolvedAPI.FindSourceGroupIdAsync), api => api.FindSourceGroupIdAsync("boj", "123")),
        E(nameof(SolvedAPI.FindSourceContestIdAsync), api => api.FindSourceContestIdAsync("boj", "456")),

        // Site / Posts
        E(nameof(SolvedAPI.GetHealthAsync), api => api.GetHealthAsync()),
        E(nameof(SolvedAPI.GetSiteStatsAsync), api => api.GetSiteStatsAsync()),
        E(nameof(SolvedAPI.GetJudgesAsync), api => api.GetJudgesAsync()),
        E(nameof(SolvedAPI.GetNoticeListAsync), api => api.GetNoticeListAsync(2, "event")),
        E(nameof(SolvedAPI.GetNoticeAsync), api => api.GetNoticeAsync("hello-world")),
        E(nameof(SolvedAPI.GetHelpTreeAsync), api => api.GetHelpTreeAsync()),
        E(nameof(SolvedAPI.GetHelpPageAsync), api => api.GetHelpPageAsync("getting-started/first-steps")),
        E(nameof(SolvedAPI.GetHelpArticleAsync), api => api.GetHelpArticleAsync("rating")),
        E(nameof(SolvedAPI.GetPolicyAsync), api => api.GetPolicyAsync(SolvedPolicyKind.Tos)),
        E(nameof(SolvedAPI.GetPolicyAsync), api => api.GetPolicyAsync(SolvedPolicyKind.DeveloperTerms, "v3"), "GetPolicyAsync(version)"),

        // OAuth
        E(nameof(SolvedAPI.ExchangeCodeAsync), api => api.ExchangeCodeAsync("code", "https://example.com/callback", SolvedPkce.Create())),
        E(nameof(SolvedAPI.ExchangeCodeAsync), api => api.ExchangeCodeAsync("code", "https://example.com/callback", SolvedPkce.Create().CodeVerifier), "ExchangeCodeAsync(verifier)"),
        E(nameof(SolvedAPI.RefreshTokenAsync), api => api.RefreshTokenAsync("refresh", [SolvedScope.Profile, SolvedScope.Solves])),
        E(nameof(SolvedAPI.RevokeTokenAsync), api => api.RevokeTokenAsync("token")),
    ];

    public static Entry Get(string label) => All.Single(e => e.Label == label);

    public static IEnumerable<object[]> Labels() => All.Select(e => new object[] { e.Label });

    static Entry E<T>(string method, Func<SolvedAPI, Task<SolvedResult<T>>> call, string? label = null)
        => new(label ?? method, method, async api => (await call(api)).Exception);
}
