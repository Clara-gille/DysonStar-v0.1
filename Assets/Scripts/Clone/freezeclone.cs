using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  

public class FreezeClone : MonoBehaviour
{
    // Reference to the CloneMovement script attached to the clone GameObject
    private PlayerMouvement cloneMovement;
    public Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [SerializeField] private InputActionReference freeze;

    // Start is called before the first frame update
    void Start()
    {
        // Get the CloneMovement component attached to the clone GameObject
        cloneMovement = GetComponent<PlayerMouvement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        freeze.action.Enable();
        freeze.action.performed += Freeze;
    }

    private void OnDisable()
    {
        freeze.action.Disable();
        freeze.action.performed -= Freeze;
    }

    public void Freeze(InputAction.CallbackContext context)
    {
        // Check if the X key is pressed and toggle freeze/unfreeze
        if (context.performed && cloneMovement.IsGrounded() == true)
        {
            // Toggle freeze/unfreeze the clone
            cloneMovement.enabled = !cloneMovement.enabled;
            cloneMovement.frozen = !cloneMovement.frozen;
            rb.velocity = Vector2.zero;
            anim.SetBool("IsFrozen", cloneMovement.frozen);
        }
    }
    
}
