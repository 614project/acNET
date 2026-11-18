using System.Globalization;
using System.Text;

namespace AcNET;

/// <summary>
/// URL 쿼리 문자열을 만듭니다. 모든 키와 값은 URL 인코딩됩니다. null 인 값은 생략됩니다.
/// </summary>
internal sealed class SolvedQuery
{
    readonly List<KeyValuePair<string, string>> items = [];

    public SolvedQuery Add(string key, string? value)
    {
        if (value is not null)
            items.Add(new(key, value));
        return this;
    }

    public SolvedQuery Add(string key, long? value)
        => Add(key, value?.ToString(CultureInfo.InvariantCulture));

    public SolvedQuery Add<TEnum>(string key, TEnum? value) where TEnum : struct, Enum
        => Add(key, value is TEnum v ? v.ToApiValue() : null);

    public SolvedQuery Add<TEnum>(string key, TEnum value) where TEnum : struct, Enum
        => Add(key, value.ToApiValue());

    public override string ToString()
    {
        if (items.Count == 0)
            return string.Empty;
        StringBuilder builder = new();
        foreach (var (key, value) in items)
        {
            builder.Append(builder.Length == 0 ? '?' : '&');
            builder.Append(Uri.EscapeDataString(key)).Append('=').Append(Uri.EscapeDataString(value));
        }
        return builder.ToString();
    }

    /// <summary>
    /// 경로의 한 구간을 URL 인코딩합니다.
    /// </summary>
    public static string Segment(string value, [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(value))] string? name = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, name);
        return Uri.EscapeDataString(value);
    }

    /// <summary>
    /// 경로의 한 구간(숫자)을 문자열로 만듭니다.
    /// </summary>
    public static string Segment(long value) => value.ToString(CultureInfo.InvariantCulture);
}
