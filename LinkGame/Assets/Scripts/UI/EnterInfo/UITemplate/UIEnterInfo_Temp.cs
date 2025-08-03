using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIEnterInfo : BaseUI
    {	protected Button mExitBtn;	protected Button mHelpBtn;	protected Toggle mPayType1Toggle;	protected Image mPY1Icon;	protected Toggle mPayType2Toggle;	protected Image mPY2Icon;	protected Toggle mPayType3Toggle;	protected Image mPY3Icon;	protected RectTransform mTick;	protected InputField mNameInput;	protected Text mNamePlaceholder;	protected InputField mAddressInput;	protected Text mAddressPlaceholder;	protected InputField mAddressOrPhoneInput;	protected Text mAddressOrPhonePlaceholder;	protected RectTransform mPhone;	protected Text mPhoneAreaCodeText;	protected InputField mPhoneInput;	protected Text mPhonePlaceholder;	protected Button mConfirmBtn;	protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();		mHelpBtn = mTransform.Find("Plane/ContentBg/Options/HelpBtn").GetComponent<Button>();		mPayType1Toggle = mTransform.Find("Plane/ContentBg/Options/Options/Options/PayType1Toggle").GetComponent<Toggle>();		mPY1Icon = mTransform.Find("Plane/ContentBg/Options/Options/Options/PayType1Toggle/Mask/PY1Icon").GetComponent<Image>();		mPayType2Toggle = mTransform.Find("Plane/ContentBg/Options/Options/Options/PayType2Toggle").GetComponent<Toggle>();		mPY2Icon = mTransform.Find("Plane/ContentBg/Options/Options/Options/PayType2Toggle/Mask/PY2Icon").GetComponent<Image>();		mPayType3Toggle = mTransform.Find("Plane/ContentBg/Options/Options/Options/PayType3Toggle").GetComponent<Toggle>();		mPY3Icon = mTransform.Find("Plane/ContentBg/Options/Options/Options/PayType3Toggle/Mask/PY3Icon").GetComponent<Image>();		mTick = mTransform.Find("Plane/ContentBg/Options/Options/Tick").GetComponent<RectTransform>();		mNameInput = mTransform.Find("Plane/ContentBg/InputArea/NameInput").GetComponent<InputField>();		mNamePlaceholder = mTransform.Find("Plane/ContentBg/InputArea/NameInput/NamePlaceholder").GetComponent<Text>();		mAddressInput = mTransform.Find("Plane/ContentBg/InputArea/AddressInput").GetComponent<InputField>();		mAddressPlaceholder = mTransform.Find("Plane/ContentBg/InputArea/AddressInput/AddressPlaceholder").GetComponent<Text>();		mAddressOrPhoneInput = mTransform.Find("Plane/ContentBg/InputArea/AddressOrPhoneInput").GetComponent<InputField>();		mAddressOrPhonePlaceholder = mTransform.Find("Plane/ContentBg/InputArea/AddressOrPhoneInput/AddressOrPhonePlaceholder").GetComponent<Text>();		mPhone = mTransform.Find("Plane/ContentBg/InputArea/Phone").GetComponent<RectTransform>();		mPhoneAreaCodeText = mTransform.Find("Plane/ContentBg/InputArea/Phone/PhoneAreaCode/PhoneAreaCodeText").GetComponent<Text>();		mPhoneInput = mTransform.Find("Plane/ContentBg/InputArea/Phone/PhoneInput").GetComponent<InputField>();		mPhonePlaceholder = mTransform.Find("Plane/ContentBg/InputArea/Phone/PhoneInput/PhonePlaceholder").GetComponent<Text>();		mConfirmBtn = mTransform.Find("Plane/ConfirmBtn").GetComponent<Button>();		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mHelpBtn.onClick.AddListener( OnHelpBtnClickHandle);		mConfirmBtn.onClick.AddListener( OnConfirmBtnClickHandle);
			mPayType1Toggle.onValueChanged.AddListener(OnPayType1TogChangeHandle);
            mPayType2Toggle.onValueChanged.AddListener(OnPayType2TogChangeHandle);
            mPayType3Toggle.onValueChanged.AddListener(OnPayType3TogChangeHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mHelpBtn.onClick.RemoveAllListeners();		mConfirmBtn.onClick.RemoveAllListeners();
        }
    
    }
}