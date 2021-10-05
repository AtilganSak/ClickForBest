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
    }
}
