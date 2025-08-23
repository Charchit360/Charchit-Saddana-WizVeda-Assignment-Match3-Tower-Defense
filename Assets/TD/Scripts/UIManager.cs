using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text baseHpText;
    [SerializeField] private TMP_Text upgradesText;

    private void Awake()
    {
        main = this;
    }
    //Coins
    public void UpdateCoins(int amount)
    {
        coinText.text = "Coins: " + amount;
    }
    //HP
    public void UpdateHP(int amount)
    {
        baseHpText.text = "Base HP: " + amount;
    }

    // Wave
    public void UpdateWave(int currentWave)
    {
        waveText.text = "Wave: " + currentWave;
    }
    public void UpdateUpgrades(int upgrades)
    {
        upgradesText.text = "Upgrades: " + upgrades;
    }
}
