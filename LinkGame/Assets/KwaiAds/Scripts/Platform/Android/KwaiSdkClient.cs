#if UNITY_ANDROID
using KwaiAds.Scripts.Api;
using KwaiAds.Scripts.Api.Interstitial;
using KwaiAds.Scripts.Api.Reward;
using KwaiAds.Scripts.Common;

namespace KwaiAds.Scripts.Platforms.Android
{
    public class KwaiSdkClient: ISDK
    {
        public void Init(KwaiAdConfig config, InitResultCallback initResultCallback)
        {
            KwaiAdSDKInit.Initialize(config, initResultCallback);
        }

        public IRewardAdController getRewardAdController()
        {
            return new KwaiRewardAdController();
        }

        public IInterstitialAdController getInterstitialAdController()
        {
            return new KwaiInterstitialAdController();
        }
    }
}
#endif