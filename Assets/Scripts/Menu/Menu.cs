using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Orbit Test");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
