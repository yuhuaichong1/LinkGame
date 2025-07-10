using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeRotateLeftRight : MonoBehaviour
{
    private RectTransform rect;
    public bool ifloop;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        
        if(ifloop)
            rect.DOPunchRotation(new Vector3(0, 0, 6), 3f, 6, 1).SetLoops(-1);
        else
            rect.DOPunchRotation(new Vector3(0, 0, 6), 3f, 6, 1);
    }

    void OnDisable()
    {
        rect.rotation = Quaternion.identity;
        rect.DOKill();
    }
}
