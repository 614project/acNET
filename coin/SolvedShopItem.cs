using AcNET.Type;
using Newtonsoft.Json;

namespace AcNET.Coin;

/// <summary>
/// 코인샵에서 판매하고 있는 상품입니다.
/// </summary>
public class SolvedShopItem : Jsonable
{
    /// <summary>
    /// 상품의 ID입니다.
    /// </summary>
    [JsonProperty("skuId")]
    public int SkuId { get; set; }
    /// <summary>
    /// 판매하고 있는 아이템입니다.
    /// </summary>
    [JsonProperty("item")]
    public SolvedItem Item { get; set; } = null!;
    /// <summary>
    /// 1회 구매에 획득하는 개수입니다.
    /// </summary>
    [JsonProperty("units")]
    public int Units { get; set; }
    /// <summary>
    /// 가격입니다. 코인은 나누기 100을 해야 표시 가격이 됩니다.
    /// </summary>
    [JsonProperty("price")]
    public int Price { get; set; }
    /// <summary>
    /// 가격의 단위입니다.
    /// </summary>
    [JsonProperty("priceUnit")]
    public string PriceUnit { get; set; } = string.Empty;
    /// <summary>
    /// 아이템 사용 시간의 제한 여부입니다.
    /// </summary>
    [JsonProperty("itemUseTimeLimited")]
    public bool ItemUseTimeLimited { get; set; }
    /// <summary>
    /// 아이템 구매 시간의 제한 여부입니다.
    /// </summary>
    [JsonProperty("itemSellTimeLimited")]
    public bool ItemSellTimeLimited { get; set; }

    /// <summary>
    /// 이 아이템이 코인으로 구매하는지에 대한 여부입니다.
    /// </summary>
    public bool IsRequireCoin => PriceUnit == "coins";
    /// <summary>
    /// 코인/별조각 여부에 따라 정확한 가격을 구합니다.
    /// </summary>
    public double RealPrice => IsRequireCoin ? this.Price * 0.01 : this.Price;
}