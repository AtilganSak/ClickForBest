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

    private void Start()
    {
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
#if UNITY_EDITOR
        selected_power.Appear();
#else
        if (ReferenceKeeper.Instance.GooglePlayServices.internet)
        {
            if (ReferenceKeeper.Instance.RewardAdsController.IsReadyAds())
            {
                selected_power.Appear();
            }
        }
        else
        {
            StartCoroutine(TryShow());
        }
#endif
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
                    break;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
