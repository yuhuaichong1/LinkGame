using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoodDefine
{
    public static int width = 96;//Ĭ�Ͽ�
    public static int height = 112;//Ĭ�ϸ�
    public static int width_correction = 0;//������ಹ��
    public static int height_correction = 100;//����߼�ಹ��

    public static float moveTime = 0.1f;//�����ƶ�ʱ��
}

//��Ʒ�ƶ�����ö��
public enum EGoodMoveDic
{
    None = 0,//���ƶ�
    Up,//�����ƶ�
    Down,//�����ƶ�
    Left,//�����ƶ�
    Right,//�����ƶ�
    UpDown_Away,//����Զ��
    UpDown_Closer,//���¾�£
    LeftRight_Away,//����Զ��
    LeftRight_Closer,//���Ҿ�£
}

//��Ʒ״̬
public enum EGoodStatus
{
    None,//����״̬
    Hide,//����״̬
    Stone,//�ϰ�״̬
}
