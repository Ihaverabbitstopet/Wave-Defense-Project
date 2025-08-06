using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    [Header("Attack Setup")]
    public Animator daggerAnimator;       // Animator controlling dagger stab animation
    public Transform attackPoint;         // Empty GameObject where the hitbox is located
    public Collider2D attackHitbox;       // Collider2D on the hitbox (usually child of attackPoint)

    [Header("Attack Settings")]
    public float attackCooldown = 1.5f;

    private float lastAttackTime;

    private void Start()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;  // Disable hitbox initially
    }

    private void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (PlayerInRange())
            {
                lastAttackTime = Time.time;
                daggerAnimator.SetTrigger("Stab");  // Play attack animation
            }
        }
    }

    // Check if player is inside attack range (you can customize this)
    private bool PlayerInRange()
    {
        if (attackPoint == null)
            return false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
                return true;
        }
        return false;
    }

    // Called by an Animation Event at the attack frame to enable the hitbox
    public void EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = true;
    }

    // Called by an Animation Event at the end of the attack to disable the hitbox
    public void DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 1.25f);
    }
}
