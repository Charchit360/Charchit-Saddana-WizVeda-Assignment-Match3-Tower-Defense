using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // must match the scene name exactly
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
