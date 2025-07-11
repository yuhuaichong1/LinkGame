using System;
using System.Collections.Generic;

namespace XrCode
{
    public static class FacadeTask
    {
        public static Action<int> OnTaskChange;                     //红点相关_当完成的任务数量发生改变时
        public static Action<int> OnDTaskChange;                    //红点相关_当完成的日常任务数量发生改变时
        public static Action<int> OnCTaskChange;                    //红点相关_当完成的挑战任务数量发生改变时


        public static Func<List<Task>> GetDailyTask;                //获取日常任务数据
        public static Func<List<Task>> GetChallageTask;             //获取挑战任务数据
        public static Action<int, int> Receive;                     //任务完成按钮点击后领取奖励
    }
}
