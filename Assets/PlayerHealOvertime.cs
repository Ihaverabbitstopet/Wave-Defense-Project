using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealOverTime : MonoBehaviour
{
    public PlayerHealth playerHealth;  // Reference to your PlayerHealth script
    public int healAmount = 1;         // How much HP to heal each tick
    public float healInterval = 1f;    // Time in seconds between heals

    private float timer;

    void Start()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerHealth>();

        timer = healInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            playerHealth.Heal(healAmount);
            timer = healInterval;
        }
    }
}
