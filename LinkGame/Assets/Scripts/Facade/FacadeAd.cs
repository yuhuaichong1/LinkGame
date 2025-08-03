using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeAd
{
    public static Action<EAdSource, Action, Action<string>> PlayRewardAd;       //播放激励广告
    public static Action<EAdSource, Action, Action<string>> PlayInterAd;        //播放插屏广告
}
