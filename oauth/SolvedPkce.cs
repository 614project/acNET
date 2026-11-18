using System.Security.Cryptography;
using System.Text;

namespace AcNET.OAuth;

/// <summary>
/// OAuth 2.0 PKCE (RFC 7636, S256) 의 code verifier 와 code challenge 쌍입니다.
/// </summary>
/// <remarks>
/// 인가 요청 때 <see cref="CodeChallenge"/> 를 보내고, 토큰 교환 때 <see cref="CodeVerifier"/> 를 보냅니다.
/// 그 사이에 verifier 를 세션 등에 안전하게 보관해야 합니다.
/// </remarks>
public sealed class SolvedPkce
{
    /// <summary>
    /// code verifier 입니다. 토큰 교환(<see cref="SolvedAPI.ExchangeCodeAsync(string, string, string, CancellationToken)"/>)에 사용합니다.
    /// </summary>
    public string CodeVerifier { get; }
    /// <summary>
    /// code challenge 입니다. (BASE64URL(SHA256(verifier))) 인가 주소에 사용합니다.
    /// </summary>
    public string CodeChallenge { get; }
    /// <summary>
    /// code challenge 방식입니다. solved.ac 는 S256 만 지원합니다.
    /// </summary>
    public string CodeChallengeMethod => "S256";

    SolvedPkce(string codeVerifier)
    {
        CodeVerifier = codeVerifier;
        using SHA256 sha256 = SHA256.Create();
        CodeChallenge = Base64Url(sha256.ComputeHash(Encoding.ASCII.GetBytes(codeVerifier)));
    }

    /// <summary>
    /// 새 무작위 verifier 로 PKCE 쌍을 만듭니다.
    /// </summary>
    public static SolvedPkce Create() => new(Base64Url(RandomBytes(32)));

    /// <summary>
    /// 보관해둔 verifier 로 PKCE 쌍을 다시 만듭니다.
    /// </summary>
    /// <param name="codeVerifier">43~128자의 verifier (영문, 숫자, '-', '.', '_', '~')</param>
    public static SolvedPkce FromVerifier(string codeVerifier)
    {
        Guard.NotNull(codeVerifier);
        if (codeVerifier.Length is < 43 or > 128 || !codeVerifier.All(IsUnreserved))
            throw new ArgumentException("code verifier 는 43~128자의 영문, 숫자, '-', '.', '_', '~' 로만 이루어져야 합니다.", nameof(codeVerifier));
        return new(codeVerifier);
    }

    /// <summary>
    /// CSRF 방지를 위한 무작위 state 값을 만듭니다.
    /// </summary>
    public static string CreateState() => Base64Url(RandomBytes(16));

    static bool IsUnreserved(char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or (>= '0' and <= '9') or '-' or '.' or '_' or '~';

    static byte[] RandomBytes(int count)
    {
        byte[] bytes = new byte[count];
        RandomNumberGenerator.Fill(bytes);
        return bytes;
    }

    /// <summary>
    /// BASE64URL (RFC 4648 §5, 패딩 없음)
    /// </summary>
    static string Base64Url(byte[] bytes)
        => Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
}
