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
        public static Action<int, int> SetReceiveInfo;              //任务完成按钮点击后设置信息
        public static Action ReceiveDataRemove;                     //祝贺界面按钮点击后领取奖励
        public static Action RefreshDailyTask;                      //刷新日常任务数据
        public static Action RefreshChallageTask;                   //刷新挑战任务数据
        public static Action CurMoneyTextShow;                      //任务钱的弹回动画

        public static Action<int> CheckLinkCount;                   //检测消除数红点是否应该添加
        public static Action<int> CheckLevelPass;                   //检测关卡红点是否应该添加（调用该方法，即说明可以添加了）
    }
}
