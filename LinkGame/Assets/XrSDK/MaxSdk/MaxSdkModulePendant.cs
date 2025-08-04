using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace XrSDK
{
    //Max Sdk Ä£¿é¹Ò¼þ
    [RegisterModule("Max SDK Module")]
    public class MaxSdkModulePendant : BaseModulePendant
    {
        public string MaxSdkKey = "";                                     //ENTER_MAX_SDK_KEY_HERE
        public string InterstitialAdUnitId_Android = "";                  //ENTER_INTERSTITIAL_AD_UNIT_ID_HERE
        public string RewardedAdUnitId_Android = "";                      //ENTER_REWARD_AD_UNIT_ID_HERE
        public string BannerAdUnitId_Android = "";                        //ENTER_BANNER_AD_UNIT_ID_HERE
        public string MRecAdUnitId_Android = "";                          //ENTER_MREC_AD_UNIT_ID_HERE
        public string InterstitialAdUnitId_Ios = "";                          //ENTER_INTERSTITIAL_AD_UNIT_ID_HERE
        public string RewardedAdUnitId_Ios = "";                              //ENTER_REWARD_AD_UNIT_ID_HERE
        public string BannerAdUnitId_Ios = "";                                //ENTER_BANNER_AD_UNIT_ID_HERE
        public string MRecAdUnitId_Ios = "";                                  //ENTER_MREC_AD_UNIT_ID_HERE
        public override string ModuleName => "MaxSDK";


        public override void CreateModule()
        {
            MaxSdkData data = new MaxSdkData();
#if UNITY_IOS

            data.InterstitialAdUnitId = InterstitialAdUnitId_Ios;
            data.RewardedAdUnitId = RewardedAdUnitId_Ios;
            data.BannerAdUnitId = BannerAdUnitId_Ios;
            data.MRecAdUnitId = MRecAdUnitId_Ios;

#elif UNITY_ANDROID || UNITY_EDITOR // UNITY_ANDROID
            data.InterstitialAdUnitId = InterstitialAdUnitId_Android;
            data.RewardedAdUnitId = RewardedAdUnitId_Android;
            data.BannerAdUnitId = BannerAdUnitId_Android;
            data.MRecAdUnitId = MRecAdUnitId_Android;
#else
            return;
#endif
            MaxSdkModule module = new MaxSdkModule(data);
            module.Load();
        }
    }

}