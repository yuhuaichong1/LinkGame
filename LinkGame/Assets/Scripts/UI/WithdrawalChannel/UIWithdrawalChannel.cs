
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawalChannel : BaseUI
    {
        protected override void OnAwake() 
        {
            mAddressOrPhonePlaceholder.text = ModuleMgr.Instance.LanguageMod.GetText("10043");
        }
        protected override void OnEnable() 
        {
            ShowAnim(mPlane);
        }
        	    private void OnExitBtnClickHandle()        {            HideAnim(mPlane, () =>             {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalChannel);            });        }	    private void OnConfirmBtnClickHandle()
        {
            if (mAddressOrPhoneInput.text != "")
            {
                PlayerFacade.SetNameAndWEmailOrPhone("", mAddressOrPhoneInput.text);
                HideAnim(mPlane, () =>
                {
                    UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);
                    UIManager.Instance.CloseUI(EUIType.EUIWithdrawalChannel);
                    UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
                });
                
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