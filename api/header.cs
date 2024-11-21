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
}
