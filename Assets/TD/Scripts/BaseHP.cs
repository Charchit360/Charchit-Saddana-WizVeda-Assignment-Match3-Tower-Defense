using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseHP : MonoBehaviour
{
    private int baseHP = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.main.UpdateHP(baseHP); // Initialize the UI with the base HP
    }

    // Update is called once per frame
    void Update()
    {
        if (baseHP == 0)
        {
            SceneManager.LoadScene("GameOver"); // Load the Game Over scene when base HP reaches 0 Defeat
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            baseHP -= 10; // Decrease base HP by 10 when an enemy reaches the base
            UIManager.main.UpdateHP(baseHP);
        }
    }
}
