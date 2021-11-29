using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardItem : MonoBehaviour
{
    [SerializeField] int reward_value;
    public int Reward_value { get => reward_value; }

    public Image ads_icon;
    public Image no_connection_icon;
    public Image check_icon;
    public TMP_Text price_text;

    public Action<int> onClick;
    
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Pressed_Button);

        if (ReferenceKeeper.Instance.GooglePlayServices != null && ReferenceKeeper.Instance.GooglePlayServices.internet)
        {
            no_connection_icon.enabled = false;
            ads_icon.enabled = true;
            button.interactable = true;
        }
        else
        {
            ads_icon.enabled = false;
            no_connection_icon.enabled = true;
            button.interactable = false;
        }

        if (ReferenceKeeper.Instance.GooglePlayServices)
        {
            ReferenceKeeper.Instance.GooglePlayServices.onInternetChanged += (state) =>
            {
                if (state)
                {
                    no_connection_icon.enabled = false;
                    ads_icon.enabled = true;
                    button.interactable = true;
                }
                else
                {
                    ads_icon.enabled = false;
                    no_connection_icon.enabled = true;
                    button.interactable = false;
                }
            };
        }

        price_text.text = "+" + reward_value.ToString();

        ReferenceKeeper.Instance.Store.onTakenReward += RewardTaken;
    }
    private void Pressed_Button()
    {
        if (onClick != null)
        {
            onClick.Invoke(Reward_value);
        }
    }
    private void RewardTaken(int _value)
    {
        if (_value == reward_value)
        {
            ReferenceKeeper.Instance.GameManager.AddScore(_value);
            ReferenceKeeper.Instance.MessageNotificationFlag.ShowNotification($"+{reward_value}");

            ads_icon.enabled = false;
            check_icon.enabled = true;
            button.interactable = false;
        }
    }
}
