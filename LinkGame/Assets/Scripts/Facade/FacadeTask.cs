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
        public static Action<int, int> Receive;                     //������ɰ�ť�������ȡ����
    }
}
