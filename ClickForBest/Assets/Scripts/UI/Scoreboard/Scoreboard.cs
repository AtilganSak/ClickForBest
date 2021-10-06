using System.Linq;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public GameObject item_prefab;
    public Transform content;
    public PauseMenu pause_menu;
    public DORotate loading;
    public GameObject noscore;

    private DOMove domove;
    private bool isopen;
    private bool isMoving;
    private bool isLoading;

    private void OnEnable()
    {
        domove = GetComponent<DOMove>();
        if (domove)
        {
            domove.doComplete.AddListener(OpenedPanel);
            domove.doRevertComplete.AddListener(ClosedPanel);
        }
        loading.gameObject.SetActive(false);
        noscore.SetActive(false);
    }
    private void Start()
    {
        //ReferenceKeeper.Instance.FirebaseService.FirebaseLoginTest();
    }
    public void OpenCloseMenu()
    {
        if (isMoving) return;

        if (!isopen)
        {
            isopen = true;
            domove.DO();
            isMoving = true;
        }
        else
        {
            isopen = false;
            domove.DORevert();
            isMoving = true;
            if (pause_menu)
            {
                pause_menu.Pressed_Pause_Button();
            }
        }
    }
    private void OpenedPanel()
    {
        isMoving = false;
        FetchDatas();
    }
    private void ClosedPanel()
    {
        isMoving = false;
    }
    [EasyButtons.Button]
    private void FetchDatas()
    {
        if (!isLoading)
        {
            if (ReferenceKeeper.Instance.FirebaseService)
            {
                ClearItems();
                noscore.SetActive(false);
                loading.gameObject.SetActive(true);
                loading.DOLoop();
                isLoading = true;
                ReferenceKeeper.Instance.FirebaseService.GetScoresOrderLimitAsync((result) => { CompletedFetchDatas(result); });
            }
        }
    }
    private void CompletedFetchDatas(ScoreBoardPlayer[] _players)
    {
        isLoading = false;
        loading.ResetDO();
        loading.gameObject.SetActive(false);
        if (_players != null && _players.Length > 0)
        {
            bool createdIsMine = false;
            for (int i = 0; i < _players.Length; i++)
            {
                ListItem newItem = Instantiate(item_prefab, content).GetComponent<ListItem>();
                newItem.Init(_players[i]);
                if (_players[i].isMine)
                    createdIsMine = true;
            }
            if (!createdIsMine)
            {
                ReferenceKeeper.Instance.FirebaseService.GetFirst5000ScoresAsync((result) => CompletedGetFirst5000(result));
            }
        }
        else
        {
            noscore.SetActive(true);
        }
    }
    private void CompletedGetFirst5000(ScoreBoardPlayer[] _players)
    {
        if (_players != null)
        {
            ScoreBoardPlayer myPlayer = null;
            bool isHere = false;
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].isMine)
                {
                    myPlayer = _players[i];
                    myPlayer.order = i + 1;
                    isHere = true;
                }
            }
            if (isHere)
            {
                ListItem lastItem = content.GetChild(content.childCount - 1).GetComponent<ListItem>();
                lastItem.Init(myPlayer);
            }
            else
            {
                ReferenceKeeper.Instance.FirebaseService.GetMyScoreAsync((result) => CompletedGetMyScore(result));
            }
        }
    }
    private void CompletedGetMyScore(ScoreBoardPlayer _player)
    {
        if (_player != null)
        {
            _player.order = -1;
            ListItem lastItem = content.GetChild(content.childCount - 1).GetComponent<ListItem>();
            lastItem.Init(_player);
        }
    }
    [EasyButtons.Button]
    private void ClearItems()
    {
        ListItem[] listItems = content.GetComponentsInChildren<ListItem>();
        if (listItems != null)
        {
            int child_count = listItems.Length;
            for (int i = 0; i < child_count; i++)
            {
                if (Application.isPlaying)
                {
                    Destroy(listItems[i].gameObject);
                }
                else
                {
                    DestroyImmediate(listItems[i].gameObject);
                }
            }
        }
    }
}
