
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UICongratfulations : BaseUI
    {
        private float awesomeMoney;
        private float awesomeMoney_Extra;
        private LanguageModule LanguageModule;

        protected override void OnAwake()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(mCSFText);

            LanguageModule = ModuleMgr.Instance.LanguageMod;
            if (GameDefines.ifIAA)
            {
                mMoney.sprite = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncDiamondBox_IconPath);
                mMoney.SetNativeSize();
            }
        }

        protected override void OnSetParam(params object[] args)
        {
            awesomeMoney = (float)args[0];
        }

        protected override void OnEnable()
        {
            awesomeMoney_Extra = awesomeMoney / 10;
            mMoneyText.text = FacadePayType.RegionalChange(awesomeMoney);
            mOnlyMoney.text = $"{ LanguageModule.GetText("10057")} { FacadePayType.RegionalChange(awesomeMoney_Extra)}";

            ShowAnim(mPlane);
        }

        private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUICongratfulations);        }
        private void OnClaimBtnClickHandle()        {
            FacadeAd.PlayRewardAd(() =>
            {
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
                    FacadeTask.CurMoneyTextShow();
                    GamePlayFacade.ChangeMoneyShow();
                });

                FacadeTask.ReceiveDataRemove();
                HideAnim(mPlane, () =>
                {
                    UIManager.Instance.CloseUI(EUIType.EUICongratfulations);
                });            }, null);        }
        private void OnOnlyMoneyBtnClickHandle()
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
                FacadeTask.CurMoneyTextShow();
                GamePlayFacade.ChangeMoneyShow();
            });

            FacadeTask.ReceiveDataRemove();

            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUICongratfulations);
            });
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}