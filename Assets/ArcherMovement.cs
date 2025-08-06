using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    public enum ArcherState { Patrolling, Waiting, Approaching, Attacking }

    [Header("References")]
    [SerializeField] private Transform archerVisual;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stoppingDistance = 3f;

    [Header("Patrol Settings")]
    [SerializeField] private float waitDuration = 2f;
    [SerializeField] private float minX = -26f;
    [SerializeField] private float maxX = 26f;
    [SerializeField] private float minY = -8f;
    [SerializeField] private float maxY = -2f;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 10f;

    private Rigidbody2D rb;
    private ArcherState currentState = ArcherState.Patrolling;
    private float waitTimer;
    private Vector2 targetPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (archerVisual == null)
            Debug.LogWarning("ArcherVisual reference not set!");
        if (animator == null)
            Debug.LogWarning("Animator reference not set!");
        if (player == null)
            Debug.LogWarning("Player reference not set!");

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
                HandleAttackingState();
                break;
        }
    }

    private void Patrol()
    {
        Vector2 direction = (targetPoint - rb.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPoint, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        AnimateAndFlip(direction);
        animator.SetFloat("Speed", moveSpeed);

        if (Vector2.Distance(rb.position, targetPoint) < 0.1f)
        {
            currentState = ArcherState.Waiting;
            waitTimer = waitDuration;
            animator.SetFloat("Speed", 0f); // Idle
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

        float distance = Vector2.Distance(rb.position, player.position);

        if (distance > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
            AnimateAndFlip(direction);
            animator.SetFloat("Speed", moveSpeed); // Walking
        }
        else
        {
            currentState = ArcherState.Attacking;
            animator.SetFloat("Speed", 0f); // Idle
            AnimateAndFlip(player.position - transform.position);
        }
    }

    private void CheckStopDistance()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange * 1.5f)
        {
            PickNewRandomPatrolPoint();
            currentState = ArcherState.Patrolling;
        }
    }

    private void HandleAttackingState()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange * 1.5f)
        {
            PickNewRandomPatrolPoint();
            currentState = ArcherState.Patrolling;
        }
        else if (distance > stoppingDistance)
        {
            currentState = ArcherState.Approaching;
        }
        else
        {
            // Still in range — just idle + face player
            animator.SetFloat("Speed", 0f);
            AnimateAndFlip(player.position - transform.position);
        }
    }

    private void AnimateAndFlip(Vector2 direction)
    {
        if (archerVisual == null) return;

        if (Mathf.Abs(direction.x) > 0.01f)
        {
            Vector3 scale = archerVisual.localScale;
            scale.x = direction.x > 0 ? 1 : -1;
            archerVisual.localScale = scale;
        }
    }

    private void PickNewRandomPatrolPoint()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        targetPoint = new Vector2(x, y);
    }

    // Optional setters
    public void SetMoveSpeed(float speed) => moveSpeed = speed;
    public void SetStoppingDistance(float distance) => stoppingDistance = distance;
    public void SetDetectionRange(float range) => detectionRange = range;
}

