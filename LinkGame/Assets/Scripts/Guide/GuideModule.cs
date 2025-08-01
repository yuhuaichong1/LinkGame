using cfg;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

public class GuideModule : BaseModule
{
    private LanguageModule LanguageModule;

    private int curStep;
    private GuideItem curGuideItems;
    private bool withdrawableUIcheck;//�ö�����صĸ������治���ظ�ִ��

    protected override void OnLoad()
    {
        base.OnLoad();

        FacadeGuide.NextStep += NextStep;
        FacadeGuide.GetCurGuideItems += GetCurGuideItems;
        FacadeGuide.GetCurStep += GetCurStep;
        FacadeGuide.NextStepHead += NextStepHead;
        FacadeGuide.GetWithdrawableUIcheck += GetWithdrawableUIcheck;
        FacadeGuide.SetWithdrawableUIcheck += SetWithdrawableUIcheck;
        FacadeGuide.CheckWithdrawableUI += CheckWithdrawableUI;

        LanguageModule = new LanguageModule();

        curGuideItems = new GuideItem();

        GetData();
    }

    private void GetData()
    {
        curStep = SPlayerPrefs.GetInt(PlayerPrefDefines.curStep);
        if (curStep == 0) 
        {
            curStep = GameDefines.firstGuideId;
        }
        else
        {
            int backStep = 0;
            if (Game.Instance.IsAb)
            {
                 backStep = ConfigModule.Instance.Tables.TBGuides.Get(curStep).BackStep;
            }
            else
            {
                backStep = ConfigModule.Instance.Tables.TBGuidesAct.Get(curStep).BackStep;
            }
            curStep = backStep != 0 ? backStep : curStep;
        }

        SetGuide(curStep);

        withdrawableUIcheck = SPlayerPrefs.GetBool(PlayerPrefDefines.withdrawableUIcheck);
    }

    private void SetGuide(int step)
    {
        curStep = step;
        if (Game.Instance.IsAb)
        {
            ConfGuides guideData = ConfigModule.Instance.Tables.TBGuides.Get(step);
            Debug.LogError("��ǰ����:" + step);
            curGuideItems.step = step;
            curGuideItems.nextStep = guideData.NextStep;
            curGuideItems.note = guideData.Notes;
            curGuideItems.backStep = guideData.BackStep;
            curGuideItems.ifBackPlay = guideData.IfBackPlay;
            curGuideItems.ifNextStep = guideData.IfNextStep;
            curGuideItems.ifNextPlay = guideData.IfNextPlay;
            curGuideItems.autohiddenTime = guideData.AutohiddenTime;
            curGuideItems.diglogContent = LanguageModule.GetText(guideData.DiglogContentId.ToString());
            curGuideItems.diglogPos = GetUIRectTrans(guideData.DiglogPos);
            curGuideItems.handPos = GetUIRectTrans(guideData.HandPos);
            curGuideItems.ifMask = guideData.IfMask;
            curGuideItems.transparentPos = GetUIRectTrans(guideData.TransparentPos);
            curGuideItems.clickPos = GetClickRectTrans(guideData.ClickPos);
            curGuideItems.extra = guideData.Extra;
        }
        else
        {
            ConfGuidesAct guideData = ConfigModule.Instance.Tables.TBGuidesAct.Get(step);
            Debug.LogError("��ǰ����:" + step);
            curGuideItems.step = step;
            curGuideItems.nextStep = guideData.NextStep;
            curGuideItems.note = guideData.Notes;
            curGuideItems.backStep = guideData.BackStep;
            curGuideItems.ifBackPlay = guideData.IfBackPlay;
            curGuideItems.ifNextStep = guideData.IfNextStep;
            curGuideItems.ifNextPlay = guideData.IfNextPlay;
            curGuideItems.autohiddenTime = guideData.AutohiddenTime;
            curGuideItems.diglogContent = LanguageModule.GetText(guideData.DiglogContentId.ToString());
            curGuideItems.diglogPos = GetUIRectTrans(guideData.DiglogPos);
            curGuideItems.handPos = GetUIRectTrans(guideData.HandPos);
            curGuideItems.ifMask = guideData.IfMask;
            curGuideItems.transparentPos = GetUIRectTrans(guideData.TransparentPos);
            curGuideItems.clickPos = GetClickRectTrans(guideData.ClickPos);
            curGuideItems.extra = guideData.Extra;
        }

    }

    /// <summary>
    /// ������һ�����������������ж��Ƿ���룩
    /// </summary>
    private void NextStep()
    {
        if(curGuideItems.ifNextStep && !CheckGuideEnd())
        {
            bool orginBool = curGuideItems.ifNextPlay;
            curStep = curGuideItems.nextStep;
            SetGuide(curStep);
            if (orginBool) 
            {
                FacadeGuide.PlayGuide();
            }
            else
            {
                FacadeGuide.CloseGuide();
            }
        }
        else
        {
            FacadeGuide.CloseGuide();
        }

        SPlayerPrefs.SetInt(PlayerPrefDefines.curStep, curStep);
    }

    /// <summary>
    /// �ֶ���ǿ�ƣ�������һ����
    /// </summary>
    private void NextStepHead()
    {
        curStep = curGuideItems.nextStep;
        CheckGuideEnd();
        SetGuide(curStep);
        SPlayerPrefs.SetInt(PlayerPrefDefines.curStep, curStep);
    }

    /// <summary>
    /// ��ȡUI·��
    /// </summary>
    /// <param name="pathData">·����str����</param>
    /// <returns>Ŀ��UI</returns>
    private string GetUIRectTrans(string pathData)
    {
        if (pathData == "") return null;
        string[] diglongPosStr = pathData.Split(',');
        string path = UIManager.Instance.GetUIPath(diglongPosStr);
        return path;
    }

    /// <summary>
    /// ��������ɵ�����ֵ�λ��
    /// </summary>
    /// <param name="pathData">·�����������������ͣ�</param>
    /// <param name="item">��ǰ������Ŀ</param>
    /// <returns>Ŀ��·��</returns>
    private string GetClickRectTrans(string pathData) 
    { 
        switch(pathData) 
        {
            case "handPos":
                return curGuideItems.handPos;
            case "transparentPos":
                return curGuideItems.transparentPos;
            default:
                return GetUIRectTrans(pathData);
        }
    }

    private GuideItem GetCurGuideItems()
    {
        return curGuideItems;
    }

    private int GetCurStep() 
    {
        return curStep;
    }

    private bool GetWithdrawableUIcheck()
    {
        return withdrawableUIcheck;
    }
    private void SetWithdrawableUIcheck(bool b)
    {
        withdrawableUIcheck = b;

        SPlayerPrefs.SetBool(PlayerPrefDefines.withdrawableUIcheck, withdrawableUIcheck);
    }

    private void CheckWithdrawableUI()
    {
        if (GamePlayFacade.GetIsTutorial() && withdrawableUIcheck)
        {
            FacadeGuide.PlayGuide();
            withdrawableUIcheck = false;
        }
    }


    /// <summary>
    /// ��������Ƿ����
    /// </summary>
    /// <returns>�����Ƿ����</returns>
    private bool CheckGuideEnd()
    {
        bool temp = ConfigModule.Instance.Tables.TBGuides.Get(curStep).NextStep == 0;
        if (temp)
        {
            D.Error("Guide End");
            GamePlayFacade.SetIsTutorial(false);
        }
        return temp;
    }
}
