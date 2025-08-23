using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    

    void Start()
    {
       
        target = LevelManager.main.path[pathIndex]; // Get the first target point from LevelManager
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length)
            {
                
                EnemySpawner.onEnemyDestroyed.Invoke(); // Notify that an enemy has reached the end of the path
                Destroy(gameObject); // Destroy the enemy if it reaches the end of the path
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex]; // Update the target to the next point in the path
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 distance = (target.position - transform.position).normalized;

        rb.linearVelocity = distance * moveSpeed; // Move the enemy towards the target point
    }
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

}
