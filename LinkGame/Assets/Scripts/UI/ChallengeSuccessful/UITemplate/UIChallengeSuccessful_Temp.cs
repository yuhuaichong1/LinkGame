using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIChallengeSuccessful : BaseUI
    {	protected Text mMoneyText;	protected Button mWithdrawBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mMoneyText = mTransform.Find("Plane/MoneyText").GetComponent<Text>();		mWithdrawBtn = mTransform.Find("Plane/WithdrawBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mWithdrawBtn.onClick.AddListener( OnWithdrawBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mWithdrawBtn.onClick.RemoveAllListeners();
        }
    
    }
}