using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    private Action<bool> messageCallback;

    [SerializeField] TMP_Text title_text;
    [SerializeField] TMP_Text message_text;
    [SerializeField] TMP_Text yes_text;
    [SerializeField] TMP_Text no_text;
    [SerializeField] Button yes_button;
    [SerializeField] Button no_button;
    [SerializeField] Button exit_button;
    [SerializeField] Toggle dont_show_toggle;
    [SerializeField] Image dimed;

    private DOScale doScale;
    private bool dont_show_again;
    private string player_key;

    private void OnEnable()
    {
        player_key = "mp_" + name;

        doScale = GetComponent<DOScale>();

        dimed.enabled = false;
    }
    private void Start()
    {
        if(yes_button && yes_button.gameObject.activeInHierarchy)
            yes_button.onClick.AddListener(Pressed_Yes);
        if (no_button && no_button.gameObject.activeInHierarchy)
            no_button.onClick.AddListener(Pressed_No);
        if (exit_button && exit_button.gameObject.activeInHierarchy)
            exit_button.onClick.AddListener(Pressed_No);
        if (dont_show_toggle && dont_show_toggle.gameObject.activeInHierarchy)
        {
            dont_show_toggle.onValueChanged.AddListener(ChangedDontShow);
            if (PlayerPrefs.HasKey(player_key))
            {
                dont_show_again = PlayerPrefs.GetInt(player_key) == 1 ? true : false;
            }
        }
    }
    public void Subscribe(string _title, string _message, string _yesText, string _noText, Action<bool> _callback)
    {
        if (title_text)
            title_text.text = _title;
        if (message_text)
            message_text.text = _message;
        if (yes_text)
            yes_text.text = _yesText;
        if (no_text)
            no_text.text = _noText;

        if (_callback != null)
            messageCallback += _callback;
    }
    public void Change(string _title = "", string _message = "", string _yesText = "", string _noText = "")
    {
        if (title_text && _title != "")
            title_text.text = _title;
        if (message_text && _message != "")
            message_text.text = _message;
        if (yes_text && _yesText != "")
            yes_text.text = _yesText;
        if (no_text && _noText != "")
            no_text.text = _noText;
    }
    public void Show()
    {
        if (!dont_show_again)
        {
            dimed.enabled = true;
            doScale.DO();
        }
        else
        {
            if (messageCallback != null)
                messageCallback.Invoke(true);
        }
    }
    public void Hide()
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.PopUp);
        dimed.enabled = false;
        doScale.DORevert();
    }
    private void Pressed_Yes()
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
        if (messageCallback != null)
        {
            messageCallback.Invoke(true);
        }
        Hide();
    }
    private void Pressed_No()
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
        if (messageCallback != null)
        {
            messageCallback.Invoke(false);
        }
        Hide();
    }
    private void ChangedDontShow(bool _value)
    {
        ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.Button);
        dont_show_again = _value;
        PlayerPrefs.SetInt(player_key, _value ? 1 : 0);
    }
}
