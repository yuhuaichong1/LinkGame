using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace XrCode
{
    public class TimeZoneModule : BaseModule
    {
        private float lastZTime;//上次上线日的凌晨时间（目前用于一天一次的评论）
        //private int lastMinute;

        protected override void OnLoad()
        {
            base.OnLoad();

            FacadeTimeZone.IfNextDay += IfNextDay;
            FacadeTimeZone.GetCurZTime += GetCurZTime;

            lastZTime = SPlayerPrefs.GetFloat(PlayerPrefDefines.lastZTime);

            //RegisetUpdateObj();
        }

        /// <summary>
        /// 检测距离上次登录是否过了24小时
        /// </summary>
        /// <returns>距离上次登录是否过了24小时</returns>
        private bool IfNextDay()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan timeSpan = currentTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double timestamp = timeSpan.TotalSeconds;

            bool b = timestamp - lastZTime > 86400;
            if (b)
            {
                DateTime zeroTime = DateTime.Now.Date;
                TimeSpan timeSpan2 = zeroTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                double timestamp2 = timeSpan2.TotalSeconds;
                lastZTime = (float)timestamp2;
                SPlayerPrefs.SetFloat(PlayerPrefDefines.lastZTime, lastZTime);
            }
            return b;
        }

        /// <summary>
        /// 获取距离今日0点所经过的分钟
        /// </summary>
        /// <returns>距离今日0点所经过的分钟</returns>
        private int GetCurZTime()
        {
            TimeSpan totalM = DateTime.Now - DateTime.Now.Date;
            return (int)totalM.TotalMinutes;
        }

        //protected override void OnUpdate()
        //{
        //    base.OnUpdate();

        //    DateTime currentTime = DateTime.Now;

        //    if (currentTime.Second == 0 && currentTime.Minute != lastMinute)
        //    {
        //        lastMinute = currentTime.Minute;
        //    }
        //}
    }
}

