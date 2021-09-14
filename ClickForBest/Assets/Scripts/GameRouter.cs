using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRouter : MonoBehaviour
{
    FirebaseService f_service;
    private void Start()
    {
        GooglePlayServices service = FindObjectOfType<GooglePlayServices>();
        f_service = FindObjectOfType<FirebaseService>();
        if (service)
        {
            service.onLogin += LoggedFirebase;
            f_service.onLogin += LoggedFirebase;

            if (service.internet)
            {
                service.Login();
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    private void LoggedGoogle(bool _state)
    {
        if (!_state)
        {
            SceneManager.LoadScene(1);
        }
    }
    private void LoggedFirebase(bool _state)
    {
        if (_state)
        {
            f_service.GettGameDBAsync((result) => LoadGameDB(result));
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    private void LoadGameDB(GameDB _db)
    {
        if (_db != null)
        {
            EasyJson.SaveJsonToFile(_db);
        }
    }
}
