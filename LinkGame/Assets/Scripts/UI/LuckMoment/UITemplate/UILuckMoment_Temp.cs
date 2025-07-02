using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UILuckMoment : BaseUI
    {	protected Button mExitBtn;	protected LanguageText mBottomText;	protected Button mSpinBtn;	protected Image mSMItem1;	protected Image mSMItem2;	protected Image mSMItem3;	protected Image mSMItem4;	protected Image mSMItem5;	protected Image mSMItem6;	protected Image mSMItem7;	protected Image mSMItem8;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/TitleBg/ExitBtn").GetComponent<Button>();		mBottomText = mTransform.Find("Plane/BottomText").GetComponent<LanguageText>();		mSpinBtn = mTransform.Find("Plane/SlotMachine/SpinBtn").GetComponent<Button>();		mSMItem1 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem1").GetComponent<Image>();		mSMItem2 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem2").GetComponent<Image>();		mSMItem3 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem3").GetComponent<Image>();		mSMItem4 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem4").GetComponent<Image>();		mSMItem5 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem5").GetComponent<Image>();		mSMItem6 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem6").GetComponent<Image>();		mSMItem7 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem7").GetComponent<Image>();		mSMItem8 = mTransform.Find("Plane/SlotMachine/SMItems/SMItem8").GetComponent<Image>();
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