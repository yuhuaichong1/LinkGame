using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public static class PlayerFacade
    {                                                                   
        public static Func<float> GetWMoney;                            //得到当前金钱数
        public static Func<string> GetWName;                               //得到兑现界面填写的姓名
        public static Func<int> GetWPhone;                              //得到兑现界面填写的电话
        public static Func<string> GetEmail;                            //得到兑现界面填写的邮件
        public static Action<string, int> SetNameAndPhone;              //设置兑现界面的姓名和电话
        public static Action<string, string> SetNameAndEmail;           //设置兑现界面的姓名和邮箱
        public static Action<string> SetEmail;                          //设置兑现界面的邮箱
    }
}
