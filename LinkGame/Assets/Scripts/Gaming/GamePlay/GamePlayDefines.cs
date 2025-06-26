using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayDefines
{
    public static Action ChangeFuncTipCount;              //�ı���ʾ���ܵĴ�����ʾ
    public static Action ChangeFuncRefushCount;           //�ı�ˢ�¹��ܵĴ�����ʾ
    public static Action ChangeFuncRemoveCount;           //�ı��Ƴ����ܵĴ�����ʾ

    public static Action CreateLevel;                     //�����ؿ�
    public static Action<Vector2, Vector2> CheckIfLink;   //����Ƿ�������
    public static Action TipFunc;                         //��ʾ����
    public static Action RefushFunc;                      //ˢ�¹���
    public static Action RemoveFunc;                      //�Ƴ�����
}


    