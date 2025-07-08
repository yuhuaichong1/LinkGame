using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIAwesome : BaseUI
    {	protected Text mMoneyText;	protected Button mClaimBtn;	protected Text mOnlyMoney;	protected Button mOnlyMoneyBtn;	protected RectTransform mCSFText;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mMoneyText = mTransform.Find("Plane/MoneyImage/moneyText").GetComponent<Text>();		mClaimBtn = mTransform.Find("Plane/ClaimBtn").GetComponent<Button>();		mOnlyMoney = mTransform.Find("Plane/OnlyMoney").GetComponent<Text>();		mOnlyMoneyBtn = mTransform.Find("Plane/OnlyMoney/OnlyMoneyBtn").GetComponent<Button>();		mCSFText = mTransform.Find("Plane/ClaimBtn/Parent/CSFText").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mClaimBtn.onClick.AddListener( OnClaimBtnClickHandle);		mOnlyMoneyBtn.onClick.AddListener( OnOnlyMoneyBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mClaimBtn.onClick.RemoveAllListeners();		mOnlyMoneyBtn.onClick.RemoveAllListeners();
        }
    
    }
}