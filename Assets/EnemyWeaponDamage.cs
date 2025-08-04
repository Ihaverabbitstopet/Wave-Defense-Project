using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;             // DaggerHitbox reference
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        if (hitbox != null)
            hitbox.enabled = false;
    }

    public void EnableHitbox()
    {
        if (hitbox != null)
        {
            hitbox.enabled = true;
            Debug.Log("Hitbox enabled.");
        }
    }

    public void DisableHitbox()
    {
        if (hitbox != null)
        {
            hitbox.enabled = false;
            Debug.Log("Hitbox disabled.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Something entered hitbox: {collision.name}");

        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Debug.Log("Hit something on player layer.");

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Player hit by dagger for {damage} damage.");
            }
            else
            {
                Debug.LogWarning("No PlayerHealth component found on the object hit.");
            }
        }
        else
        {
            Debug.Log("Hit object not on player layer.");
        }
    }
}
