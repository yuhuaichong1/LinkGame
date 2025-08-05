using System;

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

        protected override void OnLoad() 
        {
            TDAnalyticsManager = ModuleMgr.Instance.TDAnalyticsManager;

            FacadeAd.PlayRewardAd += PlayRewardAd;
            FacadeAd.PlayInterAd += PlayInterAd;
            FacadeAd.GetAdSource = GetAdSource;

            LoadData();

            AdCallback();
        }

        private void LoadData()
        {
            totalAdwatch = SPlayerPrefs.GetInt(PlayerPrefDefines.totalAdwatch);
            totalAdRevenue = SPlayerPrefs.GetDouble(PlayerPrefDefines.totalAdRevenue);
        }

        private void AdCallback()
        {
            #region MAX激励广告
            MaxSdkDefines.OnRewardedAdDisplayedEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Reward, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName);
            };
            MaxSdkDefines.OnRewardedAdFailedToDisplayEvent = (adUnitId, ErrorInfo, AdInfo) =>
            {
                string str = $"{ErrorInfo.Code} : {ErrorInfo.Message}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Reward, this.eAdSource, str, AdInfo.NetworkName);
                failedAction?.Invoke(ErrorInfo.Message);
            };
            MaxSdkDefines.OnRewardAdRevenuePaidEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Reward, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName);
                currentAdwatch += 1;
                totalAdwatch += 1;
                successfulAction?.Invoke();
            };
            #endregion

            #region KwaiNetWork激励广告
            KwaiNetWorkDefines.KNW_OnRAdShow = () =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Reward, this.eAdSource, (float)kwaiEcpm, KwaiNetWorkDefines.GetRewardedAdName());
            };
            KwaiNetWorkDefines.KNW_OnRAdShowFailed = (code, msg) =>
            {
                string str = $"{code} : {msg}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Reward, this.eAdSource, str, KwaiNetWorkDefines.GetRewardedAdName());
                failedAction?.Invoke(msg);
            };
            KwaiNetWorkDefines.KNW_OnRAdPlayComplete = () =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Reward, this.eAdSource, (float)kwaiEcpm, KwaiNetWorkDefines.GetRewardedAdName());
                currentAdwatch += 1;
                totalAdwatch += 1;
                successfulAction?.Invoke();
            };
            #endregion

            #region MAX插屏广告
            MaxSdkDefines.OnInterstitialDisplayedEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Interstitial, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName);
            };
            MaxSdkDefines.OnInterstitialFailedToDisplayEvent = (adUnitId, ErrorInfo, AdInfo) =>
            {
                string str = $"{ErrorInfo.Code} : {ErrorInfo.Message}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Interstitial, this.eAdSource, str, AdInfo.NetworkName);
                failedAction?.Invoke(ErrorInfo.Message);
            };
            MaxSdkDefines.OnInterstitialRevenuePaidEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Interstitial, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName);
                currentAdwatch += 1;
                totalAdwatch += 1;
                successfulAction?.Invoke();
            };
            #endregion
        }

        public void PlayRewardAd(EAdSource eAdSource, Action successAction, Action<string> failAction = null)
        {
            this.eAdSource = eAdSource;
            maxEcpm = MaxSdkDefines.GetRewardedAdRevenue();
            kwaiEcpm = KwaiNetWorkDefines.GetRewardedAdECPM();
            successfulAction = successAction;
            failedAction = failAction;

            if (maxEcpm >= kwaiEcpm)
            {
                MaxSdkDefines.ShowRewardedAd();
            }
            else
            {
                KwaiNetWorkDefines.KNW_ShowRewardedAd();
            }
        }

        public void PlayInterAd(EAdSource eAdSource, Action successAction, Action<string> failAction = null)
        {
            this.eAdSource = eAdSource;
            successfulAction = successAction;
            failedAction = failAction;

            MaxSdkDefines.ShowInterstitial();
        }

        protected override void OnDispose() 
        {
        
        }


        private EAdSource GetAdSource()
        {
            return eAdSource;
        }

    }
}
