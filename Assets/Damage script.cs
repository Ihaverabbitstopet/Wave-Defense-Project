using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public float attackDamage = 10f; // Damage dealt by this attacker

    public void Attack(GameObject target)
    {
        HealthSystem targetHealth = target.GetComponent<HealthSystem>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(attackDamage);
            Debug.Log(gameObject.name + " attacked " + target.name + " for " + attackDamage + " damage.");
        }
    }
}