using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawMItem
{
    public Text LevelTitle;
    public Text GoalTitle;
    public LanguageModule LanguageModule;

    public WithdrawMItem(Text LevelTitle, Text GoalTitle, LanguageModule LanguageModule)
    {
        this.LevelTitle = LevelTitle;
        this.GoalTitle = GoalTitle;
        this.LanguageModule = LanguageModule;
    }

    public void SetInfo(int level, int gold)
    {
        LevelTitle.text = string.Format(LanguageModule.GetText("10049"), level);
        GoalTitle.text = string.Format(LanguageModule.GetText("10031"), gold);
    }
}
