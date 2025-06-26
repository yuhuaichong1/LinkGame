using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayDefines
{
    public static Action ChangeFuncTipCount;              //改变提示功能的次数显示
    public static Action ChangeFuncRefushCount;           //改变刷新功能的次数显示
    public static Action ChangeFuncRemoveCount;           //改变移除功能的次数显示

    public static Action CreateLevel;                     //创建关卡
    public static Action<Vector2, Vector2> CheckIfLink;   //检测是否能链接
    public static Action TipFunc;                         //提示功能
    public static Action RefushFunc;                      //刷新功能
    public static Action RemoveFunc;                      //移除功能
}


    