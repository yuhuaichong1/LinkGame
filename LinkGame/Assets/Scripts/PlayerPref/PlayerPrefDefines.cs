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

    public const string starCount = "LinkGame_starCount";                    //PlayerModule_玩家星数

    public const string taskStatus = "LinkGame_taskStatus";                  //TaskModule_所有任务的状态
}
