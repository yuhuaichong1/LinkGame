
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UISetting : BaseUI
    {
        protected override void OnAwake() { }
        protected override void OnEnable() { }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUISetting);        }	    private void OnUserLvDetailsBtnClickHandle(){}
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}