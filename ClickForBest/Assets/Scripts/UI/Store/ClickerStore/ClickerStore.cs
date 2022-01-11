using UnityEditor;
using UnityEngine;

public class ClickerStore : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] ClickerStoreItem item_prefab;

    private ClickerStoreItem selected_item;

    [System.Serializable]
    public struct ClickerItem
    {
        public int price;
        public Sprite sprite;
    }
    public void Pressed_Item_Select(ClickerStoreItem _item)
    {
        if (selected_item)
            selected_item.UnSelect();
        selected_item = _item;
        selected_item.Select();
        ReferenceKeeper.Instance.ClickButton.sprite = selected_item.sprite;
        SelectStoreItemDB(_item.ID);
    }
    public void SelectStoreItemDB(int _id)
    {
        if (ReferenceKeeper.Instance.GameManager.gameDB != null)
        {
            ReferenceKeeper.Instance.GameManager.gameDB.selected_clicker_store_item = _id;
        }
    }
#if UNITY_EDITOR
    private byte _id;
    [EasyButtons.Button]
    private void GenerateItems()
    {
        ClearAllItems();

        ClickerItemData data = Resources.Load<ClickerItemData>("ClickerStoreItems");

        _id = 0;
        GenerateItem(data.items);
    }
    private void GenerateItem(ClickerItem[] _items)
    {
        if (_items != null && _items.Length > 0)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                ClickerStoreItem item_instance = (ClickerStoreItem)PrefabUtility.InstantiatePrefab(item_prefab, content);
                item_instance.Init(_id, _items[i].price, _items[i].sprite);
                _id++;
            }
        }
    }
    [EasyButtons.Button]
    private void ClearAllItems()
    {
        int child_count = content.childCount;
        if (child_count > 0)
        {
            for (int i = 0; i < child_count; i++)
            {
                if (content.GetChild(0).GetComponent<ClickerStoreItem>())
                    DestroyImmediate(content.GetChild(0).gameObject);
            }
        }
    }
#endif
}
