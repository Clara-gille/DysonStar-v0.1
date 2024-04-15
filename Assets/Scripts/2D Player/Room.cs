using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private CameraFollowRoom _cameraFollowRoom;
    [SerializeField] private float cameraSize = 20f;
    
    private void Start()
    {
        _cameraFollowRoom = FindObjectOfType<CameraFollowRoom>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _cameraFollowRoom.SetTarget(_target);
            _cameraFollowRoom.ChangeCameraSize(cameraSize);
        }
    }
    
}
