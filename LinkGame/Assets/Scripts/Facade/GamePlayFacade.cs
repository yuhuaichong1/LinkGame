using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayFacade
{
    public static Action ChangeMoneyShow;             //�ı䵱ǰ��Ǯ��
    public static Action ChangeTipCountShow;          //�ı���ʾ���ܵĴ�����ʾ
    public static Action ChangeRefushCountShow;       //�ı�ˢ�¹��ܵĴ�����ʾ
    public static Action ChangeRemoveCountShow;       //�ı��Ƴ����ܵĴ�����ʾ

    public static Action CreateLevel;                     //�����ؿ�
    public static Action<Vector2, Vector2> CheckIfLink;   //����Ƿ�������
    public static Action TipFunc;                         //��ʾ����
    public static Func<bool> GetIfHintFunc;               //��ȡ��ǰ�Ƿ�����ʾ����״̬
    public static Action RefushFunc;                      //ˢ�¹���
    public static Action RemoveFunc;                      //�Ƴ�����
    public static Func<Good, bool> RemoveFunc2;           //�Ƴ�����2�������ִ���������Ƴ���
    public static Func<bool> GetIfRemoveFunc;             //��ȡ��ǰ�Ƿ����Ƴ�����״̬
    public static Func<EGoodMoveDic> ChangeDirection;     //����������
    public static Action<int> ChangeTipCount;             //�ı���ʾ��������
    public static Action<int> ChangeRefushCount;          //�ı�ˢ�¹�������
    public static Action<int> ChangeRemoveCount;          //�ı��Ƴ�����������Ŀǰ����Ϊ������������������
    public static Func<int> GetCurLevel;                  //�õ���ǰ�ؿ�
    public static Action NextLevel;                       //��һ�ؿ��������ݣ�
    public static Func<int> GetTipCount;                  //��ȡ��ʾ��������
    public static Func<int> GetRefushCount;               //��ȡˢ�¹�������
    public static Func<int> GetRemoveCount;               //��ȡ�Ƴ���������
    public static Action<EMapState> ChangeMapState;       //�ı�ؿ�״̬
    public static Func<EMapState> GetMapState;            //�õ��ؿ�״̬
    public static Action<Vec2> UpdateHiddleState;         //���±���״̬(��������Ʒ��Χ��״̬)
    public static Action<int, int, bool> RemoveHidden;    //�Ƴ�������ĳ��ĳ�еı�����Ч
    public static Action<Vec2> RemoveGood;                //�Ƴ������е�����
    public static Action<bool> UpdateMap;                 //���µ�ͼ���ݣ��Ƿ������һ�أ�
    public static Func<int> GetRow;                       //��ȡ�е�����
    public static Action<bool> SetIsCheckWrong;           //
    public static Action<object> CheckIdAdd;              //
    public static Func<int, Sprite> GetGoodIcon;          //��ȡ��ƷͼƬ
    public static Action<Vec2> Select;                    //ѡ�У�GamePlayModule�е�Select��
    public static Func<GameObject[][]> GetMAPGoods;       //��ȡ��ǰ�ؿ�����������
    public static Func<int> GetCurTotalLinkCount;         //�������������
    public static Func<int> GetCurLuckMomentCount;        //������ϻ����ۼ���������
    public static Action<int> SetCurLuckMomentCount;      //�������ϻ����ۼ�����������һ��������0��
    public static Func<Queue<int>> GetWithdrawableLevel;  //��ȡ����Ŀ��ؿ���
    public static Func<int> GetCurWLevel;                 //��ȡ��ǰ����Ŀ��ؿ�
    public static Func<bool> GetIsTutorial;               //��ȡ��ǰ�Ƿ�Ϊ������������
    public static Action<bool> SetIsTutorial;             //�����Ƿ�Ϊ������������
    public static Func<float> GetMapScale;                //�����Ļ����

    public static Func<Transform> GetMapTrans;              //��ȡ��������ĸ�����
    public static Func<Transform> GetObsTrans;              //��ȡ���ɵ���������ĸ�����
    public static Func<Transform> GetFlyMoneyTarget;        //�õ���Ǯ�ƶ��յ�Ŀ��
    public static Func<Transform> GetFlyMoneyTipOrgin;      //�õ���÷�Ǯ��ʾ���
    public static Func<EFuncType ,Transform> GetFuncTarget; //�õ��ɹ����ƶ��յ�Ŀ��

    public static Func<int> GetNumberGoodCanEat;            //��ȡʣ���������Ʒ����
    public static Func<string> GetRemainPCT;                //��ȡ�ؿ�����
}


    