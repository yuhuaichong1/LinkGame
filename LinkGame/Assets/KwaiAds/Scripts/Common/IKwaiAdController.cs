using KwaiAds.Scripts.Api;

namespace KwaiAds.Scripts.Common
{
    public interface IKwaiAdController<in T, in A, in L> : IClientBidding
        where T : KwaiRequest
        where A : IAdListener
        where L : IRwardAdListener
    {
        bool Load(T request, A adListener, L AdLoadListener);
        bool IsLoaded();
        bool IsReady();
        void Show();
        void Destroy();
    }
}