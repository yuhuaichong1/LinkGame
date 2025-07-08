using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[AddComponentMenu("SimpleUI/SMovingToggle", 30)]
public class SMovingToggle : MonoBehaviour, IPointerDownHandler
{
    public bool isOn;//开关状态

    public Transform isOnTrans;//开启时滑块的坐标
    public Transform isOffTrans;//关闭时滑块的坐标

    public GameObject Handle;//滑块物体
    public Image isOnBg;//开启时的背景

    public SMTAnimType SMTAnimType;//动画类型
    public float animTime;//动画时间

    [Space]
    public UnityEvent<bool> onValueChange;//开关改变时的事件

    private STimer sTimer;//计时器
    private Action<float> isOnBgAction;//背景动画事件
    private float fillWidth;//覆盖类型事件的宽
    private RectTransform isOnBgRectTrans;//覆盖类型事件的Rect

    /// <summary>
    /// 初始化
    /// </summary>
    void Start()
    {
        Handle.transform.position = isOn ? isOnTrans.position : isOffTrans.position;
        isOnBgRectTrans = isOnBg.GetComponent<RectTransform>();
        fillWidth = isOnBgRectTrans.rect.width;
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        isOn = !isOn;
        onValueChange?.Invoke(isOn);
        Vector3 target = isOn ? isOnTrans.position : isOffTrans.position;
        switch (SMTAnimType)
        {
            case SMTAnimType.None:
                Handle.transform.position = target;
                isOnBg.gameObject.SetActive(isOn);
                break;
            case SMTAnimType.OnlyHandle:
                isOnBg.gameObject.SetActive(true);
                isOnBg.color = new Color(isOnBg.color.r, isOnBg.color.g, isOnBg.color.b, 1);
                isOnBgRectTrans.offsetMin = Vector2.zero;
                isOnBgAction = null;
                movingGameObjectAnim(target);
                break;
            case SMTAnimType.Gradient:
                isOnBg.gameObject.SetActive(true);
                isOnBgRectTrans.offsetMin = Vector2.zero;
                GradientBgGameObjectAnim(isOn);
                movingGameObjectAnim(target);
                break;
            case SMTAnimType.Cover:
                isOnBg.gameObject.SetActive(true);
                CoverBgGameObjectAnim(isOn);
                isOnBg.color = new Color(isOnBg.color.r, isOnBg.color.g, isOnBg.color.b, 1);
                movingGameObjectAnim(target);
                break;
            case SMTAnimType.Gradient_And_Cover:
                isOnBg.gameObject.SetActive(true);
                GCBgGameObjectAnim(isOn);
                movingGameObjectAnim(target);
                break;
        }
    }

    /// <summary>
    /// 滑块移动
    /// </summary>
    /// <param name="target">目标点位</param>
    private void movingGameObjectAnim(Vector3 target)
    {
        float movingTime = Mathf.Abs((Handle.transform.position.x - target.x) / (isOnTrans.position.x - isOffTrans.position.x)) * animTime;
        Vector3 recordVec3 = Vector3.zero;
        Vector3 startPos = Handle.transform.position;

        if (sTimer != null && sTimer.STimerState != STimerState.Standby)
            sTimer.Stop();

        sTimer = STimerManager.Instance.CreateSTimer(movingTime, 0, false, true, null, (time) =>
        {
            if (time < movingTime)
                recordVec3 = Vector3.Lerp(startPos, target, time / movingTime);
            else
                recordVec3 = target;

            Handle.transform.position = recordVec3;
            isOnBgAction?.Invoke(time / movingTime);
        });
    }

    /// <summary>
    /// 背景变化（整体渐变）
    /// </summary>
    /// <param name="isOn">开关状态</param>
    private void GradientBgGameObjectAnim(bool isOn)
    {
        isOnBgAction = (progress) => 
        {
            float alpha = isOn ? progress : 1 - progress;
            isOnBg.color = new Color(isOnBg.color.r, isOnBg.color.g, isOnBg.color.b, alpha);
        };
    }

    /// <summary>
    /// 背景变化（进度变化）
    /// </summary>
    /// <param name="isOn">开关状态</param>
    private void CoverBgGameObjectAnim(bool isOn)
    {
        isOnBgAction = (progress) => 
        {
            float fillP = isOn ? 1 - progress : progress;
            isOnBgRectTrans.offsetMin = new Vector2(fillWidth * fillP, 0);
        };
    }

    /// <summary>
    /// 背景变化（整体渐变 + 进度变化）
    /// </summary>
    /// <param name="isOn">开关状态</param>
    private void GCBgGameObjectAnim(bool isOn)
    {
        isOnBgAction = (progress) =>
        {
            float alpha = isOn ? progress : 1 - progress;
            float fillP = isOn ? 1 - progress : progress;
            isOnBg.color = new Color(isOnBg.color.r, isOnBg.color.g, isOnBg.color.b, alpha);
            isOnBgRectTrans.offsetMin = new Vector2(fillWidth * fillP, 0);
        };
    }
}
