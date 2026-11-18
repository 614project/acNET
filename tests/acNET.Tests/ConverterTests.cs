using AcNET.Problem;
using AcNET.User;

namespace AcNET.Tests;

public class ConverterTests
{
    [Theory]
    [InlineData(0, "Unrated")]
    [InlineData(1, "Bronze V")]
    [InlineData(15, "Gold I")]
    [InlineData(30, "Ruby I")]
    [InlineData(31, "Master")]
    public void Level_names(int level, string name)
    {
        Assert.Equal(name, SolvedConverter.LevelName(level));
        Assert.Equal(level, SolvedConverter.LevelValue(name));
    }

    [Fact]
    public void Level_out_of_range()
    {
        Assert.Null(SolvedConverter.LevelName(-1));
        Assert.Null(SolvedConverter.LevelName(32));
        Assert.Null(SolvedConverter.LevelName(null));
        Assert.Equal("#000000", SolvedConverter.LevelColor(99));
        Assert.Equal("#B491FF", SolvedConverter.LevelColor(31));
        Assert.Equal(SolvedLevel.Names.Length, SolvedLevel.Colors.Length);
    }

    [Fact]
    public void Times_and_days()
    {
        Assert.Equal(new DateTimeOffset(2025, 4, 1, 12, 30, 0, 500, TimeSpan.Zero), SolvedConverter.ToTime("2025-04-01T12:30:00.500Z"));
        Assert.Equal(new DateTime(2025, 4, 1), SolvedConverter.ToDay("2025-04-01"));
        Assert.Null(SolvedConverter.ToTime("not a time"));
        Assert.Null(SolvedConverter.ToDay(null));
    }

    [Fact]
    public void Models_round_trip_through_json()
    {
        SolvedUser? user = SolvedConverter.ParsingJson<SolvedUser>("""{ "id": 7, "handle": "a", "tier": 31, "class": 10, "classPlus": true, "bans": [{ "banType": "vote", "banReason": null, "reason": "r", "until": "2030-01-01T00:00:00Z" }] }""");
        Assert.NotNull(user);
        Assert.Equal("Master", user.GetTierName);
        Assert.Equal(10, user.Class);
        Assert.Equal(SolvedBanType.Vote, user.Bans[0].BanType);
        Assert.Equal(2030, user.Bans[0].GetUntil!.Value.Year);
        Assert.Contains("\"banType\":\"vote\"", user.ToJson());
        Assert.Equal(user.ToJson(), SolvedConverter.ParsingJson<SolvedUser>(user.ToJson())!.ToJson());
    }

    [Fact]
    public void Tag_names_fall_back_to_english_then_key()
    {
        SolvedTag tag = new() { Key = "dp", Names = new() { ["ko"] = "다이나믹 프로그래밍", ["en"] = "dynamic programming" } };
        Assert.Equal("다이나믹 프로그래밍", tag.GetName("ko"));
        Assert.Equal("dynamic programming", tag.GetName("ja"));
        Assert.Equal("dp", new SolvedTag { Key = "dp" }.GetName("ko"));
    }
}
