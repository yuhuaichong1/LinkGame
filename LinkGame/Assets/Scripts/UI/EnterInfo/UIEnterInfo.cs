
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIEnterInfo : BaseUI
    {
        private Dictionary<int, int> PayTypeDic;
        private int curPayType;

        protected override void OnAwake()
        {
            PayTypeDic = new Dictionary<int, int>();

            List<PayItem> payTypes = FacadePayType.GetPayItems();

            SetToggle(payTypes);

            //LayoutRebuilder.ForceRebuildLayoutImmediate(mPayType1Toggle.GetComponent<RectTransform>());
            //mTick.transform.position = mPayType1Toggle.transform.position;

            STimerManager.Instance.CreateSDelay(0.1f, () => 
            {
                PayInputShow(mPayType1Toggle.transform.position, PayTypeDic[1]);
            });
            
        }
        protected override void OnEnable() 
        {
            
        }
                //设置Toggle图像和事件        private void SetToggle(List<PayItem> payTypes)
        {
            mPY1Icon.sprite = payTypes[0].picture;
            mPayType1Toggle.onValueChanged.AddListener(OnPayType1TogChangeHandle);
            PayTypeDic.Add(1, payTypes[0].info);

            if (payTypes.Count >= 2)
            {
                mPayType2Toggle.gameObject.SetActive(true);
                mPY2Icon.sprite = payTypes[1].picture;
                mPayType2Toggle.onValueChanged.AddListener(OnPayType2TogChangeHandle);
                PayTypeDic.Add(1, payTypes[1].info);
            }

            if (payTypes.Count >= 3)
            {
                mPayType3Toggle.gameObject.SetActive(payTypes.Count >= 3);
                mPY3Icon.sprite = payTypes[2].picture;
                mPayType3Toggle.onValueChanged.AddListener(OnPayType3TogChangeHandle);
                PayTypeDic.Add(1, payTypes[2].info);
            }
        }	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);        }	    private void OnHelpBtnClickHandle()        {            UIManager.Instance.OpenWindowAsync<UIWithdrawalChannel>(EUIType.EUIWithdrawalChannel);        }	    private void OnConfirmBtnClickHandle()
        {
            if(mNameInput.text != "")
            {
                switch (curPayType)
                {
                    case 1:
                        if(mAddressInput.text != "")
                        {
                            PlayerFacade.SetNameAndWEmailOrPhone(mNameInput.text, mAddressInput.text);
                            UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);
                            UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
                        }
                        else
                        {
                            UIManager.Instance.OpenNotice("没填地址");
                        }
                        break;
                    case 2:
                        if (mPhoneInput.text != "")
                        {
                            PlayerFacade.SetNameAndWEmailOrPhone(mNameInput.text, mPhoneInput.text);
                            UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);
                            UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
                        }
                        else
                        {
                            UIManager.Instance.OpenNotice("没填电话");
                        }
                        break;
                    case 3:
                        if (mAddressOrPhoneInput.text != "")
                        {
                            PlayerFacade.SetNameAndWEmailOrPhone(mNameInput.text, mAddressOrPhoneInput.text);
                            UIManager.Instance.CloseUI(EUIType.EUIEnterInfo);
                            UIManager.Instance.OpenWindowAsync<UIWithdrawalInformation>(EUIType.EUIWithdrawalInformation);
                        }
                        else
                        {
                            UIManager.Instance.OpenNotice("没填地址/电话");
                        }
                        break;
                }

                
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
                PayInputShow(mPayType1Toggle.transform.position, PayTypeDic[1]);
            }
        }

        private void OnPayType2TogChangeHandle(bool b)
        {
            if (b)
            {
                PayInputShow(mPayType2Toggle.transform.position, PayTypeDic[2]);
            }
        }

        private void OnPayType3TogChangeHandle(bool b)
        {
            if (b)
            {
                PayInputShow(mPayType3Toggle.transform.position, PayTypeDic[3]);
            }
        }

        //显影输入框和指定勾位置
        private void PayInputShow(Vector3 TickPos, int payType)
        {
            mTick.transform.position = TickPos;

            mAddressInput.gameObject.SetActive(payType == 1);
            mAddressOrPhoneInput.gameObject.SetActive(payType == 3);
            mPhone.gameObject.SetActive(payType == 2);

            curPayType = payType;
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}