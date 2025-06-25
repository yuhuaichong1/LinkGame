using System;
using System.Collections.Generic;

public static class ShuffleHelper
{
    //洗牌算法
    // 定义一个静态方法 Shuffle，用于打乱列表中元素的顺序
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        // 在方法内部创建 Random 实例
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
