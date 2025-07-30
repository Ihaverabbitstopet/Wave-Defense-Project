using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    public enum KnightState { Patrolling, Waiting, Chasing }

    [Header("References")]
    public Transform knightVisual;
    public Animator animator;
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Patrol Settings")]
    public float waitDuration = 2f;
    public float minX = -26f;
    public float maxX = 26f;
    public float minY = -8f;
    public float maxY = -2f;

    [Header("Chase Settings")]
    public float sightRange = 5f;

    private Rigidbody2D rb;
    private KnightState currentState = KnightState.Patrolling;
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
            case KnightState.Patrolling:
                Patrol();
                CheckForPlayer();
                break;
            case KnightState.Waiting:
                WaitAtPoint();
                CheckForPlayer();
                break;
            case KnightState.Chasing:
                ChasePlayer();
                CheckChaseDistance();
                break;
        }
    }

    private void Patrol()
    {
        Vector2 direction = (targetPoint - rb.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPoint, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        AnimateAndFlip(direction, newPosition);

        if (Vector2.Distance(rb.position, targetPoint) < 0.1f)
        {
            currentState = KnightState.Waiting;
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
            currentState = KnightState.Patrolling;
        }
    }

    private void PickNewRandomPatrolPoint()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        targetPoint = new Vector2(x, y);
    }

    private void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        AnimateAndFlip(direction, newPosition);
    }

    private void CheckForPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= sightRange)
        {
            currentState = KnightState.Chasing;
        }
    }

    private void CheckChaseDistance()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > sightRange * 1.5f)
        {
            currentState = KnightState.Patrolling;
            PickNewRandomPatrolPoint();
        }
    }

    private void AnimateAndFlip(Vector2 direction, Vector2 newPosition)
    {
        float moveDelta = Vector2.Distance(rb.position, newPosition) / Time.fixedDeltaTime;
        animator.SetFloat("Speed", moveDelta);

        if (Mathf.Abs(direction.x) > 0.01f)
        {
            Vector3 scale = knightVisual.localScale;
            scale.x = direction.x > 0 ? 1 : -1;
            knightVisual.localScale = scale;
        }
    }
}
