using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpaceShipController : MonoBehaviour
{
    public Rigidbody _rb;
    private Camera _cam;
    private readonly float _baseFOV = 75f;
    
    // Input manager
    private SpacePlayerInputs _inputs;
    
    [Header("Sensitivities and speeds")]
    [SerializeField]
    private float camSensitivity = 0.01f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float tiltSpeed = 10f;
    [SerializeField] private float matchingSpeed = 10;
    [SerializeField] private float forwardMultiplier = 10;
    
    // Inputs 
    private Vector2 _movementInput;
    private float _upDownInput;
    private float _xRotationInput = 0;
    private float _yRotationInput = 0;
    private float _zRotationInput = 0;
    private bool _matching;
    
    private ShipHUD _hud;
    
    [Header("Thrusters")]
    [SerializeField] private GameObject[] forward;
    [SerializeField] private GameObject[] backward;
    [SerializeField] private GameObject[] up;
    [SerializeField] private GameObject[] down;
    [SerializeField] private GameObject[] left;
    [SerializeField] private GameObject[] right;

    private void Awake()
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

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        _hud = FindObjectOfType<ShipHUD>();

        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetRotateInputs();
        GetMovementInputs();
    }

    private void FixedUpdate()
    {
        //update player movement and camera
        ShipMove();
        ShipRotate();
        ManageThrusters();
    }

    //player body movement and jump
    private void ShipMove()
    {
        // Calculate horizontal movement
        Vector3 horizontal = transform.right * _movementInput.x;
        // Calculate forward/backward movement
        Vector3 vertical = transform.forward * (_movementInput.y * forwardMultiplier);
        // Calculate up/down movement
        Vector3 upDown = transform.up * _upDownInput;
        
        // Combine horizontal and vertical movement
        Vector3 movement = (horizontal + vertical + upDown) * moveSpeed;

        // Apply the movement force
        _rb.AddForce(movement, ForceMode.Force);
        
        if (_matching)
        {
            MatchSpeed();
        }
        
        
    }

    //player head and body movement
    private void ShipRotate()
    {
        Vector3 torque = new Vector3(-_yRotationInput, _xRotationInput, _zRotationInput * tiltSpeed);
        _rb.AddRelativeTorque(torque, ForceMode.Force);
    }

    private void ManageThrusters()
    {
        foreach (GameObject thruster in forward)
        {
            thruster.SetActive(_movementInput.y > 0);
        }
        
        foreach (GameObject thruster in backward)
        {
            thruster.SetActive(_movementInput.y < 0);
        }
        
        foreach (GameObject thruster in up)
        {
            thruster.SetActive(_upDownInput > 0);
        }
        
        foreach (GameObject thruster in down)
        {
            thruster.SetActive(_upDownInput < 0);
        }
        
        foreach (GameObject thruster in left)
        {
            thruster.SetActive(_movementInput.x < 0);
        }
        
        foreach (GameObject thruster in right)
        {
            thruster.SetActive(_movementInput.x > 0);
        }
    }
    
    private void GetRotateInputs()
    {
        _xRotationInput = _inputs.Player.Look.ReadValue<Vector2>().x * camSensitivity;
        _yRotationInput = _inputs.Player.Look.ReadValue<Vector2>().y * camSensitivity;
        _zRotationInput = _inputs.Player.Tilt.ReadValue<float>();
    }
    
    private void GetMovementInputs()
    {
        _movementInput = _inputs.Player.Move.ReadValue<Vector2>();
        _upDownInput = _inputs.Player.UpDown.ReadValue<float>();
        _matching = _inputs.Player.Match.IsInProgress();
    }
    
    private void MatchSpeed()
    {
        if (_hud.lockedBody != null)
        {
            // Calculate the difference in velocity
            Vector3 velocityDifference = _hud.lockedBody.velocity - _rb.velocity;
        
            // Calculate the acceleration needed to match the speed
            Vector3 acceleration = velocityDifference * matchingSpeed / Time.fixedDeltaTime;
        
            // Apply the acceleration as force
            _rb.AddForce(acceleration, ForceMode.Force);
        }
    }
}
