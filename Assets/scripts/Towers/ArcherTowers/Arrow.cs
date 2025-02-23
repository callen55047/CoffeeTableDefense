using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime = 5f;
    public int damageAmount = 10;      // Damage dealt to an enemy
    public float arrowSpeed = 20f;     // Arrow movement speed
    
    // maxDistance will be automatically set from the ArcherTower's SphereCollider radius.
    private float maxDistance;         

    private Vector3 spawnPosition;

    void Start()
    {
        // Record the spawn position of the arrow.
        spawnPosition = transform.position;

        // Automatically get maxDistance from the SphereCollider on the ArcherTower.
        GameObject tower = GameObject.FindWithTag("ArcherTower");
        if (tower != null)
        {
            SphereCollider sc = tower.GetComponent<SphereCollider>();
            if (sc != null)
            {
                maxDistance = sc.radius;
            }
            else
            {
                Debug.LogWarning("No SphereCollider found on the object tagged 'ArcherTower'.");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'ArcherTower'.");
        }

        // Ensure the arrow is destroyed after 'lifetime' seconds as a fallback.
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the arrow forward based on its arrowSpeed.
        transform.position += arrowSpeed * Time.deltaTime * transform.forward;
        
        // Calculate how far the arrow has traveled.
        float traveledDistance = Vector3.Distance(spawnPosition, transform.position);
        
        // If the arrow has traveled beyond the maxDistance, destroy it.
        if (traveledDistance > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedEnemy"))
        {
            Enemies.EnemyHealth enemyHealth = other.GetComponent<Enemies.EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning("EnemyHealth component not found on " + other.gameObject.name);
            }
            Destroy(gameObject);
        }
    }
}
