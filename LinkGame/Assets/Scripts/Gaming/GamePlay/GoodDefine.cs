using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodDefine : MonoBehaviour
{
    
}

//物品移动方向枚举
public enum EGoodMoveDic
{
    None,//不移动
    Up,//朝上移动
    Down,//朝下移动
    Left,//朝左移动
    Right,//朝右移动
}

//物品状态
public enum EGoodStatus
{
    None,//正常状态
    Hide,//隐藏状态
    Stone,//障碍状态
}
