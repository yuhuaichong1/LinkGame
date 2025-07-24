
using System;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace XrCode
{

    public partial class UIGuide : BaseUI
    {
        private Action clickAction;

        private Dictionary<int, int> level1GuideGoodDic;
        private Dictionary<string, RectTransform> posTrans;

        protected override void OnAwake() 
        {
            FacadeGuide.PlayGuide += PlayGuide;


        }
        protected override void OnEnable() 
        {
            
        }

        //引导按钮回调
        private void OnGuideBtnClickHandle()
        {
            clickAction?.Invoke();
        }

        private void SetGuideShow()
        {
            GuideItem info = FacadeGuide.GetCurGuideItems();

            bool ifshow;

            ifshow = info.diglogPos != null;
            mGuideTextBg.gameObject.SetActive(ifshow);
            if (ifshow)
            {
                mGuideText.text = info.diglogContent;
                mGuideTextBg.transform.position = info.diglogPos.position;
            }

            ifshow = info.handPos != null;
            mHander.gameObject.SetActive(ifshow);
            if(ifshow)
            {
                mHander.transform.position = info.handPos.position;
                mHand.Reset();
            }

            mHoleMask.gameObject.SetActive(info.ifMask);

            ifshow = info.transparentPos != null;
            mHole.gameObject.SetActive(ifshow);
            if(ifshow)
            {
                mHole.transform.position = info.transparentPos.position;
                mHole.sizeDelta = info.transparentPos.GetComponent<RectTransform>().sizeDelta;
            }

            float autoHiddenTime = info.autohiddenTime;
            if (autoHiddenTime != 0)
            {
                STimerManager.Instance.CreateSDelay(autoHiddenTime, () => 
                { 
                    FacadeGuide.NextStep(true); 
                });
            }
            else
            {
                ifshow = info.btnPos != null;
                mGuideBtn.gameObject.SetActive(ifshow);
                if (ifshow)
                {
                    mGuideBtn.transform.position = info.btnPos.position;
                    mGuideBtn.GetComponent<RectTransform>().sizeDelta = info.btnPos.GetComponent<RectTransform>().sizeDelta;
                }

                //mGuideBtn.onClick.RemoveAllListeners();
                //mGuideBtn.onClick.AddListener(() => { FacadeGuide.NextStep(true); });
                clickAction = () => { FacadeGuide.NextStep(true); };
            }

            if(info.extra.Count != 0)
                SetExtraShow(info.extra);
        }

        private void SetExtraShow(Dictionary<string, string> extraData)
        {
            foreach(KeyValuePair<string, string> kvp in extraData)
            {
                switch(kvp.Key) 
                {
                    case "btn":
                        float sizeScale = int.Parse(kvp.Value) == 1 ? GamePlayFacade.GetMapScale() : 1;
                        mGuideBtn.transform.localScale = new Vector3(sizeScale, sizeScale, sizeScale);
                        break;
                    case "line":
                        string[] value = kvp.Value.Split('|');

                        bool b = int.Parse(value[0]) == 1;
                        mCanNotLink.gameObject.SetActive(b);
                        if(b)
                        {
                            Vector3 target = GameObject.Find(UIManager.Instance.GetUIPath(value[1].Split(","))).transform.position;
                            mCanNotLink.transform.position = target;
                        }
                        break;
                }
            }
        }

        private void PlayGuide(int step)
        {
            FacadeGuide.SetGuide(step);
            SetGuideShow();
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}