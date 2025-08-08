using System;
using UnityEngine;

namespace XrCode
{
    public class AdModule : BaseModule
    {
        private TDAnalyticsManager TDAnalyticsManager;

        private EAdSource eAdSource;
        private double maxEcpm;
        private double kwaiEcpm;
        private Action successfulAction;
        private Action<string> failedAction;

        private int totalAdwatch;//累计广告观看次数
        private double totalAdRevenue;//累计广告收益
        private int currentAdwatch;//当局广告观看次数

        private string countryCode;//国家码
        private string notReadyStr;//未准备好错误字段

        protected override void OnLoad() 
        {
            TDAnalyticsManager = ModuleMgr.Instance.TDAnalyticsManager;

            FacadeAd.PlayRewardAd += PlayRewardAd;
            FacadeAd.PlayInterAd += PlayInterAd;
            FacadeAd.GetAdSource += GetAdSource;

            countryCode = FacadePayType.GetCountryCode();
            notReadyStr = ModuleMgr.Instance.LanguageMod.GetText("10074");

            LoadData();

            AdCallback();
        }

        //加载数据
        private void LoadData()
        {
            totalAdwatch = SPlayerPrefs.GetInt(PlayerPrefDefines.totalAdwatch);
            totalAdRevenue = SPlayerPrefs.GetDouble(PlayerPrefDefines.totalAdRevenue, true);
        }

        //广告回调添加
        private void AdCallback()
        {
            #region MAX激励广告
            MaxSdkDefines.OnRewardedAdDisplayedEvent += (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Reward, this.eAdSource, AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);
                SolarEngineDefines.AdImpresslon();
            };
            MaxSdkDefines.OnRewardedAdFailedToDisplayEvent += (adUnitId, ErrorInfo, AdInfo) =>
            {
                string str = $"{ErrorInfo.Code} : {ErrorInfo.Message}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Reward, this.eAdSource, str, AdInfo.NetworkName, AdInfo.RevenuePrecision);

                failedAction?.Invoke(ErrorInfo.Message);
            };
            MaxSdkDefines.OnRewardAdReceivedRewardEvent += (adUnitId, reward, AdInfo) =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Reward, this.eAdSource, AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);
                SolarEngineDefines.AdRevenue(AdInfo.Revenue);

                currentAdwatch += 1;
                totalAdwatch += 1;
                totalAdRevenue += AdInfo.Revenue;
                CheckAdTotalData();
                
                successfulAction?.Invoke();
            };
            MaxSdkDefines.OnRewardAdRevenuePaidEvent += (adUnitId, AdInfo) => 
            {
                TDAnalyticsManager.AdRevenuePaid(AdInfo.Revenue, AdInfo.RevenuePrecision, this.eAdSource, AdInfo.NetworkName);
            };

            #endregion

            #region KwaiNetWork激励广告
            KwaiNetWorkDefines.KNW_OnRAdShow += () =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Reward, this.eAdSource, kwaiEcpm, KwaiNetWorkDefines.GetRewardedAdName(), "kwai");
                SolarEngineDefines.AdImpresslon();
            };
            KwaiNetWorkDefines.KNW_OnRAdShowFailed += (code, msg) =>
            {
                string str = $"{code} : {msg}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Reward, this.eAdSource, str, KwaiNetWorkDefines.GetRewardedAdName(), "kwai");

                failedAction?.Invoke(msg);
            };
            KwaiNetWorkDefines.KNW_OnRAdPlayComplete += () =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Reward, this.eAdSource, kwaiEcpm, KwaiNetWorkDefines.GetRewardedAdName(), "kwai");
                SolarEngineDefines.AdRevenue(kwaiEcpm / 1000);

                currentAdwatch += 1;
                totalAdwatch += 1;
                totalAdRevenue += kwaiEcpm / 1000;
                CheckAdTotalData();

                successfulAction?.Invoke();
            };
            KwaiNetWorkDefines.KNW_OnRewardEarned += () => 
            {
                TDAnalyticsManager.AdRevenuePaid(kwaiEcpm / 1000, "kwai", this.eAdSource, KwaiNetWorkDefines.GetRewardedAdName());
            };
            #endregion

            #region MAX插屏广告
            MaxSdkDefines.OnInterstitialDisplayedEvent += (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Interstitial, this.eAdSource, AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);
                SolarEngineDefines.AdImpresslon();
            };
            MaxSdkDefines.OnInterstitialFailedToDisplayEvent += (adUnitId, ErrorInfo, AdInfo) =>
            {
                string str = $"{ErrorInfo.Code} : {ErrorInfo.Message}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Interstitial, this.eAdSource, str, AdInfo.NetworkName, AdInfo.RevenuePrecision);

                failedAction?.Invoke(ErrorInfo.Message);
            };
            MaxSdkDefines.OnInterstitialDismissedEvent += (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Interstitial, this.eAdSource, AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);
                SolarEngineDefines.AdRevenue(AdInfo.Revenue);

                currentAdwatch += 1;
                totalAdwatch += 1;
                totalAdRevenue += AdInfo.Revenue;
                CheckAdTotalData();

                successfulAction?.Invoke();
            };
            MaxSdkDefines.OnInterstitialRevenuePaidEvent += (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdRevenuePaid(AdInfo.Revenue, AdInfo.RevenuePrecision, this.eAdSource, AdInfo.NetworkName);
            };
            #endregion

            #region 其他
            MaxSdkDefines.RewardAdNotReadyEvent += () =>
            {
                UIManager.Instance.OpenNotice(notReadyStr);
            };
            #endregion
        }

        //播放激励广告
        private void PlayRewardAd(EAdSource eAdSource, Action successAction, Action<string> failAction = null)
        {
            //successAction?.Invoke();
            //return;

            this.eAdSource = eAdSource;
            maxEcpm = MaxSdkDefines.GetRewardedAdRevenue();
            kwaiEcpm = KwaiNetWorkDefines.GetRewardedAdECPM();
            successfulAction = successAction;
            failedAction = failAction;

            if(countryCode == "BR")
            {
                if (maxEcpm >= kwaiEcpm)
                {
                    MaxSdkDefines.ShowRewardedAd();
                }
                else
                {
                    KwaiNetWorkDefines.KNW_ShowRewardedAd();
                }
            }
            else
            {
                MaxSdkDefines.ShowRewardedAd();
            }
        }

        //播放插屏广告
        private void PlayInterAd(EAdSource eAdSource, Action successAction, Action<string> failAction = null)
        {
            //successAction?.Invoke();
            //return;

            this.eAdSource = eAdSource;
            successfulAction = successAction;
            failedAction = failAction;

            MaxSdkDefines.ShowInterstitial();
        }

        private void CheckAdTotalData()
        {
            switch(totalAdwatch)
            {
                case 5:
                    TDAnalyticsManager.Times_5_Ad(totalAdRevenue);
                    SolarEngineDefines.Times_5_Ad?.Invoke();
                    break;
                case 10:
                    TDAnalyticsManager.Times_10_Ad(totalAdRevenue);
                    SolarEngineDefines.Times_10_Ad?.Invoke();
                    break;
                case 15:
                    TDAnalyticsManager.Times_15_Ad(totalAdRevenue);
                    SolarEngineDefines.Times_15_Ad?.Invoke();
                    break;
                case 20:
                    TDAnalyticsManager.Times_20_Ad(totalAdRevenue);
                    break;
            }

            SPlayerPrefs.SetInt(PlayerPrefDefines.totalAdwatch, totalAdwatch);
            SPlayerPrefs.SetDouble(PlayerPrefDefines.totalAdRevenue, totalAdRevenue);
        }



        protected override void OnDispose() 
        {
        
        }

        //得到广告源
        private EAdSource GetAdSource()
        {
            return eAdSource;
        }

    }
}
