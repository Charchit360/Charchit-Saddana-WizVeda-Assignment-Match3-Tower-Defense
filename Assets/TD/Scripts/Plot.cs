//using UnityEngine;

//public class Plot : MonoBehaviour
//{
//    [Header("References")]
//    [SerializeField] private SpriteRenderer sr; // Reference to the SpriteRenderer component
//    // Start is called once before the first execution of Update after the MonoBehaviour is created


//    private GameObject turret; // Reference to the turret GameObject
//    private Color startColor;
//    void Start()
//    {
//        startColor = sr.color; // Store the initial color of the sprite 
//    }

//    private void OnMouseEnter()
//    {
//        sr.color = Color.green; // Change the color to green when the mouse enters the plot
//    }
//    private void OnMouseExit()
//    {
//        sr.color = startColor; // Reset the color to the initial color when the mouse exits the plot
//    }
//    //private void OnMouseDown()
//    //{
//    //    Debug.Log("Plot clicked"); // Log a message when the plot is clicked
//    //    if (turret != null) return;
//    //    GameObject turretToBuild = BuildManager.main.GetSelectedTower();
//    //    turret = Instantiate(turretToBuild,transform.position,Quaternion.identity);
//    //}

//    private void OnMouseDown()
//    {
//        Debug.Log("Plot clicked");
//        if (turret != null) return;

//        // Check if enough currency is available
//        if (LevelManager.main.currency >= 50)
//        {
//            GameObject turretToBuild = BuildManager.main.GetSelectedTower();
//            turret = Instantiate(turretToBuild, transform.position, Quaternion.identity);
//            LevelManager.main.SpendCurrency(50);
//        }
//        else
//        {
//            Debug.Log("Not enough money");
//        }
//    }
//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject contextMenu;
    [SerializeField] private GameObject upgradeMenu;
    public GameObject turret; // Reference to the turret GameObject, if any
    private Color startColor;
   

    void Start()
    {
        startColor = sr.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = startColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Plot clicked via pointer!");
        
        if (turret != null && (Board.Instance.Upgrades>=1))
        {
            Debug.Log("Plot already has a turret, opening upgrade options.");

            upgradeMenu.SetActive(true); // Show the upgrade menu if the turret exists and upgrades are available

            //Turret currentTurret = turret.GetComponent<Turret>();
            //currentTurret.fireRate += 0.5f; // Example of upgrading turret's fire rate
            //Debug.Log("Turret upgraded! New fire rate: " + currentTurret.fireRate);
            //Board.Instance.Upgrades--;  
            //UIManager.main.UpdateUpgrades(Board.Instance.Upgrades); // Update the UI with the new upgrade count
            //Open Upgrade option
            return;
        }

        // Open Context Menu for turret selection

        else if(turret==null  && contextMenu != null)
        {
            contextMenu.SetActive(true); // simply show menu
        }
        BuildManager.main.SetCurrentPlot(this);


        //if (LevelManager.main.currency >= 50)
        //{
        //    Tower turretToBuild = BuildManager.main.GetSelectedTower();
        //    turret = Instantiate(turretToBuild.prefab, transform.position, Quaternion.identity);
        //    LevelManager.main.SpendCurrency(turretToBuild.cost);
        //}
        //else
        //{
        //    Debug.Log("Not enough money");
        //}


    }
    public void SetTurret(GameObject _turret)
    {
        turret = _turret;
    }
    



    public void CloseContextMenu()
    {
        if (contextMenu != null)
        {
            contextMenu.SetActive(false); // instantly hide menu
        }
    }
    public void CloseUpgradesMenu()
    {
        if (upgradeMenu != null)
        {
            upgradeMenu.SetActive(false); // instantly hide menu
        }
    }
}
