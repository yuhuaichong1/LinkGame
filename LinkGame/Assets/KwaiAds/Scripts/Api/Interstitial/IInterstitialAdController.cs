using KwaiAds.Scripts.Common;

namespace KwaiAds.Scripts.Api.Interstitial
{
    public interface IInterstitialAdController : IKwaiAdController<KwaiInterstitialAdRequest, IInterstitialAdListener, IInterstitialAdLoadListener>

    {
        bool Load(KwaiInterstitialAdRequest request, IInterstitialAdListener adListener, IInterstitialAdLoadListener AdLoadListener);
    }
}