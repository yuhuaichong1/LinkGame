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
    private bool withdrawableUIcheck;//让兑现相关的各个界面不会重复执行

    protected override void OnLoad()
    {
        base.OnLoad();

        FacadeGuide.NextStep += NextStep;
        FacadeGuide.GetCurGuideItems += GetCurGuideItems;
        FacadeGuide.GetCurStep += GetCurStep;
        FacadeGuide.NextStepHead += NextStepHead;
        FacadeGuide.GetWithdrawableUIcheck += GetWithdrawableUIcheck;
        FacadeGuide.SetWithdrawableUIcheck += SetWithdrawableUIcheck;
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
            int backStep = ConfigModule.Instance.Tables.TBGuides.Get(curStep).BackStep;
            curStep = backStep != 0 ? backStep : curStep;
        }
        SetGuide(curStep);

        withdrawableUIcheck = SPlayerPrefs.GetBool(PlayerPrefDefines.withdrawableUIcheck);
    }

    private void SetGuide(int step)
    {
        curStep = step;

        ConfGuides guideData = ConfigModule.Instance.Tables.TBGuides.Get(step);
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

    /// <summary>
    /// 进入下一步引导（依据条件判断是否进入）
    /// </summary>
    private void NextStep()
    {
        if(curGuideItems.ifNextStep)
        {
            curStep = curGuideItems.nextStep;
            CheckGuideEnd();
            SetGuide(curStep);
            if (curGuideItems.ifNextPlay) 
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
    /// 手动（强制）进入下一步骤
    /// </summary>
    private void NextStepHead()
    {
        curStep = curGuideItems.nextStep;
        CheckGuideEnd();
        SetGuide(curStep);
        SPlayerPrefs.SetInt(PlayerPrefDefines.curStep, curStep);
    }

    /// <summary>
    /// 获取UI路径
    /// </summary>
    /// <param name="pathData">路径的str数据</param>
    /// <returns>目标UI</returns>
    private string GetUIRectTrans(string pathData)
    {
        if (pathData == "") return null;
        string[] diglongPosStr = pathData.Split(',');
        string path = UIManager.Instance.GetUIPath(diglongPosStr);
        return path;
    }

    /// <summary>
    /// 单独处理可点击部分的位置
    /// </summary>
    /// <param name="pathData">路径（可能是其他类型）</param>
    /// <param name="item">当前引导项目</param>
    /// <returns>目标路径</returns>
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

    /// <summary>
    /// 引导结束
    /// </summary>
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
