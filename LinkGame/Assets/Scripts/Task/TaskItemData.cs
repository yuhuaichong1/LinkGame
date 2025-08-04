using Spine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

public class TaskItemData : MonoBehaviour
{
    public Text Desc;                     //�������
    public Slider TaskProgress;           //�������
    public Text ProgressText;             //�����������
    public Button ReceiveBtn;             //��ȡ��ť
    public GameObject InProgress;         //δ���ͼ��
    public Image ReceiveIcon;

    public int TaskType;                  //��������
    public int TaskId;                    //����Id

    private string errMsg;                //������Ϣ
    private string DescTxt;
    void Awake()
    {
        ReceiveBtn.onClick.AddListener(OnReceiveBtnClick);


        errMsg = ModuleMgr.Instance.LanguageMod.GetText("");
    }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="curValue">��ǰֵ</param>
    /// <param name="targetValue">Ŀ��ֵ</param>
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
    /// ����������Ϣ
    /// </summary>
    /// <param name="desc">�������</param>
    /// <param name="taskId">����Id</param>
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
