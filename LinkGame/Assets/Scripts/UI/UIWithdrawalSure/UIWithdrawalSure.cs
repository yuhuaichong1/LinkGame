
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawalSure : BaseUI
    {
        protected override void OnAwake()
        {
                                                                            
        }
        protected override void OnEnable() 
        {
            ShowAnim(mPlane);
        }
        	    private void OnExitBtnClickHandle()        {
            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalSure);
            });        }	    private void OnConfirmBtnClickHandle()
        {
            HideAnim(mPlane, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawalSure);
                UIManager.Instance.OpenWindowAsync<UIWithdrawalSuccessful>(EUIType.EUIWithdrawalSuccessful);
            });
        }
        protected override void OnDisable() 
        {
        
        }
        protected override void OnDispose()
        {
        
        }
    }
}