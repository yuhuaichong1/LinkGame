using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeEffect
{
    public static Action<Action> PlayCloudEffect;                                       //����ˢ����Ч��Ч
    public static Action PlayRewardNoticeEffect;                                        //�����Ϸ���һ�ȡ������Ч
    public static Action<List<RewardItem>, Action> PlayRewardEffect;                    //���Ż�ȡ������Ч��Ч
    public static Action<Transform, Action> PlayLevelTargetEffect;                      //���Źؿ�Ŀ����Ч��Ч
    public static Action<Transform, Transform, float> PlayFlyMoneyEffect;               //���ŷ�Ǯ��Ч��Ч
    public static Action<Transform, Transform, float, EFuncType> PlayFlyFuncEffect;     //���ŷɵ�����Ч��Ч
    public static Action<Transform, Sprite, Action> PlayTMDEffect;                      //���ŷ���ָ����Ч��Ч
    public static Action<int, RectTransform, Transform> PlayPluralFlyMoney;             //���Ŷ����Ǯ��Ч��Ч
    public static Action<float> PlayGetMoneyTipEffect;                                  //���Ż�ȡ��Ǯ��ʾ��Ч
}
