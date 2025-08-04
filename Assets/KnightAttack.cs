using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown = 1f;

    [SerializeField] private Animator daggerAnimator; // Animator on the Dagger
    [SerializeField] private EnemyWeaponDamage weaponDamage; // Correct type here!

    private float lastAttackTime;

    private void Update()
    {
        if (PlayerInRange() && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            daggerAnimator.SetTrigger("Stab"); // Triggers stab animation
        }
    }

    private bool PlayerInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        return hit != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
