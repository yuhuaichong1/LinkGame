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
