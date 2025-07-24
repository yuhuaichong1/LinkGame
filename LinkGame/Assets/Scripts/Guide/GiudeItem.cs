using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideItem
{
    public int step;
    public int nextStep;
    public string diglogContent;
    public Transform diglogPos;
    public Transform handPos;
    public bool ifMask;
    public Transform transparentPos;
    public Transform btnPos;
    public float autohiddenTime;
    public bool ifNext;
    public Dictionary<string, string> extra;
}
