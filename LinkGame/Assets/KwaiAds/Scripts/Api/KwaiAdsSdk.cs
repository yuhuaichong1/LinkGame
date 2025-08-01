using System;
using KwaiAds.Scripts.Common;

namespace KwaiAds.Scripts.Api
{
    public static class KwaiAdsSdk
    {
        private static IClientFactory _clientFactory;

        private static ISDK _sdk;

        internal static ISDK SDK
        {
            get
            {
                if (_sdk == null)
                {
                    _sdk = GetClientFactory().BuildSDKClient();
                }

                return _sdk;
            }
        }

        internal static IClientFactory GetClientFactory()
        {
            if (_clientFactory != null)
            {
                return _clientFactory;
            }

            _clientFactory =
#if UNITY_ANDROID
                new KwaiAds.Scripts.Platforms.Android.AndroidClientFactory();
#else
                null;
            throw new PlatformNotSupportedException();
#endif
            return _clientFactory;
        }

        /// Starts the Kwai SDK
        /// @warning Call this method as early as possible to reduce  ad request fail.
        /// @param config SDK configuration
        /// @param callback Callback for starting the Kwai SDK
        /// ////
        public static void Initialize(KwaiAdConfig config, InitResultCallback initResultCallback)
        {
            SDK.Init(config, initResultCallback);
        }
    }
}

