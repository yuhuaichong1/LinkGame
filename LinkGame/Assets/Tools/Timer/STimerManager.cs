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
    /// 创建计时器
    /// </summary>
    /// <param name="targetTime">目标结束时间</param>
    /// <param name="loopCount">循环次数</param>
    /// <param name="endAction">计时结束事件</param>
    /// <param name="updateAction">计时更新事件</param>
    /// <param name="timingActions">计时节点事件集合</param>
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
    /// 清除计时器
    /// </summary>
    public static void ClaerSTimer()
    {
        runingSTimer.Clear();
        pauseSTimer.Clear();
        standbySTimer.Clear();
    }

    /// <summary>
    /// 更新计时器
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
/// 计时器状态枚举
/// </summary>
public enum STimerState
{
    Standby,//待机中
    Running,//运行中
    Pause//暂停中
}

/// <summary>
/// 计时节点事件类型枚举
/// </summary>
public enum ClickActionType
{
    Once,//仅执行一次
    Before,//在到达时间点前反复执行
    After,//在到达时间点后反复执行
}

/// <summary>
/// 计时节点事件结构图
/// </summary>
public struct timingActions
{
    public float timing;//目标节点
    public Action<float> clickAction;//事件
    public ClickActionType clickActionType;//计时节点事件类型
}
