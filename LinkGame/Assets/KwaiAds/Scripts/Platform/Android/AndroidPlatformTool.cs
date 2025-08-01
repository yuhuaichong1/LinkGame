using System;
using UnityEngine;

namespace BigoAds.Scripts.Platforms.Android
{
    internal static class AndroidPlatformTool
    {
        public const string ClassPackage = "com.kwai.network.sdk";
        private const string CurrentActivityMethod = "currentActivity";
        private const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";

        public static AndroidJavaObject GetGameActivity()
        {
            return new AndroidJavaClass(UnityPlayerClassName).GetStatic<AndroidJavaObject>(CurrentActivityMethod);
        }

        public static void CallMethodOnMainThread(Action task)
        {
            GetGameActivity()?.Call("runOnUiThread", new AndroidJavaRunnable(task));
        }
    }
}