using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int total_coin_count;

    private void Start()
    {
        Application.targetFrameRate = 90;
    }
    public void AddCoin()
    {
        total_coin_count++;
    }
    public void RemoveCoin()
    {
        if (total_coin_count - 1 >= 0)
        {
            total_coin_count--;
        }
        if(total_coin_count <= 0)
        {
            ReferenceKeeper.Instance.ClickButton.SetActive(true);
            ReferenceKeeper.Instance.BottomCollider.SetActive(true);
            ReferenceKeeper.Instance.ScreenLimitDetector.unloading = false;

            GC.Collect();
        }
    }
}
