using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public GameObject item_prefab;
    public Transform content;
    public PauseMenu pause_menu;
    public DORotate loading;
    public GameObject noscore;
    public GameObject noconnection;
    public ParticleSystem bg_particle;
    public Transform limitTransform;
    public ListItem tempListItem;

    private DOMove domove;
    private bool isopen;
    private bool isMoving;
    private bool isLoading;

    private Transform mineItem;

    string[] names = new string[]
    {
        "Arthur",
        "Nick",
        "Hector",
        "Jesus",
        "Albert",
        "Paul",
        "Hope",
        "Kevin",
        "Michael",
        "Jackson",
        "Hector",
        "Jesus",
        "Albert",
        "Paul",
        "Hope",
        "Kevin",
        "Michael",
        "Jackson"
    };

    private void Update()
    {
        if (mineItem != null)
        {
            if ((limitTransform.position.y - mineItem.position.y) < 0.05F)
            {
                limitTransform.gameObject.SetActive(false);
            }
            else
            {
                limitTransform.gameObject.SetActive(true);
            }
        }
    }
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
            ClearItems();
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
        bg_particle.Play();
        FetchDatas();
    }
    private void ClosedPanel()
    {
        isMoving = false;
        bg_particle.Stop();
    }
    [EasyButtons.Button]
    private void FetchDatas()
    {
        if (!isLoading)
        {
            noconnection.SetActive(false);
            if (ReferenceKeeper.Instance.GooglePlayServices.internet)
            {
                if (ReferenceKeeper.Instance.FirebaseService)
                {
                    noscore.SetActive(false);
                    loading.gameObject.SetActive(true);
                    loading.DOLoop();
                    isLoading = true;
                    ReferenceKeeper.Instance.FirebaseService.GetScoresOrderLimit5000Async((result) => { CompletedFetchDatas(result); });
                }
            }
            else
            {
                noconnection.SetActive(true);
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
            bool isHere = false;
            for (int i = 0; i < 50; i++)
            {
                if (i > _players.Length - 1) break;

                ListItem newItem = Instantiate(item_prefab, content).GetComponent<ListItem>();
                newItem.Init(_players[i]);
                if (_players[i].isMine)
                {
                    mineItem = newItem.transform;
                    isHere = true;
                }
            }
            if (!isHere)
            {
                ScoreBoardPlayer myPlayer = null;
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
                    ListItem newItem = Instantiate(item_prefab, content).GetComponent<ListItem>();
                    newItem.Init(myPlayer);
                    mineItem = newItem.transform;
                    limitTransform.gameObject.SetActive(true);
                    tempListItem.Init(myPlayer);
                }
                else
                {
                    ReferenceKeeper.Instance.FirebaseService.GetMyScoreAsync((result) => CompletedGetMyScore(result));
                }
            }
        }
        else
        {
            noscore.SetActive(true);
        }
    }
    private void CompletedGetMyScore(ScoreBoardPlayer _player)
    {
        if (_player != null)
        {
            _player.order = -1;
            limitTransform.gameObject.SetActive(true);
            tempListItem.Init(_player);
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
#if UNITY_EDITOR
    [EasyButtons.Button]
    private void GenerateTestDatas()
    {
        int score = 1500;
        for (int i = 0; i < names.Length; i++)
        {
            ListItem listItem = ((GameObject)PrefabUtility.InstantiatePrefab(item_prefab, content)).GetComponent<ListItem>();
            ScoreBoardPlayer newPlayer = new ScoreBoardPlayer
            {
                name = names[i],
                order = i + 1,
                score = score,
            };
            score -= 100;
            listItem.Init(newPlayer);
        }
    }
#endif
}
