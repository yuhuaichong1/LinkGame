using cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XrCode
{
    public class TaskModule : BaseModule
    {
        private LanguageModule LanguageModule;

        private Dictionary<int, int> taskStatus;

        private Dictionary<int, Task> DailyTask;
        private Dictionary<int, Task> ChallengeTask;



        protected override void OnLoad()
        {
            base.OnLoad();

            FacadeTask.GetDailyTask += GetDailyTask;
            FacadeTask.GetChallageTask += GetChallageTask;
            FacadeTask.Receive += Receive;

            LanguageModule = ModuleMgr.Instance.LanguageMod;

            DailyTask = new Dictionary<int, Task>();
            ChallengeTask = new Dictionary<int, Task>();
            //taskStatus = new Dictionary<int, int>();
            GetTaskData();
        }

        private void GetTaskData()
        {
            taskStatus = SPlayerPref.GetDictionary<int, int>(PlayerPrefDefines.taskStatus);
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
        /// 获取奖励
        /// </summary>
        /// <param name="taskId">任务Id</param>
        public void Receive(int taskId, int taskType)
        {
            taskStatus.Remove(taskId);

            if (taskType == 0)
            {
                DailyTask.Remove(taskId);
            }
            else
            {
                ChallengeTask.Remove(taskId);
            }
        }

        /// <summary>
        /// 获取有效的日常任务集合
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
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

