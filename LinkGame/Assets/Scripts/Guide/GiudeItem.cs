using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideItem
{
    public int step;                            //��ǰ����
    public int nextStep;                        //��һ����
    public string note;                         //˵��
    public int backStep;                        //�����жϺ��ٴν���ʱ��ת����
    public bool ifBackPlay;                     //�����жϺ��ٴν���ʱ�Ƿ�ִ����ת�Ĳ���
    public bool ifNextStep;                     //���/����ʱ�������Ƿ�ǰ��һ��
    public bool ifNextPlay;                     //ǰ��һ�����Ƿ�ִ�н������Ĳ���
    public float autohiddenTime;                //�����Զ���ʧʱ��
    public string diglogContent;              //��ʾ�����ݣ���ӦLanguage��
    public string diglogPos;                 //��ʾ��λ��
    public string handPos;                   //��λ��
    public bool ifMask;                         //�Ƿ��к�ɫ����
    public string transparentPos;            //͸����λ��
    public string clickPos;                  //�̳̿ɵ������
    public Dictionary<string, string> extra;    //�����ֶ�
}
