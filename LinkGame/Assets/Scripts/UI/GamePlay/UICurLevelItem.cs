using cfg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XrCode;

//�����浱ǰ�ؿ����ȵ�����
public class UICurLevelItem : MonoBehaviour
{
    public Image ItemGreyBg;//δ�������δ��ɵĹؿ���
    public Image ItemGreenBg;//�����������ɵĹؿ���
    public GameObject finishIcon;//���ͼƬ������ɵĹؿ���
    public LanguageText curLevelText;//��ǰ�ؿ���δ��ɵĹؿ���
    public GameObject wTip;//��ǰ�ؿ��Ƿ����
    public Text wText;//���������Ƿ�Ϊ˫��

    private LanguageModule LanguageModule;

    void Awake()
    {
        LanguageModule = ModuleMgr.Instance.LanguageMod;
    }

    /// <summary>
    /// ���ݹؿ����ö�Ӧ��Ϣ
    /// </summary>
    /// <param name="level">�ؿ�</param>
    public void SetCurLevelInfo(int level)
    {

        if (GameDefines.ifIAA)
        {
            int curLevel = GamePlayFacade.GetCurLevel();
            bool b = level < curLevel;

            ItemGreyBg.gameObject.SetActive(!b);
            ItemGreenBg.gameObject.SetActive(level <= curLevel);
            finishIcon.SetActive(b);
            curLevelText.text = b ? "" : string.Format(LanguageModule.GetText("10011"), level);
            wTip.SetActive(b ? false : ConfigModule.Instance.Tables.TBLevelAct.Get(level).WithdrawType == 1);
        }
        else
        {
            int curLevel = GamePlayFacade.GetCurLevel();
            bool b = level < curLevel;

            ItemGreyBg.gameObject.SetActive(!b);
            ItemGreenBg.gameObject.SetActive(level <= curLevel);
            finishIcon.SetActive(b);
            curLevelText.text = b ? "" : string.Format(LanguageModule.GetText("10011"), level);
            if (b)
                wTip.SetActive(false);
            else
            {
                switch (ConfigModule.Instance.Tables.TBLevel.Get(level).WithdrawType)
                {
                    case 0:
                        wTip.SetActive(false);
                        break;
                    case 1:
                        wTip.SetActive(true);
                        wText.text = LanguageModule.GetText("10015");
                        break;
                    case 2:
                        wTip.SetActive(true);
                        wText.text = LanguageModule.GetText("10098");
                        break;
                }
            }
        }

    }
}
