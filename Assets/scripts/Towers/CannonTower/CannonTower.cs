using UnityEngine;

public class CannonTower : MonoBehaviour
{
    [Header("Cannon Settings")]
    public GameObject cannonballPrefab;      // The cannonball prefab asset.
    public Transform cannonSpawnPoint;       // The spawn point for the cannonball.
    public float attackCooldown = 3f;        // Time between cannon shots.
    private float gravity = 9.81f;            // Gravity.
    
    // The only adjustable variable: the apex height (relative to the spawn point).
    public float apexHeight = 5f;            // e.g., the cannonball will reach 5 units above the spawn point.

    private float lastAttackTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Enemy " + other.name + " entered cannon tower range.");
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Debug.Log("Cannon Tower firing at enemy " + other.name);
                ShootCannonball(other.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    private void ShootCannonball(Transform target)
    {
        if (cannonballPrefab == null)
        {
            Debug.LogError("cannonballPrefab is null. Please assign the prefab asset from the Project folder.");
            return;
        }
        
        Vector3 spawnPos = cannonSpawnPoint.position;
        // Calculate horizontal distance (d) from spawn point to target (ignoring vertical differences)
        Vector3 toTarget = target.position - spawnPos;
        float d = new Vector2(toTarget.x, toTarget.z).magnitude;
        // For our trajectory, assume the spawn and target are at the same vertical level.
        // The apex height (ΔH) is the height above spawn.
        float deltaH = apexHeight;
        
        // Compute the launch angle theta (in radians) so that the projectile reaches the desired apex.
        float theta = Mathf.Atan(4f * deltaH / d);
        // Compute the required initial speed v0:
        float v0Squared = 2f * gravity * deltaH + (gravity * d * d) / (8f * deltaH);
        float v0 = Mathf.Sqrt(v0Squared);
        
        Debug.Log("Calculated launch angle (deg): " + (theta * Mathf.Rad2Deg));
        Debug.Log("Calculated initial speed: " + v0);
        
        // Determine the horizontal (flat) direction from spawn to target.
        Vector3 flatDirection = new Vector3(toTarget.x, 0, toTarget.z).normalized;
        // Form the full launch direction: horizontal component is cos(theta) and vertical is sin(theta).
        Vector3 launchDirection = flatDirection * Mathf.Cos(theta) + Vector3.up * Mathf.Sin(theta);
        Debug.Log("Launch direction: " + launchDirection);
        
        // Instantiate the cannonball with a rotation matching the launch direction.
        Quaternion launchRotation = Quaternion.LookRotation(launchDirection, Vector3.up);
        GameObject cannonballInstance = Instantiate(cannonballPrefab, spawnPos, launchRotation);
        Debug.Log("Cannonball " + cannonballInstance.name + " created and launched toward enemy " + target.name);
        
        // Get the Rigidbody from the cannonball and set its velocity.
        Rigidbody rb = cannonballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;  // Ensure gravity is on.
            rb.linearVelocity = launchDirection * v0;
            Debug.Log("Applied velocity: " + rb.linearVelocity);
        }
        else
        {
            Debug.LogError("No Rigidbody found on cannonball prefab!");
        }
    }
}
