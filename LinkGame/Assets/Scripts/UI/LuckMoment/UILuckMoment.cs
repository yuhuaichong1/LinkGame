
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UILuckMoment : BaseUI
    {
        private STimer sTimer;

        private Dictionary<int, Image> wheelDic;
        private Dictionary<int, float> wheelStayTime;
        private Image curImage;

        private Sprite NotActivatedBg;
        private Sprite ActivatedBg;

        private int curIcon;
        private int curTime;

        protected override void OnAwake() 
        {
            wheelDic = new Dictionary<int, Image>()
            {
                {0, mSMItem2},
                {1, mSMItem3},
                {2, mSMItem4},
                {3, mSMItem5},
                {4, mSMItem6},
                {5, mSMItem7},
                {6, mSMItem8},
                {7, mSMItem1},
            };

            wheelStayTime = new Dictionary<int, float>();

            NotActivatedBg = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.LuckMomentNotActivatedBg);
            ActivatedBg = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.LuckMomentActivatedBg);

            if(curImage != null)
                curImage.sprite = NotActivatedBg;
        }
        protected override void OnEnable() { }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUILuckMoment);        }	    private void OnSpinBtnClickHandle()
        {
            DisableSpinBtn();

            wheelStayTime.Clear();
            int random = UnityEngine.Random.Range(0, 8);
            int movTimes = GameDefines.Default_LM_Accelerate_Times + GameDefines.Default_LM_Uniform_Times + random + GameDefines.Default_LM_Moderate_Times - 1;
            int lastSome = movTimes - GameDefines.Default_LM_Moderate_Times - 1;

            float Aadd = (GameDefines.Default_LM_Accelerate_Speed - GameDefines.Default_LM_Uniform_Speed) * 1f / GameDefines.Default_LM_Accelerate_Times;
            float Madd = (GameDefines.Default_LM_Moderate_Speed - GameDefines.Default_LM_Uniform_Speed) * 1f / GameDefines.Default_LM_Moderate_Times;

            wheelStayTime.Clear();

            int j = 0;
            for(int i = 0; i < movTimes; i++)
            {
                if(i < GameDefines.Default_LM_Accelerate_Times)
                {
                    wheelStayTime.Add(i, 0.25f - i * Aadd);
                }
                else if(i >= lastSome)
                {
                    wheelStayTime.Add(i, GameDefines.Default_LM_Uniform_Speed + j * Madd);
                    j += 1;
                }  
                else
                {
                    wheelStayTime.Add(i, GameDefines.Default_LM_Uniform_Speed);
                }
            }

            curImage = mSMItem2;
            curImage.sprite = ActivatedBg;

            curTime = 0;
            curIcon = 0;

            sTimer = STimerManager.Instance.CreateSTimer(wheelStayTime[curTime], movTimes, true, true, () => 
            {
                curImage.sprite = NotActivatedBg;
                curIcon += 1;
                if (curIcon >= 8)
                    curIcon = 0;
                curImage = wheelDic[curIcon];
                curImage.sprite = ActivatedBg;

                if(wheelStayTime.ContainsKey(curTime))
                {
                    sTimer.targetTime = wheelStayTime[curTime];
                    curTime += 1;
                }
                else
                {
                    Debug.LogError("发放奖励吧：" + random);
                }
            });
        }

        private void DisableSpinBtn()
        {

        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}