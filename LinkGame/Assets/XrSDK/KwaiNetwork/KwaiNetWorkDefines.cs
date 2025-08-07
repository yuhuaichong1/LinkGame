using System;


public static class KwaiNetWorkDefines
{
    #region 激励视频
    public static Action KNW_ShowRewardedAd;                            //展示激励广告

    public static Action LoadRewardAd;                                  //加载激励广告
    public static Action<string, int, string> KNW_OnRAdLoadFailed;      //激励广告加载失败
    public static Action<string> KNW_OnRAdLoadStart;                    //激励广告开始加载
    public static Action<string, string> KNW_OnRAdLoadSuccess;          //激励广告加载成功

    public static Action KNW_OnRAdClick;                                //激励广告被点击
    public static Action KNW_OnRAdClose;                                //激励广告关闭
    public static Action KNW_OnRAdPlayComplete;                         //激励广告播放完成
    public static Action KNW_OnRAdShow;                                 //激励广告开始播放
    public static Action<int, string> KNW_OnRAdShowFailed;              //激励广告播放失败
    public static Action KNW_OnRewardEarned;                            //激励广告获取奖励

    public static Func<double> GetRewardedAdECPM;                       //获取激励视频的ecpm
    public static Func<float> GetRewardedAdRevenue;                    //获取激励视频的奖励
    public static Func<string> GetRewardedAdName;                       //获取激励视频的平台名称
    public static Action failShowAdButReward;                           //广告未获取时的回调
    public static Action stopfailShowAdButReward;                       //如果failShowAdButReward计时中可以加载广告了，则应该中断计时

    public static Action PlayInterTimer;                                //开始插屏广告60s计时
    public static Action PauseInterTimer;                               //暂停插屏广告60s计时
    #endregion

    #region 插屏广告
    public static Action KNW_ShowInterstitial;                          //展示插屏广告

    public static Action LoadInterstitial;                              //加载插屏广告
    public static Action<string, int, string> KNW_OnIAdLoadFailed;      //插屏广告加载失败
    public static Action<string> KNW_OnIAdLoadStart;                    //插屏广告开始加载
    public static Action<string, string> KNW_OnIAdLoadSuccess;          //插屏广告加载成功

    public static Action KNW_OnIAdClick;                                //插屏广告被点击
    public static Action KNW_OnIAdClose;                                //插屏广告关闭
    public static Action<double> KNW_OnIAdPlayComplete;                 //插屏广告播放完成
    public static Action KNW_OnIAdShow;                                 //插屏广告开始播放
    public static Action<int, string> KNW_OnIAdShowFailed;              //插屏广告播放失败


    #endregion
}
