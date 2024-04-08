using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreezeClone : MonoBehaviour
{
    // Reference to the CloneMovement script attached to the clone GameObject
    private _PlayerMouvement cloneMovement;

    // Start is called before the first frame update
    void Start()
    {
        // Get the CloneMovement component attached to the clone GameObject
        cloneMovement = GetComponent<_PlayerMouvement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the X key is pressed and toggle freeze/unfreeze
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            // Toggle freeze/unfreeze the clone
            cloneMovement.enabled = !cloneMovement.enabled;
        }
    }
}
