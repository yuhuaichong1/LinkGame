using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoodDefine
{
    public static int width = 124;//Ĭ�Ͽ�
    public static int height = 124;//Ĭ�ϸ�
    public static int correction = 0;//����߾ಹ��
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
