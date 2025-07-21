using System;
using System.Collections.Generic;
using UnityEngine;

public static class SPlayerPrefs
{
    public const string Separator1 = ",";//·Ö¸ô·û1
    public const string Separator2 = "/";//·Ö¸ô·û2

    #region int¡¢float¡¢string
    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public static int GetInt(string key) 
    { 
        return PlayerPrefs.GetInt(key);
    }

    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public static float GetFloat(string key) 
    {
        return PlayerPrefs.GetFloat(key);
    }

    public static void SetString(string key, string value) 
    { 
        PlayerPrefs.SetString(key, value);
    }
    public static string GetString(string key) 
    { 
        return PlayerPrefs.GetString(key);
    }
    #endregion

    //´æ´¢²¼¶û
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value? 1 : 0);
    }

    //¶ÁÈ¡²¼¶û
    public static bool GetBool(string key) 
    {
        return PlayerPrefs.GetInt(key) == 1;
    }

    //´æ´¢±í¸ñ
    public static void SetList<T>(string key, List<T> list)
    {
        List<string> strings = new List<string>();
        foreach (T item in list) 
        { 
            string str = item.ToString();
            strings.Add(str);
        }
        string value = string.Join(Separator1, strings.ToArray());

        PlayerPrefs.SetString(key, value);
    }

    //¶ÁÈ¡±í¸ñ
    public static List<T> GetList<T>(string key, bool ifdefault = false)
    {
        try
        {
            string value = PlayerPrefs.GetString(key);
            string[] strings = value.Split(Separator1);
            List<T> list = new List<T>();
            foreach (string str in strings)
            {
                list.Add((T)Convert.ChangeType(str, typeof(T)));
            }

            return list;
        }
        catch
        {
            Debug.LogWarning($"List '{key}' parsing failed");
            return ifdefault ? new List<T>() : null;
        }
    }

    //´æ´¢×Öµä
    public static void SetDictionary<T, U>(string key, Dictionary<T, U> dictionary)
    {
        List<string> dicKeys = new List<string>();
        List<string> dicValues = new List<string>();
        foreach (T keyItem in dictionary.Keys)
        {
            string str = keyItem.ToString();
            dicKeys.Add(str);
        }
        foreach (U valueItem in dictionary.Values)
        {
            string str = valueItem.ToString();
            dicValues.Add(str);
        }
        string DKStr = string.Join(Separator1, dicKeys.ToArray());
        string DVStr = string.Join(Separator1, dicValues.ToArray());
        string value = $"{DKStr}{Separator2}{DVStr}";

        PlayerPrefs.SetString(key, value);
    }

    //¶ÁÈ¡×Öµä
    public static Dictionary<T,U> GetDictionary<T, U>(string key, bool ifdefault = false)
    {
        try
        {
            Dictionary<T, U> dic = new Dictionary<T, U>();
            string[] KeyValue = PlayerPrefs.GetString(key).Split(Separator2);
            string[] dicKeys = KeyValue[0].Split(Separator1);
            string[] dicValues = KeyValue[1].Split(Separator1);
            for (int i = 0; i < dicKeys.Length; i++)
            {
                T keyItem = (T)Convert.ChangeType(dicKeys[i], typeof(T));
                U valueItem = (U)Convert.ChangeType(dicValues[i], typeof(U));
                dic.Add(keyItem, valueItem);
            }

            return dic;
        }
        catch 
        {
            Debug.LogWarning($"Dictionary '{key}' parsing failed");
            return ifdefault? new Dictionary<T, U>() : null; 
        }
    }

    internal static int GetInt(object curTotalLinkCount)
    {
        throw new NotImplementedException();
    }
}
