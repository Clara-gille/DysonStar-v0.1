using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnRock : MonoBehaviour
{

    private bool nearbutton;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject rockSpawnPoint;
    [SerializeField] private InputActionReference activate;
    private AudioSource rockSound;
    private void OnEnable()
    {
        activate.action.Enable();
        activate.action.performed += RockSpawn;
    }
    
    private void Start()
    {
        rockSound = GetComponent<AudioSource>();
    }

    private void RockSpawn(InputAction.CallbackContext context)
    {
        // If there's already a rock in the scene, delete it
        if (GameObject.Find("Big Rock(Clone)") && nearbutton)
        {
            Destroy(GameObject.Find("Big Rock(Clone)"));
        }
        if (context.performed && nearbutton)
        {
            Instantiate(rock, rockSpawnPoint.transform.position, Quaternion.identity);
            rockSound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearbutton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearbutton = false;
        }
    }
}
