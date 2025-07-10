using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIRate : BaseUI
    {	protected Button mExitBtn;	protected Button mStar1;	protected Image mStar1Btn;	protected Button mStar2;	protected Image mStar2Btn;	protected Button mStar3;	protected Image mStar3Btn;	protected Button mStar4;	protected Image mStar4Btn;	protected Button mStar5;	protected Image mStar5Btn;	protected Button mRateBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            			mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();			mStar1 = mTransform.Find("Plane/CenterBg/Stars/Star1").GetComponent<Button>();
            mStar1Btn = mTransform.Find("Plane/CenterBg/Stars/Star1").GetComponent<Image>();			mStar2 = mTransform.Find("Plane/CenterBg/Stars/Star2").GetComponent<Button>();
            mStar2Btn = mTransform.Find("Plane/CenterBg/Stars/Star2").GetComponent<Image>();			mStar3 = mTransform.Find("Plane/CenterBg/Stars/Star3").GetComponent<Button>();
            mStar3Btn = mTransform.Find("Plane/CenterBg/Stars/Star3").GetComponent<Image>();			mStar4 = mTransform.Find("Plane/CenterBg/Stars/Star4").GetComponent<Button>();
            mStar4Btn = mTransform.Find("Plane/CenterBg/Stars/Star4").GetComponent<Image>();			mStar5 = mTransform.Find("Plane/CenterBg/Stars/Star5").GetComponent<Button>();
            mStar5Btn = mTransform.Find("Plane/CenterBg/Stars/Star5").GetComponent<Image>();			mRateBtn = mTransform.Find("Plane/RateBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mStar1.onClick.AddListener( OnStar1ClickHandle);		mStar2.onClick.AddListener( OnStar2ClickHandle);		mStar3.onClick.AddListener( OnStar3ClickHandle);		mStar4.onClick.AddListener( OnStar4ClickHandle);		mStar5.onClick.AddListener( OnStar5ClickHandle);		mRateBtn.onClick.AddListener( OnRateBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mStar1.onClick.RemoveAllListeners();		mStar2.onClick.RemoveAllListeners();		mStar3.onClick.RemoveAllListeners();		mStar4.onClick.RemoveAllListeners();		mStar5.onClick.RemoveAllListeners();		mRateBtn.onClick.RemoveAllListeners();
        }
    
    }
}