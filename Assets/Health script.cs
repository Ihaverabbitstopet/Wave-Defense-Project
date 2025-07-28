using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float defensePercentage = 0f; // 0-100% reduction

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        // Calculate effective damage after defense
        float effectiveDamage = damageAmount * (1f - (defensePercentage / 100f));
        currentHealth -= effectiveDamage;

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log(gameObject.name + " took " + effectiveDamage + " damage. Remaining health: " + currentHealth);
    }

    void Die()
    {
        // Implement death logic (e.g., disable object, play death animation, etc.)
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject); // Example: Destroy the GameObject upon death
    }
}