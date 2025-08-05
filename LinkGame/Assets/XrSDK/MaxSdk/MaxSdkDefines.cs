using System;
using static MaxSdkBase;

// Max SDK 接口定义
public static class MaxSdkDefines
{
    #region 插屏广告
    public static Action ShowInterstitial;                                                  //展示插屏广告

    public static Action<string, AdInfo> OnInterstitialLoadedEvent;                         //插屏广告加载成功
    public static Action<string, ErrorInfo> OnInterstitialLoadFailedEvent;                  //插屏广告加载失败
    public static Action<string, AdInfo> OnInterstitialDisplayedEvent;                      //插屏广告展示成功
    public static Action<string, ErrorInfo, AdInfo> OnInterstitialFailedToDisplayEvent;     //插屏广告展示失败
    public static Action<string, AdInfo> OnInterstitialDismissedEvent;                      //插屏广告被隐藏
    public static Action<string, AdInfo> OnInterstitialClickedEvent;                        //插屏广告被点击
    public static Action<string, AdInfo> OnInterstitialRevenuePaidEvent;                    //插屏广告奖励给予奖励完毕
    #endregion

    #region 激励视频
    public static Action ShowRewardedAd;                                                    //展示激励视频

    public static Action<string, AdInfo> OnRewardedAdLoadedEvent;                           //激励广告加载成功
    public static Action<string, ErrorInfo> OnRewardedAdLoadFailedEvent;                    //激励广告加载失败
    public static Action<string, AdInfo> OnRewardedAdDisplayedEvent;                        //激励广告展示成功
    public static Action<string, ErrorInfo, AdInfo> OnRewardedAdFailedToDisplayEvent;       //激励广告展示失败
    public static Action<string, AdInfo> OnRewardAdClickedEvent;                            //激励广告被点击
    public static Action<string, AdInfo> OnRewardAdHiddenEvent;                             //激励广告被隐藏
    public static Action<string, Reward, AdInfo> OnRewardAdReceivedRewardEvent;             //激励广告展示完毕，给予奖励
    public static Action<string, AdInfo> OnRewardAdRevenuePaidEvent;                        //激励广告奖励给予奖励完毕

    public static Func<double> GetRewardedAdRevenue;                                         //获取激励视频的ecpm
    public static Action RewardAdNotReadyEvent;                                              //激励广告未准备完成回调
    #endregion

    #region 横幅广告
    public static Action ToggleBannerVisibility;                                            //横幅广告展示or关闭
    public static Action BannerAdLoadedEvent;                                               //横幅广告加载失败
    public static Action<string, MaxSdkBase.ErrorCode> BannerAdFailedEvent;                 //横幅广告加载失败


    #endregion

    #region MREC
    public static Action ToggleMRecVisibility;                                              //展示MREC广告
    public static Action MRecAdLoadedEvent;                                                 //MREC广告加载完成
    public static Action<string, MaxSdkBase.ErrorCode> MRecAdFailedEvent;                   //MREC广告加载失败
    #endregion
}
