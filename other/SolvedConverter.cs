using System.Globalization;
using System.Text.Json;
using AcNET.Problem;
using AcNET.Type;

namespace AcNET;

/// <summary>
/// sovled.ac API 에서 받은 값을 C# 데이터 형식에 맞게 변환하기 위한 클래스.
/// </summary>
public static class SolvedConverter
{
    /// <summary>
    /// solved.ac API 의 시간 문자열(ISO 8601)을 <see cref="DateTimeOffset"/> 으로 변환합니다.
    /// </summary>
    /// <param name="time">solved.ac API 에서 제공하는 시간 문자열</param>
    /// <returns>변환 실패시 null을 반환합니다.</returns>
    public static DateTimeOffset? ToTime(string? time)
    {
        if (string.IsNullOrEmpty(time))
            return null;
        return DateTimeOffset.TryParse(time, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result)
            ? result
            : null;
    }

    /// <summary>
    /// solved.ac API 의 날짜 문자열(<c>yyyy-MM-dd</c>)을 <see cref="DateOnly"/> 로 변환합니다.
    /// </summary>
    /// <param name="day">날짜 문자열</param>
    /// <returns>변환 실패시 null을 반환합니다.</returns>
    public static DateOnly? ToDay(string? day)
    {
        if (string.IsNullOrEmpty(day))
            return null;
        return DateOnly.TryParseExact(day, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)
            ? result
            : null;
    }

    /// <summary>
    /// 정수형 레벨(티어)값을 레벨 이름으로 반환합니다. (예: Gold IV)
    /// </summary>
    /// <param name="value">레벨(티어)값</param>
    /// <returns>음수 또는 레벨 개수 이상의 값을 넣을경우 null이 반환됩니다.</returns>
    public static string? LevelName(int? value)
    {
        if (value is not int level || level < 0 || level >= SolvedLevel.Names.Length) return null;
        return SolvedLevel.Names[level];
    }

    /// <summary>
    /// 레벨(티어) 이름을 레벨 값으로 반환합니다.
    /// </summary>
    /// <param name="name">레벨 이름 (예: Gold IV)</param>
    /// <returns>존재하지 않으면 null</returns>
    public static int? LevelValue(string name)
    {
        int index = Array.IndexOf(SolvedLevel.Names, name);
        return index < 0 ? null : index;
    }

    /// <summary>
    /// 정수형 레벨(티어)값에 맞는 색을 6자리 Hex 코드로 변환합니다.
    /// </summary>
    /// <param name="value">음수가 아닌 정수 레벨(티어)값</param>
    /// <returns>'#rrggbb' 형식으로 길이가 7인 문자열을 반환합니다. (변환 실패시 '#000000')</returns>
    public static string LevelColor(int? value)
    {
        if (value is not int level || level < 0 || level >= SolvedLevel.Colors.Length) return "#000000";
        return SolvedLevel.Colors[level];
    }

    /// <summary>
    /// solved.ac API 에서 가져온 Json 문자열을 acNET 내에 있는 클래스로 변환합니다.
    /// </summary>
    /// <typeparam name="T">acNET.Type.Jsonable 을 상속한 클래스</typeparam>
    /// <param name="json">Json 형식의 문자열</param>
    /// <returns>클래스 내 변수에 값이 담긴채로 반환</returns>
    public static T? ParsingJson<T>(string json) where T : Jsonable
        => JsonSerializer.Deserialize<T>(json, SolvedJson.Read);

    /// <summary>
    /// solved.ac API 에서 가져온 Json 문자열을 acNET 내에 있는 클래스의 리스트로 변환합니다.
    /// </summary>
    /// <typeparam name="T">acNET.Type.Jsonable 을 상속한 클래스</typeparam>
    /// <param name="json">Json 형식의 문자열 (상위가 배열이여야됨)</param>
    /// <returns>클래스가 담긴 리스트</returns>
    public static List<T>? ParsingJsonList<T>(string json) where T : Jsonable
        => JsonSerializer.Deserialize<List<T>>(json, SolvedJson.Read);
}
