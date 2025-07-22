using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public Animator animator;
    // You can also drag the object with the Animator component to this slot
    // In the Inspector, drag the object with the Animator component to this field

    void Start()
    {
        animator = GetComponent<Animator>();
        // Alternatively, if the animator is on a child object:
        // animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is for left mouse button
        {
            animator.SetTrigger("Attack");
        }
    }
}