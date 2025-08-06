using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    public enum ArcherState { Patrolling, Waiting, Approaching, Attacking }

    [Header("References")]
    public Transform player;
    public Animator animator;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float stopDistance = 8f;       // Distance to stop before attacking
    public float sightRange = 15f;

    [Header("Patrol Settings")]
    public float waitDuration = 2f;
    public float minX = -26f;
    public float maxX = 26f;
    public float minY = -8f;
    public float maxY = -2f;

    private Rigidbody2D rb;
    private ArcherState currentState = ArcherState.Patrolling;
    private float waitTimer;
    private Vector2 targetPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewRandomPatrolPoint();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case ArcherState.Patrolling:
                Patrol();
                CheckForPlayer();
                break;

            case ArcherState.Waiting:
                WaitAtPoint();
                CheckForPlayer();
                break;

            case ArcherState.Approaching:
                ApproachPlayer();
                break;

            case ArcherState.Attacking:
                AnimateAndFlip(Vector2.zero);
                // ArcherAttack script should handle attacking independently
                break;
        }
    }

    private void Patrol()
    {
        Vector2 direction = (targetPoint - rb.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPoint, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        AnimateAndFlip(direction);

        if (Vector2.Distance(rb.position, targetPoint) < 0.1f)
        {
            currentState = ArcherState.Waiting;
            waitTimer = waitDuration;
            animator.SetFloat("Speed", 0f);
        }
    }

    private void WaitAtPoint()
    {
        waitTimer -= Time.fixedDeltaTime;
        if (waitTimer <= 0f)
        {
            PickNewRandomPatrolPoint();
            currentState = ArcherState.Patrolling;
        }
    }

    private void PickNewRandomPatrolPoint()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        targetPoint = new Vector2(x, y);
    }

    private void CheckForPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= sightRange)
        {
            currentState = ArcherState.Approaching;
        }
    }

    private void ApproachPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
            AnimateAndFlip(direction);
        }
        else
        {
            currentState = ArcherState.Attacking;
        }
    }

    private void AnimateAndFlip(Vector2 direction)
    {
        if (animator != null)
        {
            float moveAmount = direction.magnitude;
            animator.SetFloat("Speed", moveAmount);
        }

        if (Mathf.Abs(direction.x) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = direction.x > 0 ? 1 : -1;
            transform.localScale = scale;
        }
    }
}
