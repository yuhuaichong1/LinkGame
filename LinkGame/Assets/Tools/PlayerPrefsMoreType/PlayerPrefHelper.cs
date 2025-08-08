using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SPlayerPrefs
{
    public const string Separator1 = ",";//�ָ���1
    public const string Separator2 = "/";//�ָ���2

    #region int��float��string
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
    /// �洢Bool
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="boolean">�洢ֵ</param>
    public static void SetBool(string key, bool boolean)
    {
        PlayerPrefs.SetInt(key, boolean ? 1 : 0);
    }

    /// <summary>
    /// ��ȡBool
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="defaultValue">����������ʱ��Ĭ��ֵ</param>
    /// <returns>Boolֵ</returns>
    public static bool GetBool(string key, bool defaultValue = true)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key) == 1;
        }
        // ��������ʱ����Ĭ��ֵ��Ĭ��Ϊtrue��
        return defaultValue;
    }

    /// <summary>
    /// �洢Double
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="value">�洢ֵ</param>
    public static void SetDouble(string key, double value)
    {
        PlayerPrefs.SetString(key, value.ToString());
    }

    /// <summary>
    /// ��ȡDouble
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
    /// �洢List
    /// </summary>
    /// <typeparam name="T">List����</typeparam>
    /// <param name="key">Key</param>
    /// <param name="list">�洢ֵ</param>
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
    /// ��ȡList
    /// </summary>
    /// <typeparam name="T">List����</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">�Ƿ��Զ�������ֵ</param>
    /// <returns>Listֵ</returns>
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
    /// �洢Stack
    /// </summary>
    /// <typeparam name="T">Stack����</typeparam>
    /// <param name="key">Key</param>
    /// <param name="stack">�洢ֵ</param>
    public static void SetStack<T>(string key, Stack<T> stack)
    {
        SetList(key, new List<T>(stack));
    }

    /// <summary>
    /// ��ȡջ
    /// </summary>
    /// <typeparam name="T">Stack����</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">�Ƿ��Զ�������ֵ</param>
    /// <returns>Stackֵ</returns>
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
    /// �洢Queue
    /// </summary>
    /// <typeparam name="T">Queue����</typeparam>
    /// <param name="key">Key</param>
    /// <param name="queue">�洢ֵ</param>
    public static void SetQueue<T>(string key, Queue<T> queue)
    {
        SetList(key, new List<T>(queue));
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <typeparam name="T">Queue����</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">�Ƿ��Զ�������ֵ</param>
    /// <returns>Queueֵ</returns>
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
    /// �洢Dictionary
    /// </summary>
    /// <typeparam name="T">Dictionary��Keys������</typeparam>
    /// <typeparam name="U">Dictionary��Values������</typeparam>
    /// <param name="key">Key</param>
    /// <param name="dictionary">�洢ֵ</param>
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
    /// ��ȡDictionary
    /// </summary>
    /// <typeparam name="T">Dictionary��Keys������</typeparam>
    /// <typeparam name="U">Dictionary��Values������</typeparam>
    /// <param name="key">Key</param>
    /// <param name="ifdefault">�Ƿ��Զ�������ֵ</param>
    /// <returns>Dictionaryֵ</returns>
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
