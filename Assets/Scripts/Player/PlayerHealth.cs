using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;

        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // You can disable movement, show game over screen, etc.
        gameObject.SetActive(false);
    }
}
