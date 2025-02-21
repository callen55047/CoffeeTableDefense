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
                // Additional game over logic goes here.
            }
        }
    }
}