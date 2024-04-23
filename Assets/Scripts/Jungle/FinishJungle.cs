using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FinishJungle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Load the next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Orbit tests");
        }
    }
}
