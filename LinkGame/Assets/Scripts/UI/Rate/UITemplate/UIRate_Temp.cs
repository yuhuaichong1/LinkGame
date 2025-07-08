using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIRate : BaseUI
    {	protected Button mExitBtn;	protected Button mStar1Btn;	protected Image mStar1;	protected Button mStar2Btn;	protected Image mStar2;	protected Button mStar3Btn;	protected Image mStar3;	protected Button mStar4Btn;	protected Image mStar4;	protected Button mStar5Btn;	protected Image mStar5;	protected Button mRateBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            			mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();
            mStar1Btn = mTransform.Find("Plane/CenterBg/Stars/Star1").GetComponent<Button>();			mStar1 = mTransform.Find("Plane/CenterBg/Stars/Star1").GetComponent<Image>();
            mStar2Btn = mTransform.Find("Plane/CenterBg/Stars/Star2").GetComponent<Button>();			mStar2 = mTransform.Find("Plane/CenterBg/Stars/Star2").GetComponent<Image>();
            mStar3Btn = mTransform.Find("Plane/CenterBg/Stars/Star3").GetComponent<Button>();			mStar3 = mTransform.Find("Plane/CenterBg/Stars/Star3").GetComponent<Image>();
            mStar4Btn = mTransform.Find("Plane/CenterBg/Stars/Star4").GetComponent<Button>();			mStar4 = mTransform.Find("Plane/CenterBg/Stars/Star4").GetComponent<Image>();
            mStar5Btn = mTransform.Find("Plane/CenterBg/Stars/Star5").GetComponent<Button>();			mStar5 = mTransform.Find("Plane/CenterBg/Stars/Star5").GetComponent<Image>();			mRateBtn = mTransform.Find("Plane/RateBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            			mExitBtn.onClick.AddListener( OnExitBtnClickHandle);
            mStar1Btn.onClick.AddListener( OnStar1ClickHandle);
            mStar2Btn.onClick.AddListener( OnStar2ClickHandle);
            mStar3Btn.onClick.AddListener( OnStar3ClickHandle);
            mStar4Btn.onClick.AddListener( OnStar4ClickHandle);
            mStar5Btn.onClick.AddListener( OnStar5ClickHandle);		    mRateBtn.onClick.AddListener( OnRateBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		    mExitBtn.onClick.RemoveAllListeners();
            mStar1Btn.onClick.RemoveAllListeners();
            mStar2Btn.onClick.RemoveAllListeners();
            mStar3Btn.onClick.RemoveAllListeners();
            mStar4Btn.onClick.RemoveAllListeners();
            mStar5Btn.onClick.RemoveAllListeners();		    mRateBtn.onClick.RemoveAllListeners();
        }
    
    }
}