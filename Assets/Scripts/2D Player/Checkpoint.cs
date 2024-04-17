using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform playerSpawn;
    private Renderer visualCheckpointRenderer;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        GameObject visualCheckpointObject = GameObject.FindGameObjectWithTag("VisualCheckpoint");
        if (visualCheckpointObject != null)
            visualCheckpointRenderer = visualCheckpointObject.GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && visualCheckpointRenderer != null)
        {
            // Change the color to green
            visualCheckpointRenderer.material.color = Color.green;

            playerSpawn.position = transform.position;
            Destroy(gameObject);
        }
    }
}
