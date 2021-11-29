using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsService : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
    }
    public void RewardAdsEvent(GAAdAction _action, string _unitId)
    {
        GameAnalytics.NewAdEvent(_action, GAAdType.RewardedVideo, "UnityAds", _unitId);
    }
}
