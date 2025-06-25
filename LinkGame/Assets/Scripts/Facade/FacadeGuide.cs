using System;
using System.Collections.Generic;
using UnityEngine;

public static class FacadeGuide
{
    public static Action SetPos;
    public static Action<string> SetHand;
    public static Action<string, string> SetDialog;
    public static Action<string> SetHole;
    public static Action<string, Action> SetBtn;
    public static Action<Transform> SetObjToHere;
    public static Func<List<RectTransform>> GetGuideTargetPos;
    public static Func<Dictionary<int, int>> GetLevel1GuideGoodDic;
    public static Action<int> Step123Action;

}
