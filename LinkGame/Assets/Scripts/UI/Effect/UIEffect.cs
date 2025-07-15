using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

namespace XrCode
{
    public partial class UIEffect : BaseUI
    {
        private LanguageModule LanguageModule;
        private List<Sprite> icons;

        private float PGRNotice_OrginY;
        private float PGRNotice_MoveY;

        private int ScreenWidth;
        private float LeftCloudOrignPosX;
        private float RightCloudOrignPosX;
        private float LeftCloudMovePosX;
        private float RightCloudMovePosX;

        private Stack<GameObject> flyMoneyPool;
        private Stack<GameObject> flyFuncPool;

        protected override void OnAwake() 
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            flyMoneyPool = new Stack<GameObject>();
            flyFuncPool = new Stack<GameObject>();

            List<PayItem> temp = FacadePayType.GetPayItems();
            icons = new List<Sprite>();
            foreach (PayItem item in temp) 
            {
                icons.Add(item.icon);
            }

            FacadeEffect.PlayCloudEffect += PlayCloudEffect;
            FacadeEffect.PlayRewardNoticeEffect += PlayRewardNoticeEffect;
            FacadeEffect.PlayRewardEffect += PlayRewardEffect;
            FacadeEffect.PlayLevelTargetEffect += PlayLevelTargetEffect;
            FacadeEffect.PlayFlyMoneyEffect += PlayFlyMoneyEffect;
            FacadeEffect.PlayFlyFuncEffect += PlayFlyFuncEffect;
            FacadeEffect.PlayTMDEffect += PlayTMDEffect;

            mTileMoveDir.gameObject.SetActive(false);
            mMask.gameObject.SetActive(false);
            mReward.gameObject.SetActive(false);
            ScreenWidth = Screen.width;
            mLevelTarget.anchoredPosition = new Vector2 (-ScreenWidth, 0);
            Vector2 referenceResolution = UIManager.Instance.GetCanvasReferenceResolution();
            LeftCloudOrignPosX = -referenceResolution.x / 2;
            RightCloudOrignPosX = referenceResolution.x / 2;
            LeftCloudMovePosX = LeftCloudOrignPosX + mLeftCloud.sizeDelta.x - 100;
            RightCloudMovePosX = RightCloudOrignPosX - mRightCloud.sizeDelta.x + 100;

            PGRNotice_OrginY = mPlayerGetRewardNotice.anchoredPosition.y;
            PGRNotice_MoveY = PGRNotice_OrginY - mPlayerGetRewardNotice.sizeDelta.y;
        }
        protected override void OnEnable() 
        {

        }

        /// <summary>
        /// 播放刷新特效
        /// </summary>
        /// <param name="refushAction">刷新功能执行方法</param>
        private void PlayCloudEffect(Action refushAction)
        {
            mMask.gameObject.SetActive(true);
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(mLeftCloud.DOLocalMoveX(LeftCloudMovePosX, 1).SetEase(Ease.OutBack));
            sequence.Join(mRightCloud.DOLocalMoveX(RightCloudMovePosX, 1).SetEase(Ease.OutBack));
            sequence.AppendCallback(()=> { refushAction.Invoke(); });
            sequence.AppendInterval(1);
            sequence.Append(mLeftCloud.DOLocalMoveX(LeftCloudOrignPosX, 1)); // 564
            sequence.Join(mRightCloud.DOLocalMoveX(RightCloudOrignPosX, 1));
            sequence.Play().OnComplete(() =>
            {
                mMask.gameObject.SetActive(false);
            });
        }

        private void PlayRewardNoticeToggle(bool toggle)
        {
            //STimerManager.Instance.CreateSDelay
        }

        //播放上方玩家获取奖励特效
        private void PlayRewardNoticeEffect()
        {
            mIcon.sprite = icons[UnityEngine.Random.Range(0, icons.Count)];
            mNameText.text = PlayerFacade.GetRandomPlayerName();

            string[] temp = PlayerFacade.GetRandomAttemptAndMoney();
            string attempt = temp[0];
            string money = temp[1];
            mPGRNText.text = string.Format(LanguageModule.GetText(""), attempt, money);

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(mPlayerGetRewardNotice.DOLocalMoveY(PGRNotice_MoveY, GameDefines.PRN_Effect_MoveTime));
            sequence.AppendInterval(GameDefines.PRN_Effect_StayTime);
            sequence.Append(mPlayerGetRewardNotice.DOLocalMoveY(PGRNotice_OrginY, GameDefines.PRN_Effect_MoveTime));
            sequence.Play();
        }

        //播放获取奖励特效
        private void PlayRewardEffect(List<UIRibbonRewardItem> items)
        {
            mReward.gameObject.SetActive(true);


        }

        /// <summary>
        /// 播放关卡目标特效
        /// </summary>
        /// <param name="targetTrans">向上移动目标位置</param>
        /// <param name="targetAction">移动完成后的回调</param>
        private void PlayLevelTargetEffect(Transform targetTrans, Action targetAction)
        {
            //mLevelTargetText.text = string.Format(LanguageModule.GetText(""), GamePlayFacade.GetCurLevel);

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(mLevelTarget.DOLocalMoveX(0, GameDefines.TL_Effect_XMoveTime).SetEase(Ease.OutBack));
            sequence.AppendInterval(GameDefines.TL_Effect_StayTime);
            sequence.Append(mLevelTarget.DOMoveY(targetTrans.position.y, GameDefines.TL_Effect_YMoveTime));
            sequence.Join(mLevelTarget.DOScale(0, GameDefines.TL_Effect_YMoveTime));
            sequence.Play().OnComplete(() => { targetAction.Invoke(); });
        }

        //播放飞钱特效
        private void PlayFlyMoneyEffect(Transform orginPos, Transform targetPos)
        {
            GameObject obj = flyMoneyPool.Count != 0? flyMoneyPool.Pop() : ResourceMod.Instance.SyncLoad<GameObject>(GameDefines.FlyMoney_ObjPath);
            obj.SetActive(true);
            obj.transform.position = orginPos.position;
            obj.transform.DOMove(targetPos.position, GameDefines.FlyMoney_ObjTime).OnComplete(() => 
            {
                obj.gameObject.SetActive(false);
                flyMoneyPool.Push(obj);
            });
        }

        //播放飞道具特效
        private void PlayFlyFuncEffect(Transform orginPos, Transform targetPos)
        {
            GameObject obj = flyMoneyPool.Count != 0 ? flyMoneyPool.Pop() : ResourceMod.Instance.SyncLoad<GameObject>(GameDefines.FlyFunc_ObjPath);
            obj.SetActive(true);
            obj.transform.position = orginPos.position;
            obj.transform.DOMove(targetPos.position, GameDefines.FlyFunc_ObjTime).OnComplete(() =>
            {
                obj.gameObject.SetActive(false);
                flyMoneyPool.Push(obj);
            });
        }

        //播放方向指明特效
        private void PlayTMDEffect(Transform targetPos, Sprite icon, Action TDMAction)
        {
            mTileMoveDir.gameObject.SetActive(true);
            mTMDIconRect.localPosition = Vector3.zero;
            mTMDIcon.sprite = icon;
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(mTMDIconRect.DOPunchRotation(new Vector3(0, 0, 20), GameDefines.TMDIcon_RoteTime, 12, 0));
            sequence.Append(mTMDIconRect.DOMove(targetPos.position, GameDefines.TMDIcon_MoveTime));
            sequence.Play().OnComplete(() => 
            {
                mTileMoveDir.gameObject.SetActive(false);
                TDMAction.Invoke();
            });
        }

        protected override void OnDisable() 
        { 
            
        }

        protected override void OnDispose() 
        {
            
        }
    }
}