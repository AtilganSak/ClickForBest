using UnityEngine;

[CreateAssetMenu(fileName ="StoreItems")]
public class StoreItemData : ScriptableObject
{
    public CoinStore.Item[] silver_items;
    public CoinStore.Item[] green_items;
    public CoinStore.Item[] red_items;
    public CoinStore.Item[] gold_items;
    public CoinStore.Item[] gem_items;
    public CoinStore.Item[] special_items;
}
