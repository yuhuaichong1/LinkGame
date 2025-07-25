using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UILuckMoment : BaseUI
    {	protected Button mExitBtn;	protected LanguageText mBottomText;	protected Button mSpinBtn;	protected RectTransform mAbleBg;	protected RectTransform mDisAbleBg;	protected LuckMomentItem mSMItem1;	protected LuckMomentItem mSMItem2;	protected LuckMomentItem mSMItem3;	protected LuckMomentItem mSMItem4;	protected LuckMomentItem mSMItem5;	protected LuckMomentItem mSMItem6;	protected LuckMomentItem mSMItem7;	protected LuckMomentItem mSMItem8;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/TitleBg/ExitBtn").GetComponent<Button>();		mBottomText = mTransform.Find("Plane/BottomText").GetComponent<LanguageText>();		mSpinBtn = mTransform.Find("Plane/SlotMachine/SpinBtn").GetComponent<Button>();		mAbleBg = mTransform.Find("Plane/SlotMachine/SpinBtn/AbleBg").GetComponent<RectTransform>();		mDisAbleBg = mTransform.Find("Plane/SlotMachine/SpinBtn/DisAbleBg").GetComponent<RectTransform>();		mSMItem1 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem1").GetComponent<LuckMomentItem>();		mSMItem2 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem2").GetComponent<LuckMomentItem>();		mSMItem3 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem3").GetComponent<LuckMomentItem>();		mSMItem4 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem4").GetComponent<LuckMomentItem>();		mSMItem5 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem5").GetComponent<LuckMomentItem>();		mSMItem6 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem6").GetComponent<LuckMomentItem>();		mSMItem7 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem7").GetComponent<LuckMomentItem>();		mSMItem8 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem8").GetComponent<LuckMomentItem>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mSpinBtn.onClick.AddListener( OnSpinBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mSpinBtn.onClick.RemoveAllListeners();
        }
    
    }
}