using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItem
{
    public ERewardType type;
    public float count;
    public int extra;//额外值，目前用于区分当type为Func时的对应下三功能
}
