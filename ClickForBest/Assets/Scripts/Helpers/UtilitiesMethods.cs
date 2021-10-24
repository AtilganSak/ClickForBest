using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public enum TimeFormat
{
    HhMmSs,
    MmSs
}
public static class UtilitiesMethods
{
    public static DateTime RandomDate(int startYear = 1995)
    {
        DateTime start = new DateTime(startYear, 1, 1);
        System.Random gen = new System.Random();
        int range = (DateTime.Now - start).Days;
        return start.AddDays(gen.Next(range));
    }
    public static int GetNumberOfDecimalPlaces(decimal value, int maxNumber)
    {
        if (maxNumber == 0)
            return 0;

        if (maxNumber > 28)
            maxNumber = 28;

        bool isEqual = false;
        int placeCount = maxNumber;
        while (placeCount > 0)
        {
            decimal vl = Math.Round(value, placeCount - 1);
            decimal vh = Math.Round(value, placeCount);
            isEqual = (vl == vh);

            if (isEqual == false)
                break;

            placeCount--;
        }
        return Math.Min(placeCount, maxNumber);
    }
    public static byte[] BinarySerialize(object graph)
    {
        using(var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(stream, graph);
            }
            catch(Exception e)
            {
                Console.WriteLine("Serialization failed: {0}", e.Message);
                throw;
            }

            return stream.ToArray();
        }
    }
    public static object BinaryDeserialize(byte[] buffer)
    {
        using(var stream = new MemoryStream(buffer))
        {
            var formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);
        }
    }
    public static void FixNameForClone(UnityEngine.GameObject obj)
    {
        string newName = obj.name.Remove(obj.name.Length - 7);
        obj.name = newName;
    }
    public static string ConvertSecondsToFormattedTimeString(float seconds, TimeFormat resultFormat = TimeFormat.MmSs)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string format = "";
        switch(resultFormat)
        {
            case TimeFormat.HhMmSs:
                format = @"hh\:mm\:ss";
                break;
            case TimeFormat.MmSs:
                format = @"mm\:ss";
                break;
            default:
                break;
        }
        return time.ToString(format);
    }
    public static void SetActivityObjects(bool state, params GameObject[] objs)
    {
        for(int i = 0; i < objs.Length; i++)
        {
            if(objs[i] != null)
                objs[i].gameObject.SetActive(state);
        }
    }
    public static string Triming(string text)
    {
        string newText = string.Join(" ", text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
        return newText;
    }
    public static string Triming(string text, string sperator, string targetChar)
    {
        string newText = string.Join(sperator.ToString(), text.Split(new string[] { targetChar.ToString() }, StringSplitOptions.RemoveEmptyEntries));
        return newText;
    }
    public static bool AnimatorIsPlaying(Animator _animator, string _stateName)
    {
        return (_animator.GetCurrentAnimatorStateInfo(0).length >
               _animator.GetCurrentAnimatorStateInfo(0).normalizedTime) && _animator.GetCurrentAnimatorStateInfo(0).IsName(_stateName);
    }
    public static Color HexToColor(string hex, byte a = 255)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, a);
    }
    public static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }
    public static float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
        return diagonalInches;
    }
    public static int ScreenAspectRation()
    {
        return Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
    }
    public static string ShortenString(string str, int limit)
    {
        if (str != null && str != "" && str.Length > 1)
        {
            if (limit > 0)
            {
                if (limit < str.Length)
                {
                    return str.Substring(0, limit) + "...";
                }
                else
                {
                    return str;
                }
            }
        }
        return "";
    }
}