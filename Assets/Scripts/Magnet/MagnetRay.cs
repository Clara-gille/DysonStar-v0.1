using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetRay : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject emitPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private String rayColor;
    
    //Detect when collision with a trigger is occurring
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(tag))
        {
            // Repusle the player in the opposite direction of the ray
            Vector3 direction = endPoint.transform.position - emitPoint.transform.position;
            player.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * 20);
        }
        else if (other.CompareTag(CompareTag("Blue") ? "Red" : "Blue"))
        {
            // Attract the player towards the magnet
            Vector3 direction = other.transform.position - player.transform.position;
            player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 20);
        }
    }
}
