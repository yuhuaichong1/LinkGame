
using System;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIGamePlay : BaseUI
    {
        private GamePlayModule GamePlayModule;
        

        protected override void OnAwake() 
        {
            GamePlayModule = ModuleMgr.Instance.GamePlayMod;
            LanguageModule languageMod = ModuleMgr.Instance.LanguageMod;

            GamePlayDefines.ChangeFuncTipCount += ChangeFuncTipCount;
            GamePlayDefines.ChangeFuncRefushCount += ChangeFuncRefushCount;
            GamePlayDefines.ChangeFuncRemoveCount += ChangeFuncRemoveCount;

            //mTipText.text = languageMod.GetText("");
            //mRefushText.text = languageMod.GetText("");
            //mRemoveText.text = languageMod.GetText("");

            ChangeFuncTipCount();
            ChangeFuncRefushCount();
            ChangeFuncRemoveCount();
        }
        protected override void OnEnable() 
        { 
            
        }
        	    private void OnSettingBtnClickHandle()        {
            //UIManager.Instance.OpenWindowAsync<UISetting>(EUIType.EUISetting);
        }
        private void OnTipBtnClickHandle()        {
            if(GamePlayModule.TipCount > 0)
            {
                GamePlayDefines.TipFunc();
                GamePlayModule.TipCount -= 1;
                ChangeFuncTipCount();
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Tip);
            }
                    }	    private void OnRefushBtnClickHandle()        {
            if(GamePlayModule.RefushCount > 0)
            {
                GamePlayDefines.RefushFunc();
                GamePlayModule.RefushCount -= 1;
                ChangeFuncRefushCount();
            }
            else
            {
                UIManager.Instance.OpenWindowAsync<UIFuncPopup>(EUIType.EUIFuncPopup, null, EFuncType.Refush);
            }        }	    private void OnRemoveBtnClickHandle()
        {
            if( GamePlayModule.RemoveCount > 0) 
            {
                GamePlayDefines.RemoveFunc();
                GamePlayModule.RemoveCount -= 1;
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
            mTipCountText.text = GetCountText(GamePlayModule.TipCount);
        }
        private void ChangeFuncRefushCount()
        {
            mRefushCountText.text = GetCountText(GamePlayModule.RefushCount);
        }
        private void ChangeFuncRemoveCount()
        {
            mRemoveCountText.text = GetCountText(GamePlayModule.RemoveCount);
        }

        protected override void OnDisable()
        { 
        
        }
        protected override void OnDispose()
        {
        
        }
    }
}