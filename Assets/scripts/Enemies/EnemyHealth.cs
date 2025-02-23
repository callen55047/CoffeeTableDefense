using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        private int currentHealth;

        // Reference to the enemy's health bar slider (assign this in the prefab)
        public Slider healthBar;

        void Start()
        {
            currentHealth = maxHealth;
            if (healthBar != null)
            {
                healthBar.maxValue = maxHealth;
                healthBar.value = currentHealth;
            }
            Debug.Log(gameObject.name + " starting health: " + currentHealth);
        }

        // Call this method to deduct health
        public void TakeDamage(int damage)
        {
            Debug.Log(gameObject.name + " health before damage: " + currentHealth);
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (healthBar != null)
            {
                healthBar.value = currentHealth;
                Debug.Log(gameObject.name + " slider value updated to: " + healthBar.value);
            }
            Debug.Log(gameObject.name + " health after damage: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log(gameObject.name + " has died.");
            Destroy(gameObject);
        }
    }
}