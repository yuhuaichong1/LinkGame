using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeEffect
{
    public static Action<Action> PlayCloudEffect;                           //���Ų���ˢ����Ч��Ч
    public static Action PlayRewardNoticeEffect;                            //�����Ϸ���һ�ȡ������Ч
    public static Action<List<UIRibbonRewardItem>> PlayRewardEffect;        //���Ų��Ż�ȡ������Ч��Ч
    public static Action<Transform, Action> PlayLevelTargetEffect;          //���Ų��Źؿ�Ŀ����Ч��Ч
    public static Action<Transform, Transform> PlayFlyMoneyEffect;          //���Ų��ŷ�Ǯ��Ч��Ч
    public static Action<Transform, Transform> PlayFlyFuncEffect;           //���Ų��ŷɵ�����Ч��Ч
    public static Action<Transform, Sprite, Action> PlayTMDEffect;                  //���Ų��ŷ���ָ����Ч��Ч
}
