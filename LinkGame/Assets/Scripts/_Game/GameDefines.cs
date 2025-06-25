
/* 游戏定义脚本
 * 游戏中常量、枚举等内容请在这里统一定义
 */


#region 常量

public abstract class GameDefines
{
    public static string URL = "https://www.";
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
#endregion
