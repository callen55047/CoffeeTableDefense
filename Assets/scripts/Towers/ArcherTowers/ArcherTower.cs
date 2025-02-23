using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;         // The arrow prefab to be fired
    public Transform arrowSpawnPoint;      // Where the arrow spawns on the tower
    public float attackCooldown = 1f;        // Time between shots

    private float lastAttackTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Enemy " + other.name + " entered tower range.");
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                ShootArrow(other.transform);
                lastAttackTime = Time.time;
            }
            else
            {
                Debug.Log("Attack cooldown not finished. Next available attack at: " + (lastAttackTime + attackCooldown));
            }
        }
        else
        {
            Debug.Log("Object " + other.name + " is not tagged as 'RedEnemy'.");
        }
    }

    private void ShootArrow(Transform target)
    {
        if (arrowPrefab == null)
        {
            return;
        }
    
        Vector3 spawnPos = arrowSpawnPoint.position;
        Vector3 direction = target.position - spawnPos;
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        // Instantiate the arrow at the spawn point with the calculated rotation.
        GameObject arrowInstance = Instantiate(arrowPrefab, spawnPos, rotation);
    }
}
