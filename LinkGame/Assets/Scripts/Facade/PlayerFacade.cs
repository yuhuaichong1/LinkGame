using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public static class PlayerFacade
    {                                                                   
        public static Func<float> GetWMoney;                            //得到当前金钱数

        public static Func<string> GetWName;                            //得到兑现界面填写的姓名
        public static Func<string> GetWEmailOrPhone;                    //得到兑现界面填写的电话/邮件

        public static Func<int> GetUserLevel;                           //得到玩家等级
        public static Func<int> GetCurUserExp;                          //得到玩家经验



        public static Action<float> SetWMoney;                          //设置当前金钱数

        public static Action<string, string> SetNameAndWEmailOrPhone;    //设置兑现界面的姓名和邮箱
        public static Action<string> SetEmail;                          //设置兑现界面的邮箱

        public static Action<int> SetUserLevel;                         //设置玩家等级
        public static Action<int> SetCurUserExp;                        //设置玩家经验
    }
}
