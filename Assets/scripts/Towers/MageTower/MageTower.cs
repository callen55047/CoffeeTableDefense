using UnityEngine;

public class MageTower : MonoBehaviour
{
    [Header("Spell Settings")]
    public GameObject spellPrefab;         // The spell prefab asset.
    public Transform spellSpawnPoint;      // Where the spell spawns on the tower.
    public float attackCooldown = 1f;      // Time between spells.

    private float lastAttackTime;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is an enemy.
        if (other.CompareTag("RedEnemy"))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                ShootSpell(other.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    private void ShootSpell(Transform target)
    {
        if (spellPrefab == null)
        {
            Debug.LogError("spellPrefab is null. Please assign the prefab asset from the Project folder.");
            return;
        }
        
        // Determine spawn position.
        Vector3 spawnPos = spellSpawnPoint.position;
        // Calculate direction from spawn point to target (ignoring vertical difference for a straight shot).
        Vector3 direction = (target.position - spawnPos).normalized;
        // Calculate the rotation so that the spell faces the target.
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        
        // Instantiate the spell.
        GameObject spellInstance = Instantiate(spellPrefab, spawnPos, rotation);
    }
}