using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D col;

    [Header("Ground Collision Details")]
    private Transform groundCheck;
    private LayerMask groundLayer;

    [Header("Movement Details")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private float horizontal;
    private bool facingRight = true;

    [Header("Bunny Hop Details")]
    [SerializeField] private float bunnyHopBonus = 1.2f;    // speed multiplier on successful hop
    [SerializeField] private float bunnyHopWindow = 0.15f;  // seconds after landing to qualify
    private float landedTime = -1f;
    private bool wasGrounded = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<CapsuleCollider2D>();

        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        UpdateMovement();
        HandleAnimations();
        HandleFlip();
        TrackLanding();
    }

    private void HandleAnimations()
    {
        bool isMoving = rb.linearVelocity.x != 0;

        animator.SetBool("isMoving", isMoving);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(col.bounds.size.x, 0.1f), 0f, groundLayer);
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
            float timeSinceLanded = Time.time - landedTime;
            bool isBunnyHop = timeSinceLanded <= bunnyHopWindow;

            float newJumpForce = jumpForce;

            if (isBunnyHop)
            {
                newJumpForce = jumpForce * 1.1f; // slightly higher jump on bunny hop
                horizontal *= bunnyHopBonus;     // preserve + boost horizontal speed
            }

            rb.linearVelocity = new Vector2(horizontal * moveSpeed, newJumpForce);
        }

        if (context.canceled && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void TrackLanding()
    {
        bool grounded = IsGrounded();

        if (!wasGrounded && grounded) // just landed this frame
        {
            landedTime = Time.time;
        }

        wasGrounded = grounded;
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

    private void OnDrawGizmos()
    {
        if (groundCheck == null || col == null) return;

        Gizmos.color = IsGrounded() ? Color.green : Color.red;

        // OverlapBox
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(col.bounds.size.x, 0.1f, 0f));
    }
}
