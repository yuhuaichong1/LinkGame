using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawableMultiple : BaseUI
    {	protected Button mExitBtn;	protected Text mLevelTitle;	protected Text mGoalTitle;	protected Text mCurWMoney;	protected Button mWithdrawBtn;	protected Text mPreLevelTitle;	protected Text mPregoalTitle;	protected Text mPrePreLevelTitle;	protected Text mPrePregoalTitle;	protected Text mDNTitle;	protected Text mPNpeopleCount;	protected Text mACNpeopleCount;	protected Text mAWNpeopleCount;	protected AutoCarousel mNoticeScrollView;
        protected override void LoadPanel()
        {
            base.LoadPanel();
            		mExitBtn = mTransform.Find("Planes/Plane/ExitBtn").GetComponent<Button>();		mLevelTitle = mTransform.Find("Planes/Plane/TitleBg/LevelTitle").GetComponent<Text>();		mGoalTitle = mTransform.Find("Planes/Plane/TitleBg/TitleBg2/goalTitle").GetComponent<Text>();		mCurWMoney = mTransform.Find("Planes/Plane/CenterBg/curWMoney").GetComponent<Text>();		mWithdrawBtn = mTransform.Find("Planes/Plane/WithdrawBtn").GetComponent<Button>();		mPreLevelTitle = mTransform.Find("Planes/PreLevelPlane/TitleBg/PreLevelTitle").GetComponent<Text>();		mPregoalTitle = mTransform.Find("Planes/PreLevelPlane/TitleBg/TitleBg2/PregoalTitle").GetComponent<Text>();		mPrePreLevelTitle = mTransform.Find("Planes/PrePreLevelPlane/TitleBg/PrePreLevelTitle").GetComponent<Text>();		mPrePregoalTitle = mTransform.Find("Planes/PrePreLevelPlane/TitleBg/TitleBg2/PrePregoalTitle").GetComponent<Text>();		mDNTitle = mTransform.Find("DayNotice/DNTitle").GetComponent<Text>();		mPNpeopleCount = mTransform.Find("DayNotice/DayInfo/peopleNum/PNpeopleCount").GetComponent<Text>();		mACNpeopleCount = mTransform.Find("DayNotice/DayInfo/averageCNum/ACNpeopleCount").GetComponent<Text>();		mAWNpeopleCount = mTransform.Find("DayNotice/DayInfo/averageWNum/AWNpeopleCount").GetComponent<Text>();		mNoticeScrollView = mTransform.Find("DayNotice/NoticeScrollView").GetComponent<AutoCarousel>();
        }
    
        protected override void BindButtonEvent() 
        {
            		mExitBtn.onClick.AddListener( OnExitBtnClickHandle);		mWithdrawBtn.onClick.AddListener( OnWithdrawBtnClickHandle);
        }
    
        protected override void UnBindButtonEvent() 
        {
            		mExitBtn.onClick.RemoveAllListeners();		mWithdrawBtn.onClick.RemoveAllListeners();
        }
    
    }
}