using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GoToSpace : MonoBehaviour
{

    private bool readyToGo;
    SceneManager sceneManager;
    [SerializeField] private InputActionReference activate;

    private void OnEnable()
    {
        activate.action.Enable();
        activate.action.performed += leave;
    }

    private void OnDisable()
    {
        activate.action.Disable();
        activate.action.performed -= leave;
    }

    private void leave(InputAction.CallbackContext context)
    {
        if(context.performed && readyToGo)
        {
            SceneManager.LoadScene("Orbit tests");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            readyToGo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            readyToGo = false;
        }
    }
}
