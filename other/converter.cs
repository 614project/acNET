using acNET.Type;
using acNET.Problem;
using Newtonsoft.Json;

namespace acNET;

/// <summary>
/// sovled.ac API 에서 추출한 값을 C# 데이터 형식에 맞게 변환하기 위한 클래스.
/// </summary>
public static class Converter
{
    /// <summary>
    /// solved.ac API의 시간 형식을 C# DateTime으로 변환합니다.
    /// </summary>
    /// <param name="time">Solved.ac API에서 제공하는 시간 문자열</param>
    /// <returns>변환 실패시 null을 반환합니다.</returns>
    public static DateTime? Time(string time)
    {
        return DateTime.TryParseExact(
            string.Join(' ', time.Remove(time.Length - 1).Split('T')),
            "yyyy-MM-dd HH:mm:ss.fff",
            null,
            System.Globalization.DateTimeStyles.None,
            out var result
        )?result : null;
    }

    /// <summary>
    /// 정수형 레벨(티어)값을 레벨 이름으로 반환합니다. (예: Gold IV)
    /// </summary>
    /// <param name="value">레벨(티어)값</param>
    /// <returns>음수 또는 레벨 개수 이상의 값을 넣을경우 null이 반환됩니다.</returns>
    public static string? LevelName(int value)
    {
        if (value < 0 || value >= Level.Names.Length) return null;
        return Level.Names[value];
    }
    /// <summary>
    /// 레벨(티어) 이름을 레벨 값으로 반환합니다.
    /// </summary>
    /// <param name="name">레벨 이름 (예: Gold IV)</param>
    /// <returns>존재하지 않으면 null</returns>
    public static int? LevelValue(string name)
    {
        for (int i = 0; i < Level.Names.Length; i++) if (Level.Names[i] == name) return i;
        return null;
    }
    /// <summary>
    /// 정수형 레벨(아레나)값을 아레나 티어 이름으로 변환합니다. (예: A+)
    /// </summary>
    /// <param name="value">레벨(아레나)값</param>
    /// <returns></returns>
    public static string? ArenaName(long value)
    {
        if (value < 0 || value >= Arena.SolvedArena.Names.Length)
            return null;
        return Arena.SolvedArena.Names[value];
    }
    /// <summary>
    /// solved.ac API에서 가져온 문자열 Json을 acNET 내에 있는 클래스로 변환합니다.
    /// </summary>
    /// <typeparam name="T">acNET.Type.BaseBody를 상속한 클래스</typeparam>
    /// <param name="json">Json 형식의 문자열</param>
    /// <returns>클래스 내 변수에 값이 담긴채로 반환</returns>
    public static T? ParsingJson<T>(string json) where T : Jsonable
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
    /// <summary>
    /// solved.ac API에서 가져온 문자열 Json을 acNET 내에 있는 클래스의 리스트로 변환합니다.
    /// </summary>
    /// <typeparam name="T">acNET.Type.BaseBody를 상속한 클래스</typeparam>
    /// <param name="json">Json 형식의 문자열 (상위가 배열이여야됨)</param>
    /// <returns>클래스가 담긴 리스트</returns>
    public static List<T>? ParsingJsonList<T>(string json) where T : Jsonable
    {
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
    /// <summary>
    /// 정수형 레벨(티어)값에 맞는 색을 6자리 Hex 코드로 변환합니다.
    /// </summary>
    /// <param name="value">음수가 아닌 정수 레벨(티어)값</param>
    /// <returns>'#rrggbb' 형식으로 길이가 7인 문자열을 반환합니다. (변환 실패시 '#000000')</returns>
    public static string LevelColor(long value)
    {
        if (value < 0 || value >= Level.Colors.Length) return "#000000";
        return Level.Colors[value];
    }
}
