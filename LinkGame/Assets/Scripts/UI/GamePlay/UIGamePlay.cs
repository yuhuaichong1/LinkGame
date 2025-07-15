using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using System.IO;
using System.Collections.Generic;
using cfg;

namespace XrCode
{

    public partial class UIGamePlay : BaseUI
    {
        private int curLevel;
        private Sprite curLevelDicIcon;
        private int lastLevelId;
        private int secondLastLevelId;
        private STimer showDelay;
        private STimer loopDelay;
        private Dictionary<int, ShakeRotateLeftRight> funcTips;
        private ShakeRotateLeftRight curFuncTip;

        private LanguageModule LanguageModule;

        private Dictionary<int, float> sliderDic;

        private bool ifDicEffectShow;

        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            GamePlayFacade.ChangeTipCountShow += ChangeFuncTipCount;
            GamePlayFacade.ChangeRefushCountShow += ChangeFuncRefushCount;
            GamePlayFacade.ChangeRemoveCountShow += ChangeFuncRemoveCount;
            GamePlayFacade.GetMapTrans += GetMapTrans;
            //mTipText.text = languageMod.GetText("");
            //mRefushText.text = languageMod.GetText("");
            //mRemoveText.text = languageMod.GetText("");

            funcTips = new Dictionary<int, ShakeRotateLeftRight>
            {
                {0, mTipFuncTip},
                {1, mRefushFuncTip}, 
                {2, mRemoveFuncTip},
            };

            ChangeFuncTipCount();
            ChangeFuncRefushCount();
            ChangeFuncRemoveCount();

            //根据屏幕宽高比控制物体缩放大小
            float screenScale = Screen.width * 1f / Screen.height;
            float mapScale = -3.3507f * screenScale + 2.8858f;
            mMap.localScale = new Vector3(mapScale, mapScale, mapScale);

            
            lastLevelId = ConfigModule.Instance.Tables.TBLevel.DataList.Count - 1;
            secondLastLevelId = lastLevelId - 1;
            sliderDic = new Dictionary<int, float>() 
            {
                {1, 0},
                {2, 0.25f},
                {secondLastLevelId, 0.75f},
                {lastLevelId, 1f}
            };

            UIManager.Instance.OpenWindowAsync<UIEffect>(EUIType.EUIEffect);
        }
        protected override void OnEnable() 
        {
            curLevel = GamePlayFacade.GetCurLevel();

            GamePlayFacade.CreateLevel?.Invoke();

            SetTipInfo();

            FuncTipShow();

            mWithdrawTip.gameObject.SetActive(false);
            mCurDir.gameObject.SetActive(false);
            STimerManager.Instance.CreateSDelay(1, () => 
            {
                //WithdrawTipShow();
                TMDTipShow();
            });          
        }

        //设置当前关卡的可显示信息
        private void SetTipInfo()
        {
            Debug.LogError("curLevel  " + curLevel);

            //兑现所有的目标关卡提示
            mWithdrawTipText.text = string.Format(LanguageModule.GetText(""), curLevel);

            //箭头图片
            
            int MoveDicId = ConfigModule.Instance.Tables.TBLevel.Get(curLevel).MoveDic;
            ifDicEffectShow = MoveDicId != 0;
            if (ifDicEffectShow)
            {
                string MDPath = ConfigModule.Instance.Tables.TBLevelDicIcon.GetOrDefault(MoveDicId).Path;
                curLevelDicIcon = ResourceMod.Instance.SyncLoad<Sprite>(MDPath);
                mCurDir.sprite = curLevelDicIcon;
            }

            Dictionary<int, ConfLevel> levelDic = ConfigModule.Instance.Tables.TBLevel.DataMap;

            int[] levels;

            if (curLevel == 1 || curLevel == 2)
            {
                levels = new int[5] { 1, 2, 3, 4, 5 };
            }
            else if(curLevel == secondLastLevelId || curLevel == lastLevelId)
            {
                levels = new int[5] { secondLastLevelId - 3, secondLastLevelId - 2, secondLastLevelId - 1, secondLastLevelId, lastLevelId };
            }
            else
            {
                levels = new int[5] { curLevel - 2, curLevel - 1, curLevel, curLevel + 1, curLevel + 2 };
            }
            mCurLevelItem1.SetCurLevelInfo(levels[0]);
            mCurLevelItem2.SetCurLevelInfo(levels[1]);
            mCurLevelItem3.SetCurLevelInfo(levels[2]);
            mCurLevelItem4.SetCurLevelInfo(levels[3]);
            mCurLevelItem5.SetCurLevelInfo(levels[4]);

            if(sliderDic.ContainsKey(curLevel))
                mSlider.value = sliderDic[curLevel];
            else
                mSlider.value = 0.5f;


        }

        //兑现按钮点击
        private void OnWithdrawalBtnClickHandle()        {
            if(PlayerFacade.GetPayType() == 0)
            {
                UIManager.Instance.OpenWindowAsync<UIEnterInfo>(EUIType.EUIEnterInfo);
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
            }
        }
        //老虎机按钮点击
        private void OnPrizeDrawBtnClickHandle()        {
            UIManager.Instance.OpenWindowAsync<UILuckMoment>(EUIType.EUILuckMoment);
        }
        //任务按钮点击
        private void OnTaskBtnClickHandle()        {
            UIManager.Instance.OpenWindowAsync<UITask>(EUIType.EUITask);
        }
        //设置按钮点击
        private void OnSettingBtnClickHandle()        {
            UIManager.Instance.OpenWindowAsync<UISetting>(EUIType.EUISetting);
        }
        //提示功能按钮点击
        private void OnTipBtnClickHandle()        {
            if(GamePlayFacade.GetTipCount?.Invoke() > 0)
            {
                GamePlayFacade.TipFunc?.Invoke();
                GamePlayFacade.ChangeTipCount?.Invoke(-1);
                ChangeFuncTipCount();
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Tip);
            }
                    }
        //刷新功能按钮点击
        private void OnRefushBtnClickHandle()        {
            if(GamePlayFacade.GetRefushCount?.Invoke() > 0)
            {
                FacadeEffect.PlayCloudEffect(() => 
                {
                    GamePlayFacade.RefushFunc?.Invoke();
                    GamePlayFacade.ChangeRefushCount?.Invoke(-1);
                    ChangeFuncRefushCount();
                });
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Refush);
            }        }
        //移除（现为变向）功能按钮点击
        private void OnRemoveBtnClickHandle()
        {
            if(GamePlayFacade.GetRemoveCount?.Invoke() > 0) 
            {
                //GamePlayFacade.RemoveFunc?.Invoke();
                EGoodMoveDic newDic = GamePlayFacade.ChangeDirection.Invoke();
                GamePlayFacade.ChangeRemoveCount.Invoke(-1);
                ChangeFuncRemoveCount();

                string path = ConfigModule.Instance.Tables.TBLevelDicIcon.Get((int)newDic).Path;
                curLevelDicIcon = ResourceMod.Instance.SyncLoad<Sprite>(path);
                mCurDir.sprite = curLevelDicIcon;
                mCurDir.gameObject.SetActive(false);
                TMDTipShow();
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Remove);
            }
        }
        //中心按钮被点击
        private void OnCurMoneyBtnClickHandle()
        {
            UIManager.Instance.OpenWindowAsync<UIWithdrawableMultiple>(EUIType.EUIWithdrawableMultiple);
        }
        //决定功能按钮剩余数量的显示方式
        private string GetCountText(int count)
        {
            return count == 0 ? "+" : count.ToString();
        }

        //改变提示功能的剩余数量
        private void ChangeFuncTipCount()
        {
            mTipCountText.text = GetCountText(GamePlayFacade.GetTipCount.Invoke()) ;
        }
        //改变刷新功能的剩余数量
        private void ChangeFuncRefushCount()
        {
            mRefushCountText.text = GetCountText(GamePlayFacade.GetRefushCount.Invoke());
        }
        //改变移除（现为变向）功能的剩余数量
        private void ChangeFuncRemoveCount()
        {
            mRemoveCountText.text = GetCountText(GamePlayFacade.GetRemoveCount.Invoke());
        }
        //得到生成物体的父对象
        private Transform GetMapTrans()
        {
            return mMap.transform;
        }

        //随机显示功能按钮提示
        private void FuncTipShow()
        {
            mTipFuncTip.gameObject.SetActive(false);
            mRefushFuncTip.gameObject.SetActive(false);
            mRemoveFuncTip.gameObject.SetActive(false);

            
            if (loopDelay == null)
            {
                loopDelay = STimerManager.Instance.CreateSTimer(GameDefines.Default_FuncLoopTipDelay * 2, -1, true, false, null, null, new timingActions 
                {
                    timing = GameDefines.Default_FuncLoopTipDelay,
                    clockAction = (time) => 
                    {
                        curFuncTip = funcTips[UnityEngine.Random.Range(0, 3)];
                        curFuncTip.gameObject.SetActive(true);
                    },
                    clockActionType = ClockActionType.Once

                }, new timingActions 
                {
                    timing = GameDefines.Default_FuncLoopTipDelay * 2,
                    clockAction = (time) => 
                    {
                        curFuncTip.gameObject.SetActive(false);
                    },
                    clockActionType = ClockActionType.Once
                });
            }
            else
            {
                loopDelay.Stop();
            }

            if(showDelay != null)
            {
                showDelay = null;
            }
            showDelay = STimerManager.Instance.CreateSDelay(GameDefines.Default_FuncShowTipDelay, () => 
            {
                loopDelay.Start();
            });
        }

        //提现目标特效以及UI生成
        private void WithdrawTipShow()
        {
            FacadeEffect.PlayLevelTargetEffect(mWithdrawTip.transform, () => 
            {
                mWithdrawTip.gameObject.SetActive(true);
            });
        }

        //指明方向特效以及UI生成
        private void TMDTipShow()
        {
            if (!ifDicEffectShow) return;

            FacadeEffect.PlayTMDEffect(mCurDir.transform, curLevelDicIcon, () => 
            { 
                mCurDir.gameObject.SetActive(true);
            });
        }

        protected override void OnDisable()
        { 
        
        }
        protected override void OnDispose()
        {
        
        }
    }
}