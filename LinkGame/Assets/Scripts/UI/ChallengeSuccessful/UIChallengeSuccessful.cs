
using cfg;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIChallengeSuccessful : BaseUI
    {
        LanguageModule LanguageModule;

        float adRewardValue;
        float rewardValue;

        protected override void OnAwake()
        {
            LanguageModule = new LanguageModule();
        }
        protected override void OnEnable()
        {
            bool ifwithdraw;
            if (!GameDefines.ifIAA)
            {
                ConfLevel level = ConfigModule.Instance.Tables.TBLevel.Get(GamePlayFacade.GetCurLevel() - 1);
                rewardValue = level.Reward;
                ifwithdraw = level.WithdrawType == 1;
                WithdrawText.text = LanguageModule.GetText("10015");
            }
            else
            {
                ConfLevelAct level = ConfigModule.Instance.Tables.TBLevelAct.Get(GamePlayFacade.GetCurLevel() - 1);
                rewardValue = level.Reward;
                ifwithdraw = level.WithdrawType == 1;
                WithdrawText.text = LanguageModule.GetText("10038");
                mMoney.sprite = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncDiamondBox_IconPath);
            }
            string str = FacadePayType.RegionalChange(rewardValue);
            mMoneyText.text = str;

            mWithdrawBtn.gameObject.SetActive(ifwithdraw);
            mClaimBtn.gameObject.SetActive(!ifwithdraw);
            mOnlyMoney.gameObject.SetActive(!ifwithdraw);
            if (!ifwithdraw)
            {
                rewardValue = rewardValue / 10;
                LayoutRebuilder.ForceRebuildLayoutImmediate(mParent);
                mOnlyMoney.text = $"{LanguageModule.GetText("10057")} {FacadePayType.RegionalChange(rewardValue)}";
            }

            mParticle.Play();

            if (GamePlayFacade.GetIsTutorial())
            {
                FacadeGuide.NextStepHead();
                FacadeGuide.PlayGuide();
                FacadeGuide.SetWithdrawableUIcheck(true);
            }
        }

        private void OnWithdrawBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
            UIManager.Instance.OpenSync<UIGamePlay>(GameDefines.ifIAA ? EUIType.EUIGamePlayBy : EUIType.EUIGamePlay);

            PlayerFacade.AddWMoney(adRewardValue);
        }

        private void OnClaimBtnClickHandle()
        {
         /*   FacadeAd.PlayRewardAd(() =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
                UIManager.Instance.OpenSync<UIGamePlay>(GameDefines.ifIAA ? EUIType.EUIGamePlayBy : EUIType.EUIGamePlay);
            }, null);*/
        }

        private void OnOnlyMoneyBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
            UIManager.Instance.OpenSync<UIGamePlay>(GameDefines.ifIAA ? EUIType.EUIGamePlayBy : EUIType.EUIGamePlay);
            PlayerFacade.AddWMoney(rewardValue);
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}