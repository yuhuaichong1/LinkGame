using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeTimeZone
{
    public static Func<bool> IfNextDay;                         //�������ϴε�¼�Ƿ����24Сʱ
    public static Func<int> GetCurZTime;                        //��ȡ�������0���������ķ���
}
