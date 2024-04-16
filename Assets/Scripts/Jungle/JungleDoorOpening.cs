using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JungleDoorOpening : MonoBehaviour
{
    [SerializeField] private GameObject doorToOpen;
    [SerializeField] private InputActionReference activate;
    [SerializeField] private bool doorState;
    private bool nearbutton;

    private void OnEnable()
    {
        activate.action.Enable();
        activate.action.performed += openDoor;
    }

    private void OnDisable()
    {
        activate.action.Disable();
        activate.action.performed -= openDoor;
    }

    private void openDoor(InputAction.CallbackContext context)
    {
        if (context.performed && nearbutton)
        {
            doorState = !doorState;
            doorToOpen.SetActive(doorState);
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
