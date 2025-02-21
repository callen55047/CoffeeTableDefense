using System.Collections;
using UnityEngine;

namespace Castle
{
    public class EnemyAttackTower : MonoBehaviour
    {
        public int damageAmount = 10; // Damage inflicted per attack
        public float attackInterval = 2f; // Time between subsequent attacks

        private Coroutine attackCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enemy entered trigger: " + other.name);
            // Now checking for the enemy tag ("RedEnemy")
            if (other.CompareTag("RedEnemy"))
            {
                // If not already attacking, begin the attack routine.
                if (attackCoroutine == null)
                {
                    // Get the CastleHealth component from the parent (the main castle)
                    CastleHealth castleHealth = GetComponentInParent<CastleHealth>();
                    if (castleHealth != null)
                    {
                        Debug.Log("CastleHealth component found on parent.");
                        // Deliver an immediate hit
                        castleHealth.TakeDamage(damageAmount);
                        // Start the coroutine that will attack at intervals.
                        attackCoroutine = StartCoroutine(AttackCastle(castleHealth));
                    }
                    else
                    {
                        Debug.LogWarning("CastleHealth component not found in parent.");
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("RedEnemy") && attackCoroutine != null)
            {
                Debug.Log("Enemy exited castle trigger: " + other.name);
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }

        private IEnumerator AttackCastle(CastleHealth castle)
        {
            // Wait for the specified interval, then apply damage repeatedly.
            while (true)
            {
                yield return new WaitForSeconds(attackInterval);
                Debug.Log("Attacking castle for " + damageAmount + " damage.");
                castle.TakeDamage(damageAmount);
            }
        }
    }

}