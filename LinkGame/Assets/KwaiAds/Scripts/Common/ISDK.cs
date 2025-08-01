using KwaiAds.Scripts.Api;
using KwaiAds.Scripts.Api.Interstitial;
using KwaiAds.Scripts.Api.Reward;

namespace KwaiAds.Scripts.Common
{
    public interface ISDK
    {
        void Init(KwaiAdConfig config, InitResultCallback initResultCallback);

        IRewardAdController getRewardAdController();

        IInterstitialAdController getInterstitialAdController();
    }
}