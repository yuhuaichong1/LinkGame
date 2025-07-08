
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawalInformation : BaseUI
    {
        protected override void OnAwake() { }
        protected override void OnEnable() { }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIWithdrawalInformation);            UIManager.Instance.OpenWindowAsync<UIWithdrawalChannel>(EUIType.EUIWithdrawalChannel);        }	    private void OnReEnterBtnClickHandle(){}
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}