using cfg;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace XrCode
{
    public class TaskModule : BaseModule
    {
        private LanguageModule LanguageModule;

        private Dictionary<int, int> taskStatus;

        private Dictionary<int, Task> DailyTask;
        private Dictionary<int, Task> ChallengeTask;

        private int re_taskId;
        private int re_taskType;

        private int curDailyRDId;
        private int curChallengeRDId;

        protected override void OnLoad()
        {
            base.OnLoad();

            FacadeTask.GetDailyTask += GetDailyTask;
            FacadeTask.GetChallageTask += GetChallageTask;
            FacadeTask.SetReceiveInfo += SetReceiveInfo;
            FacadeTask.ReceiveDataRemove += ReceiveDataRemove;
            FacadeTask.CheckLinkCount += CheckLinkCount;
            FacadeTask.CheckLevelPass += CheckLevelPass;

            LanguageModule = ModuleMgr.Instance.LanguageMod;

            DailyTask = new Dictionary<int, Task>();
            ChallengeTask = new Dictionary<int, Task>();
            GetTaskData();
        }

        private void GetTaskData()
        {
            taskStatus = SPlayerPrefs.GetDictionary<int, int>(PlayerPrefDefines.taskStatus);
            if (taskStatus != null)
            {
                foreach (KeyValuePair<int, int> status in taskStatus)
                {
                    ConfTask temp = ConfigModule.Instance.Tables.TBTask.Get(status.Key);
                    Task task = new Task()
                    {
                        Id = temp.Sn,
                        Type = temp.Type,
                        Content = string.Format(LanguageModule.GetText(temp.Content), temp.Target),
                        Target = temp.Target,
                        Reward = temp.Reward,
                        taskStatus = (ETaskStatus)status.Value,
                    };

                    if (task.Type == 0)
                    {
                        DailyTask.Add(temp.Sn, task);
                        if (task.taskStatus == ETaskStatus.Receive)
                            FacadeRedDot.AddRDNode_ByName(GameDefines.Reddot_Name_Daily, 1);
                    }
                    else
                    {
                        ChallengeTask.Add(temp.Sn, task);
                        if (task.taskStatus == ETaskStatus.Receive)
                            FacadeRedDot.AddRDNode_ByName(GameDefines.Reddot_Name_Challenge, 1);
                    }

                    curDailyRDId = SPlayerPrefs.GetInt(PlayerPrefDefines.curDailyRDId);
                    curChallengeRDId = SPlayerPrefs.GetInt(PlayerPrefDefines.curChallengeRDId);
                }
            }
            else
            {
                bool firstDSn = true;
                bool firstCSn = true;

                taskStatus = new Dictionary<int, int>();
                List<ConfTask> tasks = ConfigModule.Instance.Tables.TBTask.DataList;
                foreach (ConfTask task in tasks)
                {
                    taskStatus.Add(task.Sn, 0);
                    Task item = new Task()
                    {
                        Id = task.Sn,
                        Type = task.Type,
                        Content = LanguageModule.GetText(string.Format(task.Content, task.Target)),
                        Target = task.Target,
                        Reward = task.Reward,
                        taskStatus = ETaskStatus.Progress,
                    };
                    if (task.Type == 0)
                    {
                        DailyTask.Add(task.Sn, item);
                        if (firstDSn)
                        {
                            firstDSn = false;
                            curDailyRDId = task.Sn;
                        }
                    }
                    else
                    {
                        ChallengeTask.Add(task.Sn, item);
                        if (firstCSn)
                        {
                            firstCSn = false;
                            curChallengeRDId = task.Sn;
                        }
                    }
                }

                SPlayerPrefs.SetDictionary<int, int>(PlayerPrefDefines.taskStatus, taskStatus);
                SPlayerPrefs.SetInt(PlayerPrefDefines.curDailyRDId, curDailyRDId);
                SPlayerPrefs.SetInt(PlayerPrefDefines.curChallengeRDId, curChallengeRDId);
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }

        /// <summary>
        /// 设置奖励信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskType"></param>
        public void SetReceiveInfo(int taskId, int taskType)
        {
            re_taskId = taskId;
            re_taskType = taskType;
        }

        /// <summary>
        /// 获取奖励后移除
        /// </summary>
        public void ReceiveDataRemove()
        {
            taskStatus.Remove(re_taskId);
            SPlayerPrefs.SetDictionary(PlayerPrefDefines.taskStatus, taskStatus);

            if (re_taskType == 0)
            {
                DailyTask.Remove(re_taskId);
                FacadeRedDot.ReduceRDNode_ByName(GameDefines.Reddot_Name_DailyBy, 1);
                FacadeRedDot.ReduceRDNode_ByName(GameDefines.Reddot_Name_Daily, 1);
                FacadeTask.RefreshDailyTask();
            }
            else
            {
                ChallengeTask.Remove(re_taskId);
                FacadeRedDot.ReduceRDNode_ByName(GameDefines.Reddot_Name_ChallengeBy, 1);
                FacadeRedDot.ReduceRDNode_ByName(GameDefines.Reddot_Name_Challenge, 1);
                FacadeTask.RefreshChallageTask();
            }
        }

        /// <summary>
        /// 获取有效的日常任务集合
        /// </summary>
        /// <returns>有效的日常任务集合</returns>
        public List<Task> GetDailyTask()
        {
            List<Task> tasks = new List<Task>();
            foreach (Task task in DailyTask.Values)
            {
                tasks.Add(task);
            }
            return tasks;
        }

        /// <summary>
        /// 获取有效的挑战任务集合
        /// </summary>
        /// <returns>有效的挑战任务集合</returns>
        public List<Task> GetChallageTask()
        {
            List<Task> tasks = new List<Task>();
            foreach (Task task in ChallengeTask.Values)
            {
                tasks.Add(task);
            }
            return tasks;
        }

        #region 红点增值相关
        public void SetDailyTaskRecive(int id)
        {
            taskStatus[id] = 1;
            ConfTask temp = ConfigModule.Instance.Tables.TBTask.Get(id);
            switch (temp.Type)
            {
                case 0:
                    FacadeRedDot.AddRDNode_ByName(GameDefines.Reddot_Name_Daily, 1);
                    FacadeRedDot.AddRDNode_ByName(GameDefines.Reddot_Name_DailyBy, 1);
                    break;
                case 1:
                    FacadeRedDot.AddRDNode_ByName(GameDefines.Reddot_Name_Challenge, 1);
                    FacadeRedDot.AddRDNode_ByName(GameDefines.Reddot_Name_ChallengeBy, 1);
                    break;
            }
            SPlayerPrefs.SetDictionary(PlayerPrefDefines.taskStatus, taskStatus);
        }

        private void CheckLinkCount(int count)
        {
            int target = ConfigModule.Instance.Tables.TBTask.Get(curDailyRDId).Target;
            if (count >= target)
            {
                SetDailyTaskRecive(curDailyRDId);
                curDailyRDId += 1;
                SPlayerPrefs.SetInt(PlayerPrefDefines.curDailyRDId, curDailyRDId);
            }
        }

        private void CheckLevelPass(int level)
        {
            int target = ConfigModule.Instance.Tables.TBTask.Get(curChallengeRDId).Target;
            if (level >= target)
            {
                SetDailyTaskRecive(curChallengeRDId);
                curChallengeRDId += 1;
                SPlayerPrefs.SetInt(PlayerPrefDefines.curChallengeRDId, curChallengeRDId);
            }
        }
        #endregion
    }
}

