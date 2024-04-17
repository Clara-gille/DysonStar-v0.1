using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Opening : MonoBehaviour
{
    [SerializeField] private GameObject DoorToOpen;
    [SerializeField] private AudioSource openSound;
    [SerializeField] private AudioSource closeSound;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorToOpen.SetActive(false);
            openSound.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorToOpen.SetActive(true);
            closeSound.Play();
        }
    }

}
