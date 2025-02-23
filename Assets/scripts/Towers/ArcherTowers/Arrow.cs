using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime = 5f;
    public int damageAmount = 10;      // Damage dealt to an enemy
    public float arrowSpeed = 20f;     // Arrow movement speed
    // Remove public access so maxDistance isn't adjustable in the Inspector.
    private float maxDistance;         // Maximum travel distance (auto-set)

    private Vector3 spawnPosition;

    void Start()
    {
        // Record the spawn position of the arrow.
        spawnPosition = transform.position;

        // Automatically get maxDistance from the SphereCollider on the object tagged "initArrow".
        GameObject init = GameObject.FindWithTag("ArcherTower");
        if (init != null)
        {
            SphereCollider sc = init.GetComponent<SphereCollider>();
            if (sc != null)
            {
                maxDistance = sc.radius;
                Debug.Log("Max distance set from sphere collider: " + maxDistance);
            }
            else
            {
                Debug.LogWarning("No SphereCollider found on the object tagged 'initArrow'.");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag 'ArcherTower'.");
        }

        // Fallback: Destroy the arrow after a set time if nothing else happens.
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the arrow forward in its local space.
        transform.position += arrowSpeed * Time.deltaTime * transform.forward;

        // Check if the arrow has traveled beyond the maxDistance from its spawn position.
        if (Vector3.Distance(spawnPosition, transform.position) > maxDistance)
        {
            Debug.Log("Arrow exceeded max distance, destroying itself.");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Arrow triggered enemy: " + other.gameObject.name);
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
