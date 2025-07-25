
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIEnterInfo : BaseUI
    {
        private Dictionary<int, PayItem> PayTypeDic;
        private int curPayInfo;
        private EPayType curPayType;
        private LanguageModule LanguageModule;

        protected override void OnAwake()
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            PayTypeDic = new Dictionary<int, PayItem>();

            List<PayItem> payTypes = FacadePayType.GetPayItems();

            SetToggle(payTypes);

            mNamePlaceholder.text = LanguageModule.GetText("10035");
            mAddressPlaceholder.text = LanguageModule.GetText("10036");
            mPhonePlaceholder.text = LanguageModule.GetText("10037");
            mAddressOrPhonePlaceholder.text = LanguageModule.GetText("10043");
            mPhoneAreaCodeText.text = $"+{FacadePayType.GetNANP()}";
        }
        protected override void OnEnable() 
        {
            OldValue();
        }

        //如果是从Withrawal Information界面跳转回来的，则赋予旧值
        protected void OldValue()
        {
            int select = 1;

            curPayType = PlayerFacade.GetPayType();

            if(curPayType != EPayType.None && curPayType != EPayType.Other)
            {
                foreach(KeyValuePair<int, PayItem> item in PayTypeDic)
                {
                    if(curPayType == item.Value.payType)
                    {
                        select = item.Key;

                        break;
                    }
                }
            }

            STimerManager.Instance.CreateSDelay(0.05f, () =>
            {
                PayInputShow(mPayType1Toggle.transform.position, PayTypeDic[select].info, PayTypeDic[select].payType);
            });

            mNameInput.text = PlayerFacade.GetWName();

            switch (PayTypeDic[select].info)
            {
                case 1:
                    mAddressInput.text = PlayerFacade.GetWEmailOrPhone();
                    break;
                case 2:
                    mPhoneInput.text = PlayerFacade.GetWEmailOrPhone();
                    break;
                case 3:
                    mAddressOrPhoneInput.text = PlayerFacade.GetWEmailOrPhone();
                    break;
            }

        }


        //设置Toggle图像和事件
        private void SetToggle(List<PayItem> payTypes)
        {
            mPY1Icon.sprite = payTypes[0].picture;
            mPayType1Toggle.onValueChanged.AddListener(OnPayType1TogChangeHandle);
            PayTypeDic.Add(1, payTypes[0]);

            if (payTypes.Count >= 2)
            {
                mPayType2Toggle.gameObject.SetActive(true);
                mPY2Icon.sprite = payTypes[1].picture;
                mPayType2Toggle.onValueChanged.AddListener(OnPayType2TogChangeHandle);
                PayTypeDic.Add(2, payTypes[1]);
            }

            if (payTypes.Count >= 3)
            {
                mPayType3Toggle.gameObject.SetActive(payTypes.Count >= 3);
                mPY3Icon.sprite = payTypes[2].picture;
                mPayType3Toggle.onValueChanged.AddListener(OnPayType3TogChangeHandle);
                PayTypeDic.Add(3, payTypes[2]);
            }
        }	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);            FacadeGuide.CheckWithdrawableUI();        }	    private void OnHelpBtnClickHandle()        {            UIManager.Instance.OpenWindowAsync<UIWithdrawalChannel>(EUIType.EUIWithdrawalChannel);        }	    private void OnConfirmBtnClickHandle()
        {
            if(mNameInput.text != "")
            {
                string name = mNameInput.text;
                string pe = "";
                switch (curPayInfo)
                {
                    case 1:
                        if(mAddressInput.text != "")
                        {
                            pe = mAddressInput.text;
                        }
                        else
                        {
                            UIManager.Instance.OpenNotice("没填地址");
                            return;
                        }
                        break;
                    case 2:
                        if (mPhoneInput.text != "")
                        {
                            pe = mPhoneInput.text;
                        }
                        else
                        {
                            UIManager.Instance.OpenNotice("没填电话");
                            return;
                        }
                        break;
                    case 3:
                        if (mAddressOrPhoneInput.text != "")
                        {
                            pe = mAddressOrPhoneInput.text;
                        }
                        else
                        {
                            UIManager.Instance.OpenNotice("没填地址/电话");
                            return;
                        }
                        break;
                }

                PlayerFacade.SetPayType(curPayType);
                PlayerFacade.SetNameAndWEmailOrPhone(name, pe);
                UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);
                UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
            }
            else
            {
                UIManager.Instance.OpenNotice("没填姓名");
            }
        }

        private void OnPayType1TogChangeHandle(bool b)
        {
            if (b)
            {
                PayInputShow(mPayType1Toggle.transform.position, PayTypeDic[1].info, PayTypeDic[1].payType);
            }
        }

        private void OnPayType2TogChangeHandle(bool b)
        {
            if (b)
            {
                PayInputShow(mPayType2Toggle.transform.position, PayTypeDic[2].info, PayTypeDic[2].payType);
            }
        }

        private void OnPayType3TogChangeHandle(bool b)
        {
            if (b)
            {
                PayInputShow(mPayType3Toggle.transform.position, PayTypeDic[3].info, PayTypeDic[3].payType);
            }
        }

        //显影输入框和指定勾位置
        private void PayInputShow(Vector3 TickPos, int payInfo, EPayType type)
        {
            mTick.transform.position = TickPos;

            mAddressInput.gameObject.SetActive(payInfo == 1);
            mAddressOrPhoneInput.gameObject.SetActive(payInfo == 3);
            mPhone.gameObject.SetActive(payInfo == 2);

            curPayInfo = payInfo;
            curPayType = type;
        }

        protected override void OnDisable() 
        { 

        }

        protected override void OnDispose() 
        {
            
        }
    }
}