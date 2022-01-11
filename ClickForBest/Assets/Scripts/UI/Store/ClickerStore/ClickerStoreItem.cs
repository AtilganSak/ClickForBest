using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ClickerStoreItem : MonoBehaviour
{
    public byte ID;

    [SerializeField] TMP_Text price_text;
    [SerializeField] Image icon_image;
    [SerializeField] Image background_image;
    [SerializeField] Image check_image;
    [SerializeField] DOFade frame_image;
    [SerializeField] Image token_image;

    public Sprite sprite;

    public bool isPurchased;
    public int price;

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pressed_Button);
    }
    public void Init(byte _id, int _price, Sprite _icon)
    {
        sprite = _icon;
        price = _price;

        price_text.text = price.ToString();

        ID = _id;

        icon_image.sprite = _icon;

        if (price == 0)
        {
            price_text.gameObject.SetActive(false);
            token_image.enabled = false;
            check_image.enabled = true;
        }
    }
    private void Pressed_Button()
    {
        if (!isPurchased)
        {
            //ReferenceKeeper.Instance.Store.Pressed_Item_Button(ID, ReferenceKeeper.Instance.GameManager.HaveScore(p_underK, p_K));
        }
        else
        {
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Select);
            ReferenceKeeper.Instance.ClickerStore.Pressed_Item_Select(this);
        }
    }
    public void Purchased()
    {
        isPurchased = true;

        price_text.enabled = false;
        token_image.enabled = false;
        check_image.enabled = true;
        button.interactable = true;
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
