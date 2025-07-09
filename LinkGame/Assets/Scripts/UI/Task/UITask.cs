
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UITask : BaseUI
    {
        protected override void OnAwake()
        { 
        
        }
        protected override void OnEnable()        {
            mDailyTasksToggle.isOn = true;            mChallengeTaskToggle.isOn = false;        }	    private void OnExitBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUITask);
        }

        private void OnDailyTasksTogChangeHandle(bool b)
        {
            mDailyScroll.gameObject.SetActive(b);
            mChallengeScroll.gameObject.SetActive(!b);
            mDailyScrollSR.verticalNormalizedPosition = 1f;
        }

        private void OnChallengeTasksTogChangeHandle(bool b)
        {
            mChallengeScroll.gameObject.SetActive(b);
            mDailyScroll.gameObject.SetActive(!b);
            mChallengeScrollSR.verticalNormalizedPosition = 1f;
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}