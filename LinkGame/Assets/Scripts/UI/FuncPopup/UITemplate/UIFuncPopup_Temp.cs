using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIFuncPopup : BaseUI
    {	protected Button mExitBtn;	protected Image mIcon;	protected Text mContent;	protected Button mAdGetBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mIcon = mTransform.Find("Plane/Icon").GetComponent<Image>();		mContent = mTransform.Find("Plane/Content").GetComponent<Text>();		mAdGetBtn = mTransform.Find("Plane/AdGetBtn").GetComponent<Button>();
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