using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawalChannel : BaseUI
    {	protected Button mExitBtn;	protected InputField mAddressOrPhoneInput;	protected Text mAddressOrPhonePlaceholder;	protected Button mConfirmBtn;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/Plane/ExitBtn").GetComponent<Button>();		mAddressOrPhoneInput = mTransform.Find("Plane/Plane/AddressOrPhoneInput").GetComponent<InputField>();		mAddressOrPhonePlaceholder = mTransform.Find("Plane/Plane/AddressOrPhoneInput/AddressOrPhonePlaceholder").GetComponent<Text>();		mConfirmBtn = mTransform.Find("Plane/Plane/ConfirmBtn").GetComponent<Button>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
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