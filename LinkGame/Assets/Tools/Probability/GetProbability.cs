using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//����Ȩ�ػ�ȡ��Ӧֵ
public static class GetProbability
{
    /// <summary>
    /// ����Ȩ�صõ���Ӧ��ֵ
    /// </summary>
    /// <typeparam name="T">��Ӧֵ������ </typeparam>
    /// <param name="pros">�ֵ����ݣ�Ȩ��<==>��Ӧֵ��</param>
    /// <returns>��Ӧֵ</returns>
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
