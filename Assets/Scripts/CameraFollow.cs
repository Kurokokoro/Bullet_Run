using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 cameraOffset;

    private Vector3 _smoothedPosition;

    private void FixedUpdate()
    {
        _smoothedPosition = Vector3.Lerp(transform.position, target.position + cameraOffset, smoothSpeed);
        transform.position = _smoothedPosition;
    }
}
