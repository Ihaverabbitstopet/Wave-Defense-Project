using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDamageTest : MonoBehaviour
{
    public int damageAmount = 10;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth component not found!");
            return;
        }

        Invoke(nameof(ApplyDamage), 3f);  // Damage after 3 seconds
    }

    private void ApplyDamage()
    {
        playerHealth.TakeDamage(damageAmount);
        Debug.Log($"Auto applied {damageAmount} damage to player.");
    }
}
