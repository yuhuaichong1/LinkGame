using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EDataManager
{
    [MenuItem("EDataManager/Clear/RankData")]
    public static void ClearRankData()
    {
        PlayerPrefs.DeleteKey("GoodMatch_rankNames");
        PlayerPrefs.DeleteKey("GoodMatch_addLevelTotal");
        PlayerPrefs.DeleteKey("GoodMatch_addMoneyTotal");
        PlayerPrefs.DeleteKey("GoodMatch_lastTime");
    }

    [MenuItem("EDataManager/Clear/CurLevelData")]
    public static void ClearCurLevelData()
    {
        PlayerPrefs.DeleteKey("GoodMatch_curLevelPassNum");
        PlayerPrefs.DeleteKey("GoodMatch_curLevelAverageTimes");
        PlayerPrefs.DeleteKey("GoodMatch_curLevelAverageMoney");
    }

    [MenuItem("EDataManager/Clear/All")]
    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
