using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIChallengeSuccessful : BaseUI
    {
        protected Text mMoneyText;
        protected Button mWithdrawBtn;
        protected Button mClaimBtn;
        protected Text mOnlyMoney;
        protected Text WithdrawText;
        protected Button mOnlyMoneyBtn;
        protected Image mMoney;
        protected RectTransform mParent;
        protected ParticleSystem mParticle;
        protected override void LoadPanel()
        {
            base.LoadPanel();

            mMoneyText = mTransform.Find("Plane/MoneyText").GetComponent<Text>();
            mMoney = mTransform.Find("Plane/Image/Money").GetComponent<Image>();
            mWithdrawBtn = mTransform.Find("Plane/WithdrawBtn").GetComponent<Button>();
            WithdrawText = mTransform.Find("Plane/WithdrawBtn/Text").GetComponent<Text>();
            mClaimBtn = mTransform.Find("Plane/ClaimBtn").GetComponent<Button>();
            mOnlyMoney = mTransform.Find("Plane/OnlyMoney").GetComponent<Text>();
            mOnlyMoneyBtn = mTransform.Find("Plane/OnlyMoney/OnlyMoneyBtn").GetComponent<Button>();
            mParent = mTransform.Find("Plane/ClaimBtn/Parent").GetComponent<RectTransform>();
            mParticle = mTransform.Find("Particle").GetComponent<ParticleSystem>();
        }

        protected override void BindButtonEvent()
        {

            mWithdrawBtn.onClick.AddListener(OnWithdrawBtnClickHandle);
            mClaimBtn.onClick.AddListener(OnClaimBtnClickHandle);
            mOnlyMoneyBtn.onClick.AddListener(OnOnlyMoneyBtnClickHandle);
        }

        protected override void UnBindButtonEvent()
        {

            mWithdrawBtn.onClick.RemoveAllListeners();
            mClaimBtn.onClick.RemoveAllListeners();
            mOnlyMoneyBtn.onClick.RemoveAllListeners();
        }

    }
}