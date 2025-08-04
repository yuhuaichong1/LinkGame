using AdjustSdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrSDK;
using static MaxSdkCallbacks;

namespace XrSDK
{
    [RegisterModule("Adjust Module")]
    public class AdjustModulePendant : BaseModulePendant
    {
        public bool EventBuffering;
        public bool SendInBackground;
        public bool LaunchDeferredDeeplink;
        public string AppToken_Andriod;
        public string AppToken_IOS;
        public AdjustLogLevel LogLevel;
        public AdjustEnvironment Environment;

        public override string ModuleName => "Adjust";

        public override void CreateModule()
        {
            AdjustData data = new AdjustData();

            data.EventBuffering = EventBuffering;
            data.SendInBackground = SendInBackground;
            data.LaunchDeferredDeeplink = LaunchDeferredDeeplink;
            data.AppToken_Android = AppToken_Andriod;
            data.AppToken_IOS = AppToken_IOS;
            data.LogLevel = LogLevel;
            data.Environment = Environment;

            AdjustModule module = new AdjustModule(data);
            module.Load();
        }
    }
}
    
