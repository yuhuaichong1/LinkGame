using System;

//��ʱ�ڵ��¼��ṹͼ
public struct timingActions
{
    public float timing;//Ŀ��ڵ�
    public Action<float> clockAction;//�¼�
    public ClockActionType clockActionType;//��ʱ�ڵ��¼�����
}
