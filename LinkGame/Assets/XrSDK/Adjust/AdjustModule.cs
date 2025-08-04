using AdjustSdk;
using System;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

public class AdjustModule : BaseModule
{
    private const string errorMsgEditor = "[Adjust]: SDK can not be used in Editor.";
    private const string errorMsgStart = "[Adjust]: SDK not started. Start it manually using the 'start' method.";
    private const string errorMsgPlatform = "[Adjust]: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.";

    public bool eventBuffering;
    public bool sendInBackground;
    public bool launchDeferredDeeplink;
    public string appToken;
    public AdjustLogLevel logLevel;
    public AdjustEnvironment environment;

#if UNITY_IOS
        // Delegate references for iOS callback triggering
        private static List<Action<int>> authorizationStatusDelegates = null;
        private static Action<string> deferredDeeplinkDelegate = null;
        private static Action<AdjustEventSuccess> eventSuccessDelegate = null;
        private static Action<AdjustEventFailure> eventFailureDelegate = null;
        private static Action<AdjustSessionSuccess> sessionSuccessDelegate = null;
        private static Action<AdjustSessionFailure> sessionFailureDelegate = null;
        private static Action<AdjustAttribution> attributionChangedDelegate = null;
        private static Action<int> conversionValueUpdatedDelegate = null;
#endif

    public AdjustModule(AdjustData data)
    {
        if (data != null)
        {
            eventBuffering = data.EventBuffering;
            sendInBackground = data.SendInBackground;
            launchDeferredDeeplink = data.LaunchDeferredDeeplink;
#if UNITY_ANDROID
            appToken = data.AppToken_Android;
#elif UNITY_IOS
            appToken = data.AppToken_IOS;
#endif
            logLevel = data.LogLevel;
            environment = data.Environment;
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        if (IsEditor())
        {
            return;
        }

        AdjustConfig adjustConfig = new AdjustConfig(this.appToken, this.environment, (this.logLevel == AdjustLogLevel.Suppress));
        adjustConfig.LogLevel = this.logLevel;
        adjustConfig.IsSendingInBackgroundEnabled = this.sendInBackground;
        adjustConfig.IsDeferredDeeplinkOpeningEnabled = this.launchDeferredDeeplink;


        //Adjust.start(adjustConfig);

        AdjustDefines.TrackEvent += trackEvent;
    }

    public void trackEvent(AdjustEvent adjustEvent)
    {
        if (IsEditor())
        {
            return;
        }

        if (adjustEvent == null)
        {
            Debug.Log("[Adjust]: Missing event to track.");
            return;
        }
#if UNITY_IOS
            AdjustiOS.TrackEvent(adjustEvent);
#elif UNITY_ANDROID
        AdjustAndroid.TrackEvent(adjustEvent);
#elif (UNITY_WSA || UNITY_WP8)
            AdjustWindows.TrackEvent(adjustEvent);
#else
            Debug.Log(errorMsgPlatform);
#endif
    }


#if UNITY_IOS
        public void GetNativeAttribution(string attributionData)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.attributionChangedDelegate == null)
            {
                Debug.Log("[Adjust]: Attribution changed delegate was not set.");
                return;
            }

            var attribution = new AdjustAttribution(attributionData);
            Adjust.attributionChangedDelegate(attribution);
        }

        public void GetNativeEventSuccess(string eventSuccessData)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.eventSuccessDelegate == null)
            {
                Debug.Log("[Adjust]: Event success delegate was not set.");
                return;
            }

            var eventSuccess = new AdjustEventSuccess(eventSuccessData);
            Adjust.eventSuccessDelegate(eventSuccess);
        }

        public void GetNativeEventFailure(string eventFailureData)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.eventFailureDelegate == null)
            {
                Debug.Log("[Adjust]: Event failure delegate was not set.");
                return;
            }

            var eventFailure = new AdjustEventFailure(eventFailureData);
            Adjust.eventFailureDelegate(eventFailure);
        }

        public void GetNativeSessionSuccess(string sessionSuccessData)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.sessionSuccessDelegate == null)
            {
                Debug.Log("[Adjust]: Session success delegate was not set.");
                return;
            }

            var sessionSuccess = new AdjustSessionSuccess(sessionSuccessData);
            Adjust.sessionSuccessDelegate(sessionSuccess);
        }

        public void GetNativeSessionFailure(string sessionFailureData)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.sessionFailureDelegate == null)
            {
                Debug.Log("[Adjust]: Session failure delegate was not set.");
                return;
            }

            var sessionFailure = new AdjustSessionFailure(sessionFailureData);
            Adjust.sessionFailureDelegate(sessionFailure);
        }

        public void GetNativeDeferredDeeplink(string deeplinkURL)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.deferredDeeplinkDelegate == null)
            {
                Debug.Log("[Adjust]: Deferred deeplink delegate was not set.");
                return;
            }

            Adjust.deferredDeeplinkDelegate(deeplinkURL);
        }

        public void GetNativeConversionValueUpdated(string conversionValue)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.conversionValueUpdatedDelegate == null)
            {
                Debug.Log("[Adjust]: Conversion value updated delegate was not set.");
                return;
            }

            int cv = -1;
            if (Int32.TryParse(conversionValue, out cv))
            {
                if (cv != -1)
                {
                    Adjust.conversionValueUpdatedDelegate(cv);
                }
            }
        }

        public void GetAuthorizationStatus(string authorizationStatus)
        {
            if (IsEditor()) 
            {
                return;
            }

            if (Adjust.authorizationStatusDelegates == null)
            {
                Debug.Log("[Adjust]: Authorization status delegates were not set.");
                return;
            }

            foreach (Action<int> callback in Adjust.authorizationStatusDelegates)
            {
                callback(Int16.Parse(authorizationStatus));
            }
            Adjust.authorizationStatusDelegates.Clear();
        }
#endif

    private bool IsEditor()
    {
#if UNITY_EDITOR
        Debug.Log(errorMsgEditor);
        return true;
#else
            return false;
#endif
    }

    public void SetTestOptions(Dictionary<string, string> testOptions)
    {
        if (IsEditor())
        {
            return;
        }

#if UNITY_IOS
            AdjustiOS.SetTestOptions(testOptions);
#elif UNITY_ANDROID
        AdjustAndroid.SetTestOptions(testOptions);
#elif (UNITY_WSA || UNITY_WP8)
            AdjustWindows.SetTestOptions(testOptions);
#else
            Debug.Log("[Adjust]: Cannot run integration tests. None of the supported platforms selected.");
#endif
    }
}