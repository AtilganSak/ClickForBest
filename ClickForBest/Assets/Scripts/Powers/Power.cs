using System;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public int time;
    public TMPro.TMP_Text time_text;

    public Action timeIsOver;

    private DOMove doMove;
    private Button button;

    internal bool used;

    private void OnEnable()
    {
        VirtualEnable();
    }
    private void OnDestroy()
    {
        timeIsOver = null;
    }
    internal virtual void VirtualEnable()
    {
        if(time_text)
            time_text.text = time.ToString() + "s";

        doMove = GetComponent<DOMove>();
        button = GetComponentInChildren<Button>();
        if (button)
        {
            button.onClick.AddListener(ClickedPower);
        }
#if UNITY_ANDROID && !UNITY_EDITOR
        ReferenceKeeper.Instance.AdsMessagePanel.Subscribe("Window", "Do you want to watch ads?", "Yes", "No",
            (state) => {
                if (state)
                {   
                    ReferenceKeeper.Instance.RewardAdsController.ShowAd();
                }
            });
#endif
    }
    internal virtual void ClickedPower()
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
#if UNITY_EDITOR
        Use();
#else
        ReferenceKeeper.Instance.RewardAdsController.onAdsShowComplete += () =>
        {
            Use();
        };
        ReferenceKeeper.Instance.AdsMessagePanel.Show();
#endif
    }
    internal virtual void Use()
    {
        Debug.Log("Used Power");

        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Time, true);

        used = true;
        Hide();
    }
    internal void TimeOver()
    {
        ReferenceKeeper.Instance.UISound.StopSound();
        if (timeIsOver != null)
        {
            timeIsOver.Invoke();
        }
    }
    public void Appear()
    {
        doMove.DO();
    }
    public void Hide()
    {
        doMove.DORevert();
    }
}
