using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UICongratfulations : BaseUI
    {
        protected Button mExitBtn;
        protected RectTransform mCSFText;
        protected Button mClaimBtn;
        protected Image mMoney;
        protected Text mMoneyText;
        protected Text mOnlyMoney;
        protected Button mOnlyMoneyBtn;
        protected RectTransform mPlane;
        protected override void LoadPanel()
        {
            base.LoadPanel();

            mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();
            mCSFText = mTransform.Find("Plane/ClaimBtn/Parent/CSFText").GetComponent<RectTransform>();
            mClaimBtn = mTransform.Find("Plane/ClaimBtn").GetComponent<Button>();
            mMoney = mTransform.Find("Plane/MoneyImage/Money").GetComponent<Image>();
            mMoneyText = mTransform.Find("Plane/MoneyImage/moneyText").GetComponent<Text>();
            mOnlyMoney = mTransform.Find("Plane/OnlyMoney").GetComponent<Text>();
            mOnlyMoneyBtn = mTransform.Find("Plane/OnlyMoney/OnlyMoneyBtn").GetComponent<Button>();
            mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }

        protected override void BindButtonEvent()
        {

            mExitBtn.onClick.AddListener(OnExitBtnClickHandle);
            mClaimBtn.onClick.AddListener(OnClaimBtnClickHandle);
            mOnlyMoneyBtn.onClick.AddListener(OnOnlyMoneyBtnClickHandle);
        }

        protected override void UnBindButtonEvent()
        {

            mExitBtn.onClick.RemoveAllListeners();
            mClaimBtn.onClick.RemoveAllListeners();
            mOnlyMoneyBtn.onClick.RemoveAllListeners();
        }

    }
}