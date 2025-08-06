using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
	[SerializeField] private Animator archerAnimator;
    [SerializeField] private float attackCooldown = 1.5f;

    private float lastAttackTime;
    private Transform player;
    private Transform archerVisual;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogWarning("Player not found. Make sure the Player has the tag 'Player'.");

        // Assumes this script is on the Bow → child of ArcherVisual
        archerVisual = transform.root.Find("ArcherVisual");
        if (archerVisual == null)
            Debug.LogWarning("ArcherVisual not found under root.");
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < 8f && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
			if (archerAnimator != null)
                archerAnimator.SetTrigger("Shoot"); // Trigger BowShot animation
            Attack();
        }
    }

    private void Attack()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Flip the arrow's direction if ArcherVisual is flipped (scale.x = -1)
        float flipX = Mathf.Sign(archerVisual.localScale.x);
        direction = new Vector2(direction.x * flipX, direction.y);

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<ArrowProjectile>().Initialize(direction);
    }
}
