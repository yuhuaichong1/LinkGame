using System;
using System.Diagnostics;

namespace XrCode
{
    //主场景
    public class MainScene : BaseScene
    {
        //protected BaseActor hero;
        /// 主场景进入
        protected override void OnLoad()
        {
            //UIManager.Instance.OpenAsync<UIMainCity>(EUIType.EUIMainCity);
            UIManager.Instance.OpenAsync<UIGamePlay>(EUIType.EUIGamePlay, (BaseUI) =>
            {
                ModuleMgr.Instance.TDAnalyticsManager.EnterMainUI();
                ModuleMgr.Instance.TDAnalyticsManager.LoginSuccess();
                CheckZeroTime();
            });
        }

        //检测是否跨0点
        private void CheckZeroTime()
        {
            DateTime currentUtcTime = DateTime.UtcNow;
            DateTime nextMidnightUtc = currentUtcTime.Date.AddDays(1);
            TimeSpan timeUntilMidnight = nextMidnightUtc - currentUtcTime;
            float secondsUntilMidnight = (float)timeUntilMidnight.TotalSeconds;
            STimerManager.Instance.CreateSDelay(secondsUntilMidnight, () =>
            {
                ModuleMgr.Instance.TDAnalyticsManager.LoginSuccess();
            });
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }
}