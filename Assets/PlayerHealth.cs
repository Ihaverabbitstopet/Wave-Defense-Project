using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Defense Settings")]
    [Range(0, 100)]
    public int defensePercent = 20;

    [Header("Death Settings")]
    public GameObject deathEffect; // Optional

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount, bool ignoresDefense = false)
    {
        int finalDamage = damageAmount;

        if (!ignoresDefense)
        {
            finalDamage = Mathf.RoundToInt(damageAmount * (1f - defensePercent / 100f));
        }
        else
        {
        }

        currentHealth -= finalDamage;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent below zero

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player died!");

        if (deathEffect)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
    }

    public int GetCurrentHealth() => currentHealth;
}
