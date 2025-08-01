using KwaiAds.Scripts.Common;

namespace KwaiAds.Scripts.Api.Reward
{
    public interface IRewardAdController : IKwaiAdController<KwaiRewardAdRequest, IRewardAdListener, IRewardAdLoadListener>

    {
        bool Load(KwaiRewardAdRequest request, IRewardAdListener adListener, IRewardAdLoadListener AdLoadListener); 
    }
}
