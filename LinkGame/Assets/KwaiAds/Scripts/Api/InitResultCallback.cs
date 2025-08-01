namespace KwaiAds.Scripts.Api
{
    public interface InitResultCallback
    {
        // init success.
        void OnSuccess();
        // init fail.
        void OnFail(int code, string msg);
    }
}