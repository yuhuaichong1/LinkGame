using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIRate : BaseUI
    {	protected Button mExitBtn;	protected Button mStar1;	protected RectTransform mStar1Light;	protected Button mStar2;	protected RectTransform mStar2Light;	protected Button mStar3;	protected RectTransform mStar3Light;	protected Button mStar4;	protected RectTransform mStar4Light;	protected Button mStar5;	protected RectTransform mStar5Light;	protected Button mRateBtn;	protected RectTransform mGreyRateBtn;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mStar1 = mTransform.Find("Plane/CenterBg/Stars/Star1").GetComponent<Button>();		mStar1Light = mTransform.Find("Plane/CenterBg/Stars/Star1/Star1Light").GetComponent<RectTransform>();		mStar2 = mTransform.Find("Plane/CenterBg/Stars/Star2").GetComponent<Button>();		mStar2Light = mTransform.Find("Plane/CenterBg/Stars/Star2/Star2Light").GetComponent<RectTransform>();		mStar3 = mTransform.Find("Plane/CenterBg/Stars/Star3").GetComponent<Button>();		mStar3Light = mTransform.Find("Plane/CenterBg/Stars/Star3/Star3Light").GetComponent<RectTransform>();		mStar4 = mTransform.Find("Plane/CenterBg/Stars/Star4").GetComponent<Button>();		mStar4Light = mTransform.Find("Plane/CenterBg/Stars/Star4/Star4Light").GetComponent<RectTransform>();		mStar5 = mTransform.Find("Plane/CenterBg/Stars/Star5").GetComponent<Button>();		mStar5Light = mTransform.Find("Plane/CenterBg/Stars/Star5/Star5Light").GetComponent<RectTransform>();		mRateBtn = mTransform.Find("Plane/RateBtn").GetComponent<Button>();		mGreyRateBtn = mTransform.Find("Plane/GreyRateBtn").GetComponent<RectTransform>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
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