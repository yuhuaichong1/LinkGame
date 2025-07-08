using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIUserLevel : BaseUI
    {	protected Button mExitBtn;	protected Text mCULText;	protected Slider mCULSlider;	protected Text mCULSText;	protected Button mContinueBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mCULText = mTransform.Find("Plane/ContentBg/CULText").GetComponent<Text>();		mCULSlider = mTransform.Find("Plane/ContentBg/CULSlider").GetComponent<Slider>();		mCULSText = mTransform.Find("Plane/ContentBg/CULSlider/CULSText").GetComponent<Text>();		mContinueBtn = mTransform.Find("Plane/ContinueBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mContinueBtn.onClick.AddListener( OnContinueBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mContinueBtn.onClick.RemoveAllListeners();
        }
    
    }
}