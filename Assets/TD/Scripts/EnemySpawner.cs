using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyIncrease = 0.75f;
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    [Header("Events")]
    public static UnityEvent onEnemyDestroyed = new UnityEvent();

    private void Awake()
    {
        onEnemyDestroyed.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        UIManager.main.UpdateWave(currentWave); // Initialize the UI with the current wave
        StartCoroutine(StartWave()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && (enemiesLeftToSpawn>0))
        {
            SpawnEnemy();
            enemiesLeftToSpawn--; // Decrease the count of enemies left to spawn
            enemiesAlive++; // Increase the count of alive enemies
            timeSinceLastSpawn = 0f; // Reset the timer after spawning an enemy
        }

        if(enemiesAlive ==0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }

    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves); // Wait for the specified time before starting the wave
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    private void EndWave()
    {
        isSpawning = false; // Stop spawning enemies
        currentWave++; // Increase the wave count
        if (currentWave > 3)
        {
            SceneManager.LoadScene("GameOverVictory");   // Game Over in Victory
        }
        UIManager.main.UpdateWave(currentWave); // Update UI
        timeSinceLastSpawn = 0f; // Reset the spawn timer
        StartCoroutine(StartWave()); 
       
    }
    private void EnemyDestroyed()
    {
        enemiesAlive--; // Decrease the count of alive enemies
    }
    //private void SpawnEnemy()
    //{
    //    GameObject prefabToSpawn = enemyPrefab;
    //    Instantiate(prefabToSpawn,LevelManager.main.startPoint.position,Quaternion.identity);
    //}

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave,difficultyIncrease));
    }
    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, LevelManager.main.startPoint.position, Quaternion.identity);

        Enemy enemy = newEnemy.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.SetStats(currentWave);
        }
    }


}
