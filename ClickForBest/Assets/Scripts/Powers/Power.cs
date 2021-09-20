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

        ReferenceKeeper.Instance.AdsMessagePanel.Subscribe("Window", "Do you want to watch ads?", "Yes", "No",
            (state) => {
                if (state)
                {   
                    ReferenceKeeper.Instance.RewardAdsController.ShowAd();
                }
            });
    }
    internal virtual void ClickedPower()
    {
        ReferenceKeeper.Instance.RewardAdsController.onAdsShowComplete += () =>
        {
            Use();
        };
        ReferenceKeeper.Instance.AdsMessagePanel.Show();
    }
    internal virtual void Use()
    {
        Debug.Log("Used Power");

        used = true;
        Hide();
    }
    internal void TimeOver()
    {
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
