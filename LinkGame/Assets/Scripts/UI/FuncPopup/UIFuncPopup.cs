using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIFuncPopup : BaseUI
    {
        private LanguageModule LanguageModule;
        private EFuncType eFuncType;
        private EAdSource eAdSource;
        private Action btnAction;

        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            LayoutRebuilder.ForceRebuildLayoutImmediate(mCSFText);
        }
        
        protected override void OnSetParam(params object[] args)
        {
            eFuncType = (EFuncType)args[0];
        }

        protected override void OnEnable()
        {
            //mProgessText.text = LanguageModule.GetText("");
            mProgessText.text = $"{GamePlayFacade.GetRemainPCT()}%";

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

            ShowAnim(mProgessTextRect);
            ShowAnim(mPlane);
        }

        //显示提示相关UI
        private void AddTipPlane()
        {
            mIcon.sprite = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Func_Hint_IconPath);
            //mContent.text = LanguageModule.GetText("");
            mContent.text = "Find title to clear";
            AddEffect();
        }

        //显示刷新相关UI
        private void AddRefushPlane()
        {
            mIcon.sprite = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Func_Refush_IconPath);
            //mContent.text = LanguageModule.GetText("");
            mContent.text = "Shuffle all titles";
            AddEffect();
        }

        //显示移除相关UI（现为改变方向功能)
        private void AddRemovePlane()
        {
            mIcon.sprite = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Func_Shift_IconPath);
            mContent.text = LanguageModule.GetText("10056");
            AddEffect();
        }

        private void AddEffect()
        {
            float addMoney = UnityEngine.Random.Range(GameDefines.GetFunc_ExtraMoney_Num.x, GameDefines.GetFunc_ExtraMoney_Num.y);
            int addFunc = 1;
            switch (eFuncType)
            {
                case EFuncType.Tip:
                    addFunc = GameDefines.GetFunc_Hint_Num;
                    eAdSource = EAdSource.FuncPopup_Hint;
                    break;
                case EFuncType.Refush:
                    addFunc = GameDefines.GetFunc_Refresh_Num;
                    eAdSource = EAdSource.FuncPopup_Refresh;
                    break;
                case EFuncType.Shift:
                    addFunc = GameDefines.GetFunc_Shift_Num;
                    eAdSource = EAdSource.FuncPopup_Remove;
                    break;
            }
            btnAction = () => {
                FacadeAd.PlayRewardAd(eAdSource, () =>
                {
                    FacadeEffect.PlayRewardEffect(new List<RewardItem>
                    {
                        new RewardItem()
                        {
                            type = ERewardType.Func,
                            count = addFunc,
                            extra = (int)eFuncType,
                        },
                        new RewardItem()
                        {
                            type = ERewardType.Money,
                            count = addMoney,
                        }
                    }, () =>
                    {
                        GamePlayFacade.ChangeTipCountShow.Invoke();
                        switch (eFuncType)
                        {
                            case EFuncType.Tip:
                                GamePlayFacade.ChangeTipCountShow.Invoke();
                                break;
                            case EFuncType.Refush:
                                GamePlayFacade.ChangeRefushCountShow.Invoke();
                                break;
                            case EFuncType.Shift:
                                GamePlayFacade.ChangeRemoveCountShow?.Invoke();
                                break;
                        }
                        GamePlayFacade.ChangeMoneyShow.Invoke();
                        FacadeEffect.PlayGetMoneyTipEffect(addMoney);
                    });

                    switch(eFuncType)
                    {
                        case EFuncType.Tip:
                            GamePlayFacade.ChangeTipCount(addFunc);
                            break;
                        case EFuncType.Refush:
                            GamePlayFacade.ChangeRefushCount(addFunc);
                            break;
                        case EFuncType.Shift:
                            GamePlayFacade.ChangeRemoveCount(addFunc);
                            break;
                    }
                    PlayerFacade.AddWMoney(addMoney);
                    HideAnim(mProgessTextRect);
                    HideAnim(mPlane, () =>
                    {
                        UIManager.Instance.CloseUI(EUIType.EUIFuncPopup);
                    });
                }, null);
            };
        }

        //退出界面
        private void OnExitBtnClickHandle()
        {
            HideAnim(mProgessTextRect);
            HideAnim(mPlane, () => 
            {
                UIManager.Instance.CloseUI(EUIType.EUIFuncPopup);
            });
        }
	    
        private void OnAdGetBtnClickHandle()
        {
            btnAction?.Invoke();
        }
        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}