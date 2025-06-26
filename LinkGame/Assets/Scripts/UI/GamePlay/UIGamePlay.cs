
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

            GamePlayDefines.ChangeTipCountShow += ChangeFuncTipCount;
            GamePlayDefines.ChangeRefushCountShow += ChangeFuncRefushCount;
            GamePlayDefines.ChangeRemoveCountShow += ChangeFuncRemoveCount;

            //mTipText.text = languageMod.GetText("");
            //mRefushText.text = languageMod.GetText("");
            //mRemoveText.text = languageMod.GetText("");

            ChangeFuncTipCount();
            ChangeFuncRefushCount();
            ChangeFuncRemoveCount();
        }
        protected override void OnEnable() 
        {
            GamePlayDefines.CreateLevel?.Invoke();
        }
        	    private void OnSettingBtnClickHandle()        {
            //UIManager.Instance.OpenWindowAsync<UISetting>(EUIType.EUISetting);
        }
        private void OnTipBtnClickHandle()        {
            if(GamePlayDefines.GetTipCount?.Invoke() > 0)
            {
                GamePlayDefines.TipFunc?.Invoke();
                GamePlayDefines.ChangeTipCount?.Invoke(-1);
                ChangeFuncTipCount();
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Tip);
            }
                    }	    private void OnRefushBtnClickHandle()        {
            if(GamePlayDefines.GetRefushCount?.Invoke() > 0)
            {
                GamePlayDefines.RefushFunc?.Invoke();
                GamePlayDefines.ChangeTipCount?.Invoke(-1);
                ChangeFuncRefushCount();
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Refush);
            }        }	    private void OnRemoveBtnClickHandle()
        {
            if(GamePlayDefines.GetRemoveCount?.Invoke() > 0) 
            {
                GamePlayDefines.RemoveFunc?.Invoke();
                GamePlayDefines.ChangeRemoveCount.Invoke(-1);
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
            mTipCountText.text = GetCountText(GamePlayDefines.GetTipCount.Invoke()) ;
        }
        private void ChangeFuncRefushCount()
        {
            mRefushCountText.text = GetCountText(GamePlayDefines.GetRefushCount.Invoke());
        }
        private void ChangeFuncRemoveCount()
        {
            mRemoveCountText.text = GetCountText(GamePlayDefines.GetRemoveCount.Invoke());
        }

        protected override void OnDisable()
        { 
        
        }
        protected override void OnDispose()
        {
        
        }
    }
}