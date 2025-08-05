using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnApplicationFocus(bool focus)
    {
        if(!focus) 
        {
            Debug.LogError("ƒ„œÎÕÀ”Œœ∑£ø");
        }
    }
}
