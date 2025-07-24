
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
            rewardValue = level.Reward;
            bool ifwithdraw = level.WithdrawType == 1;

            string str = FacadePayType.RegionalChange(rewardValue);
            mMoneyText.text = str;

            mWithdrawBtn.gameObject.SetActive(ifwithdraw);
            mClaimBtn.gameObject.SetActive(!ifwithdraw);
            mOnlyMoney.gameObject.SetActive(!ifwithdraw);
            if (!ifwithdraw)
            {
                rewardValue = rewardValue / 10;
                LayoutRebuilder.ForceRebuildLayoutImmediate(mParent);
                mOnlyMoney.text = string.Format(LanguageModule.GetText(""), FacadePayType.RegionalChange(rewardValue));
            }

            mParticle.Play();

            if (GamePlayFacade.GetIsTutorial())
            {
                Debug.LogError(FacadeGuide.GetCurStep());
                FacadeGuide.PlayGuide(FacadeGuide.GetCurStep());
            }
        }
        	    private void OnWithdrawBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
            UIManager.Instance.OpenSync<UIGamePlay>(EUIType.EUIGamePlay);
            PlayerFacade.AddWMoney(adRewardValue);
        }

        private void OnClaimBtnClickHandle()
        {
            FacadeAd.PlayRewardAd(() =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
                UIManager.Instance.OpenSync<UIGamePlay>(EUIType.EUIGamePlay);
                PlayerFacade.AddWMoney(adRewardValue);
            }, null);
        }

        private void OnOnlyMoneyBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIChallengeSuccessful);
            UIManager.Instance.OpenSync<UIGamePlay>(EUIType.EUIGamePlay);
            PlayerFacade.AddWMoney(rewardValue);
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}