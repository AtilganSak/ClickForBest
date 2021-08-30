using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBar : MonoBehaviour
{
    //180,360
    [Range(5, 10)]
    public int show_interval = 60;

    public Power[] powers;

    private Power selected_power;

    private void Start()
    {
        SelectPower();
    }
    private void SelectPower()
    {
        Power power = Instantiate(powers[Random.Range(0, powers.Length)], transform);
        power.GetComponent<DOMove>().doRevertComplete.AddListener(SelectPower);
        selected_power = power;
        Invoke("ShowPower", Random.Range(3, 6));
    }
    private void ShowPower()
    {
        selected_power.Appear();
    }
}
