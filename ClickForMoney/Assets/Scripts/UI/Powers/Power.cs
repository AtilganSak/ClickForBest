using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public int time;
    public TMPro.TMP_Text time_text;

    private DOMove doMove;
    private Button button;

    internal bool used;

    private void OnEnable()
    {
        VirtualEnable();
    }
    internal virtual void VirtualEnable()
    {
        time_text.text = time.ToString();

        doMove = GetComponent<DOMove>();
        doMove.doRevertComplete.AddListener(() => { 
            Destroy(gameObject);
        });
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
    
    public void Appear()
    {
        doMove.DO();
    }
    public void Hide()
    {
        doMove.DORevert();
    }
}
