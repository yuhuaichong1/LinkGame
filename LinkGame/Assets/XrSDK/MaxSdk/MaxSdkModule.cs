using System;
using UnityEngine;
using UnityEngine.UI;
using XrCode;
using ThinkingData.Analytics;
using AdjustSdk;

namespace XrSDK
{
    public class MaxSdkModule : BaseModule
    {
        private string maxSdkKey;

        private string interstitialAdUnitId;
        private string rewardedAdUnitId;
        private string bannerAdUnitId;
        private string mRecAdUnitId;

        private bool isBannerShowing;
        private bool isMRecShowing;

        private int interstitialRetryAttempt;
        private int rewardedRetryAttempt;

        private double ecpm;

        private string RAdName;
        private double RAdIncome;
        private string IAdName;
        private double IAdIncome;

        public MaxSdkModule(MaxSdkData data)
        {
            if (data != null)
            {
                maxSdkKey = data.MaxSdkKey;
                interstitialAdUnitId = data.InterstitialAdUnitId;
                rewardedAdUnitId = data.RewardedAdUnitId;
                bannerAdUnitId = data.BannerAdUnitId;
                mRecAdUnitId = data.MRecAdUnitId;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            //showInterstitialButton.onClick.AddListener(ShowInterstitial);
            //showRewardedButton.onClick.AddListener(ShowRewardedAd);
            //showBannerButton.onClick.AddListener(ToggleBannerVisibility);
            //showMRecButton.onClick.AddListener(ToggleMRecVisibility);
            //mediationDebuggerButton.onClick.AddListener(MaxSdk.ShowMediationDebugger);

            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
            {
                // AppLovin SDK is initialized, configure and start loading ads.
                Debug.Log("MAX SDK Initialized");

                if (!string.IsNullOrEmpty(interstitialAdUnitId)) InitializeInterstitialAds();
                if (!string.IsNullOrEmpty(rewardedAdUnitId)) InitializeRewardedAds();
                if (!string.IsNullOrEmpty(bannerAdUnitId)) InitializeBannerAds();
                if (!string.IsNullOrEmpty(mRecAdUnitId)) InitializeMRecAds();

                // Initialize Adjust SDK
                //AdjustConfig adjustConfig = new AdjustConfig("YourAppToken", AdjustEnvironment.Sandbox);
                //Adjust.start(adjustConfig);

                //MaxSdk.ShowMediationDebugger();
            };
            ConnectTD();
            MaxSdk.SetSdkKey(maxSdkKey);
            MaxSdk.InitializeSdk();
        }

        #region Interstitial Ad Methods

        private void InitializeInterstitialAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialDismissedEvent;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialRevenuePaidEvent;

            MaxSdkDefines.ShowInterstitial += ShowInterstitial;

            // Load the first interstitial
            LoadInterstitial();
        }

        void LoadInterstitial()
        {
            //interstitialStatusText.text = "Loading...";
            MaxSdk.LoadInterstitial(interstitialAdUnitId);
        }

        void ShowInterstitial()
        {
            if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
            {
                MaxSdkDefines.PauseInterTimer();
                //interstitialStatusText.text = "Showing";
                MaxSdk.ShowInterstitial(interstitialAdUnitId);

                ModuleMgr.Instance.TDAnalyticsManager.IAdNameAndIncomeStart(IAdName, (float)IAdIncome);
            }
            else
            {
                LoadInterstitial();
                //interstitialStatusText.text = "Ad not ready";
            }
        }

        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
            //interstitialStatusText.text = "Loaded";
            Debug.Log("Interstitial loaded");
            // Reset retry attempt
            interstitialRetryAttempt = 0;
            MaxSdkDefines.InterstitialLoadedEvent?.Invoke();

            IAdIncome = adInfo.Revenue;
            IAdName = adInfo.NetworkName;
        }

        private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            interstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

            //interstitialStatusText.text = "Load failed: " + errorInfo.Code + "\nRetrying in " + retryDelay + "s...";
            Debug.Log("Interstitial failed to load with error code: " + errorInfo.Code);
            //广播插屏广告加载失败事件
            MaxSdkDefines.InterstitialFailedEvent?.Invoke(adUnitId, errorInfo.Code);
            MonoInst.Instance.Invoke("LoadInterstitial", (float)retryDelay);
        }

        private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad failed to display. We recommend loading the next ad
            Debug.Log("Interstitial failed to display with error code: " + errorInfo.Code);
            //广播插屏广告展示失败事件
            MaxSdkDefines.InterstitialFailedToDisplayEvent?.Invoke(adUnitId, errorInfo.Code);
            LoadInterstitial();
        }

        private void OnInterstitialDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is hidden. Pre-load the next ad
            Debug.Log("Interstitial dismissed");
            //广播插屏广告关闭事件
            MaxSdkDefines.InterstitialDismissedEvent?.Invoke(adInfo.Revenue);
            LoadInterstitial();
        }

        private void OnInterstitialRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad revenue paid. Use this callback to track user revenue.
            Debug.Log("Interstitial revenue paid");

            // Ad revenue
            double revenue = adInfo.Revenue;

            // Miscellaneous data
            string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"!
            string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
            string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
            string placement = adInfo.Placement; // The placement this ad's postbacks are tied to

            TrackAdRevenue(adInfo);

            ModuleMgr.Instance.TDAnalyticsManager.IAdNameAndIncomeEnd(adInfo.NetworkName, (float)adInfo.Revenue);
        }

        #endregion

        #region Rewarded Ad Methods

        private void InitializeRewardedAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;

            MaxSdkDefines.ShowRewardedAd += ShowRewardedAd;
            MaxSdkDefines.GetRewardedAdRevenue += () => 
            {
                return ecpm; 
            };

            // Load the first RewardedAd
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            MaxSdk.LoadRewardedAd(rewardedAdUnitId);
        }

        private void ShowRewardedAd()
        {
            if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
            {
                MaxSdk.ShowRewardedAd(rewardedAdUnitId);
                MaxSdkDefines.PauseInterTimer();

                ModuleMgr.Instance.TDAnalyticsManager.RAdNameAndIncomeStart(RAdName, (float)RAdIncome);
            }
            else
            {
                MaxSdkDefines.failShowAdButReward?.Invoke();
                LoadRewardedAd();
            }
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'
            Debug.Log("Rewarded ad loaded");

            // Reset retry attempt
            rewardedRetryAttempt = 0;

            //GetECPM
            ecpm = adInfo.Revenue * 1000;

            RAdIncome = adInfo.Revenue;
            RAdName = adInfo.NetworkName;

            Debug.LogError("广告加载完毕：" + adInfo.DspName + " | " + adInfo.Revenue);
        }

        private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

            Debug.Log("Rewarded ad failed to load with error code: " + errorInfo.Code);

            MonoInst.Instance.Invoke("LoadRewardedAd", (float)retryDelay);
            //广播激励视频加载失败事件
            MaxSdkDefines.RewardedAdFailedEvent?.Invoke(adUnitId, errorInfo.Code);
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad failed to display. We recommend loading the next ad
            Debug.Log("Rewarded ad failed to display with error code: " + errorInfo.Code);
            LoadRewardedAd();
            //广播激励视频显示失败事件
            MaxSdkDefines.RewardedAdFailedToDisplayEvent?.Invoke(adUnitId, errorInfo.Code);

        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Rewarded ad displayed");
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Rewarded ad clicked");
        }

        private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            Debug.Log("Rewarded ad dismissed");
            LoadRewardedAd();
            MaxSdkDefines.RewardedAdDismissedEvent?.Invoke();
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad was displayed and user should receive the reward
            Debug.Log("Rewarded ad received reward");
            MaxSdkDefines.RewardedAdReceivedRewardEvent?.Invoke(adInfo.Revenue);

            ModuleMgr.Instance.TDAnalyticsManager.RAdNameAndIncomeEnd(adInfo.NetworkName, (float)adInfo.Revenue);
        }

        private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad revenue paid. Use this callback to track user revenue.
            Debug.Log("Rewarded ad revenue paid");

            // Ad revenue
            double revenue = adInfo.Revenue;

            // Miscellaneous data
            string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"!
            string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
            string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
            string placement = adInfo.Placement; // The placement this ad's postbacks are tied to

            TrackAdRevenue(adInfo);
        }

        #endregion

        #region Banner Ad Methods

        private void InitializeBannerAds()
        {
            // Attach Callbacks
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdFailedEvent;
            MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;

            MaxSdkDefines.ToggleBannerVisibility += ToggleBannerVisibility;

            // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
            // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
            MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.TopCenter);

            // Set background or background color for banners to be fully functional.
            MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);
        }

        private void ToggleBannerVisibility()
        {
            if (!isBannerShowing)
            {
                MaxSdk.ShowBanner(bannerAdUnitId);
                //showBannerButton.GetComponentInChildren<Text>().text = "Hide Banner";
            }
            else
            {
                MaxSdk.HideBanner(bannerAdUnitId);
                //showBannerButton.GetComponentInChildren<Text>().text = "Show Banner";
            }

            isBannerShowing = !isBannerShowing;
        }

        private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Banner ad is ready to be shown.
            // If you have already called MaxSdk.ShowBanner(BannerAdUnitId) it will automatically be shown on the next ad refresh.
            Debug.Log("Banner ad loaded");
            //横幅广告加载完成
            MaxSdkDefines.BannerAdLoadedEvent?.Invoke();
        }

        private void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Banner ad failed to load. MAX will automatically try loading a new ad internally.
            Debug.Log("Banner ad failed to load with error code: " + errorInfo.Code);
            //横幅广告加载失败
            MaxSdkDefines.BannerAdFailedEvent?.Invoke(adUnitId, errorInfo.Code);

        }

        private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Banner ad clicked");
        }

        private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Banner ad revenue paid. Use this callback to track user revenue.
            Debug.Log("Banner ad revenue paid");

            // Ad revenue
            double revenue = adInfo.Revenue;

            // Miscellaneous data
            string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"!
            string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
            string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
            string placement = adInfo.Placement; // The placement this ad's postbacks are tied to

            TrackAdRevenue(adInfo);
        }

        #endregion

        #region MREC Ad Methods

        private void InitializeMRecAds()
        {
            // Attach Callbacks
            MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMRecAdLoadedEvent;
            MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMRecAdFailedEvent;
            MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMRecAdClickedEvent;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnMRecAdRevenuePaidEvent;

            MaxSdkDefines.ToggleMRecVisibility += ToggleMRecVisibility;

            // MRECs are automatically sized to 300x250.
            MaxSdk.CreateMRec(mRecAdUnitId, MaxSdkBase.AdViewPosition.BottomCenter);
        }

        private void ToggleMRecVisibility()
        {
            if (!isMRecShowing)
            {
                MaxSdk.ShowMRec(mRecAdUnitId);
                //showMRecButton.GetComponentInChildren<Text>().text = "Hide MREC";
            }
            else
            {
                MaxSdk.HideMRec(mRecAdUnitId);
                //showMRecButton.GetComponentInChildren<Text>().text = "Show MREC";
            }

            isMRecShowing = !isMRecShowing;
        }

        private void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // MRec ad is ready to be shown.
            // If you have already called MaxSdk.ShowMRec(MRecAdUnitId) it will automatically be shown on the next MRec refresh.
            Debug.Log("MRec ad loaded");
            //MRec 广告加载完成
            MaxSdkDefines.MRecAdLoadedEvent?.Invoke();
        }

        private void OnMRecAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // MRec ad failed to load. MAX will automatically try loading a new ad internally.
            Debug.Log("MRec ad failed to load with error code: " + errorInfo.Code);
            //MRec 广告加载失败
            MaxSdkDefines.MRecAdFailedEvent?.Invoke(adUnitId, errorInfo.Code);
        }

        private void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("MRec ad clicked");
        }

        private void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // MRec ad revenue paid. Use this callback to track user revenue.
            Debug.Log("MRec ad revenue paid");

            // Ad revenue
            double revenue = adInfo.Revenue;

            // Miscellaneous data
            string countryCode = MaxSdk.GetSdkConfiguration().CountryCode; // "US" for the United States, etc - Note: Do not confuse this with currency code which is "USD"!
            string networkName = adInfo.NetworkName; // Display name of the network that showed the ad (e.g. "AdColony")
            string adUnitIdentifier = adInfo.AdUnitIdentifier; // The MAX Ad Unit ID
            string placement = adInfo.Placement; // The placement this ad's postbacks are tied to

            TrackAdRevenue(adInfo);
        }

        #endregion

        private void TrackAdRevenue(MaxSdkBase.AdInfo adInfo)
        {
            AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue("applovin_max_sdk");

            adjustAdRevenue.SetRevenue(adInfo.Revenue, "USD");
            adjustAdRevenue.AdRevenueNetwork = adInfo.NetworkName;
            adjustAdRevenue.AdRevenueUnit = adInfo.AdUnitIdentifier;
            adjustAdRevenue.AdRevenuePlacement = adInfo.Placement;

            Adjust.TrackAdRevenue(adjustAdRevenue);
        }


        private void ConnectTD()
        {
            //TDConfig config = new TDConfig("APPID", "SERVER");
            //TDAnalytics.Init(config);
            var distinctId = TDAnalytics.GetDistinctId();
            MaxSdk.SetUserId(distinctId);
        }
    }
}