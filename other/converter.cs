using acNET.Type;
using Newtonsoft.Json;

namespace acNET
{
    /// <summary>
    /// Sovled.ac API 에서 추출한 값을 C# 데이터 형식에 맞게 변환하기 위한 클래스.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Solved.ac API의 시간 형식을 C# DateTime으로 변환합니다.
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
        /// 정수형 레벨(티어)값을 레벨 이름으로 반환합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>음수 또는 레벨 갯수 이상의 값을 넣을경우 null이 반환됩니다.</returns>
        public static string? LevelName(long value)
        {
            if (value < 0 || value >= Level.Names.Length) return null;
            return Level.Names[value];
        }

        /// <summary>
        /// Solved.ac API에서 가져온 문자열 Json을 acNET 내에 있는 클래스로 변환합니다.
        /// </summary>
        /// <typeparam name="T">acNET.Type.BaseBody를 상속한 클래스</typeparam>
        /// <param name="json">Json 형식의 문자열</param>
        /// <returns>클래스 내 변수에 값이 담긴채로 반환</returns>
        public static T? ParsingJson<T>(string json) where T : BaseBody
        {
            return JsonConvert.DeserializeObject<T>(json);
        } 
    }
}
