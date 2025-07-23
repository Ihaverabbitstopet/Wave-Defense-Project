using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTarget; // Reference to the player's transform
    public float movementSpeed = 2f; // Base movement speed
    public float stoppingDistance = 1f; // Distance at which the enemy stops
    public float minSpeedMultiplier = 0.5f; // Minimum speed multiplier
    public float maxSpeedMultiplier = 2f; // Maximum speed multiplier
    public float closeDistanceThreshold = 3f; // Distance at which speed multiplier increases

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component for flipping
        spriteRenderer = GetComponent<SpriteRenderer>(); 

        // Find the player if not set in the inspector (by tag)
        if (playerTarget == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerTarget = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player GameObject not found! Make sure it has the 'Player' tag.");
            }
        }
    }

    void Update()
    {
        if (playerTarget != null)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

            // Determine if the enemy should move and in which direction
            if (distanceToPlayer > stoppingDistance)
            {
                // Calculate movement direction
                Vector2 direction = (playerTarget.position - transform.position).normalized;

                // Adjust speed based on distance
                float currentSpeed = movementSpeed;
                if (distanceToPlayer < closeDistanceThreshold)
                {
                    // Linearly interpolate speed multiplier between min and max based on proximity
                    float t = 1 - (distanceToPlayer / closeDistanceThreshold); // Invert 't' so closer means faster
                    currentSpeed *= Mathf.Lerp(minSpeedMultiplier, maxSpeedMultiplier, t);
                }

                // Move towards the player
                transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, currentSpeed * Time.deltaTime);
            }

            // Flip the enemy based on player's position
            if (playerTarget.position.x < transform.position.x)
            {
                // Player is to the left, flip the sprite to face left
                spriteRenderer.flipX = true; 
            }
            else if (playerTarget.position.x > transform.position.x)
            {
                // Player is to the right, ensure the sprite faces right
                spriteRenderer.flipX = false;
            }
        }
    }
}