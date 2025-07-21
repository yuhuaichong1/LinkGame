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

        protected override void OnLoad()
        {
            base.OnLoad();

            FacadeTask.GetDailyTask += GetDailyTask;
            FacadeTask.GetChallageTask += GetChallageTask;
            FacadeTask.SetReceiveInfo += SetReceiveInfo;
            FacadeTask.ReceiveDataRemove += ReceiveDataRemove;

            LanguageModule = ModuleMgr.Instance.LanguageMod;

            DailyTask = new Dictionary<int, Task>();
            ChallengeTask = new Dictionary<int, Task>();
            //taskStatus = new Dictionary<int, int>();
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
                        Content = LanguageModule.GetText(string.Format(temp.Content, temp.Target)),
                        Target = temp.Target,
                        Reward = temp.Reward,
                        taskStatus = (ETaskStatus)status.Value,
                    };
                    if (task.Type == 0)
                        DailyTask.Add(temp.Sn, task);
                    else
                        ChallengeTask.Add(temp.Sn, task);
                }
            }
            else
            {
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
                        DailyTask.Add(task.Sn, item);
                    else
                        ChallengeTask.Add(task.Sn, item);
                }
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
                FacadeTask.RefreshDailyTask();
            }
            else
            {
                ChallengeTask.Remove(re_taskId);
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
    }
}

