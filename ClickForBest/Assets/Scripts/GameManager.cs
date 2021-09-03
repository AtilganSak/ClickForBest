using System;
using UnityEngine;
using TMPro;
using System.Globalization;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text score_text;
    [SerializeField] TMP_Text full_score_text;
    [SerializeField] TMP_Text level_text;

    private int boost = 1;
    private int total_coin_count;
    private int score;
    private int level;

    private void Start()
    {
        Application.targetFrameRate = 90;

        boost = 1;

        //Get score and level on the PlayerPrefs or Firebase
        score_text.text = score.ToKMB();
        full_score_text.text = score.ToString();

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
            ReferenceKeeper.Instance.ClickButton.interactable = true;
            ReferenceKeeper.Instance.BottomCollider.SetActive(true);
            ReferenceKeeper.Instance.Handle.ResetUp();

            GC.Collect();
        }
    }
    public void AddScore()
    {
        if (boost > 1)
            ReferenceKeeper.Instance.BoostTextControl.Show(boost + "x");
        if (score + boost >= int.MaxValue || score + boost < 0)
        {
            score = 0;
            if (level == 0)
                level_text.gameObject.SetActive(true);
            level++;
            level_text.text = level.ToKMB();
        }
        else
        {
            score += boost;
        }
        score_text.text = score.ToKMB();
        full_score_text.text = score.ToString();
    }
    [EasyButtons.Button]
    public void AddScore(int _value)
    {
        if (score + _value >= int.MaxValue || score + _value < 0)
        {
            score = 0;
            if (level == 0)
                level_text.gameObject.SetActive(true);
            level++;
            level_text.text = level.ToKMB();
        }
        else
        {
            score += _value;
        }
        score_text.text = score.ToKMB();
        full_score_text.text = score.ToString();
    }
    public void SetBoost(int _value)
    {
        boost = _value;
    }
    private void HandleActivated()
    {
        ReferenceKeeper.Instance.ClickButton.interactable = false;
        ReferenceKeeper.Instance.BottomCollider.SetActive(false);
    }
    private void HandleDeactivated()
    {
        ReferenceKeeper.Instance.ClickButton.interactable = true;
        ReferenceKeeper.Instance.BottomCollider.SetActive(true);
    }
}
