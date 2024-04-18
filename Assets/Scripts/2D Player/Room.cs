using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private CameraFollowRoom _cameraFollowRoom;
    [SerializeField] private float cameraSize = 20f;
    private GameObject _player;
    
    private void Start()
    {
        _cameraFollowRoom = FindObjectOfType<CameraFollowRoom>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GameObject() == _player)
        {
            _cameraFollowRoom.SetTarget(_target);
            _cameraFollowRoom.ChangeCameraSize(cameraSize);
        }
    }
    
}

