using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIPopup : BaseUI
    {
        protected Text mLabTips;
        protected RectTransform mSelectGroup;
        protected Button mBtnOK;
        protected Button mBtnCancel;
        protected override void LoadPanel()
        {
            base.LoadPanel();

            mLabTips = mTransform.Find("ImgBox/LabTips").GetComponent<Text>();
            mSelectGroup = mTransform.Find("ImgBox/SelectGroup").GetComponent<RectTransform>();
            mBtnOK = mTransform.Find("ImgBox/SelectGroup/BtnOK").GetComponent<Button>();
            mBtnCancel = mTransform.Find("ImgBox/SelectGroup/BtnCancel").GetComponent<Button>();
        }

        protected override void BindButtonEvent()
        {

            mBtnOK.onClick.AddListener(OnBtnOKClickHandle);
            mBtnCancel.onClick.AddListener(OnBtnCancelClickHandle);
        }

        protected override void UnBindButtonEvent()
        {

            mBtnOK.onClick.RemoveAllListeners();
            mBtnCancel.onClick.RemoveAllListeners();
        }

    }
}