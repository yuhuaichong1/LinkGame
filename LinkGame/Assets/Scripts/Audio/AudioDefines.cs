using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音频定义类
public static class AudioDefines
{
    public static Action PlayBgm;
    public static Action StopBgm;
    public static Action<EAudioType> PlayEffect;
}

//音频类型
public enum EAudioType : int
{ 
    EBgm = 1,
    EButton,
    EReleaseHiddle,
    ECantMove,
    ESelect,
    EDeSelect,
    ElinkRemove,
    EGoodShuffle,
    EGoodMove,
    ELuckmount,
    EMoney,
    EReward,
    EWin,
}
