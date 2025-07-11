
using Spine.Unity;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace XrCode
{

    public partial class UIEffect : BaseUI
    {
        protected override void OnAwake() 
        {
            FacadeEffect.PlayCloudEffect += PlayCloudEffect;
            FacadeEffect.PlayRewardNoticeEffect += PlayRewardNoticeEffect;
            FacadeEffect.PlayRewardEffect += PlayRewardEffect;
            FacadeEffect.PlayLevelTargetEffect += PlayLevelTargetEffect;
            FacadeEffect.PlayFlyMoneyEffect += PlayFlyMoneyEffect;
            FacadeEffect.PlayFlyFuncEffect += PlayFlyFuncEffect;

        }
        protected override void OnEnable() 
        {

        }

        //播放刷新特效
        private void PlayCloudEffect(Action refushAction)
        {
            mLeftCloud.DOLocalMoveX(0, 1).SetEase(Ease.OutBack);
            mRightCloud.DOLocalMoveX(0, 1).SetEase(Ease.OutBack);
        }

        //播放上方玩家获取奖励特效
        private void PlayRewardNoticeEffect()
        {

        }

        //播放获取奖励特效
        private void PlayRewardEffect(Transform trans)
        {

        }

        //播放关卡目标特效
        private void PlayLevelTargetEffect(Transform trans)
        {

        }

        //播放飞钱特效
        private void PlayFlyMoneyEffect(Transform trans)
        {

        }

        //播放飞道具特效
        private void PlayFlyFuncEffect(Transform trans)
        {

        }

        protected override void OnDisable() 
        { 
            
        }

        protected override void OnDispose() 
        {
            
        }
    }
}