
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIWithdrawableMultiple : BaseUI
    {
        private LanguageModule LanguageModule;
        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;
        }
        protected override void OnEnable() 
        {
            mLevelTitle.text = string.Format(LanguageModule.GetText(""),1);
            mGoalTitle.text = string.Format(LanguageModule.GetText(""), 1); ;

            mPreLevelTitle.text = string.Format(LanguageModule.GetText(""), 1); ;
            mPregoalTitle.text = string.Format(LanguageModule.GetText(""), 1); ;

            mPrePreLevelTitle.text = string.Format(LanguageModule.GetText(""), 1); ;
            mPrePregoalTitle.text = string.Format(LanguageModule.GetText(""), 1); ;

            mCurWMoney.text = "";
        }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIWithdrawableMultiple);        }	    private void OnWithdrawBtnClickHandle()
        {
            //UIManager.Instance.OpenNotice(LanguageModule.GetText(""));
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}