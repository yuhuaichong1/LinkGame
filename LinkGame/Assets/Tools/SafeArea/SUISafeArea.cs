using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//��ʹ��Device Simulator Devicesʱ���������������⣬
//����ʵ�ʴ���ǲ���Ӱ��ģ�
//���ߣ���OnEnable��ΪStartҲ�ܽ�����⣬���ǽ�������Ϸ����ʱ�鿴��
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
[ExecuteAlways]
public class SUISafeArea : MonoBehaviour
{
    public float leftCorrection;//��߾ಹ��
    public float rightCorrection;//�ұ߾ಹ��
    public float topCorrection;//�ϱ߾ಹ��
    public float bottomCorrection;//�±߾ಹ��

    private DrivenRectTransformTracker m_Tracker;//�޶�RectTransform�����޷��޸�

    void OnEnable()
    {
        RectTransform rectTrans = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;

        // ���� SafeArea �ı߽磨�������Ļ���½ǣ�
        float safeXMin = safeArea.x;
        float safeYMin = safeArea.y;
        float safeXMax = safeArea.x + safeArea.width;
        float safeYMax = safeArea.y + safeArea.height;

        // ����ê�㷶Χ������ SafeArea��
        rectTrans.anchorMin = new Vector2(0, 0);
        rectTrans.anchorMax = new Vector2(1, 1);

        // ʹ�� offsetMin�����£��� offsetMax�����ϣ����� SafeArea �߽�
        rectTrans.offsetMin = new Vector2(
            safeXMin + leftCorrection,      // ��߽�
            safeYMin + bottomCorrection     // �±߽�
        );
        rectTrans.offsetMax = new Vector2(
            -(Screen.width - safeXMax) + rightCorrection,  // �ұ߽�
            -(Screen.height - safeYMax) + topCorrection    // �ϱ߽�
        );

        m_Tracker = new DrivenRectTransformTracker();
        m_Tracker.Clear();
        m_Tracker.Add(this, rectTrans,
            DrivenTransformProperties.AnchorMinX |
            DrivenTransformProperties.AnchorMinY |
            DrivenTransformProperties.AnchorMaxX |
            DrivenTransformProperties.AnchorMaxY);


        #region �ɵģ������½�Ϊê����ж�
        //RectTransform rectTrans = GetComponent<RectTransform>();

        //Vector2 orginAnchorMin = rectTrans.anchorMin;
        //Vector2 orginAnchorMax = rectTrans.anchorMax;
        //Vector2 orginPivot = rectTrans.pivot;

        //rectTrans.anchorMin = Vector2.zero;
        //rectTrans.anchorMax = Vector2.zero;
        //rectTrans.pivot = Vector2.zero;

        //Rect safeArea = Screen.safeArea;
        //rectTrans.anchoredPosition = new Vector2(safeArea.x + leftCorrection, safeArea.y + bottomCorrection);
        //rectTrans.sizeDelta = new Vector2(safeArea.width - rightCorrection - leftCorrection, safeArea.height - topCorrection - bottomCorrection);

        //rectTrans.anchorMin = orginAnchorMin;
        //rectTrans.anchorMax = orginAnchorMax;
        //rectTrans.pivot = orginPivot;
        #endregion
    }

    private void OnDisable()
    {
        m_Tracker.Clear();
    }
}

/* �ɵ��ж�

*/
