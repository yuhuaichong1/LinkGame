using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMoveUpDown : MonoBehaviour
{
    private RectTransform rect;
    private Vector3 orginPos;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        orginPos = rect.anchoredPosition;
    }

    void OnEnable()
    {
        rect.DOLocalMoveY(-160, 1).From(-155).From(orginPos).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    void OnDisable()
    {
        rect.anchoredPosition = new Vector3(0, 27, 0);
        rect.DOKill();
    }
}
