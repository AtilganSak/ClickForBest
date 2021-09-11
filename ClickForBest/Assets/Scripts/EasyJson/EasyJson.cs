using System.IO;
using UnityEngine;

public static class EasyJson
{
    private static string _path;
    private static string json_path
    {
        get
        {
            if (_path == null || _path == "")
                _path = Path.Combine(Application.persistentDataPath, "GameData.dat");
            return _path;
        }
        set
        {
            _path = value;
        }
    }
    public static void SaveJsonToFile(object _data, bool _is_crypto = false)
    {
        string json_text = JsonUtility.ToJson(_data);
        if (_is_crypto)
            json_text = CryptoHelper.Encrypt(json_text, "-Zinky-Games2021*-");
        File.WriteAllText(json_path, json_text);
    }
    public static void SaveJsonToFile(object _data,string _file_name, bool _is_crypto = false)
    {
        string _path = Path.Combine(Application.persistentDataPath, _file_name);
        string json_text = JsonUtility.ToJson(_data);
        if (_is_crypto)
            json_text = CryptoHelper.Encrypt(json_text, "-Zinky-Games2021*-");

        File.WriteAllText(_path, json_text);
    }
    public static T GetJsonToFile<T>(bool _is_crypto = false)
    {
        if (File.Exists(json_path))
        {
            string get_json_text = File.ReadAllText(json_path);
            if (_is_crypto)
                 get_json_text = CryptoHelper.Decrypt(get_json_text, "-Zinky-Games2021*-");

            return (T)JsonUtility.FromJson(get_json_text, typeof(T));
        }
        return default;
    }
    public static T GetJsonToFile<T>(string _file_name, bool _is_crypto = false)
    {
        string _path = Path.Combine(Application.persistentDataPath, _file_name);
        if (File.Exists(_path))
        {
            string get_json_text = File.ReadAllText(_path);
            if (_is_crypto)
                get_json_text = CryptoHelper.Decrypt(get_json_text, "-Zinky-Games2021*-");

            return (T)JsonUtility.FromJson(get_json_text, typeof(T));
        }
        return default;
    }
    public static void DeleteDataBase()
    {
        if (File.Exists(json_path))
        {
            File.Delete(json_path);
        }
        string _path = Path.Combine(Application.persistentDataPath, "PlayerData.dat");
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }
}
