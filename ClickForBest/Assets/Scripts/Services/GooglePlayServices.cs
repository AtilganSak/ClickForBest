#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class GooglePlayServices : MonoBehaviour
{
    public Action<bool> onLogin;
    public Action<bool> onLoadedFrends;
    public Action<Sprite> onLoadedProfileImage;
    public Action onLogout;
    public Action<bool> onInternetChanged;
    public float try_connection_time=10f;
    public string authCode { get; set; }

    bool isLoadedFrends;
    bool profileImageIsLoaded;
    bool closingAccount;

    public bool TryingLogout { get; private set; }
    public bool TryingLogin { get; private set; }
    public bool LoginState { get; private set; }
    public bool internet { get => Application.internetReachability != NetworkReachability.NotReachable; }

    private ILocalUser user;
    public ILocalUser User { get
        {
            if (user == null)
            {
                if (Social.localUser.authenticated)
                {
                    return Social.localUser;
                }
            }
            return user;
        } }
    public IUserProfile[] frends { get; private set; }
    public IAchievement[] achievements { get; private set; }

    private void OnEnable()
    {
#if UNITY_ANDROID
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .RequestServerAuthCode(false)
        .RequestIdToken()
        .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
#elif UNITY_IOS
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
#endif
        internet_state = internet;
        StartCoroutine(CheckInternet());
    }
    private void Update()
    {
        if (closingAccount)
        {
            if (!Social.localUser.authenticated)
            {
                LogoutCallback();
            }
        }
    }
    IEnumerator TryConnection()
    {
        while (true)
        {
            if (!LoginState)
            {
                if (internet)
                {
                    Login();
                }
            }
            Debug.LogError("Login State :"+LoginState);
            yield return new WaitForSeconds(try_connection_time);
        }
    }
    public void TryLogin()
    {
        StartCoroutine(TryConnection());
    }
    public void Login()
    {
        if (!internet) return;
        if (TryingLogin) return;

        if (!Social.localUser.authenticated)
        {
            TryingLogin = true;
            Social.localUser.Authenticate((bool success) =>
            {
                LoginCallback(success);
            });
        }
    }
    private void LoginCallback(bool state)
    {
        if (state)
        {
            LoginState = state;
            TryingLogin = false;
#if UNITY_ANDROID
            authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
            Debug.LogError(authCode);
#endif
            //GetAchievements();
        }
        if (onLogin != null)
        {
            onLogin.Invoke(state);
        }
    }
    public void Logout()
    {
        if (!internet) return;
        if (TryingLogout) return;

        if (Social.localUser.authenticated)
        {
            TryingLogout = true;
#if UNITY_ANDROID
            ((PlayGamesPlatform)Social.Active).SignOut();
#endif

            closingAccount = true;
        }
    }
    private void LogoutCallback()
    {
        closingAccount = false;
        LoginState = false;
        TryingLogout = false;
        profileImageIsLoaded = false;
    }
    public void ShowAchievements()
    {
        if (!internet || !LoginState) return;

        Social.ShowAchievementsUI();
    }
    public void ShowLeaderboard()
    {
        if (!internet || !LoginState) return;

        Social.ShowLeaderboardUI();
    }
    public void UnlockAchievement(string id)
    {
        if (!internet || !LoginState) return;

        Social.Active.ReportProgress(id, 100, state => { });
    }
    public void UnlockIncrementAchievement(string id, int steps)
    {
        if (!internet || !LoginState) return;

#if UNITY_ANDROID
        ((PlayGamesPlatform)Social.Active).IncrementAchievement(id, steps, state => { });
#endif
    }
//    public void EarnScore(int score)
//    {
//        if (!internet || !LoginState) return;

//#if UNITY_ANDROID
//        Social.Active.ReportScore(score, GPGSIds., state => { });
//#endif
//    }
    public void EarnScore(string id, int score)
    {
        if (!internet || !LoginState) return;

#if UNITY_ANDROID
        Social.Active.ReportScore(score, id, state => { });
#endif
    }
    public void GetFrends()
    {
        if (!internet || !LoginState) return;

#if UNITY_ANDROID
        PlayGamesPlatform.Instance.LoadFriends(Social.localUser, GetFrendsCallback);
#endif
    }
    private void GetFrendsCallback(bool state)
    {
        isLoadedFrends = state;
        if (state)
        {
            frends = new IUserProfile[Social.localUser.friends.Length];
            int i = 0;
            foreach (IUserProfile profile in Social.localUser.friends)
            {
                frends[i] = profile;
                i++;
            }
        }
        if (onLoadedFrends != null)
        {
            onLoadedFrends.Invoke(state);
        }
    }
    bool internet_state;
    private IEnumerator CheckInternet()
    {
        while (true)
        {
            if (internet)
            {
                if (!internet_state)
                {
                    if (onInternetChanged != null)
                        onInternetChanged.Invoke(true);
                    internet_state = true;
                }
            }
            else
            {
                if (internet_state)
                {
                    if (onInternetChanged != null)
                        onInternetChanged.Invoke(false);
                    internet_state = false;
                }
            }
            yield return new WaitForSeconds(3);
        }
    }
}
