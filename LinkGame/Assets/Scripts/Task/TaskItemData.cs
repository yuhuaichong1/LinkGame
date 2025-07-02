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
                                          
    public int TaskId;                    //����Id

    private string errMsg;                //������Ϣ

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
    }

    /// <summary>
    /// ����������Ϣ
    /// </summary>
    /// <param name="desc">�������</param>
    /// <param name="taskId">����Id</param>
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
