using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Cette vitesse sera maintenant considérée comme la force appliquée
    [SerializeField] private float jumpForce = 10f; // Force de saut, à ajuster dans l'inspecteur Unity
    private Rigidbody2D body;
    private Animator anim;
    private bool Grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        body.freezeRotation = true;
    }

    private void Update()
    {
        //Flip player Left to right
        TurnCheck();

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }

         // Check for falling
        if (!Grounded && body.velocity.y < -0.01)
        {
            anim.SetBool("FallDown", true);
        }
        else
        {
            anim.SetBool("FallDown", false);
        }

        //Set animator parameters
        anim.SetBool("Grounded", Grounded);
        anim.SetBool("Run", UserInput.instance.moveInput.x != 0);
    }

    private void FixedUpdate()
    {
        // Utilisez FixedUpdate pour les interactions avec le moteur physique
        Move();
        Jump();
    }

    private void Move()
    {
        // Appliquer une force pour le mouvement
        float horizontal = UserInput.instance.moveInput.x;
        Vector2 force = new Vector2(horizontal * speed, 0);
        body.AddForce(force);

        // Limiter la vitesse maximale
        if (Mathf.Abs(body.velocity.x) > speed)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
        }
    }

    private void TurnCheck()
    {
        // Votre code existant pour tourner le joueur
    }

    private void Turn()
    {
        // Votre code existant pour effectuer la rotation
    }
    
    private void Jump()
    {
        // Vérifier si nous sommes sur le sol pour éviter les sauts multiples
        if (Grounded)
        {
            // Appliquer une force verticale pour le saut
            Vector2 jumpVelocity = new Vector2(0, jumpForce);
            body.AddForce(jumpVelocity, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            Grounded = false; // Ne pas sauter en l'air
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Votre code existant pour détecter la collision avec le sol
    }
}