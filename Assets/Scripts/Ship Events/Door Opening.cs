using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Opening : MonoBehaviour
{
    [SerializeField] private GameObject DoorToOpen;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorToOpen.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorToOpen.SetActive(true);
        }
    }

}
