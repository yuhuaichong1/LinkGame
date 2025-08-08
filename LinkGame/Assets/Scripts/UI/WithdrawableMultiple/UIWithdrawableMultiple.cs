
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIWithdrawableMultiple : BaseUI
    {
        private LanguageModule LanguageModule;
        private WithdrawMItem[] wItems;

        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;
            mNoticeScrollView.Play();

            wItems = new WithdrawMItem[3] 
            {
                new WithdrawMItem(mLevelTitle, mGoalTitle, LanguageModule),
                new WithdrawMItem(mPreLevelTitle, mPregoalTitle, LanguageModule),
                new WithdrawMItem(mPrePreLevelTitle, mPrePregoalTitle, LanguageModule),
            };
        }
        protected override void OnEnable()
        {
            int curLevel = GamePlayFacade.GetCurLevel();
            if (curLevel <= GameDefines.withdrawLevel)
            {
                mLevelTitle.text = $"{LanguageModule.GetText("10049")} {GameDefines.withdrawLevel}";
                mGoalTitle.text = $"{LanguageModule.GetText("10031")} {1}";
            }
            else if(curLevel <= GameDefines.doubleLevel) 
            {
                mLevelTitle.text = $"{LanguageModule.GetText("10049")} {GameDefines.doubleLevel}";
                mGoalTitle.text = $"{LanguageModule.GetText("10031")} {2}";
            }
            else
            {
                mLevelTitle.text = $"{LanguageModule.GetText("10049")} {ConfigModule.Instance.Tables.TBLevel.DataList.Count}";
                mGoalTitle.text = $"{LanguageModule.GetText("10031")} {3}";
            }

            mCurWMoney.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());

            DateTime currentDate = DateTime.Now;
            mDNTitle.text = string.Format(ModuleMgr.Instance.LanguageMod.GetText("10027"), currentDate.Month, currentDate.Day, currentDate.Year);
            ShowNoticeInfo();

            ShowAnim(mDayNotice);
            ShowAnim(mPlanes, () => 
            {
                int a = FacadeGuide.GetCurStep();
                if (GamePlayFacade.GetIsTutorial() && FacadeGuide.GetCurStep() == 10014 || FacadeGuide.GetCurStep() == 10018)
                {
                    FacadeGuide.PlayGuide();
                }
            });
        }
        	    private void OnExitBtnClickHandle()        {            HideAnim(mDayNotice);            HideAnim(mPlanes, () =>             {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawableMultiple);            });        }	    private void OnWithdrawBtnClickHandle()
        {
            HideAnim(mDayNotice);            HideAnim(mPlanes, () =>
            {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawableMultiple);
                if (PlayerFacade.GetPayType() == 0)
                {
                    UIManager.Instance.OpenWindowAsync<UIEnterInfo>(EUIType.EUIEnterInfo);
                }
                else
                {
                    UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
                }
            });
        }

        private void ShowNoticeInfo()
        {
            int curLevel = GamePlayFacade.GetCurLevel();

            switch(curLevel) 
            {
                case 1:
                    ShowNoticeInfo2(FacadeTimeZone.GetCurZTime() * 50, 1, GameDefines.ifIAA ? GameDefines.Single_Link_Diamond : GameDefines.Single_Link_Money * 6);
                    break;
                case 2:
                    ShowNoticeInfo2(FacadeTimeZone.GetCurZTime() * 50, 1, GameDefines.ifIAA ? GameDefines.Single_Link_Diamond : GameDefines.Single_Link_Money * 12);
                    break;
                default:
                    ShowNoticeInfo2(FacadeTimeZone.GetCurZTime() * UnityEngine.Random.Range(20, 26), UnityEngine.Random.Range(2, 6), UnityEngine.Random.Range(3f, 10f));
                    break;
            }  
        }

        private void ShowNoticeInfo2(int PNpeople, int ACNpeople, float AWNpeople)
        {
            mPNpeopleCount.text = PNpeople.ToString();
            mACNpeopleCount.text = ACNpeople.ToString();
            mAWNpeopleCount.text = FacadePayType.RegionalChange(AWNpeople);
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}