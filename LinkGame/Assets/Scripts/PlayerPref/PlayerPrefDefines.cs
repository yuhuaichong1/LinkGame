using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerPrefDefines
{
    public const string curLevel = "LinkGame_curLevel";                      //GamePlayModule_当前关卡
    public const string tipCount = "LinkGame_tipCount";                      //GamePlayModule_剩余提示次数
    public const string refushCount = "LinkGame_refushCount";                //GamePlayModule_剩余刷新次数
    public const string removeCount = "LinkGame_removeCount";                //GamePlayModule_剩余移除次数
    public const string isTutorial = "LinkGame_isTutorial";                  //GamePlayModule_是否完成了新手教程
    public const string curTotalLinkCount = "LinkGame_curTotalLinkCount";    //GamePlayModule_累计消除次数
    public const string curLuckMomentCount = "LinkGame_curLuckMomentCount";  //GamePlayModule_老虎机计数
    public const string curTopNoticeCount = "LinkGame_curTopNoticeCount";    //GamePlayModule_顶部消息计数
    public const string curAwesomeCount = "LinkGame_curAwesomeCount";        //GamePlayModule_送钱计数

    public const string moneyCount = "LinkGame_moneyCount";                  //PlayerModule_玩家货币数
    public const string wPayType = "LinkGame_wPayType";                      //PlayerModule_玩家支付方式
    public const string wName = "LinkGame_wName";                            //PlayerModule_玩家兑现姓名
    public const string wPhoneOrEmail = "LinkGame_wPhoneOrEmail";            //PlayerModule_玩家兑现电话/邮箱
    public const string userLevel = "LinkGame_userLevel";                    //PlayerModule_玩家等级
    public const string curUserExp = "LinkGame_curUserExp";                  //PlayerModule_玩家经验

    public const string taskStatus = "LinkGame_taskStatus";                  //TaskModule_所有任务的状态

    public const string musicToggle = "LinkGame_musicToggle";                //AudioModule_音乐开关
    public const string vibrateToggle = "LinkGame_vibrateToggle";            //AudioModule_震动开关

    
}
