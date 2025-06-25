using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SUIAnimCurve", menuName = "SUIAnim/SUIAnimCurve", order = 1)]
public class SUIAnimCurve : ScriptableObject
{
    public List<SUIACItem> sUIACItems;
    private Dictionary<string, AnimationCurve> _SUIACDic;

    public Dictionary<string, AnimationCurve> SUIACDic
    {
        get
        {
            if (sUIACItems != null)
            {
                _SUIACDic = new Dictionary<string, AnimationCurve>();
                foreach (var item in sUIACItems)
                {
                    _SUIACDic.Add(item.sId, item.animationCurve);
                }
            }
            return _SUIACDic;
        }
    }
}
