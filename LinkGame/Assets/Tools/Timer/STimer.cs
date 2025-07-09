using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STimer
{
    public float nowTime;//��ǰʱ��
    public float targetTime;//Ŀ�����ʱ��

    public Action endAction;//��ʱ�����¼�
    public Action<float> updateAction;//��ʱ�����¼�
    public List<timingActions> timingActions;//��ʱ�ڵ��¼�����
    public List<timingActions> onceActions;//ִ�й���һ���Լ�ʱ�ڵ��¼�����

    public bool iftiming;//�Ƿ��ڼ�ʱ
    public int loopCount;//ѭ��������0����ѭ����-1��С��0����������ѭ������������0�ĵ����������޴�ѭ��������1����ѭ��1�Σ�
    public int currentlc;//��ǰ��ѭ������
    public bool ifscale;//�Ƿ���ʱ������Ӱ��

    private STimerState sTimerState;//��ǰ��ʱ��״̬
    public STimerState STimerState {get { return sTimerState;}}

    private bool ifClose;//�Ƿ�ر�

    public STimer()
    {
        timingActions = new List<timingActions>();
        onceActions = new List<timingActions>();
    }

    /// <summary>
    /// ���ü�ʱ����Ϣ
    /// </summary>
    /// <param name="targetTime">Ŀ�����ʱ��</param>
    /// <param name="loopCount">ѭ������</param>
    /// <param name="endAction">��ʱ�����¼�</param>
    /// <param name="updateAction">��ʱ�����¼�</param>
    /// <param name="timingActions">��ʱ�ڵ��¼�����</param>
    public void SetSTimeInfo(float targetTime, int loopCount = 0, bool ifscale = false, Action endAction = null, Action<float> updateAction = null, params timingActions[] timingActions)
    {
        ifClose = false;
        nowTime = 0;
        this.targetTime = targetTime;
        this.loopCount = loopCount;
        this.ifscale = ifscale;
        this.endAction = endAction;
        this.updateAction = updateAction;
        foreach (timingActions tAction in timingActions) 
        {
            this.timingActions.Add(tAction);
        }
        onceActions.Clear();
        currentlc = loopCount;

        if (this.timingActions != null)
        {
            this.updateAction += (nowTime) =>
            {
                for (int i = this.timingActions.Count - 1; i >= 0; i--)
                {
                    timingActions ca = this.timingActions[i];
                    switch (ca.clockActionType)
                    {
                        case ClockActionType.Once: 
                            if(nowTime >= ca.timing)
                            {
                                ca.clockAction?.Invoke(ca.timing);
                                onceActions.Add(ca);
                                this.timingActions.Remove(ca);
                            }
                            break;
                        case ClockActionType.Before: 
                            if(nowTime <= ca.timing)
                            {
                                ca.clockAction?.Invoke(nowTime);
                            }
                            break;
                        case ClockActionType.After: 
                            if(nowTime >= ca.timing)
                            {
                                ca.clockAction?.Invoke(nowTime);
                            }
                            break;
                    }
                }
            };
        }
    }

    /// <summary>
    /// ��ʼ��ʱ
    /// </summary>
    public void Start()
    {
        if (CheckClose()) return;

        if (sTimerState != STimerState.Running)
        {
            STimerManager.Instance.runingSTimer.AddLast(this);
            STimerManager.Instance.pauseSTimer.Remove(this);
        }

        iftiming = true;
        sTimerState = STimerState.Running;
    }

    /// <summary>
    /// ��ͣ��ʱ
    /// </summary>
    public void Pause()
    {
        if (CheckClose()) return;

        iftiming = false;
        sTimerState = STimerState.Pause;     
    }

    /// <summary>
    /// ֹͣ��ʱ
    /// </summary>
    public void Stop()
    {
        if (CheckClose()) return;

        ReStart();
        iftiming = false;
    }

    /// <summary>
    /// ���¿�ʼ
    /// </summary>
    public void ReStart()
    {
        if (CheckClose()) return;

        nowTime = 0;
        currentlc = loopCount;
        timingActions?.AddRange(onceActions);
        onceActions.Clear();
    }

    /// <summary>
    /// �رռ�ʱ��
    /// </summary>
    public void Close()
    {
        ifClose = true;
        iftiming = false;
        timingActions.Clear();
        onceActions.Clear();
        sTimerState = STimerState.Standby;
    }

    /// <summary>
    /// �ƽ���ʱ
    /// </summary>
    public void Update()
    {
        if (iftiming)
        {
            if (nowTime > targetTime) 
            {
                nowTime = targetTime;
                updateAction?.Invoke(nowTime);
                
                if (currentlc == 0)
                    Close();
                else if(currentlc > 0)
                    currentlc -= 1;
                else if (currentlc < 0)
                    ReStart();
                endAction?.Invoke();

                nowTime = 0;
            }
            else
            {
                updateAction?.Invoke(nowTime);
                nowTime += ifscale ? Time.deltaTime : Time.unscaledDeltaTime;
            }
        }
    }

    /// <summary>
    /// ���ü�ʱ���Ƿ��Ѿ����ر�
    /// </summary>
    /// <returns>�ü�ʱ���Ƿ��Ѿ����ر�</returns>
    private bool CheckClose()
    {
        if (ifClose)
            Debug.LogError("This STimer has been closed, please create a new one");
        return ifClose;
    }
}
