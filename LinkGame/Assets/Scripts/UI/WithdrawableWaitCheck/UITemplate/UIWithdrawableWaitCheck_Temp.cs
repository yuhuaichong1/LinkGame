using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawableWaitCheck : BaseUI
    {	protected RectTransform mPlane;	protected Button mExitBtn;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mPlane = mTransform.Find("Plane").GetComponent<RectTransform>();		mExitBtn = mTransform.Find("Plane/ExitBtn").GetComponent<Button>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();
        }
    
    }
}