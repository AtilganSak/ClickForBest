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
    public Button ClickButton;
    public GameObject BottomCollider;
    public ScreenLimitDetector ScreenLimitDetector;
    public Handle Handle;
    public ClickController ClickController;
    public TimeCircle TimeCircle;
    public BoostTextControl BoostTextControl;
}
