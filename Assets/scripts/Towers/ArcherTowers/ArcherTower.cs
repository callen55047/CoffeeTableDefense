using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;         // The arrow prefab to be fired
    public Transform arrowSpawnPoint;      // Where the arrow spawns on the tower
    public float attackCooldown = 1f;      // Time between shots

    private float lastAttackTime;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is an enemy (e.g., tagged "RedEnemy")
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Enemy " + other.name + " entered tower range.");
            // Check cooldown to avoid spamming arrows from the same enemy
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Debug.Log("Shooting arrow at enemy " + other.name);
                ShootArrow(other.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    private void ShootArrow(Transform target)
    {
        if (arrowPrefab == null)
        {
            Debug.LogError("arrowPrefab is null. Please assign the prefab asset from the Project folder.");
            return;
        }
    
        Vector3 direction = target.position - arrowSpawnPoint.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        // Instantiate the arrow at the spawn point's position and rotation.
        GameObject arrowInstance = Instantiate(arrowPrefab, arrowSpawnPoint.position, rotation);

        Debug.Log("Arrow " + arrowInstance.name + " created and fired at enemy " + target.name);
    }
}