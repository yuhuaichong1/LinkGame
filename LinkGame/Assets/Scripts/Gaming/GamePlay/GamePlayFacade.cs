using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayFacade
{
    public static Action ChangeTipCountShow;          //改变提示功能的次数显示
    public static Action ChangeRefushCountShow;       //改变刷新功能的次数显示
    public static Action ChangeRemoveCountShow;       //改变移除功能的次数显示

    public static Action CreateLevel;                     //创建关卡
    public static Action<Vector2, Vector2> CheckIfLink;   //检测是否能链接
    public static Action TipFunc;                         //提示功能
    public static Action RefushFunc;                      //刷新功能
    public static Action RemoveFunc;                      //移除功能
    public static Action<int> ChangeTipCount;             //改变提示功能数量
    public static Action<int> ChangeRefushCount;          //改变刷新功能数量
    public static Action<int> ChangeRemoveCount;          //改变移除功能数量
    public static Func<int> GetTipCount;                  //获取提示功能数量
    public static Func<int> GetRefushCount;               //获取刷新功能数量
    public static Func<int> GetRemoveCount;               //获取移除功能数量
}


    