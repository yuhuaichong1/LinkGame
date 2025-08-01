namespace KwaiAds.Scripts.Common
{
    public interface IAdListener
    {
        public void OnAdShow();

        public void OnAdShowFailed(int code, string msg);

        public void OnAdClick();

        public void OnAdClose();

        public void OnAdPlayComplete();
    }
}
