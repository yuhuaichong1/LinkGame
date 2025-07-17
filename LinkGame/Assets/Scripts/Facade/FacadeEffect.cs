using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeEffect
{
    public static Action<Action> PlayCloudEffect;                                       //播放刷新特效特效
    public static Action PlayRewardNoticeEffect;                                        //播放上方玩家获取奖励特效
    public static Action<List<RewardItem>, Action> PlayRewardEffect;                    //播放获取奖励特效特效
    public static Action<Transform, Action> PlayLevelTargetEffect;                      //播放关卡目标特效特效
    public static Action<Transform, Transform, float> PlayFlyMoneyEffect;               //播放飞钱特效特效
    public static Action<Transform, Transform, float, EFuncType> PlayFlyFuncEffect;     //播放飞道具特效特效
    public static Action<Transform, Sprite, Action> PlayTMDEffect;                      //播放方向指明特效特效
    public static Action<int, RectTransform, Transform> PlayPluralFlyMoney;             //播放多个飞钱特效特效
    public static Action<float> PlayGetMoneyTipEffect;                                  //播放获取飞钱提示特效
}
