using SuperScrollView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
	public partial class UIDailyTasks : BaseUI
	{
		protected Button mExitBtn;
		protected Text mCurMoneyText;
		protected Toggle mDailyTasksToggle;
		protected RectTransform mDTRd;
		protected Toggle mChallengeTaskToggle;
		protected RectTransform mCTRd;
		protected LoopGridView mDailyScroll;
		protected ScrollRect mDailyScrollSR;
		protected LoopGridView mChallengeScroll;
		protected ScrollRect mChallengeScrollSR;
		protected RectTransform mCurMoneyBtn;

		protected override void LoadPanel()
		{
			base.LoadPanel();

			mExitBtn = mTransform.Find("ExitBtn").GetComponent<Button>();
			mCurMoneyText = mTransform.Find("CurMoneyBtn/CurMoneyText").GetComponent<Text>();
			mDailyTasksToggle = mTransform.Find("Content/ToggleGroup/DailyTasksToggle").GetComponent<Toggle>();
			mDTRd = mTransform.Find("Content/ToggleGroup/DailyTasksToggle/DTRd").GetComponent<RectTransform>();
			mChallengeTaskToggle = mTransform.Find("Content/ToggleGroup/ChallengeTaskToggle").GetComponent<Toggle>();
			mCTRd = mTransform.Find("Content/ToggleGroup/ChallengeTaskToggle/CTRd").GetComponent<RectTransform>();
			mDailyScroll = mTransform.Find("Content/Tasks/DailyScroll").GetComponent<LoopGridView>();
			mDailyScrollSR = mTransform.Find("Content/Tasks/DailyScroll").GetComponent<ScrollRect>();
			mChallengeScroll = mTransform.Find("Content/Tasks/ChallengeScroll").GetComponent<LoopGridView>();
			mChallengeScrollSR = mTransform.Find("Content/Tasks/ChallengeScroll").GetComponent<ScrollRect>();
			mCurMoneyBtn = mTransform.Find("CurMoneyBtn").GetComponent<RectTransform>();
		}

		protected override void BindButtonEvent()
		{

			mExitBtn.onClick.AddListener(OnExitBtnClickHandle);
			mDailyTasksToggle.onValueChanged.AddListener(OnDailyTasksTogChangeHandle);
			mChallengeTaskToggle.onValueChanged.AddListener(OnChallengeTasksTogChangeHandle);
		}

		protected override void UnBindButtonEvent()
		{

			mExitBtn.onClick.RemoveAllListeners();
		}

	}
}