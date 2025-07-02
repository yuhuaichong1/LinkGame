
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UITask : BaseUI
    {
        protected override void OnAwake() { }
        protected override void OnEnable() { }	    private void OnExitBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUITask);
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}