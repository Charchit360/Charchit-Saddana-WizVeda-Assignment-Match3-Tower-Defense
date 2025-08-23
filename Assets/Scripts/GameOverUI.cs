using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RetryGame()
    {
        // Reload the current game scene
        SceneManager.LoadScene("SampleScene"); // replace with your actual game scene name
    }

    public void ReturnToMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
