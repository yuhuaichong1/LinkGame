using System;
using static MaxSdkBase;

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

        private int totalAdwatch;//�ۼƹ��ۿ�����
        private double totalAdRevenue;//�ۼƹ������
        private int currentAdwatch;//���ֹ��ۿ�����

        private string countryCode;//������
        private string notReadyStr;//δ׼���ô����ֶ�

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

        //��������
        private void LoadData()
        {
            totalAdwatch = SPlayerPrefs.GetInt(PlayerPrefDefines.totalAdwatch);
            totalAdRevenue = SPlayerPrefs.GetDouble(PlayerPrefDefines.totalAdRevenue, true);
        }

        //���ص����
        private void AdCallback()
        {
            #region MAX�������
            MaxSdkDefines.OnRewardedAdDisplayedEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Reward, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);
            };
            MaxSdkDefines.OnRewardedAdFailedToDisplayEvent = (adUnitId, ErrorInfo, AdInfo) =>
            {
                string str = $"{ErrorInfo.Code} : {ErrorInfo.Message}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Reward, this.eAdSource, str, AdInfo.NetworkName, AdInfo.RevenuePrecision);

                failedAction?.Invoke(ErrorInfo.Message);
            };
            MaxSdkDefines.OnRewardAdRevenuePaidEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Reward, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);

                currentAdwatch += 1;
                totalAdwatch += 1;
                totalAdRevenue += AdInfo.Revenue;
                CheckAdTotalData();
                
                successfulAction?.Invoke();
            };
            #endregion

            #region KwaiNetWork�������
            KwaiNetWorkDefines.KNW_OnRAdShow = () =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Reward, this.eAdSource, (float)kwaiEcpm, KwaiNetWorkDefines.GetRewardedAdName(), "kwai");
            };
            KwaiNetWorkDefines.KNW_OnRAdShowFailed = (code, msg) =>
            {
                string str = $"{code} : {msg}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Reward, this.eAdSource, str, KwaiNetWorkDefines.GetRewardedAdName(), "kwai");

                failedAction?.Invoke(msg);
            };
            KwaiNetWorkDefines.KNW_OnRAdPlayComplete = () =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Reward, this.eAdSource, (float)kwaiEcpm, KwaiNetWorkDefines.GetRewardedAdName(), "kwai");

                currentAdwatch += 1;
                totalAdwatch += 1;
                totalAdRevenue += kwaiEcpm / 1000;
                CheckAdTotalData();

                successfulAction?.Invoke();
            };
            #endregion

            #region MAX�������
            MaxSdkDefines.OnInterstitialDisplayedEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdStart(EAdtype.Interstitial, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);
            };
            MaxSdkDefines.OnInterstitialFailedToDisplayEvent = (adUnitId, ErrorInfo, AdInfo) =>
            {
                string str = $"{ErrorInfo.Code} : {ErrorInfo.Message}";
                D.Error(str);
                TDAnalyticsManager.AdFail(EAdtype.Interstitial, this.eAdSource, str, AdInfo.NetworkName, AdInfo.RevenuePrecision);

                failedAction?.Invoke(ErrorInfo.Message);
            };
            MaxSdkDefines.OnInterstitialRevenuePaidEvent = (adUnitId, AdInfo) =>
            {
                TDAnalyticsManager.AdComplete(EAdtype.Interstitial, this.eAdSource, (float)AdInfo.Revenue * 1000f, AdInfo.NetworkName, AdInfo.RevenuePrecision);

                currentAdwatch += 1;
                totalAdwatch += 1;
                totalAdRevenue += AdInfo.Revenue;
                CheckAdTotalData();

                successfulAction?.Invoke();
            };
            #endregion

            #region ����
            MaxSdkDefines.RewardAdNotReadyEvent += () =>
            {
                UIManager.Instance.OpenNotice(notReadyStr);
            };
            #endregion
        }

        //���ż������
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

        //���Ų������
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
                    break;
                case 10:
                    TDAnalyticsManager.Times_10_Ad(totalAdRevenue);
                    break;
                case 15:
                    TDAnalyticsManager.Times_15_Ad(totalAdRevenue);
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

        //�õ����Դ
        private EAdSource GetAdSource()
        {
            return eAdSource;
        }

    }
}
