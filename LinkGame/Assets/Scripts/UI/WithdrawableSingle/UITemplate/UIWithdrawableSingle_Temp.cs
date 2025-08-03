using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawableSingle : BaseUI
    {	protected Button mExitBtn;	protected Text mLevelTitle;	protected Text mCurWMoney;	protected Button mWithdrawBtn;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mLevelTitle = mTransform.Find("Plane/TitleBg/LevelTitle").GetComponent<Text>();		mCurWMoney = mTransform.Find("Plane/CenterBg/curWMoney").GetComponent<Text>();		mWithdrawBtn = mTransform.Find("Plane/WithdrawBtn").GetComponent<Button>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mWithdrawBtn.onClick.AddListener( OnWithdrawBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mWithdrawBtn.onClick.RemoveAllListeners();
        }
    
    }
}