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

    protected override void OnLoad()
    {
        base.OnLoad();

        FacadeGuide.NextStep += NextStep;
        FacadeGuide.SetGuide += SetGuide;
        FacadeGuide.GetCurGuideItems += GetCurGuideItems;
        FacadeGuide.GetCurStep += GetCurStep;

        GetData();

        LanguageModule = new LanguageModule();

        curGuideItems = new GuideItem();
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
    }

    private void SetGuide(int step)
    {
        curStep = step;

        ConfGuides guideData = ConfigModule.Instance.Tables.TBGuides.Get(step);
        curGuideItems.step = step;
        curGuideItems.nextStep = guideData.NextStep;
        curGuideItems.diglogContent = LanguageModule.GetText(guideData.DiglogContentId.ToString());
        curGuideItems.diglogPos = GetUIRectTrans(guideData.DiglogPos);
        curGuideItems.handPos = GetUIRectTrans(guideData.HandPos);
        curGuideItems.ifMask = guideData.IfMask;
        curGuideItems.transparentPos = GetUIRectTrans(guideData.TransparentPos);
        curGuideItems.btnPos = GetClickBtnRectTrans(guideData.BtnPos, curGuideItems);
        curGuideItems.autohiddenTime = guideData.AutohiddenTime;
        curGuideItems.ifNext = guideData.IfNext;
        curGuideItems.extra = guideData.Extra;
    }

    private void NextStep(bool ifPlay)
    {
        curStep = curGuideItems.nextStep;
        if (ifPlay)
            FacadeGuide.PlayGuide(curStep);
        else
            FacadeGuide.CloseGuide();

        SPlayerPrefs.SetInt(PlayerPrefDefines.curStep, curStep);
    }

    private RectTransform GetUIRectTrans(string pathData)
    {
        if (pathData == "") return null;
        string[] diglongPosStr = pathData.Split(',');
        string path = UIManager.Instance.GetUIPath(diglongPosStr);
        return GameObject.Find(path).GetComponent<RectTransform>();
    }

    private RectTransform GetClickBtnRectTrans(string pathData, GuideItem item) 
    { 
        switch(pathData) 
        {
            case "handPos":
                return item.handPos.GetComponent<RectTransform>();
            case "transparentPos":
                return item.transparentPos.GetComponent<RectTransform>();
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
}
