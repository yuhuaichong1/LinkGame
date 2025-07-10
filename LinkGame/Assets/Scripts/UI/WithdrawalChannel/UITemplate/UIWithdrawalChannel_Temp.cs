using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawalChannel : BaseUI
    {	protected Button mExitBtn;	protected InputField mAddressOrPhoneInput;	protected Text mAddressOrPhonePlaceholder;	protected Button mConfirmBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mAddressOrPhoneInput = mTransform.Find("Plane/ContentBg/AddressOrPhoneInput").GetComponent<InputField>();		mAddressOrPhonePlaceholder = mTransform.Find("Plane/ContentBg/AddressOrPhoneInput/AddressOrPhonePlaceholder").GetComponent<Text>();		mConfirmBtn = mTransform.Find("Plane/ConfirmBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mConfirmBtn.onClick.AddListener( OnConfirmBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mConfirmBtn.onClick.RemoveAllListeners();
        }
    
    }
}