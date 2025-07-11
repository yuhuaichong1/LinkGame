
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
        }

        private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIWithdrawalInformation);        }	    private void OnReEnterBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIWithdrawalInformation);            UIManager.Instance.OpenWindowAsync<UIEnterInfo>(EUIType.EUIEnterInfo);
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}