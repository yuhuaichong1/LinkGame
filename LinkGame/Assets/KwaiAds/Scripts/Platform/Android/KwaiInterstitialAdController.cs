#if UNITY_ANDROID
using BigoAds.Scripts.Platforms.Android;
using KwaiAds.Scripts.Api;
using KwaiAds.Scripts.Api.Interstitial;
using UnityEngine;

namespace KwaiAds.Scripts.Platforms.Android
{
    public class KwaiInterstitialAdController : IInterstitialAdController
    {
        private const string TAG = "[KwaiAdSDK-InterstitialAdController]";
        private const string InterstitialAdConfigBuilderClassName = AndroidPlatformTool.ClassPackage + ".loader.business.interstitial.data.KwaiInterstitialAdConfig$Builder";
        private const string KwaiInterstitialAdListenerClassName = AndroidPlatformTool.ClassPackage + ".loader.business.interstitial.interf.IKwaiInterstitialAdListener";
        private const string AdLoadListenerClassName = AndroidPlatformTool.ClassPackage + ".loader.common.interf.AdLoadListener";
        private const string KwaiInterstitialAdRequestClassName = AndroidPlatformTool.ClassPackage + ".loader.business.interstitial.data.KwaiInterstitialAdRequest";
        private const string KwaiAdLoaderManagerMethodName = "getKwaiAdLoaderManager";
        private const string WithKwaiInterstitialAdListenerMethodName = "withKwaiInterstitialAdListener";
        private const string BuildInterstitialAdLoaderMethodName = "buildInterstitialAdLoader";

        private AndroidJavaClass _KwaiAdSDKInit;
        private AdLoadListener _AdLoadListener;
        private InterstitialAdListener _InterstitialAdListener;
        private AndroidJavaObject _KwaiInterstitialAd;

        public bool Load(KwaiInterstitialAdRequest request, IInterstitialAdListener adListener, IInterstitialAdLoadListener adLoadListener)
        {
            _KwaiAdSDKInit = Android.KwaiAdSDKInit.Instance.GetKwaiAdSDKClass();
            if (_KwaiAdSDKInit == null)
            {
                Debug.Log($"{TAG}: kwaiAdSDKInit is null.");
                return false;
            }

            var loaderManager = _KwaiAdSDKInit.CallStatic<AndroidJavaObject>(KwaiAdLoaderManagerMethodName);
            if (loaderManager == null)
            {
                Debug.Log($"{TAG}: loaderManager is null.");
                return false;
            }
            else
            {
                _AdLoadListener = new AdLoadListener(adLoadListener, this);
                _InterstitialAdListener = new InterstitialAdListener(adListener);
                var kwaiInterstitialAdConfig = new AndroidJavaObject(InterstitialAdConfigBuilderClassName, _AdLoadListener);
                if (kwaiInterstitialAdConfig == null)
                {
                    Debug.LogError($"{TAG}: Failed to create kwaiInterstitialAdConfig builder.");
                    return false;
                }
                kwaiInterstitialAdConfig.Call<AndroidJavaObject>(WithKwaiInterstitialAdListenerMethodName, _InterstitialAdListener);

                var kwaiInterstitialAdAdLoader = loaderManager.Call<AndroidJavaObject>(BuildInterstitialAdLoaderMethodName, kwaiInterstitialAdConfig.Call<AndroidJavaObject>("build"));
                var adRequest = new AndroidJavaObject(KwaiInterstitialAdRequestClassName, request.TagId);
                string floorPrice = request.ExtParams[Constants.Request.BID_FLOOR_PRICE];
                if (floorPrice != null && floorPrice.Length != 0)
                {
                    Debug.Log($"{TAG}: floorPrice: {floorPrice}.");
                    adRequest.Get<AndroidJavaObject>("extParams").Call<AndroidJavaObject>("put", Constants.Request.BID_FLOOR_PRICE, floorPrice);
                }
                kwaiInterstitialAdAdLoader.Call("loadAd", adRequest);
            }
            return true;
        }

        public void Show()
        {
            if (IsReady())
            {
                AndroidJavaObject currentActivity = AndroidPlatformTool.GetGameActivity();
                if (currentActivity == null)
                {
                    Debug.Log($"{TAG}: Current Game Activity not found.");
                }
                else
                {
                    Debug.Log($"{TAG}: start showing.");
                    _KwaiInterstitialAd.Call("show", currentActivity);
                }

            }
            else
            {
                Debug.Log($"{TAG}: IsLoaded: {IsLoaded()}, IsReady: {IsReady()}.");
            }
        }

        public void Destroy()
        {
            if (_AdLoadListener != null)
            {
                _AdLoadListener.Destory();
                _AdLoadListener = null;
            }

            if (_InterstitialAdListener != null)
            {
                _InterstitialAdListener.Destory();
            }

            _KwaiInterstitialAd = null;
        }

        public bool IsLoaded()
        {
            return _KwaiInterstitialAd != null;
        }

        public bool IsReady()
        {
            return IsLoaded() && _KwaiInterstitialAd.Call<bool>("isReady");
        }

        public void NotifyLoss()
        {
            if (_KwaiInterstitialAd != null)
            {
                _KwaiInterstitialAd.Call("getBidController", "sendBidLose");
            }
        }

        public void NotifyWin()
        {
            if (_KwaiInterstitialAd != null)
            {
                _KwaiInterstitialAd.Call("getBidController", "sendBidWin");
            }
        }

        private class AdLoadListener : AndroidJavaProxy
        {
            private IInterstitialAdLoadListener _InterstitialAdLoadListener;
            private KwaiInterstitialAdController _KwaiInterstitialAdController;
            public AdLoadListener(IInterstitialAdLoadListener interstitialAdLoadListener, KwaiInterstitialAdController kwaiInterstitialAdController) : base(AdLoadListenerClassName)
            {
                this._InterstitialAdLoadListener = interstitialAdLoadListener;
                this._KwaiInterstitialAdController = kwaiInterstitialAdController;
            }

            public void onAdLoadStart(string trackId)
            {
                Debug.Log($"{TAG}: onAdLoadStart");
                if (_InterstitialAdLoadListener != null)
                {
                    _InterstitialAdLoadListener.OnAdLoadStart(trackId);
                }
            }

            public void onAdLoadSuccess(string trackId, AndroidJavaObject kwaiInterstitialAd)
            {
                _KwaiInterstitialAdController._KwaiInterstitialAd = kwaiInterstitialAd;
                string price = kwaiInterstitialAd.Call<string>("getPrice");
                Debug.Log($"{TAG}: onAdLoaded: {kwaiInterstitialAd.Call<string>("getPrice")}");
                if (_InterstitialAdLoadListener != null)
                {
                    _InterstitialAdLoadListener.OnAdLoadSuccess(trackId, price);
                }
            }

            public void onAdLoadFailed(string trackId, AndroidJavaObject kwaiError)
            {
                int code = kwaiError.Call<int>("getCode");
                string msg = kwaiError.Call<string>("getMsg");
                Debug.LogError($"{TAG}: onAdLoadFailed code = {code} msg = {msg}");
                if (_InterstitialAdLoadListener != null)
                {
                    _InterstitialAdLoadListener.OnAdLoadFailed(trackId, code, msg);
                }
            }

            public void Destory()
            {
                _InterstitialAdLoadListener = null;
                _KwaiInterstitialAdController = null;
            }
        }

        private class InterstitialAdListener : AndroidJavaProxy
        {
            private IInterstitialAdListener _InterstitialAdListener;

            public InterstitialAdListener(IInterstitialAdListener interstitialAdListener) : base(KwaiInterstitialAdListenerClassName)
            {
                _InterstitialAdListener = interstitialAdListener;
            }

            public void onAdShow()
            {
                Debug.Log($"{TAG}: onAdShow");
                if (_InterstitialAdListener != null)
                {
                    _InterstitialAdListener.OnAdShow();
                }
            }

            public void onAdShowFailed(AndroidJavaObject error)
            {
                int code = error.Call<int>("getCode");
                string msg = error.Call<string>("getMsg");
                Debug.LogError($"{TAG}: onAdShowFailed code = {code} msg = {msg}");
                if (_InterstitialAdListener != null)
                {
                    _InterstitialAdListener.OnAdShowFailed(code, msg);
                }
            }

            public void onAdClick()
            {
                Debug.Log($"{TAG}: onAdClick");
                if (_InterstitialAdListener != null)
                {
                    _InterstitialAdListener.OnAdClick();
                }
            }

            public void onAdClose()
            {
                Debug.Log($"{TAG}: onAdClose");
                if (_InterstitialAdListener != null)
                {
                    _InterstitialAdListener.OnAdClose();
                }
            }

            public void onAdPlayComplete()
            {
                Debug.Log($"{TAG}: onAdPlayComplete");
                if (_InterstitialAdListener != null)
                {
                    _InterstitialAdListener.OnAdPlayComplete();
                }
            }

            public void Destory()
            {
                _InterstitialAdListener = null;
            }
        }
    }
}
#endif