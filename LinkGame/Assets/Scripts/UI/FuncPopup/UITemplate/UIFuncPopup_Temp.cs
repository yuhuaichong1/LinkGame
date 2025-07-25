using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIFuncPopup : BaseUI
    {	protected Button mExitBtn;	protected Image mIcon;	protected Text mContent;	protected Button mAdGetBtn;	protected LanguageText mProgessText;	protected RectTransform mProgessTextRect;	protected RectTransform mCSFText;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mIcon = mTransform.Find("Plane/InsideBg/Icon").GetComponent<Image>();		mContent = mTransform.Find("Plane/InsideBg/Content").GetComponent<Text>();		mAdGetBtn = mTransform.Find("Plane/AdGetBtnGroup/AdGetBtn").GetComponent<Button>();		mProgessText = mTransform.Find("ProgessText").GetComponent<LanguageText>();
            mProgessTextRect = mTransform.Find("ProgessText").GetComponent<RectTransform>();		mCSFText = mTransform.Find("Plane/AdGetBtnGroup/AdGetBtn/Layout/CSFText").GetComponent<RectTransform>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mAdGetBtn.onClick.AddListener( OnAdGetBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mAdGetBtn.onClick.RemoveAllListeners();
        }
    
    }
}