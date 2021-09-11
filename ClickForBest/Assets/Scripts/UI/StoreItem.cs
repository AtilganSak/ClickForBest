using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StoreItem : MonoBehaviour
{
    public byte ID;

    [SerializeField] TMP_Text price_text;
    [SerializeField] Image icon_image;
    [SerializeField] Image background_image;
    [SerializeField] Image check_image;
    [SerializeField] DOFade frame_image;

    private Button button;
    private bool isPurchased;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pressed_Button);

        ReferenceKeeper.Instance.Store.onPurchase += OnPurchased;
    }
    public void Init(byte _id, int _price, Sprite _icon, Sprite _background, Color _bgColor)
    {
        if (_price <= 0)
            _price = 0;
        price_text.text = _price.ToString();

        ID = _id;

        icon_image.sprite = _icon;
        background_image.sprite = _background;
        background_image.color = _bgColor;
    }
    private void Pressed_Button()
    {
        if (!isPurchased)
        {
            ReferenceKeeper.Instance.Store.Pressed_Item_Button(ID);
        }
        else
        {
            ReferenceKeeper.Instance.Store.Pressed_Item_Select(this);
        }
    }
    private void OnPurchased(byte _id)
    {
        if (_id == ID)
        {
            Purchased();
        }
    }
    private void Purchased()
    {
        isPurchased = true;

        price_text.enabled = false;
        check_image.enabled = true;
    }
    public void Select()
    {
        frame_image.DOLoop();
    }
    public void UnSelect()
    {
        frame_image.ResetDO();
    }
}
