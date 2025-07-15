
/* 游戏定义脚本
 * 游戏中常量、枚举等内容请在这里统一定义
 */


#region 常量

using System.Collections.Generic;

public abstract class GameDefines
{
    public static string URL = "https://www.";                                                              //后台网址
    public static string nameString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";     //随机名称数组

    public const int map_margin_bottom = 5;                                                                 //关卡显示区域下边距

    public const int OBS_FIXED_ID = 99;                                                                     //不可移动的障碍物
    public const int OBS_MOVING_ID = 98;                                                                    //可移动的障碍物
    public const int HID_FIXED_ID = 97;                                                                     //不可移动的隐藏物
    public const int HID_MOVING_ID = 96;                                                                    //可移动的隐藏物

    public const int STATE_NORMAL = 0;                                                                      //物体状态——一般
    public const int STATE_HIDE = 1;                                                                        //物体状态——隐藏
    public const int STATE_DISAPPEARING = 2;                                                                //物体状态——消失中

    public static bool ifWithdrawal = true;                                                                 //是否提现

    public const string Default_Channels = "0";                                                             //默认支付方式
    public const string Default_Mark = "$";                                                                 //默认货币符号
    public const int Default_Decimal = 2;                                                                   //默认小数点位
    public const int Default_ExchangeRate = 1;                                                              //默认汇率
    public const ELanguageType Default_Language = ELanguageType.English;                                    //默认语言

    public const string Func_Hint_IconPath = "UI/FuncIcon/FuncIcon_Hint.png";                                //下三功能_提示_图片
    public const string Func_Refush_IconPath = "UI/FuncIcon/FuncIcon_Refush.png";                            //下三功能_刷新_图片
    public const string Func_Remove_IconPath = "UI/FuncIcon/FuncIcon_Remove.png";                            //下三功能_移除_图片
    public const string Func_Shift_IconPath = "UI/FuncIcon/FuncIcon_Shift.png";                              //下三功能_换向_图片

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

    public const float PRN_Effect_MoveTime = 0.5f;                                                            //“上方随机玩家兑现提示”特效移动时间
    public const float PRN_Effect_StayTime = 2.5f;                                                            //“上方随机玩家兑现提示”特效滞留时间
    public const float TL_Effect_XMoveTime = 0.5f;                                                            //“目标关卡提示”特效横向移动时间
    public const float TL_Effect_StayTime = 1f;                                                               //“目标关卡提示”特效滞留时间
    public const float TL_Effect_YMoveTime = 1f;                                                              //“目标关卡提示”特效竖向移动时间
    public const string FlyMoney_ObjPath = "Prefabs/Effect/FlyMoney.prefab";                                 //飞钱特效预制体
    public const float FlyMoney_ObjTime = 0.25f;                                                             //飞钱特效持续时间
    public const string FlyFunc_ObjPath = "Prefabs/Effect/FlyFunc.prefab";                                   //飞功能特效预制体
    public const float FlyFunc_ObjTime = 0.25f;                                                              //飞功能特效持续时间
    public const float TMDIcon_RoteTime = 1;                                                                 //指明移动方向旋转时间 
    public const float TMDIcon_MoveTime = 1;                                                                 //指明移动方向移动时间 
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
public enum EFuncType
{ 
    Tip,
    Refush,
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

#endregion
