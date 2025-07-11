
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawalChannel : BaseUI
    {
        protected override void OnAwake() { }
        protected override void OnEnable() { }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIWithdrawalChannel);        }	    private void OnConfirmBtnClickHandle()
        {
            if (mAddressOrPhoneInput.text != "")
            {
                PlayerFacade.SetNameAndWEmailOrPhone("", mAddressOrPhoneInput.text);
                UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalChannel);
                UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
            }
            else
            {
                UIManager.Instance.OpenNotice("没填地址/电话");
            }
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}