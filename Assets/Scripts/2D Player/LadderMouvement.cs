using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputActionReference move;
    private Animator anim;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        move.action.performed += Climb;
        move.action.canceled += stopClimb;
    }

    public void Update()
    {
        anim.SetBool("IsClimbing", isClimbing);
    }

    private void Climb(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<Vector2>().y;

        if (vertical != 0f)
        { 
            if (isLadder && vertical > 0f)
            {
                isClimbing = true;
            }
        }
    }

    private void stopClimb(InputAction.CallbackContext context)
    {
        vertical = 0f;
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Ladder"))
        {

            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {

            isLadder = false;
            isClimbing = false;
        }
    }

}