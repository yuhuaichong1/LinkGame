using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoodDefine
{
    public static int width = 90;//Ĭ�Ͽ�
    public static int height = 120;//Ĭ�ϸ�
    public static int width_correction = 0;//������ಹ��
    public static int height_correction = 100;//����߼�ಹ��
}

//��Ʒ�ƶ�����ö��
public enum EGoodMoveDic
{
    None,//���ƶ�
    Up,//�����ƶ�
    Down,//�����ƶ�
    Left,//�����ƶ�
    Right,//�����ƶ�
}

//��Ʒ״̬
public enum EGoodStatus
{
    None,//����״̬
    Hide,//����״̬
    Stone,//�ϰ�״̬
}
