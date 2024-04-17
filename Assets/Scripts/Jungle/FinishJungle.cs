using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FinishJungle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sans doute mettre un dialogue ici avant et attendre qu'il finisse avant de charger la scène suivante
        if (collision.gameObject.CompareTag("Player"))
        {
            // Load the next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Orbit tests");
        }
    }
}
