#if UNITY_ANDROID
using KwaiAds.Scripts.Common;

namespace KwaiAds.Scripts.Platforms.Android
{
    class AndroidClientFactory : IClientFactory
    {
        public ISDK BuildSDKClient()
        {
            return new KwaiSdkClient();
        }
    }
}
#endif