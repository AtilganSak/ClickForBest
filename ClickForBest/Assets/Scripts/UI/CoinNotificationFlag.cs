using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinNotificationFlag : MonoBehaviour
{
    [SerializeField] TMP_Text message_text;
    [SerializeField] Image image;
    [SerializeField] ParticleSystem particle_effect;
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
    public void Adjust(Sprite _sprite, string _message = "New Coin")
    {
        message_text.text = _message;
        image.sprite = _sprite;
    }
    [EasyButtons.Button]
    public void ShowNotification()
    {
        if (!opened)
        {
            opened = true;
            particle_effect.Play();
            doAnchorPos.DO();
        }
    }
    private void HideNotification()
    {
        if (opened)
        {
            opened = false;
            particle_effect.Stop();
            doAnchorPos.DORevert();
        }
    }
    private void OpenedPanel()
    {
        Invoke("HideNotification", notification_time);
    }
}
