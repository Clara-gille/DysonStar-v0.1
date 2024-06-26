using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JungleDoorOpening : MonoBehaviour
{
    [SerializeField] private GameObject[] doorToOpen;
    [SerializeField] private InputActionReference activate;
    private bool nearbutton;
    [SerializeField] private AudioSource openSound;
    [SerializeField] private AudioSource closeSound;
    
    private void OnEnable()
    {
        activate.action.Enable();
        activate.action.performed += openDoor;
    }

    private void openDoor(InputAction.CallbackContext context)
    {
        if (context.performed && nearbutton)
        {

            foreach (GameObject doorToOpen in doorToOpen)
            {
                doorToOpen.SetActive(!doorToOpen.activeInHierarchy);
                if (doorToOpen.activeInHierarchy)
                {
                    closeSound.Play();
                }
                else
                {
                    openSound.Play();
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            nearbutton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            nearbutton = false;
        }
    }
}
