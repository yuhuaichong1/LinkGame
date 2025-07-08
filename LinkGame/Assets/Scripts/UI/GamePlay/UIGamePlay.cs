
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIGamePlay : BaseUI
    {
        

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
        }

        private void OnWithdrawalBtnClickHandle()        {
            if(PlayerFacade.GetWName() == "")
            {
                UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIWithdrawalChannel>(EUIType.EUIWithdrawalChannel);
            }
            
            
        }
        private void OnPrizeDrawBtnClickHandle()        {
            UIManager.Instance.OpenWindowAsync<UILuckMoment>(EUIType.EUILuckMoment);
        }
        private void OnTaskBtnClickHandle()        {
            UIManager.Instance.OpenWindowAsync<UITask>(EUIType.EUITask);
        }

        private void OnSettingBtnClickHandle()        {
            UIManager.Instance.OpenWindowAsync<UISetting>(EUIType.EUISetting);
        }
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
                    }	    private void OnRefushBtnClickHandle()        {
            if(GamePlayFacade.GetRefushCount?.Invoke() > 0)
            {
                GamePlayFacade.RefushFunc?.Invoke();
                GamePlayFacade.ChangeRefushCount?.Invoke(-1);
                ChangeFuncRefushCount();
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Refush);
            }        }	    private void OnRemoveBtnClickHandle()
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

        private string GetCountText(int count)
        {
            return count == 0 ? "+" : count.ToString();
        }

        private void ChangeFuncTipCount()
        {
            mTipCountText.text = GetCountText(GamePlayFacade.GetTipCount.Invoke()) ;
        }
        private void ChangeFuncRefushCount()
        {
            mRefushCountText.text = GetCountText(GamePlayFacade.GetRefushCount.Invoke());
        }
        private void ChangeFuncRemoveCount()
        {
            mRemoveCountText.text = GetCountText(GamePlayFacade.GetRemoveCount.Invoke());
        }
        private Transform GetMapTrans()
        {
            return mMap.transform;
        }


        protected override void OnDisable()
        { 
        
        }
        protected override void OnDispose()
        {
        
        }
    }
}