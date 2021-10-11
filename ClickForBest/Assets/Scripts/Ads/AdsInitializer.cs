using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string google_play_id;
    [SerializeField] bool test_mode = true;
    [SerializeField] bool enable_Per_Placement_Mode = true;

    public bool isActive { get; private set; }

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(google_play_id, test_mode, enable_Per_Placement_Mode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.LogFormat("Unity Ads initialization complete.");

        isActive = true;

        ReferenceKeeper.Instance.RewardAdsController.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");

        isActive = false;
    }
}
