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

    public bool isPurchased;
    public int p_underK;
    public int p_K;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pressed_Button);

        ReferenceKeeper.Instance.Store.onPurchase += OnPurchased;

        //if (isPurchased)
        //{
        //    Purchased(true);
        //}
    }
    public void Init(byte _id, int _p_underK, int _p_k, Sprite _icon, Sprite _background, Color _bgColor)
    {
        p_underK = _p_underK;
        p_K = _p_k;
        if (p_K > 0)
        {
            price_text.text = p_K + "." + (p_underK > 0 ? p_underK.ToString().Substring(0, 1) : p_underK.ToString()) + "K";
        }
        else
        {
            price_text.text = p_underK.ToString();
        }

        ID = _id;

        icon_image.sprite = _icon;
        background_image.sprite = _background;
        background_image.color = _bgColor;
    }
    private void Pressed_Button()
    {
        if (!isPurchased)
        {
            ReferenceKeeper.Instance.Store.Pressed_Item_Button(ID, ReferenceKeeper.Instance.GameManager.HaveScore(p_underK, p_K));
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
    public void Purchased(bool _onStart = false)
    {
        isPurchased = true;

        price_text.enabled = false;
        check_image.enabled = true;

        if(!_onStart)
            ReferenceKeeper.Instance.GameManager.RemoveScore(p_underK, p_K);
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
