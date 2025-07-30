using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField, Range(0f, 1f)] private float defense = 0.2f; // 20% reduction

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float rawDamage, bool ignoreDefense = false)
    {
        float damageTaken = ignoreDefense ? rawDamage : rawDamage * (1f - defense);
        currentHealth -= damageTaken;

        Debug.Log($"{gameObject.name} took {damageTaken} damage! HP left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Destroy(gameObject);
    }
}
