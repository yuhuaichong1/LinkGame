using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIGamePlayNotice : BaseUI
    {	protected RectTransform mLevelTarget;	protected RectTransform mReward;	protected RectTransform mPlayerGetRewardNotice;	protected Text mPGRNText;	protected Image mIcon;	protected Text mNameText;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mLevelTarget = mTransform.Find("LevelTarget").GetComponent<RectTransform>();		mReward = mTransform.Find("Reward").GetComponent<RectTransform>();		mPlayerGetRewardNotice = mTransform.Find("PlayerGetRewardNotice").GetComponent<RectTransform>();		mPGRNText = mTransform.Find("PlayerGetRewardNotice/PGRNText").GetComponent<Text>();		mIcon = mTransform.Find("PlayerGetRewardNotice/ContentBg/IconMask/Icon").GetComponent<Image>();		mNameText = mTransform.Find("PlayerGetRewardNotice/ContentBg/NameText").GetComponent<Text>();
        }
    
        protected override void BindButtonEvent() 
        {
            
        }
    
        protected override void UnBindButtonEvent() 
        {
            
        }
    
    }
}