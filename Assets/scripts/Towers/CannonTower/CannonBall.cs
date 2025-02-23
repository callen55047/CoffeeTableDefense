using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float lifetime = 5f;
    public int damageAmount = 20;      // Damage dealt to an enemy
    private Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position;
        Debug.Log("Cannonball spawned at: " + spawnPosition);
        Destroy(gameObject, lifetime);
    }

    // OnTriggerEnter is used for collision detection.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cannonball collided with: " + other.gameObject.name);

        // If the cannonball hits an enemy, apply damage.
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Cannonball hit enemy: " + other.gameObject.name);
            Enemies.EnemyHealth enemyHealth = other.GetComponent<Enemies.EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Damage applied: " + damageAmount + " to enemy: " + other.gameObject.name);
            }
            else
            {
                Debug.LogWarning("EnemyHealth component not found on " + other.gameObject.name);
            }
            Destroy(gameObject);
        }
        // If the cannonball hits the level mesh collider, destroy it.
        else if (other.GetComponent<MeshCollider>() != null)
        {
            Debug.Log("Cannonball hit level mesh collider (" + other.gameObject.name + "), destroying itself.");
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, do nothing. The cannonball continues its flight.
            Debug.Log("Cannonball hit a non-critical object (" + other.gameObject.name + ").");
        }
    }
}
