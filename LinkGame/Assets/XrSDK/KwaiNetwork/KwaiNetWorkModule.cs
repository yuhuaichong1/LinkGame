using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KwaiAds.Scripts.Api;
using KwaiAds.Scripts.Api.Reward;
using KwaiAds.Scripts.Common;
using System;
using KwaiAds.Scripts.Api.Interstitial;
using XrSDK;
using XrCode;

public class KwaiNetWorkModule : BaseModule
{
    private string appId;
    private string token;
    private string rewardTagId;
    private string interstitialTagId;
    private bool debug;

    private IRewardAdController rewardAdController;

    private IInterstitialAdController interAdController;

    private double ecpm;
    private string KAdName = "KwaiNetwork";
    private float KAdIncome;

    private Action LoadRewardAdAction;

    public KwaiNetWorkModule(KwaiNetWorkData data)
    {
        if (data != null)
        {
            appId = data.appId;
            token = data.token;
            rewardTagId = data.rewardTagId;
            interstitialTagId = data.interstitialTagId;
            debug = data.debug;
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        KwaiNetWorkDefines.KNW_ShowRewardedAd += ShowRewardAd;
        KwaiNetWorkDefines.KNW_ShowInterstitial += ShoInterAd;
        KwaiNetWorkDefines.KNW_OnRAdLoadSuccess += (id, price) =>
        {
            ecpm = double.Parse(price);
            KAdIncome = (float)(ecpm / 1000);
        };

        KwaiNetWorkDefines.GetRewardedAdECPM += () =>
        {
            return ecpm;
        };

        KwaiNetWorkDefines.GetRewardedAdRevenue += () =>
        {
            return KAdIncome;
        };

        KwaiNetWorkDefines.GetRewardedAdName += () =>
        {
            return KAdName;
        };

        KwaiNetWorkDefines.LoadRewardAd += LoadRewardAd;
        KwaiNetWorkDefines.LoadInterstitial += LoadInterstitial;
        Init();
    }

    /// <summary>
    /// 海外快手SDK（KwaiNetWork）
    /// </summary>
    public void Init()
    {

        //var kwaiAdConfig = new KwaiAdConfig.Builder()
        //                .SetAppId("YOUR_APPID") // 必填.
        //                .SetToken("YOUR_TOKEN") // 必填.
        //                .SetAppName("TESTAPP") // 可选
        //                .SetAppDomain("YOUR_DOMAIN") // 可选
        //                .SetAppStoreUrl("YOUR_STORE_URL") //可选
        //                .SetDebugLog(debug) // 可选
        //                .Build();

        var kwaiAdConfig = new KwaiAdConfig.Builder()
                        .SetAppId(appId)
                        .SetToken(token)
                        .SetDebugLog(debug)
                        .Build();

        KwaiAdsSdk.Initialize(kwaiAdConfig, new InitResultCallbackImpl());
    }

    /// <summary>
    /// 初始化回调
    /// </summary>
    private class InitResultCallbackImpl : InitResultCallback
    {
        public void OnFail(int code, string msg)
        {
            Debug.LogError($"NewBehaviourScript code:{code}, msg: {msg}");
        }

        public void OnSuccess()
        {
            Debug.LogError($"NewBehaviourScript OnSuccess.");
            KwaiNetWorkDefines.LoadRewardAd?.Invoke();
            KwaiNetWorkDefines.LoadInterstitial?.Invoke();
        }
    }
    /**********************************************************************************************************************************************************************************************************************************************/
    /// <summary>
    /// 播放激励视频
    /// </summary>
    public void ShowRewardAd()
    {
        if (rewardAdController != null)
        {
            rewardAdController.Show();
            KwaiNetWorkDefines.PauseInterTimer();

            ModuleMgr.Instance.TDAnalyticsManager.KwaiAdNameAndIncomeStart(KAdName, KAdIncome);
        }
        else
        {
            KwaiNetWorkDefines.failShowAdButReward?.Invoke();
            LoadRewardAd();
        }
    }

    /// <summary>
    /// 加载激励广告
    /// </summary>
    public void LoadRewardAd()
    {
        if (rewardAdController != null)
        {
            rewardAdController.Destroy();
            rewardAdController = null;
        }
        rewardAdController = KwaiAdsSdk.SDK.getRewardAdController();

        KwaiRewardAdRequest kwaiRewardAdRequest = new KwaiRewardAdRequest(rewardTagId);

        kwaiRewardAdRequest.ExtParams[Constants.Request.BID_FLOOR_PRICE] = "0";

        rewardAdController.Load(kwaiRewardAdRequest, new RewardAdListener(), new RewardAdLoadListener());
    }

    /// <summary>
    /// 激励视频回调状态
    /// </summary>
    private class RewardAdListener : IRewardAdListener
    {
        //激励广告调整转换页
        public void OnAdClick()
        {
            KwaiNetWorkDefines.KNW_OnRAdClick?.Invoke();
        }
        //激励广告关闭
        public void OnAdClose()
        {
            KwaiNetWorkDefines.KNW_OnRAdClose?.Invoke();

            KwaiNetWorkDefines.LoadRewardAd?.Invoke();
        }
        //激励视频播放完成
        public void OnAdPlayComplete()
        {
            KwaiNetWorkDefines.KNW_OnRAdPlayComplete?.Invoke();
        }
        //激励视频曝光
        public void OnAdShow()
        {
            KwaiNetWorkDefines.KNW_OnRAdShow?.Invoke();
        }
        //激励视频曝光失败
        public void OnAdShowFailed(int code, string msg)
        {
            Debug.LogError($"RewardAdListener#OnAdShowFailed , code:{code}, msg:{msg}");

            KwaiNetWorkDefines.KNW_OnRAdShowFailed?.Invoke(code, msg);

            KwaiNetWorkDefines.LoadRewardAd?.Invoke();
        }
        //获取到激励
        public void OnRewardEarned()
        {
            KwaiNetWorkDefines.KNW_OnRewardEarned?.Invoke();
            ModuleMgr.Instance.TDAnalyticsManager.KwaiAdNameAndIncomeStart(KwaiNetWorkDefines.GetRewardedAdName(), KwaiNetWorkDefines.GetRewardedAdRevenue());

            KwaiNetWorkDefines.LoadRewardAd?.Invoke();
        }
    }

    /// <summary>
    /// 激励广告加载状态回调
    /// </summary>
    private class RewardAdLoadListener : IRewardAdLoadListener
    {
        // 受国内环境限制，国内无法请求到海外广告，需要加白。可以将trackId反馈给对接同学进行加白。
        public void OnAdLoadFailed(string trackId, int code, string msg)
        {
            D.Error($"RewardAdLoadListener#OnAdLoadFailed , trackId:{trackId}, code:{code}, msg:{msg}");

            KwaiNetWorkDefines.KNW_OnRAdLoadFailed?.Invoke(trackId, code, msg);
        }

        //激励视频开始加载
        public void OnAdLoadStart(string trackId)
        {
            KwaiNetWorkDefines.KNW_OnRAdLoadStart?.Invoke(trackId);
        }

        //激励视频加载成功
        // price 单位是$(美元，ecpm)
        public void OnAdLoadSuccess(string trackId, string price)
        {
            KwaiNetWorkDefines.KNW_OnRAdLoadSuccess?.Invoke(trackId, price);
        }
    }
    /**********************************************************************************************************************************************************************************************************************************************/
    /// <summary>
    /// 播放插屏广告
    /// </summary>
    public void ShoInterAd()
    {
        if (interAdController != null)
        {
            interAdController.Show();
            KwaiNetWorkDefines.PauseInterTimer();
        }
        else
        {
            LoadInterstitial();
        }
    }

    /// <summary>
    /// 加载插屏广告
    /// </summary>
    private void LoadInterstitial()
    {
        if (interAdController != null)
        {
            interAdController.Destroy();
            interAdController = null;
        }
        interAdController = KwaiAdsSdk.SDK.getInterstitialAdController();

        KwaiInterstitialAdRequest interstitialRewardAdRequest = new KwaiInterstitialAdRequest(interstitialTagId);

        interstitialRewardAdRequest.ExtParams[Constants.Request.BID_FLOOR_PRICE] = "0";

        interAdController.Load(interstitialRewardAdRequest, new InterstitialAdListener(), new InterstitialAdLoadListener());
    }

    /// <summary>
    /// 插页视频回调状态
    /// </summary>
    private class InterstitialAdListener : IInterstitialAdListener
    {
        public void OnAdClick()
        {
            // 插页广告调整转换页 ｜ Interstitial ad agjust conversion page 
            KwaiNetWorkDefines.KNW_OnIAdClick?.Invoke();
        }

        public void OnAdClose()
        {
            // 插页广告关闭 | Interstitial ad close 
            KwaiNetWorkDefines.KNW_OnIAdClose?.Invoke();

            KwaiNetWorkDefines.LoadInterstitial?.Invoke();
        }

        public void OnAdPlayComplete()
        {
            // 插页视频播放完成 | Interstitial video play complete 
            KwaiNetWorkDefines.KNW_OnIAdPlayComplete?.Invoke(KwaiNetWorkDefines.GetRewardedAdECPM());
        }

        public void OnAdShow()
        {
            // 插页视频曝光 | Interstitial video show 
            KwaiNetWorkDefines.KNW_OnIAdShow?.Invoke();
        }

        public void OnAdShowFailed(int code, string msg)
        {
            Debug.LogError($"InterstitialAdListener#OnAdShowFailed , code:{code}, msg:{msg}");

            KwaiNetWorkDefines.KNW_OnIAdShowFailed?.Invoke(code, msg);

            KwaiNetWorkDefines.LoadInterstitial?.Invoke();
        }
    }

    /// <summary>
    /// 插页广告加载状态回调
    /// </summary>
    private class InterstitialAdLoadListener : IInterstitialAdLoadListener
    {
        // 受国内环境限制，国内无法请求到海外广告，需要加白。可以将trackId反馈给对接同学进行加白
        public void OnAdLoadFailed(string trackId, int code, string msg)
        {
            Debug.LogError($"InterstitialAdLoadListener#OnAdLoadFailed , trackId:{trackId}, code:{code}, msg:{msg}");

            KwaiNetWorkDefines.KNW_OnIAdLoadFailed?.Invoke(trackId, code, msg);
        }
        public void OnAdLoadStart(string trackId)
        {
            KwaiNetWorkDefines.KNW_OnIAdLoadStart?.Invoke(trackId);
        }
        // price 单位是$(美元，ecpm)
        public void OnAdLoadSuccess(string trackId, string price)
        {
            KwaiNetWorkDefines.KNW_OnIAdLoadSuccess?.Invoke(trackId, price);
        }
    }
}
