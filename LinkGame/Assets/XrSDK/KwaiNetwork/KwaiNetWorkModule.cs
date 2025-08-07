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
    /// �������SDK��KwaiNetWork��
    /// </summary>
    public void Init()
    {

        //var kwaiAdConfig = new KwaiAdConfig.Builder()
        //                .SetAppId("YOUR_APPID") // ����.
        //                .SetToken("YOUR_TOKEN") // ����.
        //                .SetAppName("TESTAPP") // ��ѡ
        //                .SetAppDomain("YOUR_DOMAIN") // ��ѡ
        //                .SetAppStoreUrl("YOUR_STORE_URL") //��ѡ
        //                .SetDebugLog(debug) // ��ѡ
        //                .Build();

        var kwaiAdConfig = new KwaiAdConfig.Builder()
                        .SetAppId(appId)
                        .SetToken(token)
                        .SetDebugLog(debug)
                        .Build();

        KwaiAdsSdk.Initialize(kwaiAdConfig, new InitResultCallbackImpl());
    }

    /// <summary>
    /// ��ʼ���ص�
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
    /// ���ż�����Ƶ
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
    /// ���ؼ������
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
    /// ������Ƶ�ص�״̬
    /// </summary>
    private class RewardAdListener : IRewardAdListener
    {
        //����������ת��ҳ
        public void OnAdClick()
        {
            KwaiNetWorkDefines.KNW_OnRAdClick?.Invoke();
        }
        //�������ر�
        public void OnAdClose()
        {
            KwaiNetWorkDefines.KNW_OnRAdClose?.Invoke();

            KwaiNetWorkDefines.LoadRewardAd?.Invoke();
        }
        //������Ƶ�������
        public void OnAdPlayComplete()
        {
            KwaiNetWorkDefines.KNW_OnRAdPlayComplete?.Invoke();
        }
        //������Ƶ�ع�
        public void OnAdShow()
        {
            KwaiNetWorkDefines.KNW_OnRAdShow?.Invoke();
        }
        //������Ƶ�ع�ʧ��
        public void OnAdShowFailed(int code, string msg)
        {
            Debug.LogError($"RewardAdListener#OnAdShowFailed , code:{code}, msg:{msg}");

            KwaiNetWorkDefines.KNW_OnRAdShowFailed?.Invoke(code, msg);

            KwaiNetWorkDefines.LoadRewardAd?.Invoke();
        }
        //��ȡ������
        public void OnRewardEarned()
        {
            KwaiNetWorkDefines.KNW_OnRewardEarned?.Invoke();
            ModuleMgr.Instance.TDAnalyticsManager.KwaiAdNameAndIncomeStart(KwaiNetWorkDefines.GetRewardedAdName(), KwaiNetWorkDefines.GetRewardedAdRevenue());

            KwaiNetWorkDefines.LoadRewardAd?.Invoke();
        }
    }

    /// <summary>
    /// ����������״̬�ص�
    /// </summary>
    private class RewardAdLoadListener : IRewardAdLoadListener
    {
        // �ܹ��ڻ������ƣ������޷����󵽺����棬��Ҫ�Ӱס����Խ�trackId�������Խ�ͬѧ���мӰס�
        public void OnAdLoadFailed(string trackId, int code, string msg)
        {
            D.Error($"RewardAdLoadListener#OnAdLoadFailed , trackId:{trackId}, code:{code}, msg:{msg}");

            KwaiNetWorkDefines.KNW_OnRAdLoadFailed?.Invoke(trackId, code, msg);
        }

        //������Ƶ��ʼ����
        public void OnAdLoadStart(string trackId)
        {
            KwaiNetWorkDefines.KNW_OnRAdLoadStart?.Invoke(trackId);
        }

        //������Ƶ���سɹ�
        // price ��λ��$(��Ԫ��ecpm)
        public void OnAdLoadSuccess(string trackId, string price)
        {
            KwaiNetWorkDefines.KNW_OnRAdLoadSuccess?.Invoke(trackId, price);
        }
    }
    /**********************************************************************************************************************************************************************************************************************************************/
    /// <summary>
    /// ���Ų������
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
    /// ���ز������
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
    /// ��ҳ��Ƶ�ص�״̬
    /// </summary>
    private class InterstitialAdListener : IInterstitialAdListener
    {
        public void OnAdClick()
        {
            // ��ҳ������ת��ҳ �� Interstitial ad agjust conversion page 
            KwaiNetWorkDefines.KNW_OnIAdClick?.Invoke();
        }

        public void OnAdClose()
        {
            // ��ҳ���ر� | Interstitial ad close 
            KwaiNetWorkDefines.KNW_OnIAdClose?.Invoke();

            KwaiNetWorkDefines.LoadInterstitial?.Invoke();
        }

        public void OnAdPlayComplete()
        {
            // ��ҳ��Ƶ������� | Interstitial video play complete 
            KwaiNetWorkDefines.KNW_OnIAdPlayComplete?.Invoke(KwaiNetWorkDefines.GetRewardedAdECPM());
        }

        public void OnAdShow()
        {
            // ��ҳ��Ƶ�ع� | Interstitial video show 
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
    /// ��ҳ������״̬�ص�
    /// </summary>
    private class InterstitialAdLoadListener : IInterstitialAdLoadListener
    {
        // �ܹ��ڻ������ƣ������޷����󵽺����棬��Ҫ�Ӱס����Խ�trackId�������Խ�ͬѧ���мӰ�
        public void OnAdLoadFailed(string trackId, int code, string msg)
        {
            Debug.LogError($"InterstitialAdLoadListener#OnAdLoadFailed , trackId:{trackId}, code:{code}, msg:{msg}");

            KwaiNetWorkDefines.KNW_OnIAdLoadFailed?.Invoke(trackId, code, msg);
        }
        public void OnAdLoadStart(string trackId)
        {
            KwaiNetWorkDefines.KNW_OnIAdLoadStart?.Invoke(trackId);
        }
        // price ��λ��$(��Ԫ��ecpm)
        public void OnAdLoadSuccess(string trackId, string price)
        {
            KwaiNetWorkDefines.KNW_OnIAdLoadSuccess?.Invoke(trackId, price);
        }
    }
}
