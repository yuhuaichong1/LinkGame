using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace XrSDK
{
    [RegisterModule("KwaiNetWork Module")]
    public class KwaiNetWorkPendant : BaseModulePendant
    {
        public string appId_Android = "";
        public string token_Android = "";
        public string rewardTagId_Android = "";
        public string interstitialTagId_Android = "";
        public string appId_Ios = "";
        public string token_Ios = "";
        public string rewardTagId_Ios = "";
        public string interstitialTagId_Ios = "";
        public bool debug = true;

        public override string ModuleName => "KwaiNetWork";

        public override void CreateModule()
        {
            KwaiNetWorkData data = new KwaiNetWorkData();
#if UNITY_IOS

            data.appId = appId_Ios;
            data.token = token_Ios;
            data.rewardTagId = rewardTagId_Ios;
            data.interstitialTagId = interstitialTagId_Ios;
            data.debug = debug;

#elif UNITY_ANDROID || UNITY_EDITOR // UNITY_ANDROID
            data.appId = appId_Android;
            data.token = token_Android;
            data.rewardTagId = rewardTagId_Android;
            data.interstitialTagId = interstitialTagId_Android;
            data.debug = debug;
#else
            return;
#endif
            KwaiNetWorkModule module = new KwaiNetWorkModule(data);
            module.Load();
        }
    }
}