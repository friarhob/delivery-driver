using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject element;
    void LateUpdate()
    {
        transform.position = element.transform.position + new Vector3(0, 0, -10);
    }
}
