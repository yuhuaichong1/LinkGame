
/* 游戏定义脚本
 * 游戏中常量、枚举等内容请在这里统一定义
 */


#region 常量

public abstract class GameDefines
{
    public static string URL = "https://www.";//后台网址

    public const int map_margin_bottom = 5;//关卡显示区域下边距

    public const int OBS_FIXED_ID = 99;//不可移动的障碍物
    public const int OBS_MOVING_ID = 98;//可移动的障碍物
    public const int HID_FIXED_ID = 97;//不可移动的隐藏物
    public const int HID_MOVING_ID = 96;//可移动的隐藏物

    public const int STATE_NORMAL = 0;//物体状态——一般
    public const int STATE_HIDE = 1;//物体状态——隐藏
    public const int STATE_DISAPPEARING = 2;//物体状态——消失中
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

#endregion
