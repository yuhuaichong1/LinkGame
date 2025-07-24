using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTest : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError(FacadeGuide.GetCurStep());
        }
    }
}
