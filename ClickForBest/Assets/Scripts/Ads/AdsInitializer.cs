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
    private void Start()
    {
        ReferenceKeeper.Instance.GooglePlayServices.onInternetChanged += ChangedInternetState;
    }
    public void InitializeAds()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Advertisement.Initialize(google_play_id, test_mode, enable_Per_Placement_Mode, this);
        }
    }

    public void OnInitializationComplete()
    {
        isActive = true;

        ReferenceKeeper.Instance.RewardAdsController.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        isActive = false;
    }

    private void ChangedInternetState(bool _state)
    {
        if (!isActive)
        {
            InitializeAds();
        }
    }
}
