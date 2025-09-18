using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    [Header("References")]
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator axeAnimator;
    [SerializeField] private GameObject attackHitbox;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private Camera mainCamera;
    private PlayerControls controls;

    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private float lastDashTime = -Mathf.Infinity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        controls = new PlayerControls();

        // Movement
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Attack
        controls.Player.Attack.performed += ctx => Attack();

        // Dash
        controls.Player.Dash.performed += ctx => TryDash();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        HandleFlip();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = moveInput.normalized * dashSpeed;
            dashTimeLeft -= Time.fixedDeltaTime;

            if (dashTimeLeft <= 0f)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.velocity = moveInput * moveSpeed;
        }
    }

    private void HandleFlip()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float direction = mouseWorldPos.x - transform.position.x;

        Vector3 localScale = transform.localScale;
        localScale.x = direction >= 0 ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    private void UpdateAnimations()
    {
        float speed = moveInput.sqrMagnitude;
        playerAnimator.SetFloat("Speed", speed);
    }

    private void Attack()
    {
        if (axeAnimator != null)
        {
            axeAnimator.SetTrigger("Swing");
        }

        // Optional: activate attack hitbox here
        // attackHitbox.SetActive(true);
    }

    private void TryDash()
    {
        if (Time.time >= lastDashTime + dashCooldown && moveInput != Vector2.zero)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDashTime = Time.time;

            Debug.Log("Player dashed!");
        }
    }
}
