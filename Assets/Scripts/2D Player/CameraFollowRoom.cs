using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRoom : MonoBehaviour
{
    private Transform _target;
    private float _targetCameraSize;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    
    
    void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _targetCameraSize, smoothSpeed);
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    public void ChangeCameraSize(float size)
    {
        _targetCameraSize = size;
    }
    
}