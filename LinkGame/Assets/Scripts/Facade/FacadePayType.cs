using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadePayType
{
    public static Func<List<PayItem>> GetPayItems;                  //��ȡ֧������
    public static Func<float, string> RegionalChange;               //��ֵ�Ի��ʵķ�ʽ��ʾ
}
