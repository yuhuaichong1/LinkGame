using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public int Id;//任务Id
    public int Type;//任务类型（每日or过关）
    public string Content;//任务内容
    public int Target;//任务目标
    public int Reward;//任务奖励

    public ETaskStatus taskStatus;//任务状态
}
