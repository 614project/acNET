using acNET.Type;

namespace acNET.Coin;

/// <summary>
/// 코인샵에서 판매하고 있는 상품입니다.
/// </summary>
public class ShopItem : BaseBody
{
    /// <summary>
    /// 상품의 ID입니다.
    /// </summary>
    public int skuId{ get; set; }
    /// <summary>
    /// 판매하고 있는 아이템입니다.
    /// </summary>
    public Item item{ get; set; }
    /// <summary>
    /// 1회 구매에 획득하는 개수입니다.
    /// </summary>
    public int units{ get; set; }
    /// <summary>
    /// 가격입니다. 코인은 나누기 100을 해야 표시 가격이 됩니다.
    /// </summary>
    public int price{ get; set; }
    /// <summary>
    /// 가격의 단위입니다.
    /// </summary>
    public string priceUnit{ get; set; }
    /// <summary>
    /// 아이템 사용 시간의 제한 여부입니다.
    /// </summary>
    public bool itemUseTimeLimited{ get; set; }
    /// <summary>
    /// 아이템 구매 시간의 제한 여부입니다.
    /// </summary>
    public bool itemSellTimeLimited{ get; set; }

    /// <summary>
    /// 이 아이템이 코인으로 구매하는지에 대한 여부입니다.
    /// </summary>
    public bool IsRequireCoin => priceUnit == "coins";
/// <summary>
/// 정확한 가격입니다.
/// </summary>
public double RealPrice => IsRequireCoin ? this.price * 0.01 : this.price;
}
