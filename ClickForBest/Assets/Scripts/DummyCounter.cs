using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCounter : MonoBehaviour
{
    public bool allowMinuse;
    public bool anim;
    private int point;
    private TMPro.TMP_Text text;
    private DOScale doscale;

    private void OnEnable()
    {
        text = GetComponent<TMPro.TMP_Text>();
        doscale = gameObject.GetComponent<DOScale>();
    }
    public void Add(int _amount)
    {
        point += _amount;
        if (text != null)
            text.text = point.ToString();
        if (anim)
            doscale.DO();
    }
    public void Remove(int _amount)
    {
        point -= _amount;
        if (point < 0)
        {
            if (!allowMinuse) point = 0;
        }
        if (text != null)
            text.text = point.ToString();
        if (anim)
            doscale.DO();
    }
    public void SetPoint(int _value)
    {
        point = _value;
    }
}
