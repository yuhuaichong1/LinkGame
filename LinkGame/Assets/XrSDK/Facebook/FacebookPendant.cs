using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrSDK
{
    [RegisterModule("Facebook Module")]
    public class FacebookPendant : BaseModulePendant
    {
        public string AppId;                  //appid
        public string Token;                  //token

        public override string ModuleName => "Facebook";


        public override void CreateModule()
        {
            FacebookData data = new FacebookData();
            data.AppId = AppId;
            data.Token = Token;
            FacebookModule module = new FacebookModule(data);
            module.Load();
        }
    }
}