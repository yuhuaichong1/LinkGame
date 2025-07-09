using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadePayType
{
    public static Func<List<PayItem>> GetPayItems;                  //获取支付类型
    public static Func<float, string> RegionalChange;               //将值以汇率的方式显示
}
