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
            currentLevel = SPlayerPrefs.GetInt(PlayerPrefDefines.curLevel);
            SPlayerPrefs.SetInt(PlayerPrefDefines.curLevel, ++currentLevel);
            Debug.LogError("���ùؿ�++:" + currentLevel);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentLevel = SPlayerPrefs.GetInt(PlayerPrefDefines.curLevel);
            SPlayerPrefs.SetInt(PlayerPrefDefines.curLevel, ++currentLevel);
            Debug.LogError("���ùؿ�--:" + currentLevel);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentLevel = ModuleMgr.Instance.GamePlayMod.CurLevel;
            Debug.LogError("��ǰ�ؿ�:" + currentLevel);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GamePlayFacade.ChangeRemoveCount(999999);
            Debug.LogError("���ô���:");
        }


    }
}
