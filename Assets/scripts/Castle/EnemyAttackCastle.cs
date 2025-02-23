using System.Collections;
using System.Collections.Generic;
using Castle;
using UnityEngine;

public class EnemyAttackCastle : MonoBehaviour
{
    public int damageAmount = 10;        // Damage per hit.
    public float attackInterval = 2f;      // Time between hits.
    
    // Reference to the CastleHealth component on the castle.
    private CastleHealth castleHealth;
    
    // Track attack coroutines per enemy.
    private Dictionary<Transform, Coroutine> enemyCoroutines = new Dictionary<Transform, Coroutine>();

    void Start()
    {
        // Assuming the CastleHealth component is on the parent of this trigger.
        castleHealth = GetComponentInParent<CastleHealth>();
        if (castleHealth == null)
        {
            Debug.LogError("CastleHealth component not found on the parent of CastleTrigger!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Enemy " + other.name + " entered castle trigger.");
            // If this enemy isn’t already attacking, start its attack coroutine.
            if (!enemyCoroutines.ContainsKey(other.transform))
            {
                Coroutine attackCoroutine = StartCoroutine(AttackEnemy(other.transform));
                enemyCoroutines.Add(other.transform, attackCoroutine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Enemy " + other.name + " exited castle trigger.");
            // Stop the attack coroutine for this enemy.
            if (enemyCoroutines.TryGetValue(other.transform, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                enemyCoroutines.Remove(other.transform);
            }
        }
    }

    private IEnumerator AttackEnemy(Transform enemy)
    {
        while (true)
        {
            // Apply damage to the castle.
            if (castleHealth != null)
            {
                castleHealth.TakeDamage(damageAmount);
                Debug.Log("Enemy " + enemy.name + " attacked castle for " + damageAmount + " damage.");
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }
}
