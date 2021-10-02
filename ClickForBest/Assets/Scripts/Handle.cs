using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Handle : MonoBehaviour
{
    public UnityEvent onActive;
    public UnityEvent onDeactive;

    public Transform pivot;

    private DOScale doscale;

    private void OnEnable()
    {
        doscale = GetComponent<DOScale>();
    }
    public void ResetUp()
    {
        if (isOn)
        {
            ONOFF();
        }
    }

    #region Handle Simple
    public TMPro.TMP_Text text;
    public Image image;
    private bool isOn;
    public void ONOFF()
    {
        if (!isOn)
        {
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.SwitchOpen);
            doscale.ResetDO();
            isOn = true;
            text.text = "ON";
            image.color = UtilitiesMethods.HexToColor("76F341");
            onActive.Invoke();
        }
        else
        {
            ReferenceKeeper.Instance.UISound.PlaySound(UISound.Sound.SwitchClose);
            isOn = false;
            text.text = "OFF";
            image.color = UtilitiesMethods.HexToColor("F34F41");
            onDeactive.Invoke();
        }
    }
    #endregion
}
