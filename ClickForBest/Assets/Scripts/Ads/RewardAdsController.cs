using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAdsController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public Action onAdsShowComplete;

    [SerializeField] string ad_id;

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        if (ReferenceKeeper.Instance.AdsInitializer.isActive)
        {
            Debug.Log("Loading Unity Ads");

            Advertisement.Load(ad_id, this);
        }
    }

    public bool IsReadyAds()
    {
        if (ReferenceKeeper.Instance.AdsInitializer.isActive)
        {
            return Advertisement.IsReady(ad_id);
        }
        else
        {
            return false;
        }
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(ad_id))
        {

        }
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        if (ReferenceKeeper.Instance.AdsInitializer.isActive)
        {
            Debug.Log("Unity Ads Showing");
            Advertisement.Show(ad_id, this);
        }
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(ad_id) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            if (onAdsShowComplete != null)
            {
                onAdsShowComplete.Invoke();
            }
            onAdsShowComplete = null;

            // Load another ad:
            LoadAd();
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
