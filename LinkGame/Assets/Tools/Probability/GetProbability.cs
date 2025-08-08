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
    /// <param name="pros">�ֵ����ݣ���Ӧֵ<==>Ȩ�أ�</param>
    /// <returns>��Ӧֵ</returns>
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

        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < weights.Count; j++)
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

        foreach (var value in id_List.Values)
        {
            GetValuesItem<T> temp2 = value;
            target.AddRange(temp2.GetRandomCountValues());
        }

        return target;
    }

    public static List<T> GetValuesOptimized<T>(Dictionary<List<T>, int> pros, int num)
    {
        // 1. ���Ƚ����м�ֵ��չ��Ϊ����Ԫ�غͶ�Ӧ��Ȩ��
        var weightedItems = new List<(T item, int weight)>();
        foreach (var kvp in pros)
        {
            foreach (var item in kvp.Key)
            {
                weightedItems.Add((item, kvp.Value));
            }
        }

        // 2. ���û���㹻��Ԫ�أ���������
        if (weightedItems.Count <= num)
        {
            return weightedItems.Select(x => x.item).ToList();
        }

        // 3. ������Ȩ��
        int totalWeight = weightedItems.Sum(x => x.weight);

        // 4. ����Ȩ�����ѡ��Ԫ��
        var result = new List<T>();
        var random = new System.Random();
        var tempList = new List<(T item, int weight)>(weightedItems);

        for (int i = 0; i < num; i++)
        {
            // ����Ѿ�û��Ȩ���ˣ�����ѭ��
            if (totalWeight <= 0)
                break;

            // ���ѡ��һ����
            int randomNumber = random.Next(0, totalWeight);
            int cumulativeWeight = 0;

            // ����Ȩ��ѡ��Ԫ��
            for (int j = 0; j < tempList.Count; j++)
            {
                cumulativeWeight += tempList[j].weight;
                if (randomNumber < cumulativeWeight)
                {
                    result.Add(tempList[j].item);
                    totalWeight -= tempList[j].weight;
                    tempList.RemoveAt(j);
                    break;
                }
            }
        }

        return result;
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
}

