using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawalInformation : BaseUI
    {	protected Button mExitBtn;	protected Image mCurPayIcon;	protected Text mInfoText;	protected Button mReEnterBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mCurPayIcon = mTransform.Find("Plane/CurPayIcon").GetComponent<Image>();		mInfoText = mTransform.Find("Plane/InfoBg/InfoText").GetComponent<Text>();		mReEnterBtn = mTransform.Find("Plane/InfoBg/ReEnterBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mReEnterBtn.onClick.AddListener( OnReEnterBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mReEnterBtn.onClick.RemoveAllListeners();
        }
    
    }
}