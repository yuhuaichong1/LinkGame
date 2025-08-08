using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SGuidePenetrate : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image mask;
    public List<RectTransform> penetrateObjs;
    public bool ifNext;

    void Awake()
    {
        penetrateObjs = new List<RectTransform>();
        mask = GetComponent<Image>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var penetrateObj in penetrateObjs)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(penetrateObj, eventData.position, eventData.pressEventCamera))
            {
                // ��������Ŀ�������ڣ��򴫵��¼�
                ExecuteEvents.Execute(penetrateObj.gameObject, eventData, ExecuteEvents.pointerClickHandler);

                FacadeGuide.NextStep();

                return;
            }
        }

        // �������򱣳�ԭ����Ϊ
        if (mask != null && mask.raycastTarget)
        {
            // ����������A�����ʱ���߼�
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var penetrateObj in penetrateObjs)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(penetrateObj, eventData.position, eventData.pressEventCamera))
            {
                ExecuteEvents.Execute(penetrateObj.gameObject, eventData, ExecuteEvents.pointerDownHandler);
            }
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var penetrateObj in penetrateObjs)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(penetrateObj, eventData.position, eventData.pressEventCamera))
            {
                ExecuteEvents.Execute(penetrateObj.gameObject, eventData, ExecuteEvents.pointerUpHandler);
            }
        }
    }

}
