using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Health healthComponent = other.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damageAmount);
            Destroy(gameObject); // Destroy projectile after hitting
        }
    }
}