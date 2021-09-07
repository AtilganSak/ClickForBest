using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    [SerializeField] TMP_Text price_text;
    [SerializeField] Image icon_image;
    [SerializeField] Image background_image;

    public void Init(int _price, Sprite _icon, Sprite _background, Color _bgColor)
    {
        if (_price <= 0)
            _price = 0;
        price_text.text = _price.ToString();

        icon_image.sprite = _icon;
        background_image.sprite = _background;
        background_image.color = _bgColor;
    }
}
