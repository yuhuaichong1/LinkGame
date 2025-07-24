using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SGuidePenetrate : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image mask;
    public RectTransform penetrateObj;

    void Awake()
    {
        mask = GetComponent<Image>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(penetrateObj, eventData.position, eventData.pressEventCamera))
        {
            // ��������Ŀ�������ڣ��򴫵��¼�
            ExecuteEvents.Execute(penetrateObj.gameObject, eventData, ExecuteEvents.pointerClickHandler);
            return;
        }

        // �������򱣳�ԭ����Ϊ
        if (mask != null && mask.raycastTarget)
        {
            // ����������A�����ʱ���߼�
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(penetrateObj, eventData.position, eventData.pressEventCamera))
        {
            ExecuteEvents.Execute(penetrateObj.gameObject, eventData, ExecuteEvents.pointerDownHandler);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(penetrateObj, eventData.position, eventData.pressEventCamera))
        {
            ExecuteEvents.Execute(penetrateObj.gameObject, eventData, ExecuteEvents.pointerUpHandler);
        }
    }

}
