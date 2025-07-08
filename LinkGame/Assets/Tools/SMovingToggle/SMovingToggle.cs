using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[AddComponentMenu("SimpleUI/SMovingToggle", 30)]
public class SMovingToggle : MonoBehaviour, IPointerDownHandler
{
    public bool isOn;//����״̬

    public Transform isOnTrans;//����ʱ���������
    public Transform isOffTrans;//�ر�ʱ���������

    public GameObject Handle;//��������
    public Image isOnBg;//����ʱ�ı���

    public SMTAnimType SMTAnimType;//��������
    public float animTime;//����ʱ��

    [Space]
    public UnityEvent<bool> onValueChange;//���ظı�ʱ���¼�

    private STimer sTimer;//��ʱ��
    private Action<float> isOnBgAction;//���������¼�
    private float fillWidth;//���������¼��Ŀ�
    private RectTransform isOnBgRectTrans;//���������¼���Rect

    /// <summary>
    /// ��ʼ��
    /// </summary>
    void Start()
    {
        Handle.transform.position = isOn ? isOnTrans.position : isOffTrans.position;
        isOnBgRectTrans = isOnBg.GetComponent<RectTransform>();
        fillWidth = isOnBgRectTrans.rect.width;
    }

    /// <summary>
    /// ������¼�
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
    /// �����ƶ�
    /// </summary>
    /// <param name="target">Ŀ���λ</param>
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
    /// �����仯�����彥�䣩
    /// </summary>
    /// <param name="isOn">����״̬</param>
    private void GradientBgGameObjectAnim(bool isOn)
    {
        isOnBgAction = (progress) => 
        {
            float alpha = isOn ? progress : 1 - progress;
            isOnBg.color = new Color(isOnBg.color.r, isOnBg.color.g, isOnBg.color.b, alpha);
        };
    }

    /// <summary>
    /// �����仯�����ȱ仯��
    /// </summary>
    /// <param name="isOn">����״̬</param>
    private void CoverBgGameObjectAnim(bool isOn)
    {
        isOnBgAction = (progress) => 
        {
            float fillP = isOn ? 1 - progress : progress;
            isOnBgRectTrans.offsetMin = new Vector2(fillWidth * fillP, 0);
        };
    }

    /// <summary>
    /// �����仯�����彥�� + ���ȱ仯��
    /// </summary>
    /// <param name="isOn">����״̬</param>
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
