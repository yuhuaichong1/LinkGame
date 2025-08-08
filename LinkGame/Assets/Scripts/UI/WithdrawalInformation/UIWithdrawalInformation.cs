
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawalInformation : BaseUI
    {
        protected override void OnAwake() 
        { 
        
        }
        protected override void OnEnable() 
        {
            //mCurPayIcon.sprite = PlayerFacade.GetWName
            string ifn = PlayerFacade.GetWName?.Invoke() == "" ? "" : "\n";
            mInfoText.text = $"{PlayerFacade.GetWName?.Invoke()}{ifn}{PlayerFacade.GetWEmailOrPhone?.Invoke()}";

            ShowAnim(mPlane);

            mWNEnterBtn.gameObject.SetActive(PlayerFacade.GetWMoney() != 0 && GamePlayFacade.GetCurLevel() > GameDefines.withdrawLevel);
        }

        private void OnExitBtnClickHandle()        {            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalInformation);
            });            FacadeGuide.CheckWithdrawableUI();        }	    private void OnReEnterBtnClickHandle()
        {
            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalInformation);
                UIManager.Instance.OpenWindowAsync<UIEnterInfo>(EUIType.EUIEnterInfo);
            });
            
        }

        private void OnWNEnterBtnClickHandle()
        {
            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalInformation);
                
                if (GamePlayFacade.GetCurLevel() > GameDefines.doubleLevel)
                {
                    UIManager.Instance.OpenWindowAsync<UIWithdrawalSuccessful>(EUIType.EUIWithdrawalSuccessful);
                }
                else
                {
                    UIManager.Instance.OpenWindowAsync<UIWithdrawalSure>(EUIType.EUIWithdrawalSure);
                }
                
            });
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}