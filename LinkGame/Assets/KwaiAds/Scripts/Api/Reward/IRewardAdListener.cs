using KwaiAds.Scripts.Common;

namespace KwaiAds.Scripts.Api.Reward
{
    public interface IRewardAdListener : IAdListener
    {
        void OnRewardEarned();
    }
}
