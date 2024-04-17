// Mettre ce script sur le GameObject Player
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouvement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private Animator anim;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;

    private float horizontal;
    public float magnetHorizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 16f;
    private bool isFacingRight = true;
    public bool frozen = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
    }
    // Update is called once per frame
    void Update()
    {
        //Decrease the magnetHorizontal value and cap it at 0
        magnetHorizontal = Mathf.Lerp(magnetHorizontal, 0, Time.deltaTime * 3);
        rb.velocity = new Vector2(horizontal * speed + magnetHorizontal, rb.velocity.y);
        
        
        anim.SetFloat("HorizontalSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        if(horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if(horizontal < 0f && isFacingRight)
        {
            Flip();
        }
        if(IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            if (jumpBufferCounter > 0)
            {
                PerformJump();
                jumpBufferCounter = 0; 
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (coyoteTimeCounter > 0f)
            {
                PerformJump();
            }
            else
            {
                jumpBufferCounter = jumpBufferTime;
            }
        }
        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }
    private void PerformJump()
    {
        if (frozen)
        {
            return;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0; 
    }
    public bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        anim.SetBool("IsGrounded", grounded);
        return grounded;
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
}

