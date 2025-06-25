using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public static class FacadeGamePlay
    {
        public static Action<int> OnCountMoveChange;
        public static Action<int> OnCountRollbackChange;
        public static Action<int> OnCountRefreshChange;

        public static Action OnAddSpacePass;

        public static Action OnMinuteChange;


        public static Func<RectTransform> GetGamePlayMoneyRect;
        public static Func<RectTransform> GetGamePlayDiamondRect;
        public static Func<RectTransform> GetGamePlayMovePropRect;
        public static Func<RectTransform> GetGamePlayRollbackPropRect;
        public static Func<RectTransform> GetGamePlayRefreshPropRect;
        public static Action<int, bool> ChangeCountBool;

        public static Action OnMoneyFlyFinish;
        public static Action OnDiamondFlyFinish;

        public static Action ShowCompleteMask;
        public static Action OpenWidthdrawTip;
    }
}
