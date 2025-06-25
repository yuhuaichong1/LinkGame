using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIGuide : BaseUI
    {	protected SHoleMask mHoleMask;	protected RectTransform mHander;	protected RectTransform mGuideTextBg;	protected Text mGuideText;	protected RectTransform mHole;	protected Button mGuideBtn;	protected RectTransform mObjTrans;	protected RectTransform mPos;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mHoleMask = mTransform.Find("HoleMask").GetComponent<SHoleMask>();		mHander = mTransform.Find("Hander").GetComponent<RectTransform>();		mGuideTextBg = mTransform.Find("GuideTextBg").GetComponent<RectTransform>();		mGuideText = mTransform.Find("GuideTextBg/GuideText").GetComponent<Text>();		mHole = mTransform.Find("HoleMask/HoleMask/Hole").GetComponent<RectTransform>();		mGuideBtn = mTransform.Find("GuideBtn").GetComponent<Button>();		mObjTrans = mTransform.Find("ObjTrans").GetComponent<RectTransform>();		mPos = mTransform.Find("Pos").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mGuideBtn.onClick.AddListener( OnGuideBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mGuideBtn.onClick.RemoveAllListeners();
        }
    
    }
}