using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenCodex : MonoBehaviour
{
    private bool readyToRead;

    // Update is called once per frame
    void Update()
    {
        if (readyToRead && Input.GetKeyDown(KeyCode.UpArrow))
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
