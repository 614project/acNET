using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acNET;

public partial class acAPI
{
    /// <summary>
    /// 응답을 받을 언어입니다.
    /// </summary>
    public enum solvedacLanguage
    {
        Korean = 0,
        English,
        Japanese
    }
    private Header _convert_language(solvedacLanguage language)
    {
        return new("x-solvedac-language" , language switch {
            solvedacLanguage.English => "en",
            solvedacLanguage.Japanese => "ja",
            _ => "ko"
        });
    }
}
