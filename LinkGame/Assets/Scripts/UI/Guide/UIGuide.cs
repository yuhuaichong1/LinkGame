using System;
using System.Collections.Generic;
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
            FacadeGuide.CloseGuide += CloseGuide;
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
                mGuideTextBg.transform.position = FindTrans(info.diglogPos).position;
            }

            ifshow = info.handPos != null;
            mHander.gameObject.SetActive(ifshow);
            if (ifshow)
            {
                mHander.transform.position = FindTrans(info.handPos).position;
                mHand.Reset();
            }

            mHoleMask.alpha = info.ifMask ? 1 : 0;

            ifshow = info.transparentPos != null;
            mHole.gameObject.SetActive(ifshow);
            if (ifshow)
            {
                mHole.transform.position = FindTrans(info.transparentPos).position;
                mHole.sizeDelta = FindTrans(info.transparentPos).GetComponent<RectTransform>().sizeDelta;
            }

            float autoHiddenTime = info.autohiddenTime;
            if (autoHiddenTime != 0)
            {
                STimerManager.Instance.CreateSDelay(autoHiddenTime, () =>
                {
                    FacadeGuide.NextStep();
                });
            }
            else
            {
                ifshow = info.clickPos != null;
                mMask.penetrateObjs.Clear();
                if (ifshow)
                {
                    foreach(string str in info.clickPos)
                    {
                        mMask.penetrateObjs.Add(FindTrans(str).GetComponent<RectTransform>());
                    }
                    mMask.ifNext = info.ifNextPlay;
                }
            }

            if (info.extra != null && info.extra.Count != 0)
                SetExtraShow(info.extra);
        }

        private void SetExtraShow(Dictionary<string, string> extraData)
        {
            foreach (KeyValuePair<string, string> kvp in extraData)
            {
                switch (kvp.Key)
                {
                    case "line":
                        string[] value = kvp.Value.Split('|');

                        bool b = int.Parse(value[0]) == 1;
                        if (GameDefines.ifIAA) mCanNotLinkAct.gameObject.SetActive(b);
                        else
                        {
                            mCanNotLink.gameObject.SetActive(b);
                        }

                        if (b)
                        {
                            Vector3 target = GameObject.Find(UIManager.Instance.GetUIPath(value[1].Split(","))).transform.position;
                            float sc = GamePlayFacade.GetMapScale();
                            if (GameDefines.ifIAA)
                            {
                                mCanNotLinkAct.transform.position = target;
                                mCanNotLinkAct.transform.localScale = new Vector3(sc, sc, sc);
                            }
                            else
                            {
                                mCanNotLink.transform.position = target;
                                mCanNotLink.transform.localScale = new Vector3(sc, sc, sc);
                            }
                        }
                        break;
                    case "extraClick":
                        break;
                }
            }
        }

        private void PlayGuide()
        {
            mGuidePlane.gameObject.SetActive(true);

            SetGuideShow();
        }

        private void CloseGuide()
        {
            mGuidePlane.gameObject.SetActive(false);
        }

        private Transform FindTrans(string path)
        {
            return GameObject.Find(path).transform;
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}