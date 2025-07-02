using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

public class TaskItemData : MonoBehaviour
{
    public Text Desc;                     //任务介绍
    public Slider TaskProgress;           //任务进度
    public Text ProgressText;             //任务进度文字
    public Button ReceiveBtn;             //领取按钮
    public GameObject InProgress;         //未完成图样
                                          
    public int TaskId;                    //任务Id

    private string errMsg;                //错误信息

    void Awake()
    {
        ReceiveBtn.onClick.AddListener(OnReceiveBtnClick);

        errMsg = ModuleMgr.Instance.LanguageMod.GetText("");
    }

    /// <summary>
    /// 设置任务进度
    /// </summary>
    /// <param name="curValue">当前值</param>
    /// <param name="targetValue">目标值</param>
    public void SetProgress(int curValue, int targetValue)
    {
        float progress = curValue * 1f / targetValue;
        progress = progress > 1 ? 1 : progress;
        TaskProgress.value = progress;

        curValue = curValue > targetValue ? targetValue : curValue;
        ProgressText.text = $"{curValue}/{targetValue}";
    }

    /// <summary>
    /// 设置任务信息
    /// </summary>
    /// <param name="desc">任务介绍</param>
    /// <param name="taskId">任务Id</param>
    public void SetMsg(string desc, int taskId)
    {
        Desc.text = desc;
        TaskId = taskId;
    }

    private void OnReceiveBtnClick()
    {
        ModuleMgr.Instance.TaskModule.Receive(TaskId);
    }
}
