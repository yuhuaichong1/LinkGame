
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawableWaitCheck : BaseUI
    {
        protected override void OnAwake() { }
        protected override void OnEnable() 
        {
            ShowAnim(mPlane);
        }
        	    private void OnExitBtnClickHandle()
        {
            HideAnim(mPlane, () => 
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawableWaitCheck);
            });
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}