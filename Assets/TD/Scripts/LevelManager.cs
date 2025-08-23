using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        currency = 50; // Initialize currency to 100 at the start of the game
        UIManager.main.UpdateCoins(currency);
        
    }
    public void AddCurrency(int amount)
    {
        currency += amount; // Add the specified amount to the current currency
        UIManager.main.UpdateCoins(currency); // UI

    }
    public bool SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount; // Subtract the specified amount from the current currency to buy turret
            UIManager.main.UpdateCoins(currency); //UI
            return true;
        }
        else
        {
            Debug.Log("Not enough currency!"); // Log a message if there is not enough currency
            return false;
        }
    }
}
