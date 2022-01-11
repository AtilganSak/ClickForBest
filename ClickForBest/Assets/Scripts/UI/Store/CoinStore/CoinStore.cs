using UnityEditor;
using UnityEngine;
using UnityEngine.Advertisements;

public class CoinStore : MonoBehaviour
{
    [SerializeField] StoreItem item_prefab;
    [SerializeField] Transform content;
    [SerializeField] PauseMenu pause_menu;
    [SerializeField] MessagePanel message_panel;
    [SerializeField] MessagePanel noads_message_panel;
    [SerializeField] RewardItem reward_item_500;
    [SerializeField] TMPro.TMP_Text score_text;

    public System.Action<int> onTakenReward;

    private StoreItem selected_item;
    private StoreItem[] items;

    private DOMove domove;

    private bool pressed_reward;
    private bool isopen;
    private int will_taken_reward;

    [System.Serializable]
    public struct Item
    {
        public int price_underK;
        public int price_k;
        public int price_m;
        public Sprite icon;
        public Sprite background;
        public GameObject prefab;
    }

    private void Start()
    {
        domove = GetComponent<DOMove>();
        items = content.GetComponentsInChildren<StoreItem>();
        if (items != null)
        {
            items[0].isActive = true;
        }

        if (message_panel)
        {
            message_panel.Subscribe("Window", "Are you sure?", "Yes", "No", MessagePanelResult);
        }
        if (noads_message_panel)
        {
            noads_message_panel.Subscribe("Window", "No ads available!", "OK", "OK", null);
        }

        if (reward_item_500)
            reward_item_500.onClick += Pressed_Reward_Item;

        CheckAvailableStoreItems();
        GetCurrentScore();
        LoadSelectedItem();
    }

    public void OpenCloseMenu()
    {
        if (!isopen)
        {
            GetCurrentScore();
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Slide);
            CheckAvailableStoreItems();
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
    public void Pressed_Item_Select(StoreItem _item)
    {
        if (selected_item)
            selected_item.UnSelect();
        selected_item = _item;
        selected_item.Select();
        if (selected_item.coin_prefab != null)
            ReferenceKeeper.Instance.CoinSpawner.SetCoinPrefab(selected_item.coin_prefab);
        else
            ReferenceKeeper.Instance.CoinSpawner.SetCoinSprite(selected_item.coin_sprite);
        SelectStoreItemDB(_item.ID);
    }
    public void CheckAvailableStoreItems(bool _showNot = false)
    {
        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (!items[i].isActive)
                {
                    if (ReferenceKeeper.Instance.GameManager.HaveScore(items[i].p_underK, items[i].p_K, items[i].p_M))
                    {
                        items[i].Activate(_showNot);
                        if (_showNot)
                        {
                            ReferenceKeeper.Instance.CoinNotificationFlag.Adjust(items[i].coin_sprite);
                            ReferenceKeeper.Instance.CoinNotificationFlag.ShowNotification();
                        }
                    }
                }
            }
        }
    }
    private void MessagePanelResult(bool _result)
    {
        if (_result)
        {
            if (pressed_reward)
            {
                //ReferenceKeeper.Instance.RewardAdsController.onAdsShowComplete += () =>
                //{
                //if (onTakenReward != null)
                //{
                //    onTakenReward.Invoke(will_taken_reward);
                //}
                //pressed_reward = false;
                //};
                ReferenceKeeper.Instance.RewardAdsController.ShowAd(AdsShowCompleted);
            }
        }
        else
        {
            will_taken_reward = 0;
            pressed_reward = false;
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
                ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.PopUp);
                message_panel.Change(_message: "Do you want to watch ads?");
                message_panel.Show();
            }
        }
        else
        {
            if (noads_message_panel)
            {
                ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Fail);
                noads_message_panel.Show();
            }
        }
    }
    private void AdsShowCompleted(ShowResult _result)
    {
        if (_result == ShowResult.Finished)
        {
            if (onTakenReward != null)
            {
                onTakenReward.Invoke(will_taken_reward);
            }
            GetCurrentScore();
            pressed_reward = false;
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
    private void LoadSelectedItem()
    {
        int selectedItemIndex = 0;
        StoreItem[] items = content.GetComponentsInChildren<StoreItem>();
        if (ReferenceKeeper.Instance.GameManager.gameDB != null)
        {
            selectedItemIndex = ReferenceKeeper.Instance.GameManager.gameDB.selected_store_item;
            if (items != null)
            {
                SelectStoreItem(selectedItemIndex);
            }
        }
        else
        {
            SelectStoreItem(selectedItemIndex);
        }
    }
    private void SelectStoreItem(int _index)
    {
        selected_item = items[_index];
        selected_item.Select();
        if (selected_item.coin_prefab != null)
            ReferenceKeeper.Instance.CoinSpawner.SetCoinPrefab(selected_item.coin_prefab);
        else
            ReferenceKeeper.Instance.CoinSpawner.SetCoinSprite(selected_item.coin_sprite);
    }

    #region Editor
#if UNITY_EDITOR
    private byte _id;
    [EasyButtons.Button]
    private void GenerateItems()
    {
        ClearAllItems();

        StoreItemData data = Resources.Load<StoreItemData>("CoinStoreItems");

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
                item_instance.Init(_id, _items[i].price_underK, _items[i].price_k, _items[i].price_m, _items[i].icon, _items[i].background, _items[i].prefab, _bgColor);
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
        StoreItemData data = Resources.Load<StoreItemData>("CoinStoreItems");
        ChangeItemsPrice(data.silver_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangeGreenItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("CoinStoreItems");
        ChangeItemsPrice(data.green_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangRedItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("CoinStoreItems");
        ChangeItemsPrice(data.red_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangGoldItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("CoinStoreItems");
        ChangeItemsPrice(data.gold_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangGemItemsPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("CoinStoreItems");
        ChangeItemsPrice(data.gem_items, _underK, _k);
    }
    [EasyButtons.Button]
    private void ChangSpecialPrice(int _underK, int _k)
    {
        StoreItemData data = Resources.Load<StoreItemData>("CoinStoreItems");
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
    #endregion
}
