using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ƶ������
public static class AudioDefines
{
    public static Action PlayBgm;
    public static Action StopBgm;
    public static Action<EAudioType> PlayEffect;
}

//��Ƶ����
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
