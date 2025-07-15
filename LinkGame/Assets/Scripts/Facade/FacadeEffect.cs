using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeEffect
{
    public static Action<Action> PlayCloudEffect;                           //播放播放刷新特效特效
    public static Action PlayRewardNoticeEffect;                            //播放上方玩家获取奖励特效
    public static Action<List<UIRibbonRewardItem>> PlayRewardEffect;        //播放播放获取奖励特效特效
    public static Action<Transform, Action> PlayLevelTargetEffect;          //播放播放关卡目标特效特效
    public static Action<Transform, Transform> PlayFlyMoneyEffect;          //播放播放飞钱特效特效
    public static Action<Transform, Transform> PlayFlyFuncEffect;           //播放播放飞道具特效特效
    public static Action<Transform, Sprite, Action> PlayTMDEffect;                  //播放播放方向指明特效特效
}
