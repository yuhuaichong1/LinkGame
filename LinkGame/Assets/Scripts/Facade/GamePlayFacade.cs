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
    public static Func<EGoodMoveDic> ChangeDirection;     //方向变更功能
    public static Action<int> ChangeTipCount;             //改变提示功能数量
    public static Action<int> ChangeRefushCount;          //改变刷新功能数量
    public static Action<int> ChangeRemoveCount;          //改变移除功能数量（目前作用为“方向变更”的数量）
    public static Func<int> GetCurLevel;                  //得到当前关卡
    public static Func<int> GetTipCount;                  //获取提示功能数量
    public static Func<int> GetRefushCount;               //获取刷新功能数量
    public static Func<int> GetRemoveCount;               //获取移除功能数量
    public static Action<EMapState> ChangeMapState;       //改变关卡状态
    public static Func<EMapState> GetMapState;            //得到关卡状态
    public static Action<Vec2> UpdateHiddleState;         //更新冰冻状态(消除的物品周围的状态)
    public static Action<int, int, bool> RemoveHidden;    //移除场景中某行某列的冰冻特效
    public static Action<Vec2> RemoveGood;                //移除场景中的物体
    public static Action<bool> UpdateMap;                 //更新地图数据（是否进入下一关）
    public static Func<int> GetRow;                       //获取行的数量
    public static Action<bool> SetIsCheckWrong;           //
    public static Action<object> CheckIdAdd;              //
    public static Func<int, Sprite> GetGoodIcon;          //获取物品图片
    public static Action<Vec2> Select;                    //选中（GamePlayModule中的Select）
    public static Func<GameObject[][]> GetMAPGoods;       //获取当前关卡的所有物体

    public static Func<Transform> GetMapTrans;            //获取生成物体的父对象
}


    