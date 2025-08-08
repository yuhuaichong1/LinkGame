using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SPlayerPrefs
{
    public const string Separator1 = ",";//分隔符1
    public const string Separator2 = "/";//分隔符2

    #region int、float、string
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

    /// <summary>
    /// 存储Bool
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="boolean">存储值</param>
    public static void SetBool(string key, bool boolean)
    {
        PlayerPrefs.SetInt(key, boolean ? 1 : 0);
    }

    /// <summary>
    /// 读取Bool
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="defaultValue">当键不存在时的默认值</param>
    /// <returns>Bool值</returns>
    public static bool GetBool(string key, bool defaultValue = true)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key) == 1;
        }
        // 键不存在时返回默认值（默认为true）
        return defaultValue;
    }

    /// <summary>
    /// 存储Double
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="value">存储值</param>
    public static void SetDouble(string key, double value)
    {
        PlayerPrefs.SetString(key, value.ToString());
    }

    /// <summary>
    /// 读取Double
    /// </summary>
    /// <param name="key">Key</param>
    public static double GetDouble(string key, bool ifdefault = false)
    {
        string temp = PlayerPrefs.GetString(key);
        double value;
        if (temp == "" && ifdefault)
            value = 0;
        else
            value = double.Parse(temp);
        return value;
    }

    /// <summary>
    /// 存储List
    /// </summary>
    /// <typeparam name="T">List类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="list">存储值</param>
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

    /// <summary>
    /// 读取List
    /// </summary>
    /// <typeparam name="T">List类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">是否自动创建新值</param>
    /// <returns>List值</returns>
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

    /// <summary>
    /// 存储Stack
    /// </summary>
    /// <typeparam name="T">Stack类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="stack">存储值</param>
    public static void SetStack<T>(string key, Stack<T> stack)
    {
        SetList(key, new List<T>(stack));
    }

    /// <summary>
    /// 读取栈
    /// </summary>
    /// <typeparam name="T">Stack类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">是否自动创建新值</param>
    /// <returns>Stack值</returns>
    public static Stack<T> GetStack<T>(string key, bool ifdefault = false) 
    {
        try
        {
            string value = PlayerPrefs.GetString(key);
            string[] strings = value.Split(Separator1);
            strings.Reverse();
            Stack<T> stack = new Stack<T>();
            foreach (string str in strings)
            {
                stack.Push((T)Convert.ChangeType(str, typeof(T)));
            }

            return stack;
        }
        catch
        {
            Debug.LogWarning($"Stack '{key}' parsing failed");
            return ifdefault ? new Stack<T>() : null;
        }
    }

    /// <summary>
    /// 存储Queue
    /// </summary>
    /// <typeparam name="T">Queue类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="queue">存储值</param>
    public static void SetQueue<T>(string key, Queue<T> queue)
    {
        SetList(key, new List<T>(queue));
    }

    /// <summary>
    /// 读取队列
    /// </summary>
    /// <typeparam name="T">Queue类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">是否自动创建新值</param>
    /// <returns>Queue值</returns>
    public static Queue<T> GetQueue<T>(string key, bool ifdefault = false)
    {
        try
        {
            string value = PlayerPrefs.GetString(key);
            string[] strings = value.Split(Separator1);
            strings.Reverse();
            Queue<T> queue = new Queue<T>();
            foreach (string str in strings)
            {
                queue.Enqueue((T)Convert.ChangeType(str, typeof(T)));
            }

            return queue;
        }
        catch
        {
            Debug.LogWarning($"Stack '{key}' parsing failed");
            return ifdefault ? new Queue<T>() : null;
        }
    }

    /// <summary>
    /// 存储Dictionary
    /// </summary>
    /// <typeparam name="T">Dictionary的Keys的类型</typeparam>
    /// <typeparam name="U">Dictionary的Values的类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="dictionary">存储值</param>
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

    /// <summary>
    /// 读取Dictionary
    /// </summary>
    /// <typeparam name="T">Dictionary的Keys的类型</typeparam>
    /// <typeparam name="U">Dictionary的Values的类型</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">是否自动创建新值</param>
    /// <returns>Dictionary值</returns>
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
}
