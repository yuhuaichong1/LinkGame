using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeTimeZone
{
    public static Func<bool> IfNextDay;                         //检测距离上次登录是否过了24小时
    public static Func<int> GetCurZTime;                        //获取距离今日0点所经过的分钟
}
