using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRoom : MonoBehaviour
{
    private Transform _target;
    private float _targetCameraSize;
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Usually (0, 0, -20)
    

    // after every frames, slowly move the camera to the target position and resize it 
    void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _targetCameraSize, smoothSpeed);
    }
    
    //Change target of the camera
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    //Change the camera size
    public void ChangeCameraSize(float size)
    {
        _targetCameraSize = size;
    }
}