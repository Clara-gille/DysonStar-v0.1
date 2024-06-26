using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallenInPit : MonoBehaviour
{
    // Point to teleport the player to
    [SerializeField] private Transform respawnPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the respawn point
            other.transform.position = respawnPoint.position;
        }
    }
}
