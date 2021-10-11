using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    [SerializeField] TMP_Text order_text;
    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text score_text;
    [SerializeField] GameObject order_bg;
    [SerializeField] GameObject icon_1;
    [SerializeField] GameObject icon_2;
    [SerializeField] GameObject icon_3;
    [SerializeField] Image frame_1;
    [SerializeField] Image frame_2;

    public void Init(ScoreBoardPlayer _player)
    {
        name_text.text = _player.name;
        score_text.text = _player.score.ToString();
        if(_player.order == -1)
            order_text.text = "5000+";
        else
        {
            if (_player.order == 1)
            {
                order_bg.gameObject.SetActive(false);
                order_text.gameObject.SetActive(false);
                frame_1.color = UtilitiesMethods.HexToColor("FFCD30");
                frame_2.color = UtilitiesMethods.HexToColor("FFCD30");
                icon_1.SetActive(true);
            }
            else if (_player.order == 2)
            {
                order_bg.gameObject.SetActive(false);
                order_text.gameObject.SetActive(false);
                frame_1.color = UtilitiesMethods.HexToColor("1C63FF");
                frame_2.color = UtilitiesMethods.HexToColor("1C63FF");
                icon_2.SetActive(true);
            }
            else if (_player.order == 3)
            {
                order_bg.gameObject.SetActive(false);
                order_text.gameObject.SetActive(false);
                frame_1.color = UtilitiesMethods.HexToColor("FFBA7D");
                frame_2.color = UtilitiesMethods.HexToColor("FFBA7D");
                icon_3.SetActive(true);
            }
            else
            {
                order_text.text = _player.order.ToString();
            }
        }

        if (_player.isMine)
        {
            name_text.color = UtilitiesMethods.HexToColor("EF43D6");
        }
    }
}
