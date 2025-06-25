
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
            FacadeGuide.SetPos += SetPos;
            FacadeGuide.SetHand += SetHand;
            FacadeGuide.SetDialog += SetDialog;
            FacadeGuide.SetHole += SetHole;
            FacadeGuide.SetBtn += SetBtn;
            FacadeGuide.SetObjToHere += SetObjToHere;

            level1GuideGoodDic = FacadeGuide.GetLevel1GuideGoodDic();
            posTrans = new Dictionary<string, RectTransform>();

            GetAllChildrenRecursive(mPos);
        }
        protected override void OnEnable() 
        {
            
        }

        private void SetHand(string posTarget)
        {
            if (posTarget == "*")
            {
                mHander.gameObject.SetActive(false);
            }
            else
            {
                mHander.gameObject.SetActive(true);
                mHander.position = posTrans[$"{posTarget}"].position;
            }
            
        }

        private void SetDialog(string posTarget, string content) 
        {
            if (posTarget == "*")
            {
                mGuideTextBg.gameObject.SetActive(false);
            }
            else
            {
                mGuideTextBg.gameObject.SetActive(true);
                mGuideTextBg.position = posTrans[$"{posTarget}"].position;
                mGuideText.text = ModuleMgr.Instance.LanguageMod.GetText(content);
            }
        }

        private void SetHole(string posTarget)
        {
            if (posTarget == "*")
            {
                mHole.gameObject.SetActive(false);
            }
            else
            {
                mHole.gameObject.SetActive(true);
                RectTransform rect = posTrans[$"{posTarget}"];
                mHole.position = rect.position;
                mHole.sizeDelta = new Vector3(posTrans[$"{posTarget}"].rect.width, posTrans[$"{posTarget}"].rect.height);
            }
        }

        private void SetBtn(string posTarget, Action clickAction)
        {
            if (posTarget == "*")
            {
                mGuideBtn.gameObject.SetActive(false);
            }
            else
            {
                RectTransform rect = mGuideBtn.GetComponent<RectTransform>();
                mGuideBtn.gameObject.SetActive(true);
                rect.position = posTrans[$"{posTarget}"].position;

                rect.sizeDelta = new Vector3(posTrans[$"{posTarget}"].rect.width, posTrans[$"{posTarget}"].rect.height);
                this.clickAction = clickAction;
            }
        }

        //引导按钮回调
        private void OnGuideBtnClickHandle()
        {
            clickAction?.Invoke();
        }

        //将某个物体放置到该UI之下
        private void SetObjToHere(Transform targetTrans)
        {
            targetTrans.SetParent(mObjTrans);
        }

        //设置位置信息
        private void SetPos()
        {
            List<RectTransform> rects = FacadeGuide.GetGuideTargetPos();
            Transform Good1Pos = rects[3].transform;
            Transform Good2Pos = rects[4].transform;
            Transform Good3Pos = rects[5].transform;

            posTrans["BP1"].transform.position = Good1Pos.position;
            posTrans["BP2"].transform.position = Good2Pos.position;
            posTrans["BP3"].transform.position = Good3Pos.position;

            posTrans["HP1"].transform.position = Good1Pos.position;
            posTrans["HP2"].transform.position = Good2Pos.position;
            posTrans["HP3"].transform.position = Good3Pos.position;

            int i1 = level1GuideGoodDic[1] > 3 ? 1 : -1;
            posTrans["DP1"].anchoredPosition = new Vector2(0, posTrans["BP1"].GetComponent<RectTransform>().anchoredPosition.y + 270 * i1);
            int i2 = level1GuideGoodDic[2] > 3 ? 1 : -1;
            posTrans["DP2"].anchoredPosition = new Vector2(0, posTrans["BP2"].GetComponent<RectTransform>().anchoredPosition.y + 270 * i2);
            int i3 = level1GuideGoodDic[3] > 3 ? 1 : -1;
            posTrans["DP3"].anchoredPosition = new Vector2(0, posTrans["BP3"].GetComponent<RectTransform>().anchoredPosition.y + 270 * i3);

            posTrans["HP4"].transform.position = rects[6].transform.position;
        }

        //递归获取所有子物体
        void GetAllChildrenRecursive(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                posTrans.Add(child.name, child.GetComponent<RectTransform>());
                GetAllChildrenRecursive(child);
            }
        }

        protected override void OnDisable() { }
        protected override void OnDispose() { }
    }
}