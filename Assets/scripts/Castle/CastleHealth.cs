using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    public class CastleHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        private int currentHealth;

        public Slider healthBar; // Drag your Slider here
        public Image fillImage; // Drag the fill Image from the Slider (usually under Fill Area)

        public Color fullColor = Color.green;
        public Color zeroColor = Color.red;
        
        public GameObject explosionPrefab;  // Assign your Explosion Fire prefab here.
        public Vector3 explosionScale = new Vector3(0.1f, 0.1f, 0.1f);  // Desired scale for the explosion FX.

        void Start()
        {
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;

            if (fillImage == null && healthBar.fillRect != null)
            {
                fillImage = healthBar.fillRect.GetComponent<Image>();
            }

            if (fillImage != null)
            {
                fillImage.color = fullColor;
            }
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.value = currentHealth;

            float healthPercent = (float)currentHealth / maxHealth;
            if (fillImage != null)
            {
                fillImage.color = Color.Lerp(zeroColor, fullColor, healthPercent);
            }

            Debug.Log("Castle Health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Debug.Log("Castle Destroyed!");
                Die();
                // Additional game over logic goes here.
            }
        }
        
        private void Die()
        {
            Debug.Log("Castle Destroyed!");
            
            // Instantiate the explosion effect at the castle's position.
            if (explosionPrefab != null)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.transform.localScale = explosionScale;  // Scale it down as desired.
                Debug.Log("Explosion effect instantiated with scale: " + explosionScale);
            }
            else
            {
                Debug.LogWarning("No explosionPrefab assigned in CastleHealth!");
            }
            
            // Disable all MeshRenderers in the castle and its children
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in renderers)
            {
                r.enabled = false;
            }

            // Disable all Colliders so enemies can walk through
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider c in colliders)
            {
                c.enabled = false;
            }

            // Optionally, disable the health bar UI
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(false);
            }

            // Optionally, destroy the castle GameObject after a delay
            // Destroy(gameObject, 2f);
        }
    }
}