using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    private float Speed = -0.5f;//�Դ��ٶ�
    private void LateUpdate()
    {
        transform.Rotate(Vector3.down * Speed, Space.World);
    }
}
