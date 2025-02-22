using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime = 50f;
    public int damageAmount = 10;  // Adjust the damage as needed

    void Start()
    {
        // Destroy the arrow after a set time to prevent lingering
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RedEnemy"))
        {
            Debug.Log("Arrow collided with enemy: " + collision.gameObject.name);
            Enemies.EnemyHealth enemyHealth = collision.gameObject.GetComponent<Enemies.EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning("EnemyHealth component not found on " + collision.gameObject.name);
            }
        }
        Destroy(gameObject); // Destroy arrow on impact
    }
}