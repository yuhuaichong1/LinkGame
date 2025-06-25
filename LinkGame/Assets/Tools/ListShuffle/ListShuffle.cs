using System;
using System.Collections.Generic;

public static class ShuffleHelper
{
    //ϴ���㷨
    // ����һ����̬���� Shuffle�����ڴ����б���Ԫ�ص�˳��
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        // �ڷ����ڲ����� Random ʵ��
        Random rng = new Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
