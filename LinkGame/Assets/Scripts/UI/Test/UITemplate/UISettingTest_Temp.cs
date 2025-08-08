using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UISettingTest : BaseUI
    {
        protected Button mExitBtn;
        protected Image mIcon;
        protected LanguageText mIDText;
        protected LanguageText mUserLvText;
        protected Button mUserLvDetailsBtn;
        protected RectTransform mPlane;

        protected override void LoadPanel()
        {
            base.LoadPanel();

            mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();
            mIcon = mTransform.Find("Plane/InsideBg/IconMask/Icon").GetComponent<Image>();
            mIDText = mTransform.Find("Plane/InsideBg/IDText").GetComponent<LanguageText>();
            mUserLvText = mTransform.Find("Plane/InsideBg/UserLvText").GetComponent<LanguageText>();
            mUserLvDetailsBtn = mTransform.Find("Plane/InsideBg/UserLvDetailsBtn").GetComponent<Button>();
            mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();
        }

        protected override void BindButtonEvent()
        {

            mExitBtn.onClick.AddListener(OnExitBtnClickHandle);
            mUserLvDetailsBtn.onClick.AddListener(OnUserLvDetailsBtnClickHandle);
        }

        protected override void UnBindButtonEvent()
        {

            mExitBtn.onClick.RemoveAllListeners();
            mUserLvDetailsBtn.onClick.RemoveAllListeners();
        }

    }
}