using UnityEngine;

namespace Magnet
{
    public class MagnetRay : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject emitPoint;
        [SerializeField] private GameObject endPoint;
        [SerializeField] private float RepusleStrength = 20;
        [SerializeField] private float AttractStrength = 20;

        //Detect when collision with a trigger is occurring
        private void OnTriggerStay2D(Collider2D other) // If laser hits something
        {
            if (other.CompareTag(tag)) // If block of same color
            {
                // Repulse the player in the opposite direction of the ray
                Vector3 direction = endPoint.transform.position - emitPoint.transform.position;
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                Vector2 force = -direction.normalized * RepusleStrength / 25;
                rb.AddForce(force, ForceMode2D.Impulse); //Impulse mode is used to apply force instantly
                //Add horizontal force to the player movements
                player.GetComponent<PlayerMouvement>().magnetHorizontal = force.x * 4;
            }
            else if (other.CompareTag(CompareTag("Blue") ? "Red" : "Blue")) // If block of different color
            {
                // Attract the player towards the magnet
                Vector3 direction = endPoint.transform.position - emitPoint.transform.position;
                Vector2 force = direction.normalized * AttractStrength;
                player.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force); //Force mode is used to apply force continuously
                player.GetComponent<PlayerMouvement>().magnetHorizontal = force.x / 4;
            }
        }
    }
}
