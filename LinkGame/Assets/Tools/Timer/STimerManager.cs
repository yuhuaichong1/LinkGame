using System;
using System.Collections.Generic;
using XrCode;

public class STimerManager : Singleton<STimerManager>, ILoad, IDispose
{
    public LinkedList<STimer> runingSTimer;//�����еļ�ʱ��
    public LinkedList<STimer> pauseSTimer;//��ͣ�еļ�ʱ��
    public Stack<STimer> standbySTimer;//�����У�������У��ļ�ʱ��

    public void Load()
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
    /// <param name="ifscale">�Ƿ�ʱ������Ӱ��</param>
    /// <param name="ifstart">�Ƿ��ڴ���������ʼ��ʱ</param>
    /// <param name="endAction">��ʱ�����¼�</param>
    /// <param name="updateAction">��ʱ�����¼�</param>
    /// <param name="timingActions">��ʱ�ڵ��¼�����</param>
    /// <returns>������ɵļ�ʱ��</returns>
    public STimer CreateSTimer(float targetTime, int loopCount = 0, bool ifscale = false, bool ifstart = true, Action endAction = null, Action<float> updateAction = null, params timingActions[] timingActions)
    {
        bool b = standbySTimer.Count != 0;
        STimer timer = b ? standbySTimer.Pop() : new STimer();
        timer.SetSTimeInfo(targetTime, loopCount, ifscale, endAction, updateAction, timingActions);
        if(ifstart)
            timer.Start();
        return timer;
    }

    /// <summary>
    /// �������׵��ӳ�ִ�м�ʱ��
    /// </summary>
    /// <param name="targetTime">Ŀ�����ʱ��</param>
    /// <param name="endAction">��ʱ�����¼�</param>
    /// <returns>������ɵļ�ʱ��</returns>
    public STimer CreateSDelay(float targetTime, Action endAction)
    {
        return(CreateSTimer(targetTime, 0, false, true, endAction));
    }

    /// <summary>
    /// �������׵ķ���ִ�м�ʱ��
    /// </summary>
    /// <param name="targetTime">Ŀ�����ʱ��</param>
    /// <param name="updateAction">��ʱ�����¼�</param>
    /// <returns>������ɵļ�ʱ��</returns>
    public STimer CreateSUpdate(float targetTime, Action<float> updateAction)
    {
        return (CreateSTimer(targetTime, 0, false, true, null, updateAction));
    }

    /// <summary>
    /// �����ʱ��
    /// </summary>
    public void ClearSTimer()
    {
        runingSTimer.Clear();
        pauseSTimer.Clear();
        standbySTimer.Clear();
    }

    /// <summary>
    /// ���¼�ʱ��
    /// </summary>
    public void UpdateSTimer()
    {
        LinkedListNode<STimer> current = runingSTimer.Last;
        while (current != null)
        {
            STimer timer = current.Value;
            switch (timer.STimerState)
            {
                case STimerState.Running:
                    timer.Update();
                    break;
                case STimerState.Pause:
                    pauseSTimer.AddLast(timer);
                    runingSTimer.Remove(current);
                    break;
                case STimerState.Standby:
                    standbySTimer.Push(timer);
                    runingSTimer.Remove(current);
                    break;
            }
            current = current.Previous;
        }
    }

    public void Dispose()
    {
        ClearSTimer();
    }
}

