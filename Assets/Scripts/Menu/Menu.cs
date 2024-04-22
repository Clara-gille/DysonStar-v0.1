using UnityEngine;

namespace Menu
{
    public class Menu : MonoBehaviour
    {
        public void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void StartGame()
        {
            // Load the game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

        public void QuitGame()
        {
            // Quit the game
            Application.Quit();
        }
    }
}
