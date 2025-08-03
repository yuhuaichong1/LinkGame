using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace XrCode
{
    public class TimeZoneModule : BaseModule
    {
        private float lastZTime;//�ϴ������յ��賿ʱ�䣨Ŀǰ����һ��һ�ε����ۣ�
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
        /// �������ϴε�¼�Ƿ����24Сʱ
        /// </summary>
        /// <returns>�����ϴε�¼�Ƿ����24Сʱ</returns>
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
        /// ��ȡ�������0���������ķ���
        /// </summary>
        /// <returns>�������0���������ķ���</returns>
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

