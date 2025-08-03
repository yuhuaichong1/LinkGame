using System.Collections.Generic;
using UnityEngine;
namespace XrCode
{
    // 用户模块
    public class UserModule : BaseModule
    {

        protected override void OnLoad()
        {
            base.OnLoad();

            //添加服务器返回登录消息监听
            ModuleMgr.Instance.NotifyMod.RegisterNotification((int)EMsgCode.ES2C_Login, OnResponseLoginHandle);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            //移除服务器返回登录消息监听
            ModuleMgr.Instance.NotifyMod.RemoveNotification((int)EMsgCode.ES2C_Login, OnResponseLoginHandle);
        }

        //登录
        private void LoginToServer(string uId, string nickName, string icon)
        {

        }

        //服务器响应登录消息
        private void OnResponseLoginHandle(Notification notify)
        {

        }
    }
}