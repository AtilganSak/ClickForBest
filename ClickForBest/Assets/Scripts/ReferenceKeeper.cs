using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceKeeper : MonoBehaviour
{
    static ReferenceKeeper _instance;

    public static ReferenceKeeper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ReferenceKeeper>();
            }
            return _instance;
        }
    }

    public GameManager GameManager;
    public Image ClickButton;
    public GameObject BottomCollider;
    public ScreenLimitDetector ScreenLimitDetector;
    public Handle Handle;
    public ClickController ClickController;
    public TimeCircle TimeCircle;
    public BoostTextControl BoostTextControl;
    public GooglePlayServices GooglePlayServices;
    public FirebaseService FirebaseService;
    public Store Store;
    public AdsInitializer AdsInitializer;
    public RewardAdsController RewardAdsController;
    public MessagePanel AdsMessagePanel;
    public CoinSpawner CoinSpawner;
    public RosetteController RosetteController;
    public RosetteSpawner RosetteSpawner;
    public UISound UISound;
    public CoinNotificationFlag CoinNotificationFlag;
    public MessageNotificationFlag MessageNotificationFlag;
    public SideBar SideBar;
    public GameAnalyticsService GameAnalyticsService;
    public Canvas Canvas3;

    private void OnEnable()
    {
        GooglePlayServices = FindObjectOfType<GooglePlayServices>();
        FirebaseService = FindObjectOfType<FirebaseService>();
    }
}
