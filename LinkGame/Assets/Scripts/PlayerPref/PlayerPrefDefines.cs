using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

public abstract class PlayerPrefDefines
{
    private static string GetKey(string baseKey)
    {
        if (GameDefines.ifIAA)
        {
            return baseKey + "Act";
        }
        return baseKey;
    }

    public static string curLevel => GetKey("LinkGame_curLevel");                      //GamePlayModule_��ǰ�ؿ�
    public static string tipCount => GetKey("LinkGame_tipCount");                      //GamePlayModule_ʣ����ʾ����
    public static string refushCount => GetKey("LinkGame_refushCount");                //GamePlayModule_ʣ��ˢ�´���
    public static string removeCount => GetKey("LinkGame_removeCount");                //GamePlayModule_ʣ���Ƴ�����
    public static string isTutorial => GetKey("LinkGame_isTutorial");                  //GamePlayModule_�Ƿ���������ֽ̳�
    public static string curTotalLinkCount => GetKey("LinkGame_curTotalLinkCount");    //GamePlayModule_�ۼ���������
    public static string curLuckMomentCount => GetKey("LinkGame_curLuckMomentCount");  //GamePlayModule_�ϻ�������
    public static string curTopNoticeCount => GetKey("LinkGame_curTopNoticeCount");    //GamePlayModule_������Ϣ����
    public static string curAwesomeCount => GetKey("LinkGame_curAwesomeCount");        //GamePlayModule_��Ǯ����
    public static string withdrawableLevel => GetKey("LinkGame_withdrawableLevel");    //GamePlayModule_�ɶ��ֹؿ�
    public static string curWLevel => GetKey("LinkGame_curWLevel");                    //GamePlayModule_��ǰ���ֹؿ�����

    public static string moneyCount => GetKey("LinkGame_moneyCount");                  //PlayerModule_��һ�����
    public static string wPayType => GetKey("LinkGame_wPayType");                      //PlayerModule_���֧����ʽ
    public static string wName => GetKey("LinkGame_wName");                            //PlayerModule_��Ҷ�������
    public static string wPhoneOrEmail => GetKey("LinkGame_wPhoneOrEmail");            //PlayerModule_��Ҷ��ֵ绰/����
    public static string userLevel => GetKey("LinkGame_userLevel");                    //PlayerModule_��ҵȼ�
    public static string curUserExp => GetKey("LinkGame_curUserExp");                  //PlayerModule_��Ҿ���

    public static string taskStatus => GetKey("LinkGame_taskStatus");                  //TaskModule_���������״̬

    public static string musicToggle => GetKey("LinkGame_musicToggle");                //AudioModule_���ֿ���
    public static string vibrateToggle => GetKey("LinkGame_vibrateToggle");            //AudioModule_�𶯿���

    public static string lastZTime => GetKey("LinkGame_lastZTime");                    //TimeZoneModule_�ϴ���¼ʱ��

    public static string curDailyRDId => GetKey("LinkGame_curDailyRDId");              //TaskModule_��ǰ�ճ����������
    public static string curChallengeRDId => GetKey("LinkGame_curChallengeRDId");      //TaskModule_��ǰ��ս���������

    public static string curStep => GetKey("LinkGame_curStep");                        //GuideModule_��ǰ��������
    public static string withdrawableUIcheck => GetKey("LinkGame_withdrawableUIcheck");//GuideModule_������ؽ����ִ��һ��

    public static string ifContinue => GetKey("LinkGame_ifContinue");                  //GamePlayModule_�Ƿ������Ϸ
    public static string randomGoodIcon => GetKey("LinkGame_randomGoodIcon");          //GamePlayModule_������Ϸ�ã���ͼ������Ĵʵ�
    public static string MAP => GetKey("LinkGame_MAP");                                //�����е����������id�������ϰ�������������
    public static string MAP_FROZEN => GetKey("LinkGame_MAP_FROZEN");                  //�����е����������Ƿ�Ϊ����״̬

    public static string totalGood => GetKey("LinkGame_totalGood");                    //�����е����������
    public static string remainGood => GetKey("LinkGame_remainGood");                  //������ʣ����������

}
