using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class KeyStoreLoad
{
    static string key = "ClickForBest2021";

    static KeyStoreLoad()
    {
        if(PlayerSettings.keystorePass == "" && PlayerSettings.keyaliasPass == "")
        {
            PlayerSettings.keystorePass = key;
            PlayerSettings.keyaliasPass = key;
            Debug.Log("Loaded KeyStore Password!");
        }
    }
}