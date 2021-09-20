using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] StoreItem item_prefab;
    [SerializeField] Transform content;
    [SerializeField] PauseMenu pause_menu;
    [SerializeField] MessagePanel message_panel;
    [SerializeField] MessagePanel noads_message_panel;
    [SerializeField] MessagePanel noscore_message_panel;
    [SerializeField] RewardItem reward_item_500;
    [SerializeField] RewardItem reward_item_1000;
    [SerializeField] RewardItem reward_item_1500;
    [SerializeField] RewardItem reward_item_2000;
    [SerializeField] TMPro.TMP_Text score_text;

    public System.Action<byte> onPurchase;
    public System.Action<int> onTakenReward;

    private StoreItem selected_item;

    private DOMove domove;

    private bool pressed_reward;
    private bool isopen;
    private byte will_buy_item;
    private int will_taken_reward;

    [System.Serializable]
    public struct Item
    {
        public int price_underK;
        public int price_k;
        public Sprite icon;
        public Sprite background;
    }

    private void Start()
    {
        domove = GetComponent<DOMove>();

        if (message_panel)
        {
            message_panel.Subscribe("Window", "Are you sure?", "Yes", "No", MessagePanelResult);
        }
        if (noscore_message_panel)
        {
            noscore_message_panel.Subscribe("Window", "You don't have enough score!", "+1K", "OK", NoScoreMessagePanelResult);
        }
        if (noads_message_panel)
        {
            noads_message_panel.Subscribe("Window", "No ads available!", "OK", "OK", null);
        }

        if (reward_item_500)
            reward_item_500.onClick += Pressed_Reward_Item;
        if (reward_item_1000)
            reward_item_1000.onClick += Pressed_Reward_Item;
        if (reward_item_1500)
            reward_item_1500.onClick += Pressed_Reward_Item;
        if (reward_item_2000)
            reward_item_2000.onClick += Pressed_Reward_Item;

        GetCurrentScore();
        Load();
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
    public void Pressed_Item_Button(byte _id, bool _enoughScore)
    {
        if (_enoughScore)
        {
            will_buy_item = _id;

            if (message_panel)
            {
                message_panel.Change(_message: "Are you sure you want to buy?");
                message_panel.Show();
            }
        }
        else
        {
            if (noscore_message_panel)
            {
                noscore_message_panel.Show();
            }
        }
    }
    public void Pressed_Item_Select(StoreItem _item)
    {
        if (selected_item)
            selected_item.UnSelect();
        selected_item = _item;
        selected_item.Select();
        SelectStoreItemDB(_item.ID);
    }
    private void MessagePanelResult(bool _result)
    {
        if (_result)
        {
            if (pressed_reward)
            {
                ReferenceKeeper.Instance.RewardAdsController.onAdsShowComplete += () =>
                {
                    if (onTakenReward != null)
                    {
                        onTakenReward.Invoke(will_taken_reward);
                    }
                    pressed_reward = false;
                };
                ReferenceKeeper.Instance.RewardAdsController.ShowAd();
            }
            else
            {
                if (onPurchase != null)
                {
                    onPurchase.Invoke(will_buy_item);

                    PurchasedStoreItemDB(will_buy_item);
                }
            }
            GetCurrentScore();
        }
        else
        {
            will_buy_item = 0;
            will_taken_reward = 0;
            pressed_reward = false;
        }
    }
    private void NoScoreMessagePanelResult(bool _result)
    {
        if (_result)
        {
            if (ReferenceKeeper.Instance.RewardAdsController.IsReadyAds())
            {
                ReferenceKeeper.Instance.RewardAdsController.onAdsShowComplete += () =>
                {
                    ReferenceKeeper.Instance.GameManager.AddScore(will_taken_reward);
                };
                ReferenceKeeper.Instance.RewardAdsController.ShowAd();
            }
            else
            {
                if (noads_message_panel)
                {
                    noads_message_panel.Show();
                }
            }
        }
    }
    private void Pressed_Reward_Item(int _itemValue)
    {
        pressed_reward = true;
        will_taken_reward = _itemValue;

        if (ReferenceKeeper.Instance.RewardAdsController.IsReadyAds())
        {
            if (message_panel)
            {
                message_panel.Change(_message: "Do you want to watch ads?");
                message_panel.Show();
            }
        }
        else
        {
            if (noads_message_panel)
            {
                noads_message_panel.Show();
            }
        }
    }
    private void Load()
    {
        if (ReferenceKeeper.Instance.GameManager.gameDB != null)
        {
            StoreItem[] items = content.GetComponentsInChildren<StoreItem>();
            if (items != null)
            {
                items[0].Purchased(true);
                int[] ids = ReferenceKeeper.Instance.GameManager.gameDB.purchaseStoreItems;
                if (ids != null && ids.Length > 0)
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if(!items[ids[i]].isPurchased)
                            items[ids[i]].Purchased(true);
                    }
                }
                items[ReferenceKeeper.Instance.GameManager.gameDB.selected_store_item].Select();
                selected_item = items[ReferenceKeeper.Instance.GameManager.gameDB.selected_store_item];
            }
        }
    }
    public void PurchasedStoreItemDB(int _id)
    {
        if (ReferenceKeeper.Instance.GameManager.gameDB != null)
        {
            if (ReferenceKeeper.Instance.GameManager.gameDB.purchaseStoreItems == null)
                ReferenceKeeper.Instance.GameManager.gameDB.purchaseStoreItems = new int[1] { _id };
            else
            {
                if (!ReferenceKeeper.Instance.GameManager.gameDB.purchaseStoreItems.Contains(_id))
                {
                    List<int> list = ReferenceKeeper.Instance.GameManager.gameDB.purchaseStoreItems.ToList();
                    list.Add(_id);
                    ReferenceKeeper.Instance.GameManager.gameDB.purchaseStoreItems = list.ToArray();
                }
            }
        }
    }
    public void SelectStoreItemDB(int _id)
    {
        if (ReferenceKeeper.Instance.GameManager.gameDB != null)
        {
            ReferenceKeeper.Instance.GameManager.gameDB.selected_store_item = _id;
        }
    }
    private void GetCurrentScore()
    {
        score_text.text = "Score: " + ReferenceKeeper.Instance.GameManager.score_text.text;
    }
#if UNITY_EDITOR
    private byte _id;
    [EasyButtons.Button]
    private void GenerateItems()
    {
        ClearAllItems();

        StoreItemData data = Resources.Load<StoreItemData>("StoreItems");

        _id = 0;
        GenerateItem(data.silver_items, Color.white);
        GenerateItem(data.green_items, Color.white);
        GenerateItem(data.red_items, Color.white);
        GenerateItem(data.gold_items, Color.white);
        GenerateItem(data.gem_items, UtilitiesMethods.HexToColor("F38922"));
        GenerateItem(data.special_items, Color.white);
    }
    private void GenerateItem(Item[] _items, Color _bgColor)
    {
        if (_items != null && _items.Length > 0)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                StoreItem item_instance = (StoreItem)PrefabUtility.InstantiatePrefab(item_prefab, content);
                item_instance.Init(_id, _items[i].price_underK,_items[i].price_k, _items[i].icon, _items[i].background, _bgColor);
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
                if (content.GetChild(0).GetComponent<StoreItem>())
                    DestroyImmediate(content.GetChild(0).gameObject);
            }
        }
    }

    [EasyButtons.Button]
    private void ChangSilverItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("StoreItems");
        ChangeItemsPrice(data.silver_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangeGreenItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("StoreItems");
        ChangeItemsPrice(data.green_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangRedItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("StoreItems");
        ChangeItemsPrice(data.red_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangGoldItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("StoreItems");
        ChangeItemsPrice(data.gold_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangGemItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("StoreItems");
        ChangeItemsPrice(data.gem_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangSpecialPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("StoreItems");
        ChangeItemsPrice(data.special_items, _underK, _k);
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
    }
    private void ChangeItemsPrice(Item[] _items, int _p_underK, int _p_k)
    {
        if (_items != null)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].price_underK = _p_underK;
                _items[i].price_k = _p_k;
            }
        }
    }
#endif
}
