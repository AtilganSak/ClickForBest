using UnityEngine;

[CreateAssetMenu(fileName ="StoreItems")]
public class StoreItemData : ScriptableObject
{
    public Store.Item[] silver_items;
    public Store.Item[] green_items;
    public Store.Item[] red_items;
    public Store.Item[] gold_items;
    public Store.Item[] gem_items;
    public Store.Item[] special_items;
}
