
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawableSingle : BaseUI
    {
        private LanguageModule LanguageModule;
        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;
        }
        protected override void OnEnable() 
        {
            mLevelTitle.text = $"{LanguageModule.GetText("10049")}{GamePlayFacade.GetCurLevel()}";
            mCurWMoney.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());

            ShowAnim(mPlane);
        }
        	    private void OnExitBtnClickHandle()        {            HideAnim(mPlane, () =>             {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawableSingle);            });        }	    private void OnWithdrawBtnClickHandle()
        {
            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawableSingle);                if (PlayerFacade.GetPayType() == 0)
                {
                    UIManager.Instance.OpenWindowAsync<UIEnterInfo>(EUIType.EUIEnterInfo);
                }
                else
                {
                    UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
                }            });
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}