using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] StoreItem item_prefab;
    [SerializeField] Transform content;
    [SerializeField] PauseMenu pause_menu;

    public Item[] silver_items;
    public Item[] green_items;
    public Item[] red_items;
    public Item[] gold_items;
    public Item[] gem_items;
    public Item[] special_items;

    private DOMove domove;

    private bool isopen;

    private void Start()
    {
        domove = GetComponent<DOMove>();
    }

    [System.Serializable]
    public struct Item
    {
        public int price;
        public Sprite icon;
        public Sprite background;
    }

    public void OpenCloseMenu()
    {
        if (!isopen)
        {
            isopen = true;
            domove.DO();
        }
        else
        {
            isopen = false;
            domove.DORevert();

            if (pause_menu)
            {
                pause_menu.Pressed_Pause_Button();
            }
        }
    }

    [EasyButtons.Button]
    private void GenerateItems()
    {
        ClearAllItems();
        GenerateItem(silver_items, Color.white);
        GenerateItem(green_items, Color.white);
        GenerateItem(red_items, Color.white);
        GenerateItem(gold_items, Color.white);
        GenerateItem(gem_items, UtilitiesMethods.HexToColor("F38922"));
        GenerateItem(special_items, Color.white);
    }
    private void GenerateItem(Item[] _items, Color _bgColor)
    {
        if (_items != null && _items.Length > 0)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                StoreItem item_instance = Instantiate(item_prefab, content);
                item_instance.Init(_items[i].price, _items[i].icon, _items[i].background, _bgColor);
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
                DestroyImmediate(content.GetChild(0).gameObject);
            }
        }
    }
    [EasyButtons.Button]
    private void ChangeSilverPrice(int _value)
    {
        ChangeItemsPrice(silver_items, _value);
    }
    [EasyButtons.Button]
    private void ChangGreenPrice(int _value)
    {
        ChangeItemsPrice(green_items, _value);
    }
    [EasyButtons.Button]
    private void ChangRedPrice(int _value)
    {
        ChangeItemsPrice(red_items, _value);
    }
    [EasyButtons.Button]
    private void ChangGoldPrice(int _value)
    {
        ChangeItemsPrice(gold_items, _value);
    }
    [EasyButtons.Button]
    private void ChangGemPrice(int _value)
    {
        ChangeItemsPrice(gem_items, _value);
    }
    [EasyButtons.Button]
    private void ChangSpecialPrice(int _value)
    {
        ChangeItemsPrice(special_items, _value);
    }
    private void ChangeItemsPrice(Item[] _items, int _value)
    {
        if (_items != null)
        {
            if (_value <= 0) _value = 100;

            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].price = _value;
            }
        }
    }
}
