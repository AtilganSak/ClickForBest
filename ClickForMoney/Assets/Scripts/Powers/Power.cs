using System;
using System.Collections;
using System.Collections.Generic;
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
        time_text.text = time.ToString();

        doMove = GetComponent<DOMove>();
        button = GetComponentInChildren<Button>();
        if (button)
        {
            button.onClick.AddListener(Use);
        }
    }
    internal virtual void Use() 
    {
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
