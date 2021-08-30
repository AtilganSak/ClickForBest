using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBar : MonoBehaviour
{
    public Power[] powers;

    [EasyButtons.Button]
    public void ShowPower()
    {
        powers[0].Appear();
    }
    public void HidePower()
    {
        powers[0].Hide();
    }
}
