using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint; // The part of the turret that rotates to aim at enemies
    [SerializeField] private LayerMask enemyLayer; // Layer mask to filter out enemy objects
    [SerializeField] private GameObject bulletPrefab; // Prefab for the bullet that the turret will fire
    [SerializeField] private Transform firingPoint; // The point from which the turret will fire bullets

    [Header("Attributes")]
    [SerializeField] public float targetingRange = 5f; // Range within which the turret can target enemies
    [SerializeField] public float fireRate = 2f; // Rate at which the turret fires bullets


    private Transform target;
    private float timeUntilNextFire; // Timer to control firing rate

    //private void OnDrawGizmosSelected()
    //{
        
    //    Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Always try to find the closest target if none or if current is out of range
        if (target == null || !CheckTargetIsInRange())
        {
            FindTarget();
        }

        if (target == null)
            return;

        RotateTowardsTarget();

        timeUntilNextFire += Time.deltaTime;
        if (timeUntilNextFire >= 1f / fireRate)
        {
            Shoot();
            timeUntilNextFire = 0f;
        }
    }
    //void Update()
    //{
    //    if (target == null)
    //    {
    //        FindTarget();
    //        return;
    //    }
    //    RotateTowardsTarget();
    //    if (!CheckTargetIsInRange())
    //    {
    //        target = null; // Reset target if it's out of range
           
    //    }
    //    else
    //    {
    //        timeUntilNextFire += Time.deltaTime; // Increment the timer for firing bullets
    //        if(timeUntilNextFire >= 1f/fireRate)
    //        {
    //            Shoot();
    //            timeUntilNextFire = 0f; // Reset the timer after firing
    //        }
    //    }

    //}
    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity); // Create a new bullet instance
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); // Get the Bullet script from the instantiated bullet
        bulletScript.SetTarget(target); // Set the target for the bullet to follow
    }
    //private void FindTarget()
    //{
    //    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,enemyLayer);
    //    if(hits.Length > 0)
    //    {
    //        target = hits[0].transform; // Get the first enemy hit by the circle cast
    //    }
    //}
    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyLayer);
        float closestDistance = float.MaxValue;
        Transform closestEnemy = null;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = hit.transform;
            }
        }

        target = closestEnemy;
    }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg -90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,targetRotation,250f*Time.deltaTime); // Rotate turret towards the target
    }
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
}
