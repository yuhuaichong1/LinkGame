using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class STimerManager
{
    public static LinkedList<STimer> runingSTimer;
    public static LinkedList<STimer> pauseSTimer;
    public static Stack<STimer> standbySTimer;

    static STimerManager()
    {
        runingSTimer = new LinkedList<STimer>();
        pauseSTimer = new LinkedList<STimer>();
        standbySTimer = new Stack<STimer>();
    }

    /// <summary>
    /// ������ʱ��
    /// </summary>
    /// <param name="targetTime">Ŀ�����ʱ��</param>
    /// <param name="loopCount">ѭ������</param>
    /// <param name="endAction">��ʱ�����¼�</param>
    /// <param name="updateAction">��ʱ�����¼�</param>
    /// <param name="timingActions">��ʱ�ڵ��¼�����</param>
    /// <returns></returns>
    public static STimer CreateSTimer(float targetTime, int loopCount = 0, bool ifscale = false, Action endAction = null, Action<float> updateAction = null, List<timingActions> timingActions = null)
    {
        //bool b = standbySTimer.Count != 0 && standbySTimer.Peek().sTimerState == STimerState.Standby;
        //STimer timer = b ? standbySTimer.Pop() : new STimer();

        STimer timer = new STimer();
        timer.SetSTimeInfo(targetTime, loopCount, ifscale, endAction, updateAction, timingActions);
        runingSTimer.AddLast(timer);
        timer.Start();
        return timer;
    }

    /// <summary>
    /// �����ʱ��
    /// </summary>
    public static void ClaerSTimer()
    {
        runingSTimer.Clear();
        pauseSTimer.Clear();
        standbySTimer.Clear();
    }

    /// <summary>
    /// ���¼�ʱ��
    /// </summary>
    public static void UpdateSTimer()
    {
        LinkedListNode<STimer> current = runingSTimer.Last;
        while (current != null)
        {
            STimer timer = current.Value;
            switch (timer.sTimerState)
            {
                case STimerState.Running:
                    timer.Update();
                    current = current.Previous;
                    break;
                case STimerState.Pause:
                    LinkedListNode<STimer> previous = current.Previous;
                    pauseSTimer.AddLast(timer);
                    runingSTimer.Remove(current);
                    current = previous;
                    break;
                case STimerState.Standby:
                    previous = current.Previous;
                    standbySTimer.Push(timer);
                    runingSTimer.Remove(current);
                    current = previous;
                    break;
            }
        }
    }
}

/// <summary>
/// ��ʱ��״̬ö��
/// </summary>
public enum STimerState
{
    Standby,//������
    Running,//������
    Pause//��ͣ��
}

/// <summary>
/// ��ʱ�ڵ��¼�����ö��
/// </summary>
public enum ClickActionType
{
    Once,//��ִ��һ��
    Before,//�ڵ���ʱ���ǰ����ִ��
    After,//�ڵ���ʱ���󷴸�ִ��
}

/// <summary>
/// ��ʱ�ڵ��¼��ṹͼ
/// </summary>
public struct timingActions
{
    public float timing;//Ŀ��ڵ�
    public Action<float> clickAction;//�¼�
    public ClickActionType clickActionType;//��ʱ�ڵ��¼�����
}
