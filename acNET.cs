/*
 * acNET
 *
 * C# API Library for solved.ac (API v4)
 *
 * copyright 614project.
 */

namespace AcNET;

/// <summary>
/// acNET 라이브러리 정보입니다.
/// </summary>
public static class SolvedLibrary
{
    /// <summary>
    /// 라이브러리 버전입니다. 요청의 User-Agent 에도 사용됩니다.
    /// </summary>
    public static string Version { get; } =
        typeof(SolvedLibrary).Assembly.GetName().Version?.ToString(3) ?? "1.0.0";
}
