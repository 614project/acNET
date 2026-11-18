using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcNET;

/// <summary>
/// solved.ac 의 문자열 값과 C# 열거형을 서로 변환합니다.
/// 각 멤버의 <see cref="JsonStringEnumMemberNameAttribute"/> 를 API 문자열로 사용합니다.
/// </summary>
internal static class SolvedEnum<TEnum> where TEnum : struct, Enum
{
    static readonly Dictionary<string, TEnum> fromValue = new(StringComparer.Ordinal);
    static readonly Dictionary<TEnum, string> toValue = [];

    static SolvedEnum()
    {
        foreach (FieldInfo field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            string? name = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()?.Name;
            if (name is null)
                continue;
            TEnum value = (TEnum)field.GetValue(null)!;
            fromValue[name] = value;
            toValue[value] = name;
        }
    }

    /// <summary>
    /// API 문자열을 열거형으로 바꿉니다. 모르는 값이면 <c>default</c> (Unknown) 를 돌려줍니다.
    /// </summary>
    public static TEnum Parse(string value) => fromValue.TryGetValue(value, out TEnum result) ? result : default;

    /// <summary>
    /// 열거형을 API 문자열로 바꿉니다.
    /// </summary>
    public static string ToValue(TEnum value) => toValue.TryGetValue(value, out string? result)
        ? result
        : throw new ArgumentOutOfRangeException(nameof(value), value, $"'{value}' 는 solved.ac API 로 보낼 수 없는 {typeof(TEnum).Name} 값입니다.");
}

/// <summary>
/// 모르는 값을 만나도 예외 대신 <c>default</c> (Unknown) 로 읽는 열거형 변환기입니다.
/// solved.ac 가 새 값을 추가해도 응답 전체가 실패하지 않도록 합니다.
/// </summary>
internal sealed class SolvedEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"{typeof(TEnum).Name} 은(는) 문자열이어야 합니다. (받은 토큰: {reader.TokenType})");
        return SolvedEnum<TEnum>.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        => writer.WriteStringValue(SolvedEnum<TEnum>.ToValue(value));
}

internal static class SolvedEnumExtensions
{
    /// <summary>
    /// 열거형을 API 문자열로 바꿉니다.
    /// </summary>
    public static string ToApiValue<TEnum>(this TEnum value) where TEnum : struct, Enum => SolvedEnum<TEnum>.ToValue(value);
}
