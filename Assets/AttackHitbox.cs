using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private WeaponDamage weaponDamage;

    private void Awake()
    {
        weaponDamage = GetComponentInParent<WeaponDamage>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null && weaponDamage != null)
        {
            float baseDamage = weaponDamage.GetDamage();
            bool ignoreDefense = weaponDamage.IgnoresDefense();

            enemy.TakeDamage(baseDamage, ignoreDefense);
        }
    }
}
