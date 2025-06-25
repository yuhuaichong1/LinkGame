using cfg;
using System;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

public class GuideModule : BaseModule
{
    public int step;//新手教程步骤

    private Dictionary<int, int> level1GuideGoodDic;
    private bool hasGet;

    private Dictionary<int, GiudeItem> guideItems;

    protected override void OnLoad()
    {
        base.OnLoad();

        InitGuideData();

        guideItems = new Dictionary<int, GiudeItem>();

        List<ConfGuides> guideData = ConfigModule.Instance.Tables.TBGuides.DataList;
        foreach (ConfGuides guide in guideData)
        {
            int step = guide.Step;
            string key = guide.DiglogContent;
            string DP = guide.DiglogPos;
            string HP = guide.HandPos;
            string TP = guide.TransparentPos;
            string OH = guide.OtherHighlight;
            string BP = guide.BtnPos;
            guideItems.Add(step, new GiudeItem 
            {
                step = step,
                contentKey = key, 
                DP = DP,
                HP = HP, 
                TP = TP, 
                OH = OH, 
                BP = BP
            });
        }
    }

    public void InitGuideData()
    {
        //step = PlayerPrefs.GetInt("guideStep");
    }
    public void SaveGuideData()
    {
        //PlayerPrefs.SetInt("guideStep", step);
    }

    private void SetRects()
    {
        if(!hasGet)
        {
            hasGet = true;

            FacadeGuide.SetPos();

            level1GuideGoodDic = FacadeGuide.GetLevel1GuideGoodDic();
        }
        
    }

    public void ShowGuide()
    {
        
    }
}
