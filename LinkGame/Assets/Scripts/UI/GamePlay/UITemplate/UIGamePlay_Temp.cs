using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIGamePlay : BaseUI
    {	protected Button mSettingBtn;	protected Text mCurLevelText;	protected Slider mSlider;	protected RectTransform mMap;	protected Button mTipBtn;	protected Text mTipText;	protected Text mTipCountText;	protected Button mRefushBtn;	protected Text mRefushText;	protected Text mRefushCountText;	protected Button mRemoveBtn;	protected Text mRemoveText;	protected Text mRemoveCountText;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mSettingBtn = mTransform.Find("Top/SettingBtn").GetComponent<Button>();		mCurLevelText = mTransform.Find("Top/CurLevelBg/CurLevelText").GetComponent<Text>();		mSlider = mTransform.Find("Top/LevelProgess/Slider").GetComponent<Slider>();		mMap = mTransform.Find("Center/Map").GetComponent<RectTransform>();		mTipBtn = mTransform.Find("Bottom/Funcs/TipBtn").GetComponent<Button>();		mTipText = mTransform.Find("Bottom/Funcs/TipBtn/TipText").GetComponent<Text>();		mTipCountText = mTransform.Find("Bottom/Funcs/TipBtn/Count/TipCountText").GetComponent<Text>();		mRefushBtn = mTransform.Find("Bottom/Funcs/RefushBtn").GetComponent<Button>();		mRefushText = mTransform.Find("Bottom/Funcs/RefushBtn/RefushText").GetComponent<Text>();		mRefushCountText = mTransform.Find("Bottom/Funcs/RefushBtn/Count/RefushCountText").GetComponent<Text>();		mRemoveBtn = mTransform.Find("Bottom/Funcs/RemoveBtn").GetComponent<Button>();		mRemoveText = mTransform.Find("Bottom/Funcs/RemoveBtn/RemoveText").GetComponent<Text>();		mRemoveCountText = mTransform.Find("Bottom/Funcs/RemoveBtn/Count/RemoveCountText").GetComponent<Text>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mSettingBtn.onClick.AddListener( OnSettingBtnClickHandle);		mTipBtn.onClick.AddListener( OnTipBtnClickHandle);		mRefushBtn.onClick.AddListener( OnRefushBtnClickHandle);		mRemoveBtn.onClick.AddListener( OnRemoveBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mSettingBtn.onClick.RemoveAllListeners();		mTipBtn.onClick.RemoveAllListeners();		mRefushBtn.onClick.RemoveAllListeners();		mRemoveBtn.onClick.RemoveAllListeners();
        }
    
    }
}