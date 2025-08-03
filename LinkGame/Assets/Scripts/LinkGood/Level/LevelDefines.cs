using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDefines
{
    public static string time_dead = "time_dead";
    public static string time_star_1 = "time_star_1";
    public static string time_star_2 = "time_star_2";
    public static string time_star_3 = "time_star_3";
    public static string good_fixed = "good_fixed";
    public static string row = "row";
    public static string col = "col";
    public static string id = "id";
    public static string stones_fixed = "stones_fixed";
    public static string stones_moving = "stones_moving";
    public static string frozens_fixed = "frozens_fixed";
    public static string constraint = "constraint";
    public static string direction = "direction";
    public static string cell1 = "cell1";
    public static string cell2 = "cell2";
    public static string reward = "reward";
    public static string auto_gen = "auto_gen";
    public static string type = "type";
    public static string time_gen = "time_gen";
    public static string time_gen_wait = "time_gen_wait";
    public static string number = "number";
    public static string probability = "probability";

    public static int maxLevel = 100;
}

public enum ELevelMode
{ 
    Normal,//一般关卡
    Daily,//日常关卡
}

