using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField, Range(0f, 1f)] private float defense = 0.2f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float rawDamage, bool ignoreDefense = false)
    {
        float damageTaken = ignoreDefense ? rawDamage : rawDamage * (1f - defense);
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public float GetCurrentHealth() => currentHealth;

    public float GetMaxHealth() => maxHealth;

    private void Die()
    {
        Destroy(gameObject);
    }
}
