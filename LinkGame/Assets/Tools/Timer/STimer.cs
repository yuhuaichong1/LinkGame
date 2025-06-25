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
    public List<timingActions> onceActions = new List<timingActions>();//ִ�й���һ���Լ�ʱ�ڵ��¼�����

    public bool iftiming;//�Ƿ��ڼ�ʱ
    public int loopCount;//ѭ��������0����ѭ����-1��С��0����������ѭ������������0�ĵ����������޴�ѭ��
    public int currentlc;//��ǰ��ѭ������
    public bool ifscale;//�Ƿ���ʱ������Ӱ��

    public STimerState sTimerState;//��ǰ��ʱ��״̬

    /// <summary>
    /// ���ü�ʱ����Ϣ
    /// </summary>
    /// <param name="targetTime">Ŀ�����ʱ��</param>
    /// <param name="loopCount">ѭ������</param>
    /// <param name="endAction">��ʱ�����¼�</param>
    /// <param name="updateAction">��ʱ�����¼�</param>
    /// <param name="timingActions">��ʱ�ڵ��¼�����</param>
    public void SetSTimeInfo(float targetTime, int loopCount = 0, bool ifscale = false, Action endAction = null, Action<float> updateAction = null, List<timingActions> timingActions = null)
    {
        nowTime = 0;
        this.targetTime = targetTime;
        this.loopCount = loopCount;
        this.ifscale = ifscale;
        this.endAction = endAction;
        this.updateAction = updateAction;
        this.timingActions = timingActions;
        onceActions.Clear();
        currentlc = loopCount;

        if (timingActions != null)
        {
            this.updateAction += (nowTime) =>
            {
                for (int i = this.timingActions.Count - 1; i >= 0; i--)
                {
                    var ca = this.timingActions[i];
                    switch (ca.clickActionType)
                    {
                        case ClickActionType.Once: 
                            if(nowTime >= ca.timing)
                            {
                                ca.clickAction?.Invoke(ca.timing);
                                onceActions.Add(ca);
                                this.timingActions.Remove(ca);
                            }
                            break;
                        case ClickActionType.Before: 
                            if(nowTime <= ca.timing)
                            {
                                ca.clickAction?.Invoke(nowTime);
                            }
                            break;
                        case ClickActionType.After: 
                            if(nowTime >= ca.timing)
                            {
                                ca.clickAction?.Invoke(nowTime);
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
        if(sTimerState != STimerState.Running)
        {
            STimerManager.runingSTimer.AddLast(this);
            STimerManager.pauseSTimer.Remove(this);
        }
        iftiming = true;
        sTimerState = STimerState.Running;
    }

    /// <summary>
    /// ��ͣ��ʱ
    /// </summary>
    public void Pause()
    {
        iftiming = false;
        sTimerState = STimerState.Pause;     
    }

    /// <summary>
    /// ֹͣ��ʱ
    /// </summary>
    public void Stop()
    {
        ReStart();
        iftiming = false;
        sTimerState = STimerState.Standby;
    }

    /// <summary>
    /// ���¿�ʼ
    /// </summary>
    public void ReStart()
    {
        nowTime = 0;
        currentlc = loopCount;
        timingActions?.AddRange(onceActions);
        onceActions.Clear();
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
                    Stop();
                else if(currentlc > 0)
                    currentlc -= 1;
                else if(currentlc < 0)
                    ReStart();
                endAction?.Invoke();
            }
            else
            {
                updateAction?.Invoke(nowTime);
                nowTime += ifscale ? Time.deltaTime : Time.unscaledDeltaTime;
            }
        }
    }
}
