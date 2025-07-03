
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UILuckMoment : BaseUI
    {
        protected override void OnAwake() { }
        protected override void OnEnable() { }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.ENone);        }	    private void OnSpinBtnClickHandle(){}
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}