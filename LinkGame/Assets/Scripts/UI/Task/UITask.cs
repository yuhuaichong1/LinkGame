using DG.Tweening;
using SuperScrollView;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public partial class UITask : BaseUI
    {
        private List<Task> DailyTasks;
        private List<Task> ChallageTasks;

        protected override void OnAwake()
        {
            FacadeTask.RefreshDailyTask += RefreshDailyTask;
            FacadeTask.RefreshChallageTask += RefreshChallageTask;
            FacadeTask.CurMoneyTextShow += CurMoneyTextShow;

            GetTaskInfo();
        }

        protected override void OnEnable()        {
            mCurMoneyText.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());

            mDailyTasksToggle.isOn = true;            mDailyScroll.gameObject.SetActive(true);            mChallengeTaskToggle.isOn = false;            mChallengeScroll.gameObject.SetActive(false);        }        private void GetTaskInfo()
        {
            DailyTasks = FacadeTask.GetDailyTask();
            ChallageTasks = FacadeTask.GetChallageTask();

            mDailyScroll.InitGridView(DailyTasks.Count, DailyTaskCallBack);
            mChallengeScroll.InitGridView(ChallageTasks.Count, ChallageTaskCallBack);
        }	    private void OnExitBtnClickHandle()
        {
            UIManager.Instance.CloseUI(EUIType.EUITask);
        }

        private void OnDailyTasksTogChangeHandle(bool b)
        {
            mDailyScroll.gameObject.SetActive(b);
            mChallengeScroll.gameObject.SetActive(!b);
            mDailyScrollSR.verticalNormalizedPosition = 1f;
        }

        private void OnChallengeTasksTogChangeHandle(bool b)
        {
            mChallengeScroll.gameObject.SetActive(b);
            mDailyScroll.gameObject.SetActive(!b);
            mChallengeScrollSR.verticalNormalizedPosition = 1f;
        }


        private LoopGridViewItem DailyTaskCallBack(LoopGridView cell, int index, int row, int column)
        {
            LoopGridViewItem item = mDailyScroll.NewListViewItem("DailyTaskItem");
            Task task = DailyTasks[index];
            TaskItemData dataMono = item.GetComponent<TaskItemData>();
            dataMono.SetProgress(GamePlayFacade.GetCurTotalLinkCount(), task.Target);
            dataMono.SetMsg(task.Content, task.Id, task.Type);
            dataMono.ReceiveBtn.onClick.AddListener(() => 
            {
                FacadeTask.GetDailyTask().Remove(task);
            });

            return item;
        }

        private LoopGridViewItem ChallageTaskCallBack(LoopGridView cell, int index, int row, int column)
        {
            LoopGridViewItem item = mChallengeScroll.NewListViewItem("ChallengeTaskItem");
            Task task = ChallageTasks[index];
            TaskItemData dataMono = item.GetComponent<TaskItemData>();
            dataMono.SetProgress(GamePlayFacade.GetCurLevel() - 1, task.Target);
            dataMono.SetMsg(task.Content, task.Id, task.Type);

            return item;
        }

        private void RefreshDailyTask()
        {
            DailyTasks = FacadeTask.GetDailyTask();   
            mDailyScroll.RefreshAllShownItem();
        }

        private void RefreshChallageTask()
        {
            ChallageTasks = FacadeTask.GetChallageTask();
            mChallengeScroll.RefreshAllShownItem();
        }

        //任务钱的弹回动画
        private void CurMoneyTextShow()
        {
            mCurMoneyText.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());
            mCurMoneyBtn.transform.DOScale(1.25f, GameDefines.FlyMoney_ObjTime / 2).SetLoops(GameDefines.FlyMoney_Effect_RewardCount * 2, LoopType.Yoyo);
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}