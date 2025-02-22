using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;         // The arrow prefab to be fired
    public Transform arrowSpawnPoint;      // Where the arrow spawns on the tower
    public float arrowForce = 50f;          // The force applied to the arrow
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
    
        // Instantiate the arrow at the spawn point's position and rotation.
        GameObject arrowInstance = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
    
        // Find the GameObject tagged "initArrow"
        GameObject initArrow = GameObject.FindWithTag("initArrow");
        if (initArrow != null)
        {
            // Set the arrow's transform based on the initArrow's transform.
            arrowInstance.transform.position = initArrow.transform.position;
            arrowInstance.transform.rotation = initArrow.transform.rotation;
            arrowInstance.transform.localScale = initArrow.transform.localScale;
        }
        else
        {
            Debug.LogError("No GameObject found with tag 'initArrow'.");
        }
    
        // Get the Rigidbody from the arrow and apply force.
        Rigidbody rb = arrowInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(arrowSpawnPoint.forward * arrowForce);
            Debug.Log("Arrow " + arrowInstance.name + " created and fired at enemy " + target.name);
        }
        else
        {
            Debug.LogError("No Rigidbody found on arrow prefab!");
        }
    }
}