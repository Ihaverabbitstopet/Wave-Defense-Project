using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    [Header("References")]
    public GameObject attackHitbox;        // Assign AttackHitbox here
    public Transform playerVisual;         // Assign PlayerVisual here
    private Rigidbody2D rb;
    private Animator animator;

    private PlayerControls controls;
    private bool isAttacking;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = playerVisual.GetComponent<Animator>();
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Attack.performed += ctx => HandleAttack();
    }

    void OnDisable()
    {
        controls.Player.Attack.performed -= ctx => HandleAttack();
        controls.Disable();
    }

    void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        // Flip based on mouse position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 scale = transform.localScale;
        scale.x = (mouseWorldPos.x < transform.position.x) ? -1f : 1f;
        transform.localScale = scale;

        // Set animation speed
        float speed = moveInput.magnitude;
        animator.SetFloat("Speed", speed);
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleAttack()
    {
        if (!isAttacking)
            StartCoroutine(PerformAttack());
    }

    private System.Collections.IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f); // Adjust to match animation length
        isAttacking = false;
    }

    // Called from animation events
    public void EnableHitbox() => attackHitbox.SetActive(true);
    public void DisableHitbox() => attackHitbox.SetActive(false);
}


