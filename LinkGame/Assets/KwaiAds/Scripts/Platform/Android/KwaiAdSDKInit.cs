#if UNITY_ANDROID
using System;
using BigoAds.Scripts.Platforms.Android;
using KwaiAds.Scripts.Api;
using KwaiAds.Scripts.Common;
using UnityEngine;

namespace KwaiAds.Scripts.Platforms.Android
{
    public class KwaiAdSDKInit
    {
        private const string TAG = "[KwaiAdSDK-INIT]";
        private const string SDKClientClassName = AndroidPlatformTool.ClassPackage + ".KwaiAdSDK";
        private const string SDKClientBuilder = AndroidPlatformTool.ClassPackage + ".api.SdkConfig$Builder";
        private const string SDKClientInitCallback = AndroidPlatformTool.ClassPackage + ".api.KwaiInitCallback";

        private static AndroidJavaClass kwaiAdSdkClass;
        private static KwaiAdConfig config;
        private static InitResultCallback initCallback;

        private KwaiAdSDKInit(KwaiAdConfig kwaiAdConfig, InitResultCallback initResultCallback)
        {
            if (kwaiAdConfig == null)
            {
                throw new ArgumentNullException(nameof(kwaiAdConfig), $"{TAG}: KwaiAdConfig cannot be null.");
            }
            config = kwaiAdConfig;
            initCallback = initResultCallback;
        }

        private class KwaiNetworkSingleton
        {
            private static volatile KwaiAdSDKInit _instance;
            private static object syncRoot = new object();

            public static KwaiAdSDKInit Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (_instance == null)
                            {
                                throw new InvalidOperationException($"{TAG}: KwaiAdSDK instance has not been initialized.");
                            }
                        }
                    }
                    return _instance;
                }
            }

            public static void Initialize(KwaiAdConfig kwaiAdConfig, InitResultCallback initResultCallback)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new KwaiAdSDKInit(kwaiAdConfig, initResultCallback);
                        _instance.InitializeKwaiAdSDK();
                    }
                }
            }
        }

        public static void Initialize(KwaiAdConfig kwaiAdConfig, InitResultCallback initResultCallback)
        {
            KwaiNetworkSingleton.Initialize(kwaiAdConfig, initResultCallback);
        }

        public static KwaiAdSDKInit Instance
        {
            get { return KwaiNetworkSingleton.Instance; }
        }

        public AndroidJavaClass GetKwaiAdSDKClass()
        {
            if (kwaiAdSdkClass == null)
            {
                Debug.LogError($"{TAG}: KwaiAdSDKClass has not been initialized..");
            }
            return kwaiAdSdkClass;
        }

        private void InitializeKwaiAdSDK()
        {
            var kwaiUnityCallback = new KwaiUnityCallback();
            if (Application.platform != RuntimePlatform.Android)
            {
                Debug.LogError($"{TAG}: Kwai Ad SDK can only be initialized on Android.");
                kwaiUnityCallback.onFail(2, "Kwai Ad SDK can only be initialized on Android.");
                return;
            }

            kwaiAdSdkClass = new AndroidJavaClass(SDKClientClassName);
            if (kwaiAdSdkClass == null)
            {
                Debug.LogError($"{TAG}: Failed to get KwaiAdSDK class.");
                kwaiUnityCallback.onFail(3, "Failed to get KwaiAdSDK class.");
                return;
            }

            var sdkConfigBuilder = new AndroidJavaObject(SDKClientBuilder);
            if (sdkConfigBuilder == null)
            {
                Debug.LogError($"{TAG}: Failed to create SdkConfig builder.");
                kwaiUnityCallback.onFail(4, "Failed to create SdkConfig builder.");
                return;
            }

            
            if (config != null)
            {
                sdkConfigBuilder.Call<AndroidJavaObject>("appId", config.AppId);
                sdkConfigBuilder.Call<AndroidJavaObject>("token", config.Token);
                sdkConfigBuilder.Call<AndroidJavaObject>("appName", config.AppName);
                sdkConfigBuilder.Call<AndroidJavaObject>("appDomain", config.AppDomain);
                sdkConfigBuilder.Call<AndroidJavaObject>("appStoreUrl", config.AppStoreUrl);
                sdkConfigBuilder.Call<AndroidJavaObject>("setInitCallback", kwaiUnityCallback);
                sdkConfigBuilder.Call<AndroidJavaObject>("debug", config.DebugLog);
            }

            AndroidJavaObject currentActivity = AndroidPlatformTool.GetGameActivity();
            if (currentActivity != null)
            {
                AndroidJavaObject contextObject = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
                Debug.Log("Android Context: " + contextObject.Call<string>("toString"));
                kwaiAdSdkClass.CallStatic("init", contextObject, sdkConfigBuilder.Call<AndroidJavaObject>("build"));
            }
            else
            {
                Debug.LogError("Current Activity not found.");
                kwaiUnityCallback.onFail(1, $"{TAG}: Current Activity not found.");
                return;
            }
        }

        [System.Serializable]
        private class KwaiUnityCallback : AndroidJavaProxy
        {
            public KwaiUnityCallback() : base(SDKClientInitCallback) { }

            public void onSuccess()
            {
                Debug.Log($"{TAG}: Kwai SDK initialized successfully.");
                if (initCallback != null)
                {
                    initCallback.OnSuccess();
                }
            }

            public void onFail(int code, string msg)
            {
                Debug.LogError($"{TAG}: Kwai SDK initialization failed. Code: {code}, Message: {msg}");
                if (initCallback != null)
                {
                    initCallback.OnFail(code, msg);
                }
            }
        }
    }
}
#endif