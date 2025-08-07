using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;

public class BTest : MonoBehaviour
{
    void Start()
    {
       
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError(FacadeGuide.GetCurStep());
        }
    }
}
