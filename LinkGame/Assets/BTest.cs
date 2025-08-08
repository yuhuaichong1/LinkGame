using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

public class BTest : MonoBehaviour
{
    int currentLevel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentLevel = ModuleMgr.Instance.GamePlayMod.CurLevel;
            SPlayerPrefs.SetInt(PlayerPrefDefines.curLevel, ++currentLevel);
            Debug.LogError("设置关卡++:" + currentLevel);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentLevel = ModuleMgr.Instance.GamePlayMod.CurLevel;
            SPlayerPrefs.SetInt(PlayerPrefDefines.curLevel, --currentLevel);
            Debug.LogError("设置关卡--:" + currentLevel);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentLevel = ModuleMgr.Instance.GamePlayMod.CurLevel;
            Debug.LogError("当前关卡:" + currentLevel);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GamePlayFacade.ChangeRemoveCount(10000);
            Debug.LogError("设置次数:");
        }


    }
}
