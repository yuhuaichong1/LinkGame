using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

public class UICurLevelItem : MonoBehaviour
{
    public Image ItemGreyBg;
    public Image ItemGreenBg;
    public GameObject finishIcon;
    public LanguageText curLevelText;
    public GameObject wTip;

    private LanguageModule LanguageModule;

    void Awake()
    {
        LanguageModule = ModuleMgr.Instance.LanguageMod;
    }

    public void SetCurLevelInfo(int level)
    {
        ConfLevel levelConf = ConfigModule.Instance.Tables.TBLevel.Get(level);
        int curLevel = GamePlayFacade.GetCurLevel();
        bool b = level < curLevel;

        ItemGreyBg.gameObject.SetActive(!b);
        ItemGreenBg.gameObject.SetActive(b);
        finishIcon.SetActive(b);
        curLevelText.text = b ? "" : string.Format(LanguageModule.GetText("levelSlider"), level);
        wTip.SetActive(b ? false : ConfigModule.Instance.Tables.TBLevel.Get(level).WithdrawType == 1);
    }
}
