using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace XrCode
{
    public partial class UIPopup : BaseUI
    {
        private Action okCb;
        private Action cancelCb;
        private Action autoCb;

        private Coroutine StartCountDown;

        protected override void OnAwake()
        {

        }
        protected override void OnEnable() { }

        // 临时处理
        public void SetCallBack(string content, Action okCb = null, Action cancelCb = null)
        {
            if (!string.IsNullOrEmpty(content))
            {
                mLabTips.text = content;
            }
            this.okCb = okCb;
            this.cancelCb = cancelCb;
            mSelectGroup.gameObject.SetActive(true);
        }

        //点击自动确认按钮
        private void OnBtnAutoClickHandle()
        {
            this.autoCb?.Invoke();
            if (StartCountDown != null)
            {
                Game.Instance.StopCoroutine(StartCountDown);
            }
            UIManager.Instance.CloseUI(EUIType.EUIPopup);
        }
        //点击取消按钮
        private void OnBtnCancelClickHandle()
        {
            this.cancelCb?.Invoke();
            UIManager.Instance.CloseUI(EUIType.EUIPopup);
        }
        //点击确认按钮
        private void OnBtnOKClickHandle()
        {
            this.okCb?.Invoke();
            UIManager.Instance.CloseUI(EUIType.EUIPopup);
        }






        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}