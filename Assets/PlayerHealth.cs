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
        Debug.Log("Player Health Initialized. HP: " + currentHealth);
    }

    public void TakeDamage(int damageAmount, bool ignoresDefense = false)
    {
        int finalDamage = damageAmount;

        if (!ignoresDefense)
        {
            finalDamage = Mathf.RoundToInt(damageAmount * (1f - defensePercent / 100f));
            Debug.Log($"Player took {finalDamage} damage (defense reduced by {defensePercent}%).");
        }
        else
        {
            Debug.Log($"Player took {finalDamage} raw damage (defense ignored).");
        }

        currentHealth -= finalDamage;
        Debug.Log("Current Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");

        if (deathEffect)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Disable the player object or trigger a Game Over screen
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth() => currentHealth;

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player healed. HP: " + currentHealth);
    }
}
