using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class ApplicationInfo : ScriptableObject
{
    [SerializeField]private string _version = "0.0.0";
    public string Version { get => _version; private set => _version = value; }

    [SerializeField] private int _bundleVersion = 0;
    public int BundleVersion { get => _bundleVersion; private set => _bundleVersion = value; }

#if UNITY_EDITOR
    [EasyButtons.Button]
    private void GetInformations()
    {
        _version = Application.version;
        _bundleVersion = PlayerSettings.Android.bundleVersionCode;

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
