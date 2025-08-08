using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkErrorTips : MonoBehaviour
{
    public Text text;
    private int count = 0;
    private void Start()
    {
        InvokeRepeating("ChangeSyb", 0.1f, 0.5f);
    }

    public void ChangeSyb()
    { 
        count++;
        if (count % 3 == 0)
            text.text = $"Network issue detected. Please wait a moment.";
        if (count % 3 == 1)
            text.text = $"Network issue detected. Please wait a moment..";
        if (count % 3 == 2)
            text.text = $"Network issue detected. Please wait a moment...";
    }
}
