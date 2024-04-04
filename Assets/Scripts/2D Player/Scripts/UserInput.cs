using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInput instance; // Instance singleton de cette classe.
    public Vector2 moveInput; // Vecteur pour stocker les inputs de mouvement.

    void Awake()
    {
        // Si l'instance n'existe pas, on la définit et on s'assure qu'elle ne sera pas détruite en changeant de scène.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // S'il existe déjà une instance de cette classe, on détruit ce composant pour éviter les doublons.
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Capture les inputs de mouvement horizontaux et verticaux.
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}