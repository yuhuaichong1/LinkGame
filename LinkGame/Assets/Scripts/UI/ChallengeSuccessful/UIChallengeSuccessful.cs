
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
            ConfLevel level = ConfigModule.Instance.Tables.TBLevel.Get(GamePlayFacade.GetCurLevel() - 1);
            adRewardValue = level.Reward;
            bool ifwithdraw = level.WithdrawType == 1;

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
                rewardValue = adRewardValue / 10;
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
            UIManager.Instance.OpenSync<UIGamePlay>(EUIType.EUIGamePlay);
            PlayerFacade.AddWMoney(adRewardValue);
            GamePlayFacade.ChangeMoneyShow();
        }

        private void OnClaimBtnClickHandle()
        {
            FacadeAd.PlayRewardAd(EAdSource.ChallengeSuccessful,() =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
                UIManager.Instance.OpenSync<UIGamePlay>(EUIType.EUIGamePlay);
                PlayerFacade.AddWMoney(adRewardValue);
                GamePlayFacade.ChangeMoneyShow();
            }, null);
        }

        private void OnOnlyMoneyBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
            UIManager.Instance.OpenSync<UIGamePlay>(EUIType.EUIGamePlay);
            PlayerFacade.AddWMoney(rewardValue);
            GamePlayFacade.ChangeMoneyShow();
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}