using acNET.Type;

namespace acNET.Coin
{
    /// <summary>
    /// 코인->별조각 환율
    /// </summary>
    public class ExchangeRate : BaseBody
    {
        /// <summary>
        /// 코인->별조각 환율입니다. 수수료 1%는 제외되어 있습니다.
        /// </summary>
        public int rate;
    }
}
