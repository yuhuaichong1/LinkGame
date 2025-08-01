#if UNITY_ANDROID
using BigoAds.Scripts.Platforms.Android;
using KwaiAds.Scripts.Api;
using KwaiAds.Scripts.Api.Reward;
using UnityEngine;

namespace KwaiAds.Scripts.Platforms.Android
{
    public class KwaiRewardAdController : IRewardAdController
    {
        private const string TAG = "[KwaiAdSDK-RewardAdController]";
        private const string RewardedAdConfigBuilderClassName = AndroidPlatformTool.ClassPackage + ".loader.business.reward.data.KwaiRewardAdConfig$Builder";
        private const string KwaiRewardAdListenerClassName = AndroidPlatformTool.ClassPackage + ".loader.business.reward.interf.IKwaiRewardAdListener";
        private const string AdLoadListenerClassName = AndroidPlatformTool.ClassPackage + ".loader.common.interf.AdLoadListener";
        private const string KwaiRewardAdRequestClassName = AndroidPlatformTool.ClassPackage + ".loader.business.reward.data.KwaiRewardAdRequest";
        private const string KwaiAdLoaderManagerMethodName = "getKwaiAdLoaderManager";
        private const string WithKwaiRewardAdListenerMethodName = "withKwaiRewardAdListener";
        private const string BuildRewardAdLoaderMethodName = "buildRewardAdLoader";

        private AndroidJavaClass _KwaiAdSDKInit;
        private AdLoadListener _AdLoadListener;
        private RewardAdListener _RewardAdListener;
        private AndroidJavaObject _KwaiRewardAd;

        public bool Load(KwaiRewardAdRequest request, IRewardAdListener rewardAdListener, IRewardAdLoadListener rewardAdLoadListener)
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
                _AdLoadListener = new AdLoadListener(rewardAdLoadListener, this);
                _RewardAdListener = new RewardAdListener(rewardAdListener);
                var kwaiRewardAdConfig = new AndroidJavaObject(RewardedAdConfigBuilderClassName, _AdLoadListener);
                if (kwaiRewardAdConfig == null)
                {
                    Debug.LogError($"{TAG}:Failed to create kwaiRewardAdConfig builder.");
                    return false;
                }
                kwaiRewardAdConfig.Call<AndroidJavaObject>(WithKwaiRewardAdListenerMethodName, _RewardAdListener);

                var kwaiRewardAdAdLoader = loaderManager.Call<AndroidJavaObject>(BuildRewardAdLoaderMethodName, kwaiRewardAdConfig.Call<AndroidJavaObject>("build"));
                var adRequest = new AndroidJavaObject(KwaiRewardAdRequestClassName, request.TagId);
                string floorPrice = request.ExtParams[Constants.Request.BID_FLOOR_PRICE];
                if (floorPrice != null && floorPrice.Length != 0)
                {
                    Debug.Log($"{TAG}: floorPrice: {floorPrice}.");
                    adRequest.Get<AndroidJavaObject>("extParams").Call<AndroidJavaObject>("put", Constants.Request.BID_FLOOR_PRICE, floorPrice);
                }
                kwaiRewardAdAdLoader.Call("loadAd", adRequest);
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
                    _KwaiRewardAd.Call("show", currentActivity);
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

            if (_RewardAdListener != null)
            {
                _RewardAdListener.Destory();
            }

            _KwaiRewardAd = null;
        }

        public bool IsLoaded()
        {
            return _KwaiRewardAd != null;
        }

        public bool IsReady()
        {
            return IsLoaded() && _KwaiRewardAd.Call<bool>("isReady");
        }

        public void NotifyLoss()
        {
            if (_KwaiRewardAd != null)
            {
                _KwaiRewardAd.Call("getBidController", "sendBidLose");
            }
        }

        public void NotifyWin()
        {
            if (_KwaiRewardAd != null)
            {
                _KwaiRewardAd.Call("getBidController", "sendBidWin");
            }
        }

        private class AdLoadListener : AndroidJavaProxy
        {
            private IRewardAdLoadListener _RewardAdLoadListener;
            private KwaiRewardAdController _KwaiRewardAdController;
            public AdLoadListener(IRewardAdLoadListener rewardAdListener, KwaiRewardAdController kwaiRewardAdController) : base(AdLoadListenerClassName)
            {
                this._RewardAdLoadListener = rewardAdListener;
                this._KwaiRewardAdController = kwaiRewardAdController;
            }

            public void onAdLoadStart(string trackId)
            {
                Debug.Log($"{TAG}: onAdLoadStart");
                if (_RewardAdLoadListener != null)
                {
                    _RewardAdLoadListener.OnAdLoadStart(trackId);
                }
            }

            public void onAdLoadSuccess(string trackId, AndroidJavaObject kwaiRewardAd)
            {
                _KwaiRewardAdController._KwaiRewardAd = kwaiRewardAd;
                string price = kwaiRewardAd.Call<string>("getPrice");
                Debug.Log($"{TAG}: onAdLoaded: {kwaiRewardAd.Call<string>("getPrice")}");
                if (_RewardAdLoadListener != null)
                {
                    _RewardAdLoadListener.OnAdLoadSuccess(trackId, price);
                }
            }

            public void onAdLoadFailed(string trackId, AndroidJavaObject kwaiError)
            {
                int code = kwaiError.Call<int>("getCode");
                string msg = kwaiError.Call<string>("getMsg");
                Debug.LogError($"{TAG}: onAdLoadFailed code = {code} msg = {msg}");
                if (_RewardAdLoadListener != null)
                {
                    _RewardAdLoadListener.OnAdLoadFailed(trackId, code, msg);
                }
            }

            public void Destory()
            {
                _RewardAdLoadListener = null;
                _KwaiRewardAdController = null;
            }
        }

        private class RewardAdListener : AndroidJavaProxy
        {
            private IRewardAdListener _RewardAdListener;

            public RewardAdListener(IRewardAdListener rewardAdListener) : base(KwaiRewardAdListenerClassName) {
                _RewardAdListener = rewardAdListener;
            }

            public void onAdShow()
            {
                Debug.Log($"{TAG}: onAdShow");
                if (_RewardAdListener != null)
                {
                    _RewardAdListener.OnAdShow();
                }
            }

            public void onAdShowFailed(AndroidJavaObject error)
            {
                int code = error.Call<int>("getCode");
                string msg = error.Call<string>("getMsg");
                Debug.LogError($"{TAG}: onAdShowFailed code = {code} msg = {msg}");
                if (_RewardAdListener != null)
                {
                    _RewardAdListener.OnAdShowFailed(code, msg);
                }
            }

            public void onAdClick()
            {
                Debug.Log($"{TAG}: onAdClick");
                if (_RewardAdListener != null)
                {
                    _RewardAdListener.OnAdClick();
                }
            }

            public void onAdPlayComplete()
            {
                Debug.Log($"{TAG}: onAdPlayComplete");
                if (_RewardAdListener != null)
                {
                    _RewardAdListener.OnAdPlayComplete();
                }
            }

            public void onRewardEarned()
            {
                Debug.Log($"{TAG}: onRewarded");
                if (_RewardAdListener != null)
                {
                    _RewardAdListener.OnRewardEarned();
                }
            }

            public void onAdClose()
            {
                Debug.Log($"{TAG}: onAdClose");
                if (_RewardAdListener != null)
                {
                    _RewardAdListener.OnAdClose();
                }
            }

            public void Destory()
            {
                _RewardAdListener = null;
            }
        }
    }
}
#endif