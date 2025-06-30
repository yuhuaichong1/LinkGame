using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkPath : MonoBehaviour
{
    
    void Start()
    {
        STimerManager.Instance.CreateSTimer(2, 0, true, true, () =>
        {
            Destroy(gameObject);
        });
    }
}
