using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D component for physics interactions

    [Header("Attributes")]
    [SerializeField] private float speed = 10f; // Speed of the bullet
    [SerializeField] private float lifeTime = 5f;


    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!target)
        {
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized; // Calculate the direction towards the target
        rb.linearVelocity = direction * speed; // Set the bullet's velocity towards the target
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(1); // Get the Health component of the collided object

        Destroy(gameObject); // Destroy the bullet on collision
    }
}
