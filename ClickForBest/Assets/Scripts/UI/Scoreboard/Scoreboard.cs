using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public GameObject item_prefab;
    public Transform content;
    public PauseMenu pause_menu;

    private DOMove domove;
    private bool isopen;

    private void OnEnable()
    {
        domove = GetComponent<DOMove>();
    }
    private void Start()
    {
        ReferenceKeeper.Instance.FirebaseService.FirebaseLoginTest();
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
    [EasyButtons.Button]
    public void ReportScore(int _value)
    {
        ReferenceKeeper.Instance.FirebaseService.SetScoreAsync(new ScoreBoardPlayer { score = _value }, null);
    }
    [EasyButtons.Button]
    private void FetchDatas()
    {
        ReferenceKeeper.Instance.FirebaseService.GetScoresAsync((result) => { CompletedFetchDatas(result); });
    }
    private void CompletedFetchDatas(ScoreBoardPlayer[] _players)
    {
        if (_players != null)
        {
            for (int i = 0; i < _players.Length; i++)
            {
                ListItem newItem = Instantiate(item_prefab, content).GetComponent<ListItem>();
                newItem.Init(_players[i]);
            }
        }
    }
}
