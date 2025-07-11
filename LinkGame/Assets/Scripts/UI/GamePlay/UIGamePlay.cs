using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using System.IO;
using System.Collections.Generic;

namespace XrCode
{

    public partial class UIGamePlay : BaseUI
    {
        private STimer showDelay;
        private STimer loopDelay;
        private Dictionary<int, ShakeRotateLeftRight> funcTips;
        private ShakeRotateLeftRight curFuncTip;

        protected override void OnAwake() 
        {
            LanguageModule languageMod = ModuleMgr.Instance.LanguageMod;

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
        }
        protected override void OnEnable() 
        {
            GamePlayFacade.CreateLevel?.Invoke();

            FuncTipShow();
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
                GamePlayFacade.RefushFunc?.Invoke();
                GamePlayFacade.ChangeRefushCount?.Invoke(-1);
                ChangeFuncRefushCount();
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
                GamePlayFacade.ChangeDirection?.Invoke();
                GamePlayFacade.ChangeRemoveCount.Invoke(-1);
                ChangeFuncRemoveCount();
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

        protected override void OnDisable()
        { 
        
        }
        protected override void OnDispose()
        {
        
        }
    }
}