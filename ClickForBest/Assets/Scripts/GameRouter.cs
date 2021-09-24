using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameRouter : MonoBehaviour
{
    public CanvasGroup info_part;
    public CanvasGroup info_part2;
    public CanvasGroup loading_part;
    public TMP_Text local_score_text;
    public TMP_Text local_purchased_text;
    public TMP_Text cloud_score_text;
    public TMP_Text cloud_purchased_text;
    public TMP_Text cloud_score_text2;
    public TMP_Text cloud_purchased_text2;

    private FirebaseService f_service;
    private GameDB cloud_db;

    private void Start()
    {
        GooglePlayServices service = FindObjectOfType<GooglePlayServices>();
        f_service = FindObjectOfType<FirebaseService>();
        if (service)
        {
            service.onLogin += LoggedGoogle;
            f_service.onLogin += LoggedFirebase;

            if (service.internet)
            {
                service.Login();
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    private void LoggedGoogle(bool _state)
    {
        if (!_state)
        {
            SceneManager.LoadScene(1);
        }
    }
    private void LoggedFirebase(bool _state)
    {
        if (_state)
        {
            Debug.LogFormat("Getting GameDB");
            f_service.GetGameDBAsync((gameData => { LoadGameDB(gameData); }));
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    public void LoadCloudDatas()
    {
        if (cloud_db != null)
        {
            EasyJson.SaveJsonToFile(cloud_db);
            SceneManager.LoadScene(1);
        }
    }
    public void LoadLocalDatas()
    {
        SceneManager.LoadScene(1);
    }
    [EasyButtons.Button]
    private void Test()
    {
        GameDB cloudDB = new GameDB
        {
            score = new Score
            {
                k = 6,
            },
            activeStoreItems = new int[2] { 2, 3 },
        };
        LoadGameDB(cloudDB);
    }
    private void LoadGameDB(GameDB _db)
    {
        Debug.LogFormat("Loaded GameDB: " + _db);
        if (_db != null)
        {
            bool diff = false;
            cloud_db = _db;
            GameDB local_db = EasyJson.GetJsonToFile<GameDB>();
            Debug.LogFormat("LocalDB: " + local_db);
            if (local_db == null)
            {
                //Debug.LogFormat("LocalDB is null");
                info_part2.alpha = 1;
                loading_part.alpha = 0;
                cloud_score_text2.text = cloud_db.score.Calculate(0);
                if (cloud_db.activeStoreItems != null)
                    cloud_purchased_text2.text = cloud_db.activeStoreItems.Length.ToString();
                else
                    cloud_purchased_text2.text = "0";
            }
            else
            {
                Debug.LogFormat("LocalDB isnot null");
                Debug.LogFormat("CloudDB: " + cloud_db);
                Debug.LogFormat(@"CloudDB Score: " + "k:" + cloud_db.score.k + "\n" + "underK: " + cloud_db.score.underK);
                Score grater_score = CompareScoreReturn(cloud_db.score, local_db.score);
                if (grater_score != null)
                {
                    loading_part.alpha = 0;
                    info_part.alpha = 1;

                    cloud_score_text.text = cloud_db.score.Calculate(0);
                    local_score_text.text = local_db.score.Calculate(0);

                    cloud_score_text.color = Color.green;
                    local_score_text.color = Color.red;

                    diff = true;
                }
                else
                {
                    Debug.LogFormat("CloudDB not diff");
                    grater_score = CompareScoreReturn(local_db.score, cloud_db.score);
                    if (grater_score != null)
                    {
                        loading_part.alpha = 0;
                        info_part.alpha = 1;

                        cloud_score_text.text = cloud_db.score.Calculate(0);
                        local_score_text.text = local_db.score.Calculate(0);

                        local_score_text.color = Color.green;
                        cloud_score_text.color = Color.red;

                        diff = true;
                    }
                    else
                    {
                        Debug.LogFormat("LocalDB not diff");
                    }
                }
                cloud_purchased_text.text = "0";
                local_purchased_text.text = "0";
                if (cloud_db.activeStoreItems != null && local_db.activeStoreItems != null)
                {
                    if (cloud_db.activeStoreItems.Length > local_db.activeStoreItems.Length)
                    {
                        diff = true;
                        cloud_purchased_text.color = Color.green;
                        local_purchased_text.color = Color.red;
                    }
                    else if (cloud_db.activeStoreItems.Length < local_db.activeStoreItems.Length)
                    {
                        diff = true;
                        cloud_purchased_text.color = Color.red;
                        local_purchased_text.color = Color.green;
                    }
                    if (diff)
                    {
                        loading_part.alpha = 0;
                        info_part.alpha = 1;

                        cloud_purchased_text.text = cloud_db.activeStoreItems.Length.ToString();
                        local_purchased_text.text = local_db.activeStoreItems.Length.ToString();
                    }
                }

                if (!diff)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    private Score CompareScoreReturn(Score _score1, Score _score2)
    {
        if (_score1.d > _score2.d)
            return _score1;
        else if (_score1.n > _score2.n)
            return _score1;
        else if (_score1.o > _score2.o)
            return _score1;
        else if (_score1.sp > _score2.sp)
            return _score1;
        else if (_score1.s > _score2.s)
            return _score1;
        else if (_score1.qt > _score2.qt)
            return _score1;
        else if (_score1.q > _score2.q)
            return _score1;
        else if (_score1.t > _score2.t)
            return _score1;
        else if (_score1.b > _score2.b)
            return _score1;
        else if (_score1.m > _score2.m)
            return _score1;
        else if (_score1.k > _score2.k)
            return _score1;
        else if (_score1.underK > _score2.underK)
            return _score1;
        else return null;
    }
}
