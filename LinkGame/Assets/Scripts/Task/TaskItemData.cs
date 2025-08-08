using Spine;
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
    public Image ReceiveIcon;

    public int TaskType;                  //任务类型
    public int TaskId;                    //任务Id

    private string errMsg;                //错误信息
    private string DescTxt;
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

        ReceiveBtn.gameObject.SetActive(progress == 1);
        InProgress.gameObject.SetActive(progress != 1);
    }

    /// <summary>
    /// 设置任务信息
    /// </summary>
    /// <param name="desc">任务介绍</param>
    /// <param name="taskId">任务Id</param>
    public void SetMsg(string desc, int taskId, int taskType, int Target)
    {
        //Desc.text = desc;
        Desc.text = string.Format(desc, Target);
        TaskId = taskId;
        TaskType = taskType;
        if (GameDefines.ifIAA) { ReceiveIcon.sprite = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncDiamond_IconPath); };
    }

    private void OnReceiveBtnClick()
    {
        float reward = ConfigModule.Instance.Tables.TBTask.Get(TaskId).Reward;
        UIManager.Instance.OpenWindowAsync<UICongratfulations>(EUIType.EUICongratfulations, null, reward);

        FacadeTask.SetReceiveInfo(TaskId, TaskType);
    }
}
