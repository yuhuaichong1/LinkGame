using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawalInformation : BaseUI
    {	protected Button mExitBtn;	protected Image mCurPayIcon;	protected Text mInfoText;	protected Button mReEnterBtn;	protected Button mWNEnterBtn;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mCurPayIcon = mTransform.Find("Plane/CurPayIcon").GetComponent<Image>();		mInfoText = mTransform.Find("Plane/InfoBg/InfoText").GetComponent<Text>();		mReEnterBtn = mTransform.Find("Plane/InfoBg/Btns/ReEnterBtn").GetComponent<Button>();		mWNEnterBtn = mTransform.Find("Plane/InfoBg/Btns/WNEnterBtn").GetComponent<Button>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mReEnterBtn.onClick.AddListener( OnReEnterBtnClickHandle);		mWNEnterBtn.onClick.AddListener( OnWNEnterBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mReEnterBtn.onClick.RemoveAllListeners();		mWNEnterBtn.onClick.RemoveAllListeners();
        }
    
    }
}