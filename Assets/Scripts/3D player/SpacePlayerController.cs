using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpacePlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 3f;
    private Vector2 _movementInput;

    [Header("Camera")]
    private GameObject _cam;
    [SerializeField] float camSensitivity = 0.2f;
    private float _xRotation = 0;
    private float _yRotation = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (Camera.main != null) _cam = Camera.main.gameObject;

        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        //update player movement and camera
        PlayerMove();
        PlayerLook();
    }

    //player body movement and jump
    private void PlayerMove()
    {
        _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Calculate horizontal movement
        Vector3 horizontal = transform.right * _movementInput.x;
        // Calculate forward/backward movement
        Vector3 vertical = transform.forward * _movementInput.y;
        
        Vector3 upDown = transform.up * (Input.GetKey(KeyCode.Space) ? moveSpeed : Input.GetKey(KeyCode.LeftShift) ? -moveSpeed : 0);
        

        // Combine horizontal and vertical movement
        Vector3 movement = (horizontal + vertical + upDown).normalized * moveSpeed;

        // Apply the movement force
        _rb.AddForce(movement, ForceMode.Impulse);

        
    }

    //player head and body movement
    private void PlayerLook()
    {
        // Adjust the rotation based on mouse input
        _yRotation += Input.GetAxis("Mouse X") * camSensitivity;
        _xRotation -= Input.GetAxis("Mouse Y") * camSensitivity;

        // Rotate the player around the y-axis and x-axis
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
    
}
