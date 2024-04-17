using UnityEngine;
using UnityEngine.InputSystem;

public class LadderIndustrielle : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool Ladder;
    private bool Climbing;

    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (Ladder && Mathf.Abs(vertical) > 0f)
        {
            Climbing = true;
        }
    }

    private void FixedUpdate()
    {
        if  (Climbing)
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
        if (collision.CompareTag("LadderIndustrielle"))
        {
            Ladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LadderIndustrielle"))
        {
            Ladder = false;
            Climbing = false;
        }
    }
}