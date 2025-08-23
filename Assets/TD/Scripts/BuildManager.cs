using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class BuildManager : MonoBehaviour
{
    public static BuildManager main; // Singleton instance of BuildManager
    [Header("References")]
    // Prefab for the turret to be built
    [SerializeField] private Tower[] towers;

    private Plot currentPlot;
    private GameObject turret;

    private int selectedTower = 0;


    private void Awake()
    {
        main = this; // Assign the singleton instance to the static variable


    }
    public void SetCurrentPlot(Plot plot)
    {
        currentPlot = plot;
    }
    public Tower GetSelectedTower()
    {
        return towers[selectedTower]; // Return the currently selected tower prefab
    }
    public void SetSelectedTower(int _selectedTower)
    {
        
            selectedTower = _selectedTower; // Set the currently selected tower index
            Tower turretToBuild = BuildManager.main.towers[selectedTower]; // Get the tower prefab based on the selected index

            if (LevelManager.main.currency >= turretToBuild.cost)
            {

                turret = Instantiate(turretToBuild.prefab, currentPlot.transform.position, Quaternion.identity);
                currentPlot.turret = turret; // Assign the turret to the current plot
                LevelManager.main.SpendCurrency(turretToBuild.cost);
            currentPlot.CloseContextMenu();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        
        


    }

    public void IncreaseFireRate()
    {
        if (Board.Instance.Upgrades >= 1)
        {
            Turret currentTurret = turret.GetComponent<Turret>();
            currentTurret.fireRate += 0.5f; // Example of upgrading turret's fire rate
            Debug.Log("Turret upgraded! New fire rate: " + currentTurret.fireRate);
            Board.Instance.Upgrades--;
            UIManager.main.UpdateUpgrades(Board.Instance.Upgrades); // Update the UI with the new upgrade count
        }
        else
        {
            Debug.Log("Not enough upgrades available to increase fire rate.");
        }
    }

    public void IncreaseRange()
    {
        if (Board.Instance.Upgrades >= 1)
        {
            Turret currentTurret = turret.GetComponent<Turret>();
            currentTurret.targetingRange += 0.5f; // Example of upgrading turret's fire rate
            Debug.Log("Turret upgraded! New range: " + currentTurret.targetingRange);
            Board.Instance.Upgrades--;
            UIManager.main.UpdateUpgrades(Board.Instance.Upgrades); // Update the UI with the new upgrade count
        }
        else
        {
            Debug.Log("Not enough upgrades available to increase range.");
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
       
    }
}

