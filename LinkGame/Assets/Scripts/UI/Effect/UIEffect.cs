using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace XrCode
{
    public partial class UIEffect : BaseUI
    {
        private LanguageModule LanguageModule;

        private List<Sprite> icons;//上方玩家获取奖励特效的广告Icon

        private float PGRNotice_OrginY;//上方玩家获取奖励特效的起始位置
        private float PGRNotice_MoveY;//上方玩家获取奖励特效的终点位置

        private float LeftCloudOrignPosX;//左云起始位置
        private float RightCloudOrignPosX;//左云终点位置
        private float LeftCloudMovePosX;//右云起始位置
        private float RightCloudMovePosX;//右云终点位置

        private Transform FlyMoneyTipOrginY;//飞钱提示起始位置
        private float FlyMoneyTipTargetY;//飞钱提示终点位置

        private GameObject rewardItemObj;//获取奖励的子项目预制体
        private GameObject flyMoneyObj;//飞钱预制体
        private GameObject flyFuncObj;//飞功能预制体
        private GameObject flyMoneyTipObj;//飞钱提示预制体

        private Stack<UIRibbonRewardItem> rewardObjPool;//获取奖励的子项目预制体对象池
        private Stack<GameObject> flyMoneyPool;//飞钱预制体对象池
        private Stack<GameObject> flyFuncPool;//飞功能预制体对象池
        private Stack<GameObject> flyMoneyTipPool;//飞钱提示预制体对象池

        private Sprite reward_MoneyIcon;//奖励特效物体三叠钱的图片
        private Dictionary<EFuncType, Sprite> reward_FuncIconDic;//功能类型<==>功能图片（飞功能预制体用）

        protected override void OnAwake()
        {
            LanguageModule = ModuleMgr.Instance.LanguageMod;

            rewardItemObj = ResourceMod.Instance.SyncLoad<GameObject>(GameDefines.RibbonRewardItem_ObjPath);
            flyMoneyObj = ResourceMod.Instance.SyncLoad<GameObject>(GameDefines.ifIAA ? GameDefines.FlyDiamond_ObjPath : GameDefines.FlyMoney_ObjPath);
            flyFuncObj = ResourceMod.Instance.SyncLoad<GameObject>(GameDefines.FlyFunc_ObjPath);
            flyMoneyTipObj = ResourceMod.Instance.SyncLoad<GameObject>(GameDefines.ifIAA ? GameDefines.FlyDiamondTip_ObjPath:GameDefines.FlyMoneyTip_ObjPath);


            flyMoneyPool = new Stack<GameObject>();
            flyFuncPool = new Stack<GameObject>();
            rewardObjPool = new Stack<UIRibbonRewardItem>();
            flyMoneyTipPool = new Stack<GameObject>();

            reward_MoneyIcon = ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.ifIAA ? GameDefines.Reward_FuncDiamondBox_IconPath : GameDefines.Reward_Money_IconPath); 
           reward_FuncIconDic = new Dictionary<EFuncType, Sprite>()
            {
                {EFuncType.Tip, ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncHint_IconPath)},
                {EFuncType.Refush, ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncRefresh_IconPath)},
                {EFuncType.Shift, ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncShift_IconPath)},
                {EFuncType.Remove, ResourceMod.Instance.SyncLoad<Sprite>(GameDefines.Reward_FuncRemove_IconPath)}
            };

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
            FacadeEffect.PlayPluralFlyMoney += PlayPluralFlyMoney;
            FacadeEffect.PlayGetMoneyTipEffect += PlayGetMoneyTipEffect;

            mTileMoveDir.gameObject.SetActive(false);
            mMask.gameObject.SetActive(false);
            mReward.gameObject.SetActive(false);
            mLevelTarget.anchoredPosition = new Vector2(-mLevelTarget.rect.width, 0);

            Vector2 referenceResolution = UIManager.Instance.GetCanvasReferenceResolution();

            LeftCloudOrignPosX = -referenceResolution.x / 2;
            RightCloudOrignPosX = referenceResolution.x / 2;
            LeftCloudMovePosX = LeftCloudOrignPosX + mLeftCloud.sizeDelta.x - 100;
            RightCloudMovePosX = RightCloudOrignPosX - mRightCloud.sizeDelta.x + 100;

            PGRNotice_OrginY = referenceResolution.y / 2;
            PGRNotice_MoveY = PGRNotice_OrginY - mPlayerGetRewardNotice.rect.height - 40;

            FlyMoneyTipOrginY = GamePlayFacade.GetFlyMoneyTipOrgin();
            FlyMoneyTipTargetY = FlyMoneyTipOrginY.position.y + 0.4f;
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
            sequence.AppendCallback(() => { refushAction.Invoke(); });
            sequence.AppendInterval(1);
            sequence.Append(mLeftCloud.DOLocalMoveX(LeftCloudOrignPosX, 1)); // 564
            sequence.Join(mRightCloud.DOLocalMoveX(RightCloudOrignPosX, 1));
            sequence.Play().OnComplete(() =>
            {
                mMask.gameObject.SetActive(false);
            });
        }

        /// <summary>
        /// 播放上方玩家获取奖励特效
        /// </summary>
        /// 开局展示1次，随后每消除12次（总消除次数求余），展示1次顶部随机UI
        private void PlayRewardNoticeEffect()
        {
            mIcon.sprite = icons[UnityEngine.Random.Range(0, icons.Count)];
            mNameText.text = string.Format(LanguageModule.GetText("10082"), PlayerFacade.GetRandomPlayerName());

            string[] temp = PlayerFacade.GetRandomAttemptAndMoney();
            string attempt = temp[0];
            string money = temp[1];
            mPGRNText.text = string.Format(LanguageModule.GetText("10083"), attempt, money);

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(mPlayerGetRewardNotice.DOLocalMoveY(PGRNotice_MoveY, GameDefines.PRN_Effect_MoveTime));
            sequence.AppendInterval(GameDefines.PRN_Effect_StayTime);
            sequence.Append(mPlayerGetRewardNotice.DOLocalMoveY(PGRNotice_OrginY, GameDefines.PRN_Effect_MoveTime));
            sequence.Play();

            mPGRNParticle.Play();
        }

        /// <summary>
        /// 播放获取奖励特效
        /// </summary>
        /// <param name="items">奖励集合</param>
        /// <param name="finishAction">特效播放完毕后回调</param>
        private void PlayRewardEffect(List<RewardItem> items, Action finishAction)
        {
            mReward.gameObject.SetActive(true);

            List<UIRibbonRewardItem> yurrItem = new List<UIRibbonRewardItem>();

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                foreach (RewardItem item in items)
                {
                    UIRibbonRewardItem urr = rewardObjPool.Count != 0 ? rewardObjPool.Pop() : GameObject.Instantiate(rewardItemObj, mReward).GetComponent<UIRibbonRewardItem>();
                    urr.transform.SetAsLastSibling();
                    urr.gameObject.SetActive(true);

                    switch (item.type)
                    {
                        case ERewardType.Money:
                            urr.Icon.sprite = reward_MoneyIcon;
                            urr.Count.text = FacadePayType.RegionalChange(item.count);
                            STimerManager.Instance.CreateSDelay(GameDefines.Reward_StayTime - GameDefines.FlyEffect_Start_Delay, () =>
                            {
                                PlayPluralFlyMoney(GameDefines.FlyMoney_Effect_RewardCount, urr.Icon.transform, GamePlayFacade.GetFlyMoneyTarget());
                            });
                            break;
                        case ERewardType.Func:
                            EFuncType funcType = (EFuncType)item.extra;
                            urr.Icon.sprite = reward_FuncIconDic[funcType];
                            urr.Count.text = item.count.ToString();
                            STimerManager.Instance.CreateSDelay(GameDefines.Reward_StayTime - GameDefines.FlyEffect_Start_Delay, () =>
                            {
                                PlayFlyFuncEffect(urr.Icon.transform, GamePlayFacade.GetFuncTarget(funcType), GameDefines.FlyEffect_Start_Delay, funcType);
                            });
                            break;
                    }
                    yurrItem.Add(urr);
                }
            });
            sequence.AppendInterval(GameDefines.Reward_StayTime);
            sequence.AppendCallback(() =>
            {
                foreach (UIRibbonRewardItem item in yurrItem)
                {
                    rewardObjPool.Push(item);
                    item.gameObject.SetActive(false);
                }
                mReward.gameObject.SetActive(false);
            });

            sequence.Play().OnComplete(() => { finishAction.Invoke(); });

        }

        /// <summary>
        /// 播放关卡目标特效
        /// </summary>
        /// <param name="targetTrans">向上移动目标位置</param>
        /// <param name="targetAction">移动完成后的回调</param>
        private void PlayLevelTargetEffect(Transform targetTrans, Action targetAction)
        {
            int diff = ConfigModule.Instance.Tables.TBWithdrawableLevels.Get(GamePlayFacade.GetCurWLevel()).Level - GamePlayFacade.GetCurLevel();
            if(GamePlayFacade.GetCurLevel() < GameDefines.doubleLevel)
                mLevelTargetText.text = diff == 0 ? LanguageModule.GetText("10012") : string.Format(LanguageModule.GetText("10013"), diff + 1);
            else
                mLevelTargetText.text = diff == 0 ? LanguageModule.GetText("10102") : string.Format(LanguageModule.GetText("10100"), diff + 1);
            mLevelTarget.localScale = Vector3.one;
            mLevelTarget.anchoredPosition = new Vector2(-mLevelTarget.rect.width, 0);

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(mLevelTarget.DOLocalMoveX(0, GameDefines.TL_Effect_XMoveTime).SetEase(Ease.OutBack));
            sequence.AppendInterval(GameDefines.TL_Effect_StayTime);
            sequence.Append(mLevelTarget.DOMoveY(targetTrans.position.y, GameDefines.TL_Effect_YMoveTime));
            sequence.Join(mLevelTarget.DOScale(0, GameDefines.TL_Effect_YMoveTime));
            sequence.Play().OnComplete(() => { targetAction.Invoke(); });
        }

        /// <summary>
        /// 播放多个飞钱特效
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="orginPos">起始位置</param>
        /// <param name="targetPos">目标位置</param>
        private void PlayPluralFlyMoney(int count, Transform orginPos, Transform targetPos)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 Opos = orginPos.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
                PlayFlyMoneyEffect(Opos, targetPos, GameDefines.FlyEffect_Start_Delay + i * GameDefines.FlyMoney_ObjTime);
            }
        }

        /// <summary>
        /// 播放飞钱特效
        /// </summary>
        /// <param name="orginPos">起始位置</param>
        /// <param name="targetPos">目标位置</param>
        /// <param name="delayTime">动画延迟播放时间</param>
        private void PlayFlyMoneyEffect(Transform orginPos, Transform targetPos, float delayTime)
        {
            GameObject obj = flyMoneyPool.Count != 0 ? flyMoneyPool.Pop() : GameObject.Instantiate(flyMoneyObj, mFlyIconParent);
            obj.SetActive(true);
            obj.transform.position = orginPos.position;
            obj.transform.DOMove(targetPos.position, GameDefines.FlyMoney_ObjTime).OnComplete(() =>
            {
                obj.gameObject.SetActive(false);
                flyMoneyPool.Push(obj);
                BtnScale();
            }).SetDelay(delayTime);
        }
        /// <summary>
        /// 播放飞钱特效2（多钱币需要)
        /// </summary>
        /// <param name="orginPos">起始位置</param>
        /// <param name="targetPos">目标位置</param>
        /// <param name="delayTime">动画延迟播放时间</param>
        private void PlayFlyMoneyEffect(Vector2 orginPos, Transform targetPos, float delayTime)
        {
            GameObject obj = flyMoneyPool.Count != 0 ? flyMoneyPool.Pop() : GameObject.Instantiate(flyMoneyObj, mFlyIconParent);
            obj.SetActive(true);
            obj.transform.position = orginPos;
            obj.transform.DOMove(targetPos.position, GameDefines.FlyMoney_ObjTime).OnComplete(() =>
            {
                obj.gameObject.SetActive(false);
                flyMoneyPool.Push(obj);
                BtnScale();
            }).SetDelay(delayTime);
        }

        /// <summary>
        /// 获取飞钱提示特效
        /// </summary>
        /// <param name="money">显示金额</param>
        private void PlayGetMoneyTipEffect(float money)
        {
            GameObject obj = flyMoneyTipPool.Count != 0 ? flyMoneyTipPool.Pop() : GameObject.Instantiate(flyMoneyTipObj, mFlyIconParent);
            obj.SetActive(true);
            GetMoneyTipItem getMoneyTipItem = obj.GetComponent<GetMoneyTipItem>();
            getMoneyTipItem.canvasGroup.alpha = 1;
            getMoneyTipItem.GMTText.text = $"+{FacadePayType.RegionalChange(money)}";
            obj.transform.position = FlyMoneyTipOrginY.position;
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.transform.DOMoveY(FlyMoneyTipTargetY, GameDefines.FlyMoneyTip_ObjTime));
            sequence.Join(getMoneyTipItem.canvasGroup.DOFade(0, GameDefines.FlyMoneyTip_ObjTime).SetEase(Ease.InCirc));
            sequence.Play().OnComplete(() =>
            {
                obj.SetActive(false);
                flyMoneyTipPool.Push(obj);
            });

        }

        /// <summary>
        /// 播放飞道具特效
        /// </summary>
        /// <param name="orginPos">起始位置</param>
        /// <param name="targetPos">目标位置</param>
        /// <param name="delayTime">动画延迟播放时间</param>
        /// <param name="type">道具类型（用于确定图片）</param>
        private void PlayFlyFuncEffect(Transform orginPos, Transform targetPos, float delayTime, EFuncType type)
        {
            GameObject obj = flyFuncPool.Count != 0 ? flyFuncPool.Pop() : GameObject.Instantiate(flyFuncObj, mFlyIconParent);
            obj.SetActive(true);
            obj.transform.position = orginPos.position;
            obj.transform.localScale = Vector3.one;
            obj.transform.GetChild(0).GetComponent<Image>().sprite = reward_FuncIconDic[type];

            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delayTime);
            sequence.Append(obj.transform.DOMove(targetPos.position, GameDefines.FlyFunc_ObjTime));
            sequence.Join(obj.transform.DOScale(0.55f, GameDefines.FlyFunc_ObjTime));
            sequence.Play().OnComplete(() =>
            {
                obj.gameObject.SetActive(false);
                flyFuncPool.Push(obj);
            });
        }

        /// <summary>
        /// 播放方向指明特效
        /// </summary>
        /// <param name="targetPos">方向移动目标点</param>
        /// <param name="icon">方向图片</param>
        /// <param name="TDMAction">方向移动完毕后的回调</param>
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

        /// <summary>
        /// 飞钱按钮缩放
        /// </summary>
        private void BtnScale()
        {
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(GamePlayFacade.GetFlyMoneyTipOrgin().DOScale(1.25f, GameDefines.FlyMoney_ObjTime / 2));
            sequence.Append(GamePlayFacade.GetFlyMoneyTipOrgin().DOScale(1f, GameDefines.FlyMoney_ObjTime / 2));
        }

        protected override void OnDisable()
        {

        }

        protected override void OnDispose()
        {

        }
    }
}