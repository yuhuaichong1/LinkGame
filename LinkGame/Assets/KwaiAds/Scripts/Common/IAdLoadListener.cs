namespace KwaiAds.Scripts.Common
{
    public interface IRwardAdListener
    {
        void OnAdLoadStart(string trackId);

        void OnAdLoadSuccess(string trackId, string price);

        void OnAdLoadFailed(string trackId, int code, string msg);
    }
}
