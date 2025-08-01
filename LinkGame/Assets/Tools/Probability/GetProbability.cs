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
    /// <param name="pros">字典数据（权重<==>对应值）</param>
    /// <returns>对应值</returns>
    public static T GatValue<T>(Dictionary<int, T> pros)
    {
        List<int> values = pros.Keys.ToList();
        int total = 0;
        foreach (int item in values)
        {
            total += item;
        }
        int random = UnityEngine.Random.Range(0, total);
        int currentSum = 0;
        foreach (int key in values)
        {
            currentSum += key;
            if (currentSum > random)
            {
                return pros[key];
            }
        }

        return default(T);
    }
}
