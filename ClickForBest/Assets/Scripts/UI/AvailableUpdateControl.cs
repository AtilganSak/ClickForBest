using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableUpdateControl : MonoBehaviour
{
    public MessagePanel messagePanel;
    ApplicationInfo applicationInfo;
    private void Start()
    {
        applicationInfo = Resources.Load<ApplicationInfo>("ApplicationInfo");

        ReferenceKeeper.Instance.FirebaseService?.GetSystemDBAsync(CompletedGetSystemDB);
    }
    private void CompletedGetSystemDB(SystemDB _result)
    {
        if (_result.AvailableUpdate)
        {
            if (applicationInfo.BundleVersion != _result.BundleVersion)
            {
                messagePanel.Show();
            }
        }
    }
}
