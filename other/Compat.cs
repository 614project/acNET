using System.Runtime.CompilerServices;

namespace AcNET
{
    /// <summary>
    /// 열거형 멤버가 solved.ac API 에서 어떤 문자열인지 지정합니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class ApiValueAttribute(string value) : Attribute
    {
        public string Value { get; } = value;
    }

    /// <summary>
    /// 인자 검사 도우미입니다. (.NET Standard 2.1 에는 ArgumentNullException.ThrowIfNull 등이 없습니다.)
    /// </summary>
    internal static class Guard
    {
        public static T NotNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string? name = null) where T : class
            => value ?? throw new ArgumentNullException(name);

        public static string NotNullOrEmpty(string? value, [CallerArgumentExpression(nameof(value))] string? name = null)
        {
            if (value is null)
                throw new ArgumentNullException(name);
            if (value.Length == 0)
                throw new ArgumentException("빈 문자열일 수 없습니다.", name);
            return value;
        }

        public static string NotNullOrWhiteSpace(string? value, [CallerArgumentExpression(nameof(value))] string? name = null)
        {
            if (value is null)
                throw new ArgumentNullException(name);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("비어 있거나 공백일 수 없습니다.", name);
            return value;
        }
    }
}

// .NET Standard 2.1 에 없는 컴파일러용 형식들입니다. (init 접근자, CallerArgumentExpression)
// 이 라이브러리 안에서만 쓰이며 사용하는 쪽의 프레임워크 형식과 충돌하지 않습니다.
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class CallerArgumentExpressionAttribute(string parameterName) : Attribute
    {
        public string ParameterName { get; } = parameterName;
    }
}
