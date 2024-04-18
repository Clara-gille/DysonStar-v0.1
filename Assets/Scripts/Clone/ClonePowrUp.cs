using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePowrUp : MonoBehaviour
{
    [SerializeField] public GameObject clonePlayer;
    private CameraFollowRoom _cameraFollowRoom;
    
    private void Start()
    {
        _cameraFollowRoom = FindObjectOfType<CameraFollowRoom>();
    }

    // Detect when there's a collision with a trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // If player collides with the powerup
        {
            // Create a clone of the player some distance away
            GameObject clone = Instantiate(clonePlayer, other.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            _cameraFollowRoom.SetClone(clone); // Set the clone in the camera follow script
            Destroy(gameObject); // Destroy the powerup
        }
    }
}
