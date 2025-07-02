using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIFuncPopup : BaseUI
    {	protected Button mExitBtn;	protected Image mIcon;	protected Text mContent;	protected Button mAdGetBtn;	protected LanguageText mProgessText;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mIcon = mTransform.Find("Plane/InsideBg/Icon").GetComponent<Image>();		mContent = mTransform.Find("Plane/InsideBg/Content").GetComponent<Text>();		mAdGetBtn = mTransform.Find("Plane/AdGetBtnGroup/AdGetBtn").GetComponent<Button>();		mProgessText = mTransform.Find("ProgessText").GetComponent<LanguageText>();
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