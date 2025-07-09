
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIUserLevel : BaseUI
    {
        private LanguageModule LanguageModule;

        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;
        }
        protected override void OnEnable() 
        {
            int userLevel = PlayerFacade.GetUserLevel();
            //mCULText.text = string.Format(LanguageModule.GetText(""), userLevel.ToString());
            int nextTotalExp = ConfigModule.Instance.Tables.TBUserLevel.GetOrDefault(userLevel - 1).NextLevelExp;
            int curExp = PlayerFacade.GetCurUserExp();
            float progress = curExp * 1f / nextTotalExp;
            progress = Mathf.Floor(progress * 100) / 100f;
            mCULSlider.value = progress;
            mCULSText.text = $"{progress}%";
        }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIUserLevel);        }	    private void OnContinueBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUIUserLevel);
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}