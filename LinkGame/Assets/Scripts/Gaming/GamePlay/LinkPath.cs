using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkipConfusion
{
    public class LinkPath : MonoBehaviour
    {
        void Start()
        {
            STimerManager.Instance.CreateSTimer(0.5f, 0, true, true, () =>
            {
                Destroy(gameObject);
            });
        }
    }
}

