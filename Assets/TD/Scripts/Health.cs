using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2; // Maximum health of the object
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TakeDamage(int dmg)
    {
        hitPoints-= dmg;
        if(hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroyed.Invoke(); // Notify that an enemy has been destroyed
            Destroy(gameObject);
        }
    }
    public void SetHealth(int newHP)
    {
        hitPoints = newHP;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
