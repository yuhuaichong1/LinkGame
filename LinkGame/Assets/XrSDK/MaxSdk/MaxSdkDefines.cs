using System;

// Max SDK 接口定义
public static class MaxSdkDefines
{
    #region 插屏广告
    public static Action ShowInterstitial;                                                  //展示插屏广告

    public static Action InterstitialLoadedEvent;                                           //插屏广告加载完毕，可以展示。
    public static Action<string, MaxSdkBase.ErrorCode> InterstitialFailedEvent;             //插屏广告加载失败
    public static Action<string, MaxSdkBase.ErrorCode> InterstitialFailedToDisplayEvent;    //插屏广告显示失败，自动加载下一个广告
    public static Action<double> InterstitialDismissedEvent;                                //插屏广告已隐藏，自动加载下一个广告

    public static Action PlayInterTimer;                                                    //开始插屏广告60s计时
    public static Action PauseInterTimer;                                                   //暂停插屏广告60s计时
    #endregion

    #region 激励视频
    public static Action ShowRewardedAd;                                                    //展示激励视频

    public static Action RewardedAdLoadedEvent;                                             //激励视频加载完毕，可以展示
    public static Action<string, MaxSdkBase.ErrorCode> RewardedAdFailedEvent;               //激励视频加载失败
    public static Action<string, MaxSdkBase.ErrorCode> RewardedAdFailedToDisplayEvent;      //激励视频显示失败，建议加载下一个广告
    public static Action RewardedAdDismissedEvent;                                          //激励视频已经隐藏，开始下载下一个激励视频
    public static Action<double> RewardedAdReceivedRewardEvent;                             //激励视频展示完毕，玩家应该获得奖励

    public static Func<double> GetRewardedAdRevenue;                                         //获取激励视频的ecpm
    public static Action failShowAdButReward;                                                //广告未获取时的回调
    public static Action stopfailShowAdButReward;                                            //如果failShowAdButReward计时中可以加载广告了，则应该中断计时
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
