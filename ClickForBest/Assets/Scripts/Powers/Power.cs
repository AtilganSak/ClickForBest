using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public enum PowerType
    {
        AutoClick,
        ScoreBoost,
        ExtraScore
    }

    public PowerType power_type;

    #region Auto Click
    [ConditionalField("power_type", PowerType.AutoClick)]
    [SerializeField] float click_interval = 0.3F;
    [ConditionalField("power_type", PowerType.AutoClick)]
    private WaitForSeconds WaitForSeconds;
    [ConditionalField("power_type", PowerType.AutoClick)]
    private WaitForSeconds WaitForSeconds2;
    private int counter;
    #endregion
    #region Extra Score
    [ConditionalField("power_type", PowerType.ExtraScore)]
    public int score_value;
    [ConditionalField("power_type", PowerType.ExtraScore)]
    public TMPro.TMP_Text score_text;
    #endregion
    #region ScoreBoost
    [ConditionalField("power_type", PowerType.ScoreBoost)]
    [SerializeField] TMPro.TMP_Text boost_text;
    [Header("Score Boost Parameter")]
    public int[] boost_options;
    private int selected_option;
    #endregion

    public int time;
    public TMPro.TMP_Text time_text;

    public Action timeIsOver;
    public Action onUse;

    public bool isShow { get; private set; }
    public bool used { get; private set; }

    private DOMove doMove;
    private Button button;

    private void OnEnable()
    {
        if (time_text)
            time_text.text = time.ToString() + "s";

        doMove = GetComponent<DOMove>();
        if (doMove)
        {
            doMove.doComplete.AddListener(CompletedDOAppear);
            doMove.doRevertComplete.AddListener(CompletedDOHide);
        }
        button = GetComponentInChildren<Button>();
        if (button)
        {
            button.onClick.AddListener(ClickedPower);
        }
#if UNITY_ANDROID && !UNITY_EDITOR
#endif
        ReferenceKeeper.Instance.AdsMessagePanel.Subscribe("Window", "Do you want to watch ads?", "Yes", "No",
            (state) => {
                if (state)
                {   
                    ReferenceKeeper.Instance.RewardAdsController.ShowAd(Use);
                }
            });

        #region Auto Click
        if (power_type == PowerType.AutoClick)
        {
            WaitForSeconds = new WaitForSeconds(1);
            WaitForSeconds2 = new WaitForSeconds(click_interval);
            counter = time;
        }
        #endregion
        #region Extra Score
        if (power_type == PowerType.ExtraScore)
        {
            if (score_text != null)
            {
                score_text.text = "+" + score_value.ToString();
            }
        }
        #endregion
        #region Score Boost
        if (power_type == PowerType.ScoreBoost)
        {
            selected_option = boost_options[UnityEngine.Random.Range(0, boost_options.Length)];
            boost_text.text = selected_option.ToString() + "x";
        }
        #endregion
    }
    private void ClickedPower()
    {
        if (ReferenceKeeper.Instance.GooglePlayServices.internet)
        {
            ReferenceKeeper.Instance.GameManager.click_ad_count++;
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
            ReferenceKeeper.Instance.AdsMessagePanel.Show();
        }
    }
    private void Use(ShowResult _callback)
    {
        if (_callback == ShowResult.Finished)
        {
            if (!used)
            {
                switch (power_type)
                {
                    case PowerType.AutoClick:
                        UseForAutoClick();
                        break;
                    case PowerType.ScoreBoost:
                        UseForScoreBoost();
                        break;
                    case PowerType.ExtraScore:
                        UseForExtraScore();
                        break;
                }
                used = true;
                if (IsInvoking("Hide"))
                {
                    CancelInvoke("Hide");
                }
                Hide();
            }
        }
    }
    private void TimeOver()
    {
        ReferenceKeeper.Instance.UISound.StopSound();
        if (timeIsOver != null)
        {
            timeIsOver.Invoke();
        }
    }
    public void Appear()
    {
        isShow = true;
        doMove.DO();
    }
    public void Hide()
    {
        isShow = false;
        doMove.DORevert();
    }
    private void CompletedDOAppear()
    {
        Invoke("Hide", 60);
    }
    private void CompletedDOHide()
    {
        if (!used)
        {
            TimeOver();
        }
    }
    private void UseForAutoClick()
    {
        StartCoroutine(ReferenceKeeper.Instance.UISound.PlaySoundMainThread(UISound.Sound.Time, true));
        ReferenceKeeper.Instance.TimeCircle.StartTimer(counter);
        StartCoroutine(Timer());
        StartCoroutine(Process());
    }
    private void UseForExtraScore()
    {
        ReferenceKeeper.Instance.GameManager.AddScore(score_value);

        TimeOver();
    }
    private void UseForScoreBoost()
    {
        StartCoroutine(ReferenceKeeper.Instance.UISound.PlaySoundMainThread(UISound.Sound.Time, true));
        ReferenceKeeper.Instance.GameManager.SetBoost(selected_option);
        ReferenceKeeper.Instance.TimeCircle.StartTimer(time);
        Invoke("DeactivePower", time);
    }

    #region Auto Click Methods
    private IEnumerator Timer()
    {
        while (counter > 0)
        {
            counter--;
            yield return WaitForSeconds;
        }
        counter = 0;
        TimeOver();
    }
    private IEnumerator Process()
    {
        while (counter > 0)
        {
            ReferenceKeeper.Instance.ClickController.Pressed_Click_Button();

            yield return WaitForSeconds2;
        }
    }
    #endregion
    #region Score Boost Method
    private void DeactivePower()
    {
        TimeOver();
        ReferenceKeeper.Instance.GameManager.SetBoost(1);
    }
    #endregion
}
