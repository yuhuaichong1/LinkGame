using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//根据权重获取对应值
public static class GetProbability
{
    /// <summary>
    /// 根据权重得到对应的值
    /// </summary>
    /// <typeparam name="T">对应值的类型 </typeparam>
    /// <param name="pros">字典数据（对应值<==>权重）</param>
    /// <returns>对应值</returns>
    public static T GatValue<T>(Dictionary<T, int> pros)
    {
        int total = 0;
        foreach (KeyValuePair<T, int> pair in pros)
        {
            total += pair.Value;
        }

        int random = UnityEngine.Random.Range(0, total);
        
        int current = 0;
        foreach (KeyValuePair<T, int> pair in pros)
        {
            current += pair.Value;
            if (random < current)
            {
                return pair.Key;
            }
        }

        return default(T);
    }

    public static List<T> GetValues<T>(Dictionary<List<T>, int> pros, int num)
    {
        List<T> target = new List<T>();

        Dictionary<int, GetValuesItem<T>> id_List = new Dictionary<int, GetValuesItem<T>>();
        int temp = 0;
        foreach (KeyValuePair<List<T>, int> pairs in pros)
        {
            id_List.Add(temp, new GetValuesItem<T> { count = 0, values = pairs.Key });
            temp += 1;
        }

        List<int> weights = pros.Values.ToList();

        int total = 0;
        foreach (KeyValuePair<List<T>, int> pair in pros)
        {
            total += pair.Value;
        }

        int random = UnityEngine.Random.Range(0, total);
        int current = 0;

        for (int i = 0; i< num; i++)
        {
            for(int j = 0; j < weights.Count; j++)
            {
                current += weights[j];
                if (random < current)
                {
                    id_List[current].count += 1;
                    current = 0;
                    random = UnityEngine.Random.Range(0, total);
                    continue;
                }
            }
        }

        foreach(var value in id_List.Values)
        {
            GetValuesItem<T> temp2 = value;
            target.AddRange(temp2.GetRandomCountValues());
        }

        return target;
    }
}

public class GetValuesItem<T>
{
    public int count;
    public List<T> values;

    public List<T> GetRandomCountValues()
    {
        List<T> temp = new List<T>(values);
        temp.Shuffle();
        return temp.GetRange(index: 0, count: Mathf.Min(count, temp.Count));
    }
}

