using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    private DOMove doMove;
    private Button button;

    private void OnEnable()
    {
        VirtualEnable();
    }
    internal virtual void VirtualEnable() 
    {
        doMove = GetComponent<DOMove>();
        button = GetComponentInChildren<Button>();
        if (button)
        {
            button.onClick.AddListener(Use);
        }
    }
    internal virtual void Use() { }
    
    public void Appear()
    {
        doMove.DO();
    }
    public void Hide()
    {
        doMove.DORevert();
    }
}
