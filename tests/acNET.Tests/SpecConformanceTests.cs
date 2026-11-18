using System.Reflection;
using AcNET.Tests.Infrastructure;

namespace AcNET.Tests;

/// <summary>
/// 저장해둔 solved.ac v4 OpenAPI 문서와 acNET 이 맞는지 확인합니다.
/// </summary>
public class SpecConformanceTests
{
    public static IEnumerable<object[]> Calls() => EndpointCatalog.Labels();

    [Theory]
    [MemberData(nameof(Calls))]
    public async Task Response_with_every_property_deserializes_strictly(string label)
        => await AssertConforms(label, SampleMode.Full);

    [Theory]
    [MemberData(nameof(Calls))]
    public async Task Response_with_only_required_properties_and_nulls_deserializes_strictly(string label)
        => await AssertConforms(label, SampleMode.Minimal);

    static async Task AssertConforms(string label, SampleMode mode)
    {
        SpecServer server = new(mode);
        using SolvedAPI api = server.CreateClient();

        Exception? exception = await EndpointCatalog.Get(label).Call(api);

        Assert.Empty(server.Violations);
        Assert.Single(server.Hits);
        Assert.Null(exception);
    }

    [Fact]
    public async Task Every_operation_in_the_document_is_implemented()
    {
        SpecServer server = new(SampleMode.Full);
        using SolvedAPI api = server.CreateClient();
        foreach (EndpointCatalog.Entry entry in EndpointCatalog.All)
            await entry.Call(api);

        string[] missing = OpenApiSpec.Instance.Operations
            .Select(o => o.Key)
            .Except(server.Hits)
            .Order()
            .ToArray();
        Assert.Empty(missing);
        Assert.Equal(84, OpenApiSpec.Instance.Operations.Count); // 80개 경로, 84개 operation
    }

    // 아래 두 테스트는 엄격한 역직렬화 설정이 실제로 잘못된 모델을 잡아내는지 확인하는 대조군입니다.
    sealed class TooSmallUser
    {
        public long Id { get; init; }
        public string Handle { get; init; } = string.Empty;
    }

    sealed class WrongNullabilityUser
    {
        public long Id { get; init; }
        public string Handle { get; init; } = string.Empty;
        public string Bio { get; init; } = string.Empty; // 문서에서는 string | null
    }

    [Fact]
    public void Strict_options_reject_a_model_missing_properties()
    {
        var schema = OpenApiSpec.Instance.Match("GET", "/api/v4/users/x")!.SuccessResponse().Schema!;
        string json = SampleGenerator.Create(OpenApiSpec.Instance, schema, SampleMode.Full)!.ToJsonString();
        Assert.ThrowsAny<System.Text.Json.JsonException>(() => System.Text.Json.JsonSerializer.Deserialize<TooSmallUser>(json, SpecServer.StrictOptions));
    }

    [Fact]
    public void Strict_options_reject_a_non_nullable_property_that_the_document_says_is_nullable()
    {
        string json = """{ "id": 1, "handle": "a", "bio": null }""";
        Assert.ThrowsAny<System.Text.Json.JsonException>(() => System.Text.Json.JsonSerializer.Deserialize<WrongNullabilityUser>(json, SpecServer.StrictOptions));
        var schema = OpenApiSpec.Instance.Match("GET", "/api/v4/users/x")!.SuccessResponse().Schema!;
        Assert.Contains("\"bio\":null", SampleGenerator.Create(OpenApiSpec.Instance, schema, SampleMode.Minimal)!.ToJsonString());
    }

    [Fact]
    public void Every_public_api_method_is_in_the_catalog()
    {
        string[] methods = typeof(SolvedAPI)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => m.ReturnType.IsGenericType
                && m.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)
                && m.ReturnType.GetGenericArguments()[0].IsGenericType
                && m.ReturnType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(SolvedResult<>))
            .Select(m => m.Name)
            .Distinct()
            .Order()
            .ToArray();

        string[] cataloged = EndpointCatalog.All.Select(e => e.Method).Distinct().Order().ToArray();
        Assert.Equal(methods, cataloged);
    }

    [Fact]
    public void There_are_no_synchronous_api_methods()
    {
        MethodInfo[] synchronous = typeof(SolvedAPI)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => m.ReturnType.IsGenericType && m.ReturnType.GetGenericTypeDefinition() == typeof(SolvedResult<>))
            .ToArray();
        Assert.Empty(synchronous);
    }

    public static IEnumerable<object[]> RequestEnumCalls()
    {
        foreach (SolvedProblemSort sort in Enum.GetValues<SolvedProblemSort>())
            foreach (SolvedSortDirection direction in Enum.GetValues<SolvedSortDirection>())
                yield return Call($"problems sort={sort} direction={direction}", api => api.GetSearchProblemAsync("x", 1, sort, direction));
        foreach (SolvedUserRankingSort sort in Enum.GetValues<SolvedUserRankingSort>())
        {
            yield return Call($"rankings/users sort={sort}", api => api.GetUserRankingAsync(1, sort));
            yield return Call($"rankings/users/search sort={sort}", api => api.GetSearchUserAsync("ab", sort));
        }
        foreach (SolvedCollectionSort sort in Enum.GetValues<SolvedCollectionSort>())
            foreach (SolvedItemKind kind in KnownValues<SolvedItemKind>())
                yield return Call($"collection kind={kind} sort={sort}", api => api.GetUserCollectionAsync("a", kind, sort));
        foreach (SolvedHolderSort sort in Enum.GetValues<SolvedHolderSort>())
            foreach (SolvedItemKind kind in KnownValues<SolvedItemKind>())
                yield return Call($"holders kind={kind} sort={sort}", api => api.GetAchievementItemHoldersAsync(kind, "k", 1, sort));
        foreach (SolvedVoteSort sort in Enum.GetValues<SolvedVoteSort>())
            yield return Call($"votes sort={sort}", api => api.GetProblemVotesAsync(1000, 1, sort));
        foreach (SolvedContributionSort sort in Enum.GetValues<SolvedContributionSort>())
            foreach (SolvedContributionFilter only in Enum.GetValues<SolvedContributionFilter>())
                yield return Call($"contributions sort={sort} only={only}", api => api.GetUserContributionsAsync("a", sort: sort, only: only, commented: false));
        foreach (SolvedHistoryRange range in Enum.GetValues<SolvedHistoryRange>())
        {
            yield return Call($"reputation range={range}", api => api.GetUserReputationAsync("a", range));
            yield return Call($"rating range={range}", api => api.GetUserRatingHistoryAsync("a", range));
        }
        foreach (SolvedBookmarkFolderSort sort in Enum.GetValues<SolvedBookmarkFolderSort>())
            yield return Call($"bookmarks sort={sort}", api => api.GetBookmarkFoldersAsync(1, sort));
        foreach (SolvedBookmarkProblemSort sort in Enum.GetValues<SolvedBookmarkProblemSort>())
            yield return Call($"bookmark folder sort={sort}", api => api.GetUserBookmarkFolderAsync("a", "b", 1, sort));
        foreach (SolvedAffiliationType type in KnownValues<SolvedAffiliationType>())
            yield return Call($"affiliation-board type={type}", api => api.GetAffiliationRankingAsync(1, type));
        foreach (SolvedPolicyKind kind in KnownValues<SolvedPolicyKind>())
            yield return Call($"policy {kind}", api => api.GetPolicyAsync(kind));
        foreach (SolvedFolderColor color in KnownValues<SolvedFolderColor>())
            foreach (SolvedFolderVisibility visibility in KnownValues<SolvedFolderVisibility>())
                yield return Call($"folder color={color} visibility={visibility}", api => api.UpdateBookmarkFolderAsync("a", new() { Color = color, Visibility = visibility }));
    }

    [Theory]
    [MemberData(nameof(RequestEnumCalls))]
    public async Task Every_request_enum_value_is_accepted_by_the_document(string label, Func<SolvedAPI, Task<Exception?>> call)
    {
        _ = label;
        SpecServer server = new(SampleMode.Full);
        using SolvedAPI api = server.CreateClient();
        Exception? exception = await call(api);
        Assert.Empty(server.Violations);
        Assert.Null(exception);
    }

    static object[] Call<T>(string label, Func<SolvedAPI, Task<SolvedResult<T>>> call)
        => [label, (Func<SolvedAPI, Task<Exception?>>)(async api => (await call(api)).Exception)];

    static IEnumerable<T> KnownValues<T>() where T : struct, Enum
        => Enum.GetValues<T>().Where(v => Convert.ToInt32(v) != 0 || !Enum.GetName(v)!.Equals("Unknown"));
}
