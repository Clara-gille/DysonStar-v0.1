using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JungleDoorOpening : MonoBehaviour
{
    [SerializeField] private GameObject[] doorToOpen;
    [SerializeField] private InputActionReference activate;
    private bool nearbutton;

    private void OnEnable()
    {
        activate.action.Enable();
        activate.action.performed += openDoor;
    }

    private void openDoor(InputAction.CallbackContext context)
    {
        Debug.Log("Button pressed");
        if (context.performed && nearbutton)
        {
            Debug.Log("Button pressed and player is near the button");
            foreach (GameObject doorToOpen in doorToOpen)
            {
                doorToOpen.SetActive(!doorToOpen.activeInHierarchy);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is near the button");
            nearbutton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is no longer near the button");
            nearbutton = false;
        }
    }
}
