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
    public static T GatValue<T>(Dictionary<T, int> pros)
    {
        int total = 0;
        foreach (var pair in pros)
        {
            total += pair.Value;
        }

        int random = UnityEngine.Random.Range(0, total);
        
        int current = 0;
        foreach (var pair in pros)
        {
            current += pair.Value;
            if (random < current)
            {
                return pair.Key;
            }
        }

        return default(T);
    }
}
