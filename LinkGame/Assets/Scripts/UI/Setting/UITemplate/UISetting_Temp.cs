using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UISetting : BaseUI
    {	protected Button mExitBtn;	protected Image mIcon;	protected LanguageText mIDText;	protected LanguageText mUserLvText;	protected Button mUserLvDetailsBtn;	protected SMovingToggle mMusicSMT;	protected Text mMSMTLabel;	protected SMovingToggle mVibrationSMT;	protected Text mVSMTLabel;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mIcon = mTransform.Find("Plane/InsideBg/IconMask/Icon").GetComponent<Image>();		mIDText = mTransform.Find("Plane/InsideBg/IDText").GetComponent<LanguageText>();		mUserLvText = mTransform.Find("Plane/InsideBg/UserLvText").GetComponent<LanguageText>();		mUserLvDetailsBtn = mTransform.Find("Plane/InsideBg/UserLvDetailsBtn").GetComponent<Button>();		mMusicSMT = mTransform.Find("Plane/MusicSwitch/MusicSMT").GetComponent<SMovingToggle>();		mMSMTLabel = mTransform.Find("Plane/MusicSwitch/MusicSMT/MSMTLabel").GetComponent<Text>();		mVibrationSMT = mTransform.Find("Plane/VibrationSwitch/VibrationSMT").GetComponent<SMovingToggle>();		mVSMTLabel = mTransform.Find("Plane/VibrationSwitch/VibrationSMT/VSMTLabel").GetComponent<Text>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {  			mExitBtn.onClick.AddListener( OnExitBtnClickHandle);			mUserLvDetailsBtn.onClick.AddListener( OnUserLvDetailsBtnClickHandle);
            mMusicSMT.onValueChange.AddListener( OnMusicSMTChange);
			mVibrationSMT.onValueChange.AddListener( OnVibrationSMTChange);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mUserLvDetailsBtn.onClick.RemoveAllListeners();
        }
    
    }
}