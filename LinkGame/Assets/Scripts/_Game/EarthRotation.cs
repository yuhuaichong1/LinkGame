using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    private float Speed = -0.5f;//自传速度
    private void LateUpdate()
    {
        transform.Rotate(Vector3.down * Speed, Space.World);
    }
}
