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

    public static string curLevel => GetKey("LinkGame_curLevel");                      //GamePlayModule_当前关卡
    public static string tipCount => GetKey("LinkGame_tipCount");                      //GamePlayModule_剩余提示次数
    public static string refushCount => GetKey("LinkGame_refushCount");                //GamePlayModule_剩余刷新次数
    public static string removeCount => GetKey("LinkGame_removeCount");                //GamePlayModule_剩余移除次数
    public static string isTutorial => GetKey("LinkGame_isTutorial");                  //GamePlayModule_是否完成了新手教程
    public static string curTotalLinkCount => GetKey("LinkGame_curTotalLinkCount");    //GamePlayModule_累计消除次数
    public static string curLuckMomentCount => GetKey("LinkGame_curLuckMomentCount");  //GamePlayModule_老虎机计数
    public static string curTopNoticeCount => GetKey("LinkGame_curTopNoticeCount");    //GamePlayModule_顶部消息计数
    public static string curAwesomeCount => GetKey("LinkGame_curAwesomeCount");        //GamePlayModule_送钱计数
    public static string withdrawableLevel => GetKey("LinkGame_withdrawableLevel");    //GamePlayModule_可兑现关卡
    public static string curWLevel => GetKey("LinkGame_curWLevel");                    //GamePlayModule_当前兑现关卡进度

    public static string moneyCount => GetKey("LinkGame_moneyCount");                  //PlayerModule_玩家货币数
    public static string wPayType => GetKey("LinkGame_wPayType");                      //PlayerModule_玩家支付方式
    public static string wName => GetKey("LinkGame_wName");                            //PlayerModule_玩家兑现姓名
    public static string wPhoneOrEmail => GetKey("LinkGame_wPhoneOrEmail");            //PlayerModule_玩家兑现电话/邮箱
    public static string userLevel => GetKey("LinkGame_userLevel");                    //PlayerModule_玩家等级
    public static string curUserExp => GetKey("LinkGame_curUserExp");                  //PlayerModule_玩家经验

    public static string taskStatus => GetKey("LinkGame_taskStatus");                  //TaskModule_所有任务的状态

    public static string musicToggle => GetKey("LinkGame_musicToggle");                //AudioModule_音乐开关
    public static string vibrateToggle => GetKey("LinkGame_vibrateToggle");            //AudioModule_震动开关

    public static string lastZTime => GetKey("LinkGame_lastZTime");                    //TimeZoneModule_上传登录时间

    public static string curDailyRDId => GetKey("LinkGame_curDailyRDId");              //TaskModule_当前日常任务红点进度
    public static string curChallengeRDId => GetKey("LinkGame_curChallengeRDId");      //TaskModule_当前挑战任务红点进度

    public static string curStep => GetKey("LinkGame_curStep");                        //GuideModule_当前引导步骤
    public static string withdrawableUIcheck => GetKey("LinkGame_withdrawableUIcheck");//GuideModule_兑现相关界面仅执行一次

    public static string ifContinue => GetKey("LinkGame_ifContinue");                  //GamePlayModule_是否继续游戏
    public static string randomGoodIcon => GetKey("LinkGame_randomGoodIcon");          //GamePlayModule_继续游戏用，让图样随机的词典
    public static string MAP => GetKey("LinkGame_MAP");                                //场景中的所有物体的id（包含障碍物，不包含隐藏物）
    public static string MAP_FROZEN => GetKey("LinkGame_MAP_FROZEN");                  //场景中的所有物体是否为冰冻状态

    public static string totalGood => GetKey("LinkGame_totalGood");                    //场景中的物体的总数
    public static string remainGood => GetKey("LinkGame_remainGood");                  //场景中剩余物体数量

}
