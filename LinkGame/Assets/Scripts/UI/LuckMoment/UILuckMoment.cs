using cfg;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UILuckMoment : BaseUI
    {
        private LanguageModule LanguageModule;

        private STimer sTimer;

        private Dictionary<int, LuckMomentItem> wheelDic;
        private Dictionary<int, float> wheelStayTime;
        private Image curImage;

        private Sprite NotActivatedBg;
        private Sprite ActivatedBg;

        private int curIcon;
        private int curTime;

        private int preRewardId;

        private Dictionary<int, int> proDic;

        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            wheelDic = new Dictionary<int, LuckMomentItem>()
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
            proDic = new Dictionary<int, int>();

            NotActivatedBg = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.LuckMomentNotActivatedBg);
            ActivatedBg = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.LuckMomentActivatedBg);

            if(curImage != null)
                curImage.sprite = NotActivatedBg;

            List<ConfLuckMoment> LMdata = ConfigModule.Instance.Tables.TBLuckMoment.DataList;
            foreach(ConfLuckMoment LM in LMdata)
            {
                wheelDic[LM.Sn].Icon.sprite = ResourceMod.Instance.SyncLoad<Sprite>(LM.Icon);
                wheelDic[LM.Sn].maxObj.SetActive(LM.IfMax);
                wheelDic[LM.Sn].Desc.text = GetLMDesc(LM.Type, LM.Extra, LM.Count);

                proDic.Add(LM.Sn, LM.Probability);
            }

            preRewardId = -1;

            proDic = new Dictionary<int, int>();
        }
        protected override void OnEnable() 
        {
            mExitBtn.gameObject.SetActive(true);

            int remaining = GameDefines.LuckMoment_Count_Max - GamePlayFacade.GetCurLuckMomentCount();
            bool b = remaining == 0;
            SpinBtnActive(b);
            mBottomText.text = b ? "" : string.Format(LanguageModule.GetText("10064"), remaining);

            if (preRewardId != -1)
                wheelDic[preRewardId].Bg.sprite = NotActivatedBg;

            ShowAnim(mPlane);
        }
        
        private string GetLMDesc(int type, int reward, float count)
        {
            
            switch (type)
            {
                case 1:
                    return FacadePayType.RegionalChange(count);
                case 2:
                    string str = "Func";
                    switch ((EFuncType)reward)
                    {
                        case EFuncType.Tip:
                            str = "10017";
                            break;
                        case EFuncType.Refush:
                            str = "10019";
                            break;
                        case EFuncType.Shift:
                            str = "10018";
                            break;
                        case EFuncType.Remove:
                            str = "10087";
                            break;
                    }
                    return LanguageModule.GetText(str); 
                case 3:
                    return "Withdraw";
                    //return LanguageModule.GetText("Withdraw");
                default:
                    return "";
            }
        }

        private void OnExitBtnClickHandle()
        {
            HideAnim(mPlane, () => 
            {
                UIManager.Instance.CloseUI(EUIType.EUILuckMoment);
            });
        }

	    private void OnSpinBtnClickHandle()
        {
            mExitBtn.gameObject.SetActive(false);
            SpinBtnActive(false);

            wheelStayTime.Clear();
            int random = GetProbability.GatValue(proDic);
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

            curImage = mSMItem2.Bg;
            curImage.sprite = ActivatedBg;

            curTime = 0;
            curIcon = 0;

            sTimer = STimerManager.Instance.CreateSTimer(wheelStayTime[curTime], movTimes, true, true, () => 
            {
                curImage.sprite = NotActivatedBg;
                curIcon += 1;
                if (curIcon >= 8)
                    curIcon = 0;
                curImage = wheelDic[curIcon].Bg;
                curImage.sprite = ActivatedBg;

                if(wheelStayTime.ContainsKey(curTime))
                {
                    sTimer.targetTime = wheelStayTime[curTime];
                    curTime += 1;
                }
                else
                {
                    GamePlayFacade.SetCurLuckMomentCount(0);
                    D.Error($"发放奖励：{random}");
                    preRewardId = random;
                    STimerManager.Instance.CreateSDelay(1, () => 
                    {
                        ConfLuckMoment confLuckMoment = ConfigModule.Instance.Tables.TBLuckMoment.Get(random);
                        ERewardType type = (ERewardType)confLuckMoment.Type;
                        switch (type) 
                        {
                            case ERewardType.Money:
                                //float Mcount = UnityEngine.Random.Range(1, confLuckMoment.Count);
                                float Mcount = confLuckMoment.Count;
                                PlayerFacade.AddWMoney(Mcount);
                                FacadeEffect.PlayRewardEffect(new List<RewardItem>()
                                {
                                    new RewardItem()
                                    {
                                        type = ERewardType.Money,
                                        count = Mcount
                                    }
                                }, () => 
                                {
                                    GamePlayFacade.ChangeMoneyShow();
                                });
                                break;
                            case ERewardType.Func:
                                int Fcount = (int)confLuckMoment.Count;
                                int funcTypeId = confLuckMoment.Extra;
                                switch ((EFuncType)funcTypeId)
                                { 
                                    case EFuncType.Tip:
                                        GamePlayFacade.ChangeTipCount(Fcount);
                                        break;
                                    case EFuncType.Refush:
                                        GamePlayFacade.ChangeRefushCount(Fcount);
                                        break;
                                    case EFuncType.Shift:
                                        GamePlayFacade.ChangeRemoveCount(Fcount);
                                        break;
                                }
                                FacadeEffect.PlayRewardEffect(new List<RewardItem>()
                                {
                                    new RewardItem()
                                    {
                                        type = ERewardType.Func,
                                        count = Fcount,
                                        extra = funcTypeId
                                    }
                                }, () =>
                                {
                                    switch ((EFuncType)funcTypeId)
                                    {
                                        case EFuncType.Tip:
                                            GamePlayFacade.ChangeTipCountShow();
                                            break;
                                        case EFuncType.Refush:
                                            GamePlayFacade.ChangeRefushCountShow();
                                            break;
                                        case EFuncType.Shift:
                                            GamePlayFacade.ChangeRemoveCountShow();
                                            break;
                                    }
                                });
                                break;
                            case ERewardType.Withdrawable:
                                if (PlayerFacade.GetPayType() == 0)
                                {
                                    UIManager.Instance.OpenWindowAsync<UIEnterInfo>(EUIType.EUIEnterInfo);
                                }
                                else
                                {
                                    UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
                                }
                                break;
                        }

                        HideAnim(mPlane, () =>
                        {
                            UIManager.Instance.CloseUI(EUIType.EUILuckMoment);
                        });
                    });
                }
            });
        }

        private void SpinBtnActive(bool b)
        {
            mSpinBtn.enabled = b;
            mAbleBg.gameObject.SetActive(b);
            mDisAbleBg.gameObject.SetActive(!b);

        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}