using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using System.IO;
using System.Collections.Generic;
using cfg;
using DG.Tweening;
using Unity.VisualScripting;

namespace XrCode
{
    public partial class UIGamePlayBy : BaseUI
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

        private bool loadedEffectUI;

        private float mapScale;

        protected override void OnAwake()
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            GamePlayFacade.ChangeMoneyShow += ChangeMoneyShow;
            GamePlayFacade.ChangeTipCountShow += ChangeFuncTipCount;
            GamePlayFacade.ChangeRefushCountShow += ChangeFuncRefushCount;
            GamePlayFacade.ChangeRemoveCountShow += ChangeFuncRemoveCount;
            GamePlayFacade.GetMapTrans += GetMapTrans;
            GamePlayFacade.GetObsTrans += GetObsTrans;
            GamePlayFacade.GetFlyMoneyTarget += GetFlyMoneyTarget;
            GamePlayFacade.GetFlyMoneyTipOrgin += GetFlyMoneyTipOrgin;
            GamePlayFacade.GetFuncTarget += GetFuncTarget;
            GamePlayFacade.GetMapScale += GetMapScale;

            funcTips = new Dictionary<int, ShakeRotateLeftRight>
            {
                {0, mTipFuncTip},
                {1, mRefushFuncTip},
                {2, mRemoveFuncTip},
            };

            ChangeFuncTipCount();
            ChangeFuncRefushCount();
            ChangeFuncRemoveCount();

            //根据屏幕宽高比以及物体排布数量控制物体缩放大小
            float screenScale = Screen.width * 1f / Screen.height;
            mapScale = -3.3507f * screenScale + 2.8858f;

            lastLevelId = ConfigModule.Instance.Tables.TBLevel.DataList.Count;
            secondLastLevelId = lastLevelId - 1;
            sliderDic = new Dictionary<int, float>()
            {
                {1, 0},
                {2, 0.25f},
                {secondLastLevelId, 0.75f},
                {lastLevelId, 1f}
            };

            UIManager.Instance.OpenWindowAsync<UIEffect>(EUIType.EUIEffect);

            FacadeRedDot.SetRDNodeAction_ByName(GameDefines.Reddot_Name_Out, (kind, num) =>
            {
                mReddotText.text = num.ToString();
                mReddot.gameObject.SetActive(num != 0);
            }, SetRDNodeKind.Add);
            FacadeRedDot.RefushRDNode_ByName(GameDefines.Reddot_Name_Out, true);
        }
        protected override void OnEnable()
        {
            ConfLevel level = ConfigModule.Instance.Tables.TBLevel.Get(GamePlayFacade.GetCurLevel());
            float sizeScale = mapScale * level.SizeExtra;
            mMap.localScale = new Vector3(sizeScale, sizeScale, sizeScale);

            curLevel = GamePlayFacade.GetCurLevel();

            GamePlayFacade.CreateLevel?.Invoke();

            SetTipInfo();

            if (GamePlayFacade.GetIsTutorial())
            {
                mTipFuncTip.gameObject.SetActive(false);
                mRefushFuncTip.gameObject.SetActive(false);
                mRemoveFuncTip.gameObject.SetActive(false);

                GoodShowOneByOne(() =>
                {
                    mGamePlayMask.gameObject.SetActive(false);

                    UIManager.Instance.OpenWindowAsync<UIGuide>(EUIType.EUIGuide, (baseUI) =>
                    {
                        if (FacadeGuide.GetCurGuideItems().ifBackPlay)
                            FacadeGuide.PlayGuide();
                        else
                            FacadeGuide.CloseGuide();
                    });

                });
            }
            else
            {
                FuncTipShow();

                if (true)//首次（重开）进入关卡
                {
                    mWithdrawTip.gameObject.SetActive(false);
                    mCurDir.gameObject.SetActive(false);

                    GoodShowOneByOne(WithdrawTipShow);
                }
                else
                {
                    GoodShowOneByOne(() =>
                    {
                        if (GamePlayFacade.GetNumberGoodCanEat() == 0)
                        {
                            UIManager.Instance.OpenNotice(LanguageModule.GetText("10094"));
                        }
                        else
                        {
                            STimerManager.Instance.CreateSDelay(1, () => { FacadeEffect.PlayRewardNoticeEffect(); });
                        }
                    });

                }
            }

            mCurMoneyText.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());
            mPrizeDrawBtn.gameObject.SetActive(GamePlayFacade.GetCurLevel() != 1 && GamePlayFacade.GetCurLevel() != 2);
        }

        //设置当前关卡的可显示信息
        private void SetTipInfo()
        {
            //箭头图片
            int MoveDicId = ConfigModule.Instance.Tables.TBLevel.Get(curLevel).MoveDic;
            ifDicEffectShow = MoveDicId != 0;
            if (ifDicEffectShow)
            {
                string MDPath = ConfigModule.Instance.Tables.TBLevelDicIcon.GetOrDefault(MoveDicId).Path;
                curLevelDicIcon = ResourceMod.Instance.SyncLoad<Sprite>(MDPath);
                mCurDir.sprite = curLevelDicIcon;
            }

            //关卡进度物体显示
            //兑现所有的目标关卡提示
            int[] levels;
            if (curLevel == 1 || curLevel == 2)//前2关
            {
                int fsTLevel = ConfigModule.Instance.Tables.TBWithdrawableLevels.Get(3).Level;
                levels = new int[5] { 1, 2, 3, 4, fsTLevel };
                mWithdrawTipText.text = LanguageModule.GetText("10012");
            }
            else if (curLevel == secondLastLevelId || curLevel == lastLevelId)//倒数2关
            {
                levels = new int[5] { secondLastLevelId - 3, secondLastLevelId - 2, secondLastLevelId - 1, secondLastLevelId, lastLevelId };
            }
            else
            {
                int target = GetNextTarget(curLevel);
                levels = new int[5] { curLevel - 2, curLevel - 1, curLevel, curLevel + 1, target };
                if (WLevels.Contains(curLevel))
                    mWithdrawTipText.text = LanguageModule.GetText("10012");
                else
                    mWithdrawTipText.text = string.Format(LanguageModule.GetText("10013"), WLevels.Contains(curLevel + 1) ? 2 : target - curLevel + 1);

            }
            mCurLevelItem1.SetCurLevelInfo(levels[0]);
            mCurLevelItem2.SetCurLevelInfo(levels[1]);
            mCurLevelItem3.SetCurLevelInfo(levels[2]);
            mCurLevelItem4.SetCurLevelInfo(levels[3]);
            mCurLevelItem5.SetCurLevelInfo(levels[4]);

            //关卡进度显示
            if (sliderDic.ContainsKey(curLevel))
                mSlider.value = sliderDic[curLevel];
            else
                mSlider.value = 0.5f;


        }

        
        private void OnWithdrawalBtnClickHandle()
        {

            UIManager.Instance.OpenWindowAsync<UIDailyTasks>(EUIType.EUIDailyTasks);

        }
        //老虎机按钮点击
        private void OnPrizeDrawBtnClickHandle()
        {
            UIManager.Instance.OpenWindowAsync<UILuckMoment>(EUIType.EUILuckMoment);
        }
        //任务按钮点击
        private void OnTaskBtnClickHandle()
        {
            UIManager.Instance.OpenWindowAsync<UIChallengeTask>(EUIType.EUIChallengeTask);
        }
        //设置按钮点击
        private void OnSettingBtnClickHandle()
        {
            UIManager.Instance.OpenWindowAsync<UISetting>(EUIType.EUISetting);
        }
        //提示功能按钮点击
        private void OnTipBtnClickHandle()
        {
            if (GamePlayFacade.GetNumberGoodCanEat() == 0)
            {
                UIManager.Instance.OpenNotice(LanguageModule.GetText("10094"));
                return;
            }
            if (GamePlayFacade.GetIfHintFunc())
            {
                UIManager.Instance.OpenNotice(LanguageModule.GetText("10093"));
                return;
            }

            if (GamePlayFacade.GetTipCount?.Invoke() > 0)
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
        private void OnRefushBtnClickHandle()
        {
            if (GamePlayFacade.GetRefushCount?.Invoke() > 0)
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
            }
        }
        //移除功能按钮点击
        private void OnRemoveBtnClickHandle()
        {
            if (GamePlayFacade.GetIfRemoveFunc())
            {
                UIManager.Instance.OpenNotice(LanguageModule.GetText("10093"));
                return;
            }

            if (GamePlayFacade.GetRemoveCount?.Invoke() > 0)
            {
                //GamePlayFacade.RemoveFunc?.Invoke();
                GamePlayFacade.RemoveFunc3();
                GamePlayFacade.ChangeRemoveCount.Invoke(-1);
                ChangeFuncRemoveCount();

                #region old(转向功能)
                //EGoodMoveDic newDic = GamePlayFacade.ChangeDirection.Invoke();
                //GamePlayFacade.ChangeRemoveCount.Invoke(-1);
                //ChangeFuncRemoveCount();

                //string path = ConfigModule.Instance.Tables.TBLevelDicIcon.Get((int)newDic).Path;
                //curLevelDicIcon = ResourceMod.Instance.SyncLoad<Sprite>(path);
                //mCurDir.sprite = curLevelDicIcon;
                //mCurDir.gameObject.SetActive(false);
                //TMDTipShow();
                #endregion
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Shift);
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
            mTipCountText.text = GetCountText(GamePlayFacade.GetTipCount.Invoke());
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
        //得到生成的隐藏物体的父对象
        private Transform GetObsTrans()
        {
            return mObstacle.transform;
        }
        private Transform GetFlyMoneyTarget()
        {
            return mCurMoneyIcon.transform;
        }

        private Transform GetFlyMoneyTipOrgin()
        {
            return mCurMoneyBtnRect.transform;
        }

        private Transform GetFuncTarget(EFuncType type)
        {
            switch (type)
            {
                case EFuncType.Tip:
                    return mTipFuncIcon.transform;
                case EFuncType.Refush:
                    return mRefushFuncIcon.transform;
                case EFuncType.Shift:
                    return mRemoveFuncIcon.transform;
                default:
                    return null;
            }
        }

        //改变当前金钱的数量
        private void ChangeMoneyShow()
        {
            mCurMoneyText.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());
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

            if (showDelay != null)
            {
                showDelay = null;
            }
            showDelay = STimerManager.Instance.CreateSDelay(GameDefines.Default_FuncShowTipDelay, () =>
            {
                loopDelay.Start();
            });
        }

        //逐个显示物体
        private void GoodShowOneByOne(Action successAction = null)
        {
            mGamePlayMask.gameObject.SetActive(true);

            GameObject[][] goods = GamePlayFacade.GetMAPGoods();

            foreach (GameObject[] goodsRow in goods)
            {
                foreach (GameObject obj in goodsRow)
                {
                    if (obj != null)
                    {
                        obj.gameObject.SetActive(false);
                        obj.GetComponent<Good>().mIcon.transform.localScale = Vector3.zero;
                    }
                }
            }

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < goods.Length; i++)
            {
                int currentIndex = i;
                sequence.AppendCallback(() =>
                {
                    foreach (GameObject obj in goods[currentIndex])
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                            obj.GetComponent<Good>().mIcon.transform.DOScale(1, GameDefines.GoodShowTime);
                        }
                    }
                });
                sequence.AppendInterval(GameDefines.GoodShowTime);
            }

            sequence.Play().OnComplete(() => { successAction?.Invoke(); });
        }

        //提现目标特效以及UI生成
        private void WithdrawTipShow()
        {
            FacadeEffect.PlayLevelTargetEffect(mWithdrawTip.transform, () =>
            {
                mWithdrawTip.gameObject.SetActive(true);
                TMDTipShow();
            });
        }

        //指明方向特效以及UI生成
        private void TMDTipShow()
        {
            if (!ifDicEffectShow)
            {
                TMDTipShow2();
            }
            else
            {
                FacadeEffect.PlayTMDEffect(mCurDir.transform, curLevelDicIcon, () =>
                {
                    TMDTipShow2();
                });
            }
        }
        private void TMDTipShow2()
        {
            mCurDir.gameObject.SetActive(true);
            mGamePlayMask.gameObject.SetActive(false);
            FacadeEffect.PlayRewardNoticeEffect();
        }
        private float GetMapScale()
        {
            return mapScale;
        }

        protected override void OnDisable()
        {

        }
        protected override void OnDispose()
        {

        }





        private List<int> WLevels = new List<int>();
        private int GetNextTarget(int curLevel)
        {
            List<ConfWithdrawableLevels> WLevelsData = ConfigModule.Instance.Tables.TBWithdrawableLevels.DataList;
            foreach (var item in WLevelsData)
            {
                int lv = item.Level;
                WLevels.Add(lv);
            }

            int target = ConfigModule.Instance.Tables.TBLevel.DataList.Count;

            for (int i = 0; i < WLevels.Count; i++)
            {
                if (WLevels[i] >= curLevel)
                {
                    if (WLevels[i] == curLevel + 1)
                        target = WLevels[i + 1];
                    else
                        target = WLevels[i];

                    break;
                }
            }

            return target;
        }
    }
}