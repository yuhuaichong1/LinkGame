using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMoveUpDown : MonoBehaviour
{
    private RectTransform rect;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        rect.DOLocalMoveY(-125, 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    void OnDisable()
    {
        rect.anchoredPosition = new Vector3(0, 27, 0);
        rect.DOKill();
    }
}
