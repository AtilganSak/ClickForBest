using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageNotificationFlag : MonoBehaviour
{
    [SerializeField] TMP_Text message_text;
    [SerializeField] float notification_time = 3;

    private DOAnchorPos doAnchorPos;

    private bool opened;

    private void Start()
    {
        doAnchorPos = GetComponent<DOAnchorPos>();
        if (doAnchorPos != null)
        {
            doAnchorPos.doComplete.AddListener(OpenedPanel);
        }
    }
    [EasyButtons.Button]
    public void ShowNotification(string _message)
    {
        if (!opened)
        {
            opened = true;
            message_text.text = _message;
            doAnchorPos.DO();
        }
    }
    private void HideNotification()
    {
        if (opened)
        {
            opened = false;
            doAnchorPos.DORevert();
        }
    }
    private void OpenedPanel()
    {
        Invoke("HideNotification", notification_time);
    }
}
