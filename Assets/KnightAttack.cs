using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    [SerializeField] private Animator daggerAnimator; // Reference to dagger's Animator
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;

    private float lastAttackTime;

    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            if (player != null)
            {
                Attack(player.gameObject);
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack(GameObject player)
    {
        if (daggerAnimator != null)
        {
            daggerAnimator.SetTrigger("Stab");
        }

        EnemyWeaponDamage damage = GetComponentInChildren<EnemyWeaponDamage>();
        if (damage != null)
        {
            damage.TryHitPlayer(player);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
