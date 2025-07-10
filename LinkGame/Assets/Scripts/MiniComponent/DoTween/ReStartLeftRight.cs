using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReStartLeftRight : MonoBehaviour
{
    private RectTransform rect;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        rect.DOLocalMoveX(360, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    void OnDisable()
    {
        rect.anchoredPosition = new Vector3(0, 27, 0);
        rect.DOKill();
    }

}
