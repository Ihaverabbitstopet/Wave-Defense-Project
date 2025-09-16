using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint; // where arrows spawn
    [SerializeField] private Animator archerAnimator; // Animator on ArcherVisual

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackRange = 15f;
    [SerializeField] private int arrowDamage = 10;

    private float lastAttackTime;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogWarning("Player not found. Make sure the Player has the tag 'Player'.");
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Attack only if in range and cooldown is ready
        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            if (archerAnimator != null)
                archerAnimator.SetTrigger("Shoot"); // Play BowShot animation
        }
    }

    // 🔔 Called via Animation Event in BowShot animation
    public void FireArrow()
    {
        if (player == null || arrowPrefab == null || firePoint == null) return;

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        // Initialize arrow with direction + damage
        ArrowProjectile arrowProjectile = arrow.GetComponent<ArrowProjectile>();
        if (arrowProjectile != null)
        {
            arrowProjectile.Initialize(direction, arrowDamage);
        }
    }
}
