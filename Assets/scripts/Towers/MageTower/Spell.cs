using UnityEngine;

public class Spell : MonoBehaviour
{
    public float lifetime = 5f;          // How long the spell exists.
    public float spellSpeed = 10f;       // The speed at which the spell moves.
    public int damageAmount = 15;        // Damage dealt to an enemy.

    void Start()
    {
        // Automatically destroy the spell after a set time.
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the spell forward based on its transform.forward.
        transform.position += transform.forward * spellSpeed * Time.deltaTime;
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