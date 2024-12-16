using Newtonsoft.Json.Linq;
using System.Net;

namespace acNET;

public partial class acAPI
{
    /// <summary>
    /// 응답을 받을 언어입니다.
    /// </summary>
    public enum LanguageType
    {
        /// <summary>
        /// 선택하지 않음 (기본값)
        /// </summary>
        None = 0,
        /// <summary>
        /// 한국어
        /// </summary>
        Korean,
        /// <summary>
        /// 영어
        /// </summary>
        English,
        /// <summary>
        /// 일본어
        /// </summary>
        Japanese
    }
    /// <summary>
    /// 응답을 받을 언어를 선택합니다.
    /// </summary>
    public LanguageType LanguageHeader {
        set {
            this._client.DefaultRequestHeaders.Remove("x-solvedac-language");
            if (value == LanguageType.None)
                return;
            this._client.DefaultRequestHeaders.Add("x-solvedac-language" , value switch {
                LanguageType.English => "en",
                LanguageType.Japanese => "ja",
                _ => "ko"
            });
        }
    }
    /// <summary>
    /// 쿠키에 저장될 solvedacToken입니다. 이를 설정할 경우 계정과 관련된 API를 사용하실수 있습니다. (아직 설정된 토큰이 없는 경우 빈 문자열을 반환합니다.)
    /// </summary>
    //public string SolvedacToken {
    //    set => this._cookie.Add(this._client.BaseAddress , new Cookie("solvedacToken" , value));
    //    get {
    //        try { return this._cookie.GetCookies(this._client.BaseAddress)["solvedacToken"].Value; }
    //        catch { return string.Empty; }
    //    }
    //}
}
