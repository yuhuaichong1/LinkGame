using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeAd
{
    public static Action<EAdSource, Action, Action<string>> PlayRewardAd;       //���ż������
    public static Action<EAdSource, Action, Action<string>> PlayInterAd;        //���Ų������
}
