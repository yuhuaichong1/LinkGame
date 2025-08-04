using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

//主界面当前关卡进度的子项
public class UICurLevelItem : MonoBehaviour
{
    public Image ItemGreyBg;//未激活背景（未完成的关卡）
    public Image ItemGreenBg;//激活背景（已完成的关卡）
    public GameObject finishIcon;//完成图片（已完成的关卡）
    public LanguageText curLevelText;//当前关卡（未完成的关卡）
    public GameObject wTip;//当前关卡是否兑现

    private LanguageModule LanguageModule;

    void Awake()
    {
        LanguageModule = ModuleMgr.Instance.LanguageMod;
    }

    /// <summary>
    /// 根据关卡设置对应信息
    /// </summary>
    /// <param name="level">关卡</param>
    public void SetCurLevelInfo(int level)
    {
        ConfLevel levelConf = ConfigModule.Instance.Tables.TBLevel.Get(level);
        int curLevel = GamePlayFacade.GetCurLevel();
        bool b = level < curLevel;

        ItemGreyBg.gameObject.SetActive(!b);
        ItemGreenBg.gameObject.SetActive(level <= curLevel);
        finishIcon.SetActive(b);
        curLevelText.text = b ? "" : string.Format(LanguageModule.GetText("10011"), level);
        wTip.SetActive(b ? false : ConfigModule.Instance.Tables.TBLevel.Get(level).WithdrawType == 1);
    }
}
