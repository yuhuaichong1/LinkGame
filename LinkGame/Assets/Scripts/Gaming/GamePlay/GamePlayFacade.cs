using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayFacade
{
    public static Action ChangeTipCountShow;          //�ı���ʾ���ܵĴ�����ʾ
    public static Action ChangeRefushCountShow;       //�ı�ˢ�¹��ܵĴ�����ʾ
    public static Action ChangeRemoveCountShow;       //�ı��Ƴ����ܵĴ�����ʾ

    public static Action CreateLevel;                     //�����ؿ�
    public static Action<Vector2, Vector2> CheckIfLink;   //����Ƿ�������
    public static Action TipFunc;                         //��ʾ����
    public static Action RefushFunc;                      //ˢ�¹���
    public static Action RemoveFunc;                      //�Ƴ�����
    public static Action<int> ChangeTipCount;             //�ı���ʾ��������
    public static Action<int> ChangeRefushCount;          //�ı�ˢ�¹�������
    public static Action<int> ChangeRemoveCount;          //�ı��Ƴ���������
    public static Func<int> GetTipCount;                  //��ȡ��ʾ��������
    public static Func<int> GetRefushCount;               //��ȡˢ�¹�������
    public static Func<int> GetRemoveCount;               //��ȡ�Ƴ���������
}


    