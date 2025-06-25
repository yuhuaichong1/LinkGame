using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STimer
{
    public float nowTime;//当前时间
    public float targetTime;//目标结束时间

    public Action endAction;//计时结束事件
    public Action<float> updateAction;//计时更新事件
    public List<timingActions> timingActions;//计时节点事件集合
    public List<timingActions> onceActions = new List<timingActions>();//执行过的一次性计时节点事件集合

    public bool iftiming;//是否在计时
    public int loopCount;//循环次数，0代表不循环，-1（小于0）代表无限循环，其他大于0的的数代表有限次循环
    public int currentlc;//当前的循环次数
    public bool ifscale;//是否收时间缩放影响

    public STimerState sTimerState;//当前计时器状态

    /// <summary>
    /// 设置计时器信息
    /// </summary>
    /// <param name="targetTime">目标结束时间</param>
    /// <param name="loopCount">循环次数</param>
    /// <param name="endAction">计时结束事件</param>
    /// <param name="updateAction">计时更新事件</param>
    /// <param name="timingActions">计时节点事件集合</param>
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
    /// 开始计时
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
    /// 暂停计时
    /// </summary>
    public void Pause()
    {
        iftiming = false;
        sTimerState = STimerState.Pause;     
    }

    /// <summary>
    /// 停止计时
    /// </summary>
    public void Stop()
    {
        ReStart();
        iftiming = false;
        sTimerState = STimerState.Standby;
    }

    /// <summary>
    /// 重新开始
    /// </summary>
    public void ReStart()
    {
        nowTime = 0;
        currentlc = loopCount;
        timingActions?.AddRange(onceActions);
        onceActions.Clear();
    }

    /// <summary>
    /// 推进计时
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
