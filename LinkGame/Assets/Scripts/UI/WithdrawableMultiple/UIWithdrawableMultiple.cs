
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
            int curWLevel = GamePlayFacade.GetCurWLevel();
            Stack<int> withQueue = new Stack<int>(GamePlayFacade.GetWithdrawableLevel().ToArray());
            int count = withQueue.Count;
            mPreLevelPlane.gameObject.SetActive(count >= 2);
            mPrePreLevelPlane.gameObject.SetActive(count >= 3);
            for(int i = 0; i < count; i++)
            {
                wItems[i].SetInfo(curWLevel - i, withQueue.Pop());
                if(i == 0)
                {
                    mTargetText.text = string.Format(LanguageModule.GetText("10014"), curWLevel - i);
                }
            }
            mCurWMoney.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());

            #region old
            //mLevelTitle.text = string.Format(LanguageModule.GetText(""), 1);
            //mGoalTitle.text = string.Format(LanguageModule.GetText(""), 1);
            //mCurWMoney.text = FacadePayType.RegionalChange(PlayerFacade.GetWMoney());

            //if (withQueue.Count > 1)
            //{
            //    mPreLevelTitle.text = string.Format(LanguageModule.GetText(""), 2);
            //    mPregoalTitle.text = string.Format(LanguageModule.GetText(""), 2);
            //}
            //if(withQueue.Count > 2)
            //{
            //    mPrePreLevelTitle.text = string.Format(LanguageModule.GetText(""), 12);
            //    mPrePregoalTitle.text = string.Format(LanguageModule.GetText(""), 12);
            //}
            #endregion

            DateTime currentDate = DateTime.Now;
            mDNTitle.text = string.Format(ModuleMgr.Instance.LanguageMod.GetText("10027"), currentDate.Year, currentDate.Month, currentDate.Day);
            ShowNoticeInfo();

            if(GamePlayFacade.GetIsTutorial() && FacadeGuide.GetWithdrawableUIcheck())
            {
                FacadeGuide.PlayGuide();
            }

            //ShowAnim(mDayNotice);
            //ShowAnim(mPlanes);
        }
        	    private void OnExitBtnClickHandle()        {            HideAnim(mDayNotice);            HideAnim(mPlanes, () =>             {
                UIManager.Instance.CloseUI(EUIType.EUIWithdrawableMultiple);            });        }	    private void OnWithdrawBtnClickHandle()
        {
            //UIManager.Instance.OpenNotice(LanguageModule.GetText(""));
            //HideAnim(mDayNotice);            //HideAnim(mPlanes, () =>
            //{
                            //});

            UIManager.Instance.CloseUI(EUIType.EUIWithdrawableMultiple);
            if (PlayerFacade.GetPayType() == 0)
            {
                UIManager.Instance.OpenWindowAsync<UIEnterInfo>(EUIType.EUIEnterInfo);
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
            }
        }

        private void ShowNoticeInfo()
        {
            int curLevel = GamePlayFacade.GetCurLevel();

            switch(curLevel) 
            {
                case 1:
                    ShowNoticeInfo2(FacadeTimeZone.GetCurZTime() * 50, 1, GameDefines.Single_Link_Money * 6);
                    break;
                case 2:
                    ShowNoticeInfo2(FacadeTimeZone.GetCurZTime() * 50, 1, GameDefines.Single_Link_Money * 12);
                    break;
                default:
                    ShowNoticeInfo2(FacadeTimeZone.GetCurZTime() * UnityEngine.Random.Range(20, 26), UnityEngine.Random.Range(18, 23), UnityEngine.Random.Range(100f, 200f));
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