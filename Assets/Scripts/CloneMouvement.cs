using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloneMouvement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 16f;
    private bool isFacingRight = true;
    private bool isFrozen = false; // Flag to track whether the clone is frozen or not

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isFrozen)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (horizontal > 0f && !isFacingRight)
            {
                Flip();
            }
            else if (horizontal < 0f && isFacingRight)
            {
                Flip();
            }
            if (IsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    // Method to freeze/unfreeze the clone
    public void Freeze()
    {
        isFrozen = !isFrozen;

        // If the clone is frozen, set its velocity to zero
        if (isFrozen)
        {
            rb.velocity = Vector2.zero;
        }
    }
}

