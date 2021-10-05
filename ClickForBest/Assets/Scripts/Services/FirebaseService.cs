using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Analytics;
using Firebase.Extensions;
using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;

public class FirebaseService : MonoBehaviour
{
    public FirebaseUser firebase_user { get; private set; }
    private FirebaseDatabase firebase_database;
    private DatabaseReference database_reference;
    private GooglePlayServices google_play_service;
    private FirebaseAuth auth;

    public Action<bool> onLogin;

    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    private void OnEnable()
    {
        google_play_service = FindObjectOfType<GooglePlayServices>();
        google_play_service.onLogin += OnLoginGoogle;

        auth = FirebaseAuth.DefaultInstance;
        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        //{
        //    dependencyStatus = task.Result;
        //    if (dependencyStatus == DependencyStatus.Available)
        //    {
        //        InitializeFirebase();
        //    }
        //    else
        //    {
        //        Debug.LogError(
        //          "Could not resolve all Firebase dependencies: " + dependencyStatus);
        //    }
        //});
        InitializeFirebase();
    }
    void InitializeFirebase()
    {
        firebase_database = FirebaseDatabase.GetInstance("https://click-for-best-32600433-default-rtdb.firebaseio.com/");
        //firebase_database.SetPersistenceEnabled(true);
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
    }
    private void OnLoginGoogle(bool _state)
    {
        if (_state)
        {
            LoginFirabese();
        }
    }
    public void FirebaseLoginTest()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            firebase_user = task.Result;
            Debug.LogError("User signed in successfully: " + firebase_user.DisplayName +" "+ firebase_user.UserId);
        });
    }
    private void LoginFirabese()
    {
        Credential credential = null;
        if (string.IsNullOrEmpty(google_play_service.authCode))
            credential = PlayGamesAuthProvider.GetCredential(PlayGamesPlatform.Instance.GetServerAuthCode());
        else
            credential = PlayGamesAuthProvider.GetCredential(google_play_service.authCode);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            bool state = true;
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                state = false;

            }
            else if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                state = false;
            }
            if (state)
            {
                firebase_user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    firebase_user.DisplayName, firebase_user.UserId);
            }
            if (onLogin != null)
            {
                onLogin.Invoke(state);
            }
        });
    }
    public void FirebaseOffline()
    {
        firebase_database.GoOffline();
    }
    public void FirebaseOnline()
    {
        firebase_database.GoOnline();
    }
    public void SetGameDBAsync(GameDB _value, Action<bool> firebaseSetScoreCallBack = null)
    {
        if (firebase_user == null || !google_play_service.internet) return;

        string json_value = JsonUtility.ToJson(_value);
        firebase_database.GetReference(PlayerKeys.FIREBASE_PLAYER).Child(firebase_user.UserId).Child(firebase_user.DisplayName).SetRawJsonValueAsync(json_value).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                firebaseSetScoreCallBack.Invoke(false);
            }
            else if (task.IsCompleted)
            {
                firebaseSetScoreCallBack.Invoke(true);
            }
        });
    }
    public void GetGameDBAsync(Action<GameDB> firebaseGetGameDBCallBack)
    {
        if (firebase_user == null) return;

        firebase_database.GetReference(PlayerKeys.FIREBASE_PLAYER).Child(firebase_user.UserId).Child(firebase_user.DisplayName).GetValueAsync().ContinueWith(task =>
        {
            string data = task.Result.GetRawJsonValue();
            if (data != null)
            {
                firebaseGetGameDBCallBack.Invoke(JsonUtility.FromJson<GameDB>(data));
            }
            else
            {
                firebaseGetGameDBCallBack.Invoke(null);
            }
        });
    }
    public void SetScoreAsync(ScoreBoardPlayer _value, Action<bool> firebaseSetScoreCallBack = null)
    {
        //if (firebase_user == null || !google_play_service.internet) return;

        _value.name = "ATLGAN";
        string json_value = JsonUtility.ToJson(_value);
        firebase_database.GetReference(PlayerKeys.FIREBASE_SCOREBOARD).Child("nZCfw0AZQxW7H584SLg4RFCt7872").SetRawJsonValueAsync(json_value).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Task failud: " + task.Exception);
                firebaseSetScoreCallBack.Invoke(false);
            }
            else if (task.IsCompleted)
            {
                Debug.LogError("Task completed");
                firebaseSetScoreCallBack.Invoke(true);
            }
        });
    }
    public void GetScoresAsync(Action<ScoreBoardPlayer[]> _callback)
    {
        //if (firebase_user == null) return;

        firebase_database.GetReference(PlayerKeys.FIREBASE_SCOREBOARD).OrderByChild("score").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot data = task.Result;
            List<ScoreBoardPlayer> result = new List<ScoreBoardPlayer>();
            foreach (DataSnapshot item in data.Children)
            {
                ScoreBoardPlayer player = JsonUtility.FromJson<ScoreBoardPlayer>(item.GetRawJsonValue());
                string name = player.name;
                int score = player.score;
                ScoreBoardPlayer newPlayer = new ScoreBoardPlayer
                {
                    name = name,
                    score = score
                };
                result.Add(newPlayer);
            }
            _callback.Invoke(result.ToArray());
        });
    }
    public void SetStoreItemsAsync(int[] _values, Action<bool> firebaseSetStoreItemsCallBack = null)
    {
        if (firebase_user == null || !google_play_service.internet) return;

        firebase_database.GetReference(PlayerKeys.FIREBASE_PLAYER).Child(firebase_user.UserId).Child(firebase_user.DisplayName).Child(PlayerKeys.FIREBASE_STORE_ITEMS).SetValueAsync(_values).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                firebaseSetStoreItemsCallBack.Invoke(false);
            }
            else if (task.IsCompleted)
            {
                firebaseSetStoreItemsCallBack.Invoke(true);
            }
        });
    }
    public void SetSelectedStoreItemAsync(int _value, Action<bool> firebaseSetSelectedStoreItemCallBack = null)
    {
        if (firebase_user == null || !google_play_service.internet) return;

        firebase_database.GetReference(PlayerKeys.FIREBASE_PLAYER).Child(firebase_user.UserId).Child(firebase_user.DisplayName).Child(PlayerKeys.FIREBASE_SELECTED_STORE_ITEMS).SetValueAsync(_value).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                firebaseSetSelectedStoreItemCallBack.Invoke(false);
            }
            else if (task.IsCompleted)
            {
                firebaseSetSelectedStoreItemCallBack.Invoke(true);
            }
        });
    }
}
public class Player
{
    public string username;

    public Player() { }
    public Player(string _name)
    {
        username = _name;
    }
}

