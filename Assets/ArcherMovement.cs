using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    public enum ArcherState { Patrolling, Waiting, Approaching, Attacking }

    [Header("References")]
    public Transform archerVisual;
    public Animator animator;
    public Transform player;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float stoppingDistance = 3f;       // How close the archer moves before stopping to attack

    [Header("Patrol Settings")]
    public float waitDuration = 2f;
    public float minX = -26f;
    public float maxX = 26f;
    public float minY = -8f;
    public float maxY = -2f;

    [Header("Detection Settings")]
    public float detectionRange = 10f;        // Larger detection range than Knight

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
                DetectPlayer();
                break;

            case ArcherState.Waiting:
                WaitAtPoint();
                DetectPlayer();
                break;

            case ArcherState.Approaching:
                ApproachPlayer();
                CheckStopDistance();
                break;

            case ArcherState.Attacking:
                // Placeholder - you'll add attack logic later
                AnimateAndFlip(player.position - transform.position);
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

    private void DetectPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            currentState = ArcherState.Approaching;
        }
    }

    private void ApproachPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(rb.position, player.position);

        if (distance > stoppingDistance)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
            AnimateAndFlip(direction);
            animator.SetFloat("Speed", moveSpeed);
        }
        else
        {
            currentState = ArcherState.Attacking;
            animator.SetFloat("Speed", 0f);
        }
    }

    private void CheckStopDistance()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
        {
            PickNewRandomPatrolPoint();
            currentState = ArcherState.Patrolling;
        }
    }

    private void AnimateAndFlip(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > 0.01f)
        {
            Vector3 scale = archerVisual.localScale;
            scale.x = direction.x > 0 ? 1 : -1;
            archerVisual.localScale = scale;
        }

        animator.SetFloat("Speed", direction.magnitude);
    }

    private void PickNewRandomPatrolPoint()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        targetPoint = new Vector2(x, y);
    }
}
