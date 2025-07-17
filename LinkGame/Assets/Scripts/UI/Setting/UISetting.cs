using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UISetting : BaseUI
    {
        private LanguageModule LanguageModule;
        private AudioModule AudioModule;

        private string OnText;
        private string OffText;

        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;
            AudioModule = ModuleMgr.Instance.AudioMod;

            OnText = LanguageModule.GetText("");
            OffText = LanguageModule.GetText("");
        }
        protected override void OnEnable() { }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUISetting);        }	    private void OnUserLvDetailsBtnClickHandle()
        {
            UIManager.Instance.OpenWindowAsync<UIUserLevel>(EUIType.EUIUserLevel);
        }
        private void OnMusicSMTChange(bool b)
        {
            mMSMTLabel.alignment = b ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
            mMSMTLabel.text = LanguageModule.GetText(b ? OnText : OffText);
            AudioModule.SetMusicVolume(b? 1 : 0);
            AudioModule.SetEffectsVolume(b? 1 : 0);
        }

        private void OnVibrationSMTChange(bool b)
        {
            mVSMTLabel.alignment = b ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
            mVSMTLabel.text = LanguageModule.GetText(b ? OnText : OffText);

            AudioModule.SetVibrate(b);
        }


        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}