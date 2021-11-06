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
    [SerializeField] Image shadow_image;
    [SerializeField] Image lock_image;
    [SerializeField] GameObject notification_icon;

    public GameObject coin_prefab;
    public Sprite coin_sprite;

    public bool isActive;
    public int p_underK;
    public int p_K;
    public int p_M;

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pressed_Button);
    }
    public void Init(byte _id, int _p_underK, int _p_k, int _p_m, Sprite _icon, Sprite _background, GameObject _prefab, Color _bgColor)
    {
        coin_sprite = _icon;
        coin_prefab = _prefab;
        p_underK = _p_underK;
        p_K = _p_k;
        p_M = _p_m;
        if (p_M > 0)
        {
            price_text.text = p_M + "." + (p_K > 0 ? p_K.ToString().Substring(0, 1) : p_K.ToString()) + "M";
        }
        else if (p_K > 0)
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

        if (_p_underK == 0 && _p_k == 0 && _p_m == 0)
        {
            price_text.gameObject.SetActive(false);
            shadow_image.gameObject.SetActive(false);
            lock_image.gameObject.SetActive(false);
            check_image.enabled = true;
        }
    }
    private void Pressed_Button()
    {
        if (!isActive)
        {
            //ReferenceKeeper.Instance.Store.Pressed_Item_Button(ID, ReferenceKeeper.Instance.GameManager.HaveScore(p_underK, p_K));
        }
        else
        {
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Select);
            ReferenceKeeper.Instance.Store.Pressed_Item_Select(this);
        }
    }
    public void Activate(bool _showNotIcon = false)
    {
        isActive = true;

        shadow_image.enabled = false;
        price_text.enabled = false;
        lock_image.enabled = false;
        check_image.enabled = true;
        button.interactable = true;
        if(_showNotIcon)
            notification_icon.SetActive(true);
    }
    public void Select()
    {
        if(notification_icon.activeInHierarchy)
            notification_icon.SetActive(false);
        frame_image.DOLoop();
    }
    public void UnSelect()
    {
        frame_image.ResetDO();
    }
}
