using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpacePlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    
    private SpacePlayerInputs _inputs;
    
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float leanTorqueStrength = 1f;
    private Vector2 _movementInput;
    private float _upDownInput;
    private float _tiltInput;

    [Header("Camera")]
    [SerializeField] float camSensitivity = 0.01f;
    private float _xRotation = 0;
    private float _yRotation = 0;

    void Awake()
    {
        _inputs = new SpacePlayerInputs();
    }
    
    private void OnEnable()
    {
        _inputs.Enable();
    }
    
    private void OnDisable()
    {
        _inputs.Disable();
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        GetLookInput();
        GetMovementInput();
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
        // Calculate horizontal movement
        Vector3 horizontal = transform.right * _movementInput.x;
        // Calculate forward/backward movement
        Vector3 vertical = transform.forward * _movementInput.y;
        // Calculate up/down movement
        Vector3 upDown = transform.up * _upDownInput;

        // Combine horizontal and vertical movement
        Vector3 movement = (horizontal + vertical + upDown).normalized * moveSpeed;

        // Apply the movement force
        _rb.AddForce(movement, ForceMode.Impulse);
        
        // // Apply torque for leaning based on tilt input 
        // //TODO : unclamp it so that max angular velocity doesn't have to be infinite
        // Vector3 leanTorque = transform.forward * (_tiltInput * leanTorqueStrength);
        // _rb.AddTorque(leanTorque, ForceMode.Impulse);
    }

    //player head and body movement
    private void PlayerLook()
    {
        // Rotate the player around the y-axis and x-axis
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
    
    private void GetLookInput()
    {
        _xRotation -= _inputs.Player.Look.ReadValue<Vector2>().y * camSensitivity;
        _yRotation += _inputs.Player.Look.ReadValue<Vector2>().x * camSensitivity;
    }
    
    private void GetMovementInput()
    {
        _movementInput = _inputs.Player.Move.ReadValue<Vector2>();
        _upDownInput = _inputs.Player.UpDown.ReadValue<float>();
        _tiltInput = _inputs.Player.Tilt.ReadValue<float>();
    }
    
}
