using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private Transform groundCheck;
    private LayerMask groundLayer;

    private float horizontal;
    private bool facingRight = true;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        HandleAnimations();
        HandleFlip();
    }

    private void HandleAnimations()
    {
        bool isMoving = rb.linearVelocity.x != 0;

        animator.SetBool("isMoving", isMoving);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void UpdateMovement()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (context.canceled && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.5f);

        }
    }

    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        } else if (rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }
}
