
using System;
using UnityEngine;

namespace XrCode
{
    public partial class UIFuncPopup : BaseUI
    {
        private EFuncType eFuncType;
        private Action btnAction;
        private AdModule AdModule;

        protected override void OnAwake() 
        {

        }
        
        protected override void OnSetParam(params object[] args)
        {
            eFuncType = (EFuncType)args[0];
        }

        protected override void OnEnable()
        {
            switch (eFuncType)
            {
                case EFuncType.Tip:
                    AddTipPlane();
                    break;
                case EFuncType.Refush:
                    AddRefushPlane();
                    break;
                case EFuncType.Remove:
                    AddRemovePlane();
                    break;
                default:
                    D.Error("Unknown Func Icon Type!");
                    goto case EFuncType.Tip;
            }
        }

        //显示提示相关UI
        private void AddTipPlane()
        {
            mIcon.sprite = ResourceMod.Instance.SyncLoad<Sprite>("UI/FuncIcon/FuncIcon_Tip.png");
            btnAction = () => {
                AdModule.PlayRewardAd(() =>
                {
                    GamePlayDefines.ChangeTipCount(3);
                    GamePlayDefines.ChangeTipCountShow?.Invoke();
                });
            };
        }

        //显示刷新相关UI
        private void AddRefushPlane()
        {
            mIcon.sprite = ResourceMod.Instance.SyncLoad<Sprite>("UI/FuncIcon/FuncIcon_Refush.png");
            btnAction = () => {
                AdModule.PlayRewardAd(() =>
                {
                    GamePlayDefines.ChangeRefushCount(1);
                    GamePlayDefines.ChangeRefushCountShow?.Invoke();
                });
            };
        }

        //显示移除相关UI
        private void AddRemovePlane()
        {
            mIcon.sprite = ResourceMod.Instance.SyncLoad<Sprite>("UI/FuncIcon/FuncIcon_Remove.png");
            btnAction = () => {
                AdModule.PlayRewardAd(() =>
                {
                    GamePlayDefines.ChangeRemoveCount(1);
                    GamePlayDefines.ChangeRemoveCountShow?.Invoke();
                });
            };
        }

        //退出界面
        private void OnExitBtnClickHandle()        {
            UIManager.Instance.CloseUI(EUIType.EUIFuncPopup);        }	    
        private void OnAdGetBtnClickHandle()
        {
            btnAction?.Invoke();
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}