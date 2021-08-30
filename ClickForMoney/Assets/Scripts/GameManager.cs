using System;
using UnityEngine;
using TMPro;
using System.Globalization;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text score_text;
    [SerializeField] TMP_Text level_text;

    int total_coin_count;
    int score;
    int level;

    private void Start()
    {
        Application.targetFrameRate = 90;

        //Get score and level on the PlayerPrefs or Firebase
        score_text.text = "0";

        if (level > 0)
        {
            level_text.gameObject.SetActive(true);
            level_text.text = level.ToKMB();
        }
        else
        {
            level_text.gameObject.SetActive(false);
        }

        ReferenceKeeper.Instance.Handle.onActive.AddListener(HandleActivated);
        ReferenceKeeper.Instance.Handle.onDeactive.AddListener(HandleDeactivated);
    }
    public void AddCoin()
    {
        total_coin_count++;
    }
    public void RemoveCoin()
    {
        if (total_coin_count - 1 >= 0)
        {
            total_coin_count--;
        }
        if (total_coin_count <= 0)
        {
            ReferenceKeeper.Instance.ClickButton.SetActive(true);
            ReferenceKeeper.Instance.BottomCollider.SetActive(true);
            ReferenceKeeper.Instance.Handle.ResetUp();

            GC.Collect();
        }
    }
    [EasyButtons.Button]
    public void AddScore()
    {
        if (score + 1 >= int.MaxValue || score + 1 < 0)
        {
            score = 0;
            if (level == 0)
                level_text.gameObject.SetActive(true);
            level++;
            level_text.text = level.ToKMB();
        }
        else
        {
            score++;
        }
        score_text.text = score.ToKMB();
    }
    private void HandleActivated()
    {
        ReferenceKeeper.Instance.ClickButton.SetActive(false);
        ReferenceKeeper.Instance.BottomCollider.SetActive(false);
    }
    private void HandleDeactivated()
    {
        ReferenceKeeper.Instance.ClickButton.SetActive(true);
        ReferenceKeeper.Instance.BottomCollider.SetActive(true);
    }
}
