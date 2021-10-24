using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBar : MonoBehaviour
{
    //180,360
    [Range(5, 10)] // 5 dakikada bir olabilir
    public int show_interval = 60;

    public Power[] powers;

    private Power selected_power;

    private bool trying_show;

    private void Start()
    {
        if (ReferenceKeeper.Instance.GooglePlayServices != null)
        {
            ReferenceKeeper.Instance.GooglePlayServices.onInternetChanged += ChangedInternetState;
        }

        powers.Shuffle();

        SelectPower();
    }
    private void SelectPower()
    {
        if (selected_power)
        {
            Destroy(selected_power.gameObject);
            selected_power = null;
        }
        Power power = Instantiate(powers[Random.Range(0, powers.Length)], transform);
        power.timeIsOver += SelectPower;
        selected_power = power;
        Invoke("ShowPower", Random.Range(60, 120));
    }
    private void ShowPower()
    {
        if (ReferenceKeeper.Instance.GooglePlayServices.internet)
        {
            if (ReferenceKeeper.Instance.RewardAdsController.IsReadyAds())
            {
                selected_power.Appear();
            }
        }
        else
        {
            if (!trying_show)
            {
                trying_show = true;
                StartCoroutine(TryShow());
            }
        }
    }
    private IEnumerator TryShow()
    {
        while (true)
        {
            if (ReferenceKeeper.Instance.GooglePlayServices.internet)
            {
                if (ReferenceKeeper.Instance.RewardAdsController.IsReadyAds())
                {
                    selected_power.Appear();
                    trying_show = false;
                    break;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    private void ChangedInternetState(bool _state)
    {
        if (!_state)
        {
            if (selected_power != null)
            {
                if (selected_power.isShow)
                {
                    if (!selected_power.used)
                    {
                        SelectPower();
                    }
                }
            }
        }
    }
}
