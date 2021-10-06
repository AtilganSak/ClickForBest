using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{
    [SerializeField] TMP_Text order_text;
    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text score_text;

    public void Init(ScoreBoardPlayer _player)
    {
        name_text.text = _player.name;
        score_text.text = _player.score.ToString();
        if(_player.order == -1)
            order_text.text = "5000+";
        else
            order_text.text = _player.order.ToString();

        if (_player.isMine)
        {
            name_text.color = UtilitiesMethods.HexToColor("EF43D6");
        }
    }
}
