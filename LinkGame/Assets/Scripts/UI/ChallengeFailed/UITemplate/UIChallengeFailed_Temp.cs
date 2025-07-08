using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIChallengeFailed : BaseUI
    {	protected Text mMoneyText;	protected Button mContinueBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mMoneyText = mTransform.Find("Plane/MoneyText").GetComponent<Text>();		mContinueBtn = mTransform.Find("Plane/ContinueBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mContinueBtn.onClick.AddListener( OnContinueBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mContinueBtn.onClick.RemoveAllListeners();
        }
    
    }
}