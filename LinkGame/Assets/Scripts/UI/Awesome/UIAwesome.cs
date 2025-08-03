
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIAwesome : BaseUI
    {
        private float awesomeMoney;
        private float awesomeMoney_Extra;
        private LanguageModule LanguageModule;

        protected override void OnAwake() 
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(mCSFText);

            LanguageModule = ModuleMgr.Instance.LanguageMod;
        }
        protected override void OnEnable() 
        {
            awesomeMoney = UnityEngine.Random.Range(GameDefines.Awesome_ExtraMoney_Num.x, GameDefines.Awesome_ExtraMoney_Num.y);
            awesomeMoney_Extra = awesomeMoney / 10;
            mMoneyText.text = FacadePayType.RegionalChange(awesomeMoney);
            mOnlyMoney.text = $"{LanguageModule.GetText("10057")} {FacadePayType.RegionalChange(awesomeMoney_Extra)}";

            ShowAnim(mPlane);
        }
        	    private void OnClaimBtnClickHandle()        {            FacadeAd.PlayRewardAd(EAdSource.Awesome ,() =>             {
                PlayerFacade.AddWMoney(awesomeMoney);
                FacadeEffect.PlayRewardEffect(new List<RewardItem>() 
                { 
                    new RewardItem()
                    {
                        type = ERewardType.Money,
                        count = awesomeMoney,
                    }
                }, () => 
                {
                    GamePlayFacade.ChangeMoneyShow();
                });
                HideAnim(mPlane, () => 
                {
                    UIManager.Instance.CloseUI(EUIType.EUIAwesome);
                });
                            }, null);        }	    private void OnOnlyMoneyBtnClickHandle()
        {
            PlayerFacade.AddWMoney(awesomeMoney_Extra);
            FacadeEffect.PlayRewardEffect(new List<RewardItem>()
            {
                new RewardItem()
                {
                    type = ERewardType.Money,
                    count = awesomeMoney_Extra,
                }
            }, () =>
            {
                GamePlayFacade.ChangeMoneyShow();
            });
            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIAwesome);
            });
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}