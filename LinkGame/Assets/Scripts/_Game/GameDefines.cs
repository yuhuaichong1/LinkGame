
/* 游戏定义脚本
 * 游戏中常量、枚举等内容请在这里统一定义
 */


#region 常量

using cfg;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameDefines
{
    public static string URL = "https://www.";                                                              //后台网址
    public static string nameString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";     //随机名称数组

    public const int map_margin_bottom = 5;                                                                 //关卡显示区域下边距

    public const int OBS_FIXED_ID = 10003;                                                                     //不可移动的障碍物
    public const int OBS_MOVING_ID = 10004;                                                                    //可移动的障碍物
    public const int HID_FIXED_ID = 10001;                                                                     //不可移动的隐藏物
    public const int HID_MOVING_ID = 10002;                                                                    //可移动的隐藏物
    public const string OFName = "Fixed_OBS_";                                                              //不可移动的障碍物名称
    public const string OMName = "Moving_OBS_";                                                             //可移动的障碍物名称
    public const string HFName = "Fixed_HID_";                                                              //不可移动的隐藏物名称
    public const string HMName = "Moving_HID_";                                                             //可移动的隐藏物名称

    public const int STATE_NORMAL = 0;                                                                      //物体状态——一般
    public const int STATE_HIDE = 1;                                                                        //物体状态——隐藏
    public const int STATE_DISAPPEARING = 2;                                                                //物体状态——消失中

    public static bool ifWithdrawal = true;                                                                 //是否提现

    public const float Single_Link_Money = 0.01f;                                                            //单次消除金额

    public const string Default_Channels = "0";                                                             //默认支付方式
    public const string Default_Mark = "$";                                                                 //默认货币符号
    public const int Default_Decimal = 2;                                                                   //默认小数点位
    public const int Default_ExchangeRate = 1;                                                              //默认汇率
    public const ELanguageType Default_Language = ELanguageType.English;                                    //默认语言
    public const int Default_NANP = 1;                                                                      //默认国际长途电话区号

    public const string Func_Hint_IconPath = "UI/FuncIcon/icon_hint.png";                                   //下三功能“提示”图片                        （*）
    public const string Func_Refush_IconPath = "UI/FuncIcon/icon_re.png";                                   //下三功能“刷新”图片                        （*）
    public const string Func_Remove_IconPath = "UI/FuncIcon/icon_xiaochu.png";                              //下三功能“移除”图片                        （*）
    public const string Func_Shift_IconPath = "UI/FuncIcon/icon_shift.png";                                 //下三功能“换向”图片                        （*）

    public const int GetFunc_Hint_Num = 3;                                                                  //能获取的“提示”功能的数量
    public const int GetFunc_Refresh_Num = 2;                                                               //能获取的“刷新”功能的数量
    public const int GetFunc_Shift_Num = 2;                                                                 //能获取的“转向”功能的数量
    public const int GetFunc_Remove_Num = 2;                                                                //能获取的“移除”功能的数量
    public static Vector2 GetFunc_ExtraMoney_Num = new Vector2(1, 10);                                      //获取功能时额外获取的金钱的数量

    public const string LuckMomentNotActivatedBg = "UI/LuckMoment/LMNABg.png";                              //老虎机未激活块背景路径
    public const string LuckMomentActivatedBg = "UI/LuckMoment/LMABg.png";                                  //老虎机激活块背景路径
    public const float Default_LM_Accelerate_Speed = 0.25f;                                                 //老虎机加速起始间隔
    public const float Default_LM_Uniform_Speed = 0.03f;                                                    //老虎机匀速间隔
    public const float Default_LM_Moderate_Speed = 0.33f;                                                   //老虎机减速最终间隔
    public const int Default_LM_Accelerate_Times = 3;                                                       //老虎机加速起始间隔
    public const int Default_LM_Uniform_Times = 40;                                                         //老虎机匀速间隔
    public const int Default_LM_Moderate_Times = 5;                                                         //老虎机减速最终间隔

    public const int Default_FuncShowTipDelay = 5;                                                          //主界面下三功能提示延迟显示时间
    public const int Default_FuncLoopTipDelay = 3;                                                          //主界面下三功能提示循环间隔时间

    public const string FadeMaterials = "Materials/Fade.mat";                                               //UI褪色材质

    public const float FlyEffect_Start_Delay = 0.5f;                                                           //飞行特效开始延迟时间
    public const float PRN_Effect_MoveTime = 0.5f;                                                          //“上方随机玩家兑现提示”特效移动时间
    public const float PRN_Effect_StayTime = 2.5f;                                                          //“上方随机玩家兑现提示”特效滞留时间
    public const float TL_Effect_XMoveTime = 0.5f;                                                          //“目标关卡提示”特效横向移动时间
    public const float TL_Effect_StayTime = 1f;                                                             //“目标关卡提示”特效滞留时间
    public const float TL_Effect_YMoveTime = 1f;                                                            //“目标关卡提示”特效竖向移动时间
    public const string FlyMoney_ObjPath = "Prefabs/Effect/FlyMoney.prefab";                                //飞钱特效预制体
    public const float FlyMoney_ObjTime = 0.12f;                                                            //飞钱特效持续时间（非特殊情况请保持和FlyFunc_ObjTime一致）
    public const int FlyMoney_Effect_LinkCount = 3;                                                         //消除飞钱特效（总计生成数量 * 2个飞钱）
    public const int FlyMoney_Effect_RewardCount = 8;                                                       //奖励飞钱特效数量
    public const string FlyFunc_ObjPath = "Prefabs/Effect/FlyFunc.prefab";                                  //飞功能特效预制体
    public const float FlyFunc_ObjTime = 0.25f;                                                             //飞功能特效持续时间（非特殊情况请保持和FlyMoney_ObjTime一致）
    public const string FlyMoneyTip_ObjPath = "Prefabs/Effect/GetMoneyTip.prefab";                          //飞钱奖励提示的预制体
    public const float FlyMoneyTip_ObjTime = 2f;                                                            //飞钱奖励提示特效持续时间
    public const string HidBreak_ObjPath = "Prefabs/Effect/HidBreak.prefab";                                //隐藏物体破除特效

    public const float TMDIcon_RoteTime = 1;                                                                //指明移动方向旋转时间 
    public const float TMDIcon_MoveTime = 1;                                                                //指明移动方向移动时间 

    public const float Reward_StayTime = 3f;                                                                //奖励特效滞留时间
    public const string RibbonRewardItem_ObjPath = "Prefabs/Effect/RibbonRewardItem.prefab";                //奖励特效上的预制体
    public const string Reward_Money_IconPath = "UI/LuckMoment/icon_qian.png";                              //奖励特效物体三叠钱的图片                    （*）
    public const string Reward_FuncHint_IconPath = "UI/FuncIcon/icon_hint.png";                             //奖励特效消除功能图片                        （*）
    public const string Reward_FuncRefresh_IconPath = "UI/FuncIcon/icon_re.png";                            //奖励特效刷新功能图片                        （*）
    public const string Reward_FuncShift_IconPath = "UI/FuncIcon/icon_shift.png";                           //奖励特效换向功能图片                        （*）

    public const float GoodShowTime = 0.1f;                                                                 //物品显示动画时间间隔

    public const int LuckMoment_Count_Max = 30;                                                             //可用老虎机的最大值
    public const int TopNotice_Count_Max = 12;                                                              //顶部提示展示的最大值
    public const int Awesome_Count_Max = 8;                                                                 //得钱UI展示的最大值
    public const int Rate_Count_Max = 5;                                                                    //评论UI展示的最大值

    public static Vector2 Awesome_ExtraMoney_Num = new Vector2(10, 50);                                     //Awesome界面随机钱区间

    public static string Reddot_Name_Out;                                                                   //外部红点名称
    public static string Reddot_Name_Daily;                                                                 //日常任务红点名称
    public static string Reddot_Name_Challenge;                                                             //挑战任务红点名称

    public static int firstGuideId = 10001;                                                                 //起始引导步骤序号

    public static float ShowAnimTime = 0.25f;                                                               //UI动画打开持续时间
    public static float HideAnimTime = 0.25f;                                                               //UI动画关闭持续时间
}
#endregion


#region 枚举

/// <summary>
/// 游戏状态
/// </summary>
public enum EGameState
{
    Load,             //加载游戏
    Start,            //开始游戏
    Run,              //运行游戏
    Pause,            //暂停游戏
    Exit,             //退出游戏
}

// UI类型枚举  和ui表配置一一对应
public enum EUIType
{
    ENone = 0,
    EUIMainCity = 1,
    EUIPopup = 2,
    EUILoading = 3,
    EUIGamePlay = 4,
    EUIGuide = 5,
    EUINotice = 6,
    EUIEffect = 7,
    EUIFuncPopup = 8,
    EUIRecover = 9,
    EUIEnterInfo = 10,
    EUIWithdrawalChannel = 11,
    EUITask = 12,
    EUISetting = 13,
    EUILuckMoment = 14,
    EUIWithdrawalInformation = 15,
    EUIWithdrawableSingle = 16,
    EUIWithdrawableMultiple = 17,
    EUIRate= 18,
    EUIAwesome = 19,
    EUIUserLevel = 20,
    EUIGamePlayNotice = 21,
    EUIChallengeSuccessful = 22,
    EUIChallengeFailed = 23,
    EUICongratfulations = 24,
    EUIWithdrawalSuccessful = 25,
}

public enum ESceneType : byte
{
    MainScene=1,              //主城场景
}

/// <summary>
/// UI状态
/// </summary>
public enum EUIState
{
    EUIHiding,
    EUIOpened,
    EUIClosed,
}


/// <summary>
/// 监听接口
/// </summary>
public enum EMsgCode : int
{
    EC2S_Login = 1001, //登录
    ES2C_Login = 1002,
}

/// <summary>
/// 多语言
/// </summary>
public enum ELanguageType : int
{
    None = 0,
    Chinese_s = 1,
    Chinese_t = 2,
    English = 3,
    German = 4,
    Japanese = 5,
    Brazilian_Portuguese = 6,
    French = 7,
    Spanish = 8,
    Korean = 9,
    Indonesian = 10,
    Russian = 11,
    Hindi = 12,
    Thai = 13,
    Turkish = 14,
    Arabic = 15,

    LengthTest = 99,
}

/// <summary>
/// 下三功能
/// </summary>
public enum EFuncType : int
{ 
    Tip = 1,
    Refush,
    Shift,
    Remove
}

//当局游戏状态
public enum EMapState 
{ 
    Eating,            //消除中
    Playing,           //游玩中
    Moving,            //物体移动中？
    None,              //无
    Result,            //结算中
    Pause,             //暂停中
    Dialog_recover     //对话框恢复？
}

//连线类型
public enum EPathType : int
{
    Short_V = 1,//竖短
    Short_H,//横短
    Long_V,//长竖
    Long_H,//长横
    Left_Down,//左而后下
    Left_Up,//左而后上
    Right_Down,//右而后下
    Right_Up//右而后下
}

/// <summary>
/// 支付类型
/// </summary>
public enum EPayType : int
{
    None = 0,
    PayPal = 1,
    Venmo = 2,
    Zelle = 3,
    E_Transfer = 4,
    Mercado = 5,
    BBVA = 6,
    Pix = 7,
    PicPay = 8,
    Skrill = 9,
    Revolut = 10,
    Sofort = 11,
    Paylib = 12,
    PayPay = 13,
    Other = 14,
}

/// <summary>
/// 任务状态
/// </summary>
public enum ETaskStatus : int
{
    Progress = 0,//未完成
    Receive = 1,//已完成
    Received = 2,//已领取
}

/// <summary>
/// 获取道具类型
/// </summary>
public enum ERewardType : int
{
    Money = 1,//钱
    Func,//道具
    Withdrawable,//兑现机会
}

/// <summary>
/// 关卡类型
/// </summary>
public enum ELevelType : int
{
    Random = 0,
    Fixed,
}

//广告类型
public enum EAdtype
{
    Reward,//激励
    Interstitial,//插屏
    Banner,//横幅
}

//广告来源
public enum EAdSource 
{
    Awesome,//中途额外奖励界面
    ChallengeSuccessful,//关卡结算界面
    Congratulations,//任务领取界面
    FuncPopup_Hint,//获取提示道具界面
    FuncPopup_Refresh,//获取刷新道具界面
    FuncPopup_Remove,//获取移除道具界面
}

#endregion
