
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIAwesome : BaseUI
    {
        protected override void OnAwake() 
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(mCSFText);
        }
        protected override void OnEnable() { }
        	    private void OnClaimBtnClickHandle(){}	    private void OnOnlyMoneyBtnClickHandle(){}
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}