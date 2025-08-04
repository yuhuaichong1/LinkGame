
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
                //rewardValue = level.Reward;
                adRewardValue = level.Reward;
                ifwithdraw = level.WithdrawType == 1;
                WithdrawText.text = LanguageModule.GetText("10015");
            }
            else
            {
                ConfLevelAct level = ConfigModule.Instance.Tables.TBLevelAct.Get(GamePlayFacade.GetCurLevel() - 1);
                //rewardValue = level.Reward;
                adRewardValue = level.Reward;
                ifwithdraw = level.WithdrawType == 1;
                WithdrawText.text = LanguageModule.GetText("10038");
                mMoney.sprite = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncDiamondBox_IconPath);
                mMoney.SetNativeSize();
            }
            //string str = FacadePayType.RegionalChange(rewardValue);
            string str = FacadePayType.RegionalChange(adRewardValue);
            mMoneyText.text = str;

            //mWithdrawBtn.gameObject.SetActive(ifwithdraw);
            //mClaimBtn.gameObject.SetActive(!ifwithdraw);
            //mOnlyMoney.gameObject.SetActive(!ifwithdraw);
            mWithdrawBtn.gameObject.SetActive(false);
            mClaimBtn.gameObject.SetActive(true);
            mOnlyMoney.gameObject.SetActive(true);
            //if (!ifwithdraw)
            //{
                rewardValue = rewardValue / 10;
                LayoutRebuilder.ForceRebuildLayoutImmediate(mParent);
                mOnlyMoney.text = $"{LanguageModule.GetText("10057")} {FacadePayType.RegionalChange(rewardValue)}";
            //}

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
            GamePlayFacade.ChangeMoneyShow();
        }

        private void OnClaimBtnClickHandle()
        {
            FacadeAd.PlayRewardAd(EAdSource.ChallengeSuccessful, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
                UIManager.Instance.OpenSync<UIGamePlay>(GameDefines.ifIAA ? EUIType.EUIGamePlayBy : EUIType.EUIGamePlay);
                PlayerFacade.AddWMoney(adRewardValue);
                GamePlayFacade.ChangeMoneyShow();
            }, null);

        }

        private void OnOnlyMoneyBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
            UIManager.Instance.OpenSync<UIGamePlay>(GameDefines.ifIAA ? EUIType.EUIGamePlayBy : EUIType.EUIGamePlay);
            PlayerFacade.AddWMoney(rewardValue);
            GamePlayFacade.ChangeMoneyShow();
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}