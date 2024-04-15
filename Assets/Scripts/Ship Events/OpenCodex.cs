using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OpenCodex : MonoBehaviour
{
    private bool readyToRead;
    [SerializeField] private InputActionReference activate;

    private void OnEnable()
    {
        activate.action.Enable();
        activate.action.performed += readCodex;
    }

    private void OnDisable()
    {
        activate.action.Disable();
        activate.action.performed -= readCodex;
    }

    // Update is called once per frame
    private void readCodex(InputAction.CallbackContext context)
    {
        if (context.performed && readyToRead)
        {
            Debug.Log("Codex opened");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            readyToRead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            readyToRead = false;
        }
    }
}
