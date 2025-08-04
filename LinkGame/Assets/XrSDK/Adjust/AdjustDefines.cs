using AdjustSdk;
using System;
using System.Collections.Generic;
using System.Globalization;

public static class AdjustDefines
{
    public static Action<AdjustEvent> TrackEvent;                                                   //AdjustÂñµã
    public static Action<AdjustConfig> Start;                                                       //
    public static Action<bool> SetEnabled;                                                          //
    public static Func<bool> IsEnabled;                                                             //
    public static Action<bool> SetOfflineMode;                                                      //
    public static Action<string> SetDeviceToken;                                                    //
    public static Action GdprForgetMe;                                                              //
    public static Action DisableThirdPartySharing;                                                  //
    public static Action<string> AppWillOpenUrl;                                                    //
    public static Action SendFirstPackages;                                                         //
    public static Action<string, string> AddSessionPartnerParameter;                                //
    public static Action<string, string> AddSessionCallbackParameter;                               //
    public static Action<string> RemoveSessionPartnerParameter;                                     //
    public static Action<string> RemoveSessionCallbackParameter;                                    //
    public static Action ResetSessionPartnerParameters;                                             //
    public static Action ResetSessionCallbackParameters;                                            //
    public static Action<string, string> TrackAdRevenue;                                            //
    public static Action<AdjustAdRevenue> TrackAdRevenue2;                                            //
    public static Action<AdjustAppStoreSubscription> TrackAppStoreSubscription;                     //
    public static Action<AdjustPlayStoreSubscription> TrackPlayStoreSubscription;                   //
    public static Action<AdjustThirdPartySharing> TrackThirdPartySharing;                           //
    public static Action<bool> TrackMeasurementConsent;                                             //
    public static Action<Action<int>, string> RequestTrackingAuthorizationWithCompletionHandler;    //
    public static Action<int> UpdateConversionValue;                                                //
    public static Func<int> GetAppTrackingAuthorizationStatus;                                      //
    public static Func<string> GetAdid;                                                             //
    public static Func<AdjustAttribution> GetAttribution;                                           //
    public static Func<string> GetWinAdid;                                                          //
    public static Func<string> GetIdfa;                                                             //
    public static Func<string> GetSdkVersion;                                                       //
    public static Action<string> SetReferrer;                                                       //
    public static Action<Action<string>> GetGoogleAdId;                                             //
    public static Func<string> GetAmazonAdId;                                                       //
    public static Func<bool> IsEditor;                                                              //
    public static Action<Dictionary<string, string>> SetTestOptions;                                //
}
