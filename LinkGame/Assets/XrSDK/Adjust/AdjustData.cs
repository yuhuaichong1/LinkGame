using AdjustSdk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustData
{
    public bool EventBuffering;
    public bool SendInBackground;
    public bool LaunchDeferredDeeplink;
    public string AppToken_Android;
    public string AppToken_IOS;
    public AdjustLogLevel LogLevel;
    public AdjustEnvironment Environment;
}
