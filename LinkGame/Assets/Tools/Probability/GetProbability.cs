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
