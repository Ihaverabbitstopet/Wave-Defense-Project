using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoHeal : MonoBehaviour
{
    [SerializeField] private float healRate = 1f;      // seconds between heals
    [SerializeField] private float healAmount = 1f;    // amount healed each time

    private EnemyHealth enemyHealth;
    private float timer;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth == null)
        {
            Debug.LogError("EnemyHealth component missing on this GameObject.");
            enabled = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= healRate)
        {
            timer = 0f;
            enemyHealth.Heal(healAmount);
        }
    }
}
