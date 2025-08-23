using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private Health health;

    [Header("Base Stats")]
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private int baseHealth = 2;

    public void SetStats(int wave)
    {
        // Scaling difficulty
        int scaledHealth = Mathf.RoundToInt(baseHealth * Mathf.Pow(1.4f, wave - 1));
        float scaledSpeed = baseSpeed * Mathf.Pow(1.4f, wave - 1);


        health.SetHealth(scaledHealth);
        movement.SetSpeed(scaledSpeed);
    }
}
