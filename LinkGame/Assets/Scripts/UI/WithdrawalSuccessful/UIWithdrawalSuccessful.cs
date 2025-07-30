
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawalSuccessful : BaseUI
    {
        protected override void OnAwake() 
        { 
        
        }
        protected override void OnEnable() 
        {
            mMoneyText.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());
            ShowAnim(mPlane);
        }
        	    private void OnExitBtnClickHandle()        {            CloseThisUI();        }	    private void OnConfirmBtnClickHandle()
        {
            PlayerFacade.SetWMoney(0);
            GamePlayFacade.ChangeMoneyShow();
            CloseThisUI();
        }

        private void CloseThisUI()
        {
            FacadeGuide.CheckWithdrawableUI();
            HideAnim(mPlane, () => 
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalSuccessful);
            });
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}