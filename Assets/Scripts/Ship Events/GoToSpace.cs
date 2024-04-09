using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSpace : MonoBehaviour
{

    private bool readyToGo;
    SceneManager sceneManager;
   
    // Update is called once per frame
    void Update()
    {
        if(readyToGo && Input.GetKeyDown(KeyCode.UpArrow))
        {
            //This will load the next scene
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
