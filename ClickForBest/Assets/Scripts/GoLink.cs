using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLink : MonoBehaviour
{
    public string androidMarketID;

    public void GOPlayGamesPage()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=" + androidMarketID);
#endif
    }
}
