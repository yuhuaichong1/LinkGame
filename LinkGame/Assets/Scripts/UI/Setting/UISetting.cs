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

            OnText = LanguageModule.GetText("10088");
            OffText = LanguageModule.GetText("10089");
            string id = ModuleMgr.Instance.TDAnalyticsManager.GetAccoundId();
            if (id == null || id == "") { id = "0000000"; };
            mIDText.text = $"{LanguageModule.GetText("10048")}{id}";
        }
        protected override void OnEnable() 
        {
            bool muiscb = SPlayerPrefs.GetBool(PlayerPrefDefines.musicToggle);
            bool vibration = SPlayerPrefs.GetBool(PlayerPrefDefines.vibrateToggle);
            mUserLvText.text = $"LV.{GamePlayFacade.GetCurLevel()}";


            ShowAnim(mPlane, () => 
            {
                mMusicSMT.isOn = muiscb;
                mVibrationSMT.isOn = vibration;
                MusicSMTShow(muiscb);
                VibrationSMTShow(vibration);
            });
        }
        	    private void OnExitBtnClickHandle()        {            HideAnim(mPlane, () =>             {
                UIManager.Instance.CloseUI(EUIType.EUISetting);            });        }	    private void OnUserLvDetailsBtnClickHandle()
        {
            UIManager.Instance.OpenWindowAsync<UIUserLevel>(EUIType.EUIUserLevel);
        }
        private void OnMusicSMTChange(bool b)
        {
            MusicSMTShow(b);
            AudioModule.SetMusicVolume(b ? 1 : 0);
            AudioModule.SetEffectsVolume(b ? 1 : 0);
        }
        private void MusicSMTShow(bool b)
        {
            mMSMTLabel.alignment = b ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
            mMSMTLabel.text = b ? OnText : OffText;
        }

        private void OnVibrationSMTChange(bool b)
        {
            VibrationSMTShow(b);
            AudioModule.SetVibrate(b);
        }
        private void VibrationSMTShow(bool b)
        {
            mVSMTLabel.alignment = b ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
            mVSMTLabel.text = b ? OnText : OffText;
        }


        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}