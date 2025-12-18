using Newtonsoft.Json;

namespace AcNET.Coin;

/// <summary>
/// 사용자가 사용할 수 있는 아이템입니다.
/// </summary>
public class SolvedItem
{
    /// <summary>
    /// 아이템의 ID입니다.
    /// </summary>
    [JsonProperty("itemId")]
    public string ItemId { get; set; } = string.Empty;
    /// <summary>
    /// 아이템 사진으로 가는 하이퍼링크입니다.
    /// </summary>
    [JsonProperty("itemImageUrl")]
    public string ItemImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// 최대 소유 가능 개수입니다. 호출자에 따라 달라질 수 있습니다.
    /// </summary>
    [JsonProperty("inventoryMaxUnits")]
    public int InventoryMaxUnits { get; set; }
    /// <summary>
    /// 아이템 사용 가능 여부입니다.
    /// </summary>
    [JsonProperty("usable")]
    public bool Usable { get; set; }
    /// <summary>
    /// 아이템의 이름입니다.
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;
    /// <summary>
    /// 아이템의 설명입니다.
    /// </summary>
    [JsonProperty("displayDescription")]
    public string DisplayDescription { get; set; } = string.Empty;
}