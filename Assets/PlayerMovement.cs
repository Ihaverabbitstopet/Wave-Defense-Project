using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject attackHitbox;
    public Transform playerVisual;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool isAttacking = false;
    private PlayerControls controls;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        // Flip toward mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 scale = transform.localScale;
        scale.x = mousePos.x < transform.position.x ? -1f : 1f;
        transform.localScale = scale;

        animator.SetFloat("Speed", moveInput.magnitude);
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
        yield return new WaitForSeconds(0.5f); // match animation
        isAttacking = false;
    }

    // Called by animation events
    public void EnableHitbox() => attackHitbox.SetActive(true);
    public void DisableHitbox() => attackHitbox.SetActive(false);
}

