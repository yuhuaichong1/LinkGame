
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIRate : BaseUI
    {
        List<GameObject> lightStar;

        private int ratePoint;
        protected override void OnAwake() 
        {
            lightStar = new List<GameObject>()
            {
                mStar1Light.gameObject,
                mStar2Light.gameObject,
                mStar3Light.gameObject,
                mStar4Light.gameObject,
                mStar5Light.gameObject,
            };
        }
        protected override void OnEnable()
        {
            foreach (GameObject obj in lightStar) 
            { 
                obj.SetActive(false);
            }

            mGreyRateBtn.gameObject.SetActive(true);
        }
        	    private void OnExitBtnClickHandle()        {            UIManager.Instance.CloseUI(EUIType.EUIRate);        }	    private void OnStar1ClickHandle()        {            StarBtnClick(0);        }	    private void OnStar2ClickHandle()        {            StarBtnClick(1);        }	    private void OnStar3ClickHandle()        {
            StarBtnClick(2);        }	    private void OnStar4ClickHandle()        {
            StarBtnClick(3);        }	    private void OnStar5ClickHandle()        {
            StarBtnClick(4);        }        private void StarBtnClick(int index)
        {
            ratePoint = index + 1;

            mGreyRateBtn.gameObject.SetActive(false);

            for (int i = 0; i < lightStar.Count; i++)
            {
                lightStar[i].SetActive(i <= index);
            }
        }	    private void OnRateBtnClickHandle()
        {
            D.Error($"打分：{ratePoint}");
            UIManager.Instance.CloseUI(EUIType.EUIRate);
        }
        protected override void OnDisable()
        {
        
        }
        protected override void OnDispose()
        {
        
        }
    }
}