using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIEffect : BaseUI
    {	protected RectTransform mMask;	protected RectTransform mPlayerGetRewardNotice;	protected Text mPGRNText;	protected Image mIcon;	protected Text mNameText;	protected RectTransform mReward;	protected RectTransform mLevelTarget;	protected Text mLevelTargetText;	protected RectTransform mLeftCloud;	protected RectTransform mRightCloud;	protected RectTransform mFlyIconParent;	protected RectTransform mTileMoveDir;	protected Image mTMDIcon;	protected RectTransform mTMDIconRect;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            			mMask = mTransform.Find("Mask").GetComponent<RectTransform>();			mPlayerGetRewardNotice = mTransform.Find("PlayerGetRewardNotice").GetComponent<RectTransform>();			mPGRNText = mTransform.Find("PlayerGetRewardNotice/PGRNText").GetComponent<Text>();			mIcon = mTransform.Find("PlayerGetRewardNotice/ContentBg/IconMask/Icon").GetComponent<Image>();			mNameText = mTransform.Find("PlayerGetRewardNotice/ContentBg/NameText").GetComponent<Text>();			mReward = mTransform.Find("Reward").GetComponent<RectTransform>();			mLevelTarget = mTransform.Find("LevelTarget").GetComponent<RectTransform>();			mLevelTargetText = mTransform.Find("LevelTarget/LevelTargetText").GetComponent<Text>();			mLeftCloud = mTransform.Find("RefushEffect/LeftCloud").GetComponent<RectTransform>();			mRightCloud = mTransform.Find("RefushEffect/RightCloud").GetComponent<RectTransform>();			mFlyIconParent = mTransform.Find("FlyIconParent").GetComponent<RectTransform>();			mTileMoveDir = mTransform.Find("TileMoveDir").GetComponent<RectTransform>();			mTMDIcon = mTransform.Find("TileMoveDir/TMDIcon").GetComponent<Image>();
            mTMDIconRect = mTransform.Find("TileMoveDir/TMDIcon").GetComponent<RectTransform>();
        }
    
        protected override void BindButtonEvent() 
        {
            
        }
    
        protected override void UnBindButtonEvent() 
        {
            
        }
    
    }
}