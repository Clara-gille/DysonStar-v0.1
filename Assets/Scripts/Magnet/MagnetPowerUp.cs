using UnityEngine;

namespace Magnet
{
    public class MagnetPowerUp : MonoBehaviour
    {
        [SerializeField] private GameObject magnetArm;
    
        // Detect when there's a collision with a trigger
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // If player collides with the powerup
            {
                // Enable the magnet arm
                magnetArm.SetActive(true);
                Destroy(gameObject); // Destroy the powerup
            }
        }
    }
}
