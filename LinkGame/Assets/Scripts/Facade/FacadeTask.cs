using System;
using System.Collections.Generic;

namespace XrCode
{
    public static class FacadeTask
    {
        public static Action<int> OnTaskChange;                     //������_����ɵ��������������ı�ʱ
        public static Action<int> OnDTaskChange;                    //������_����ɵ��ճ��������������ı�ʱ
        public static Action<int> OnCTaskChange;                    //������_����ɵ���ս�������������ı�ʱ

        public static Func<List<Task>> GetDailyTask;                //��ȡ�ճ���������
        public static Func<List<Task>> GetChallageTask;             //��ȡ��ս��������
        public static Action<int, int> SetReceiveInfo;              //������ɰ�ť�����������Ϣ
        public static Action ReceiveDataRemove;                     //ף�ؽ��水ť�������ȡ����
        public static Action RefreshDailyTask;                      //ˢ���ճ���������
        public static Action RefreshChallageTask;                   //ˢ����ս��������
        public static Action CurMoneyTextShow;                      //����Ǯ�ĵ��ض���

        public static Action<int> CheckLinkCount;                   //�������������Ƿ�Ӧ�����
        public static Action<int> CheckLevelPass;                   //���ؿ�����Ƿ�Ӧ����ӣ����ø÷�������˵����������ˣ�
    }
}
