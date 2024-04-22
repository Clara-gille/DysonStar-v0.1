using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SpaceShipController : MonoBehaviour
{
    public Rigidbody _rb;
    
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
    
    [Header("Sounds")]
    [SerializeField] private AudioSource thrusterSound;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private AudioSource matchingSound;
    
    [SerializeField] private ParticleSystem explosion;

    private void Awake()
    {
        _inputs = new SpacePlayerInputs();
        _inputs.Player.Inside.performed += _ => GoInside();
        _inputs.Player.Leave.performed += _ =>  BakcToMenu();
    }

    private void OnDestroy()
    {
        _inputs.Player.Inside.performed -= _ => GoInside();
        _inputs.Player.Leave.performed -= _ =>  BakcToMenu();
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
        _hud = FindObjectOfType<ShipHUD>();

        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        thrusterSound.volume = 0.25f;
    }

    private void Update()
    {   
        //get player inputs
        GetRotateInputs();
        GetMovementInputs();
        
        matchingSound.enabled = _matching && _hud.lockedBody != null;
    }
    
    //update player movement and camera every frames
    private void FixedUpdate()
    {
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
    
    //Display or hide thrusters based on player inputs
    private void ManageThrusters()
    {
        if (_matching)
        {
            return;
        }
        
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
        //if any thruster is active play sound
        thrusterSound.pitch = (Mathf.Abs(_movementInput.y) + Mathf.Abs(_movementInput.x) + Mathf.Abs(_upDownInput)) / 3f;
        
        thrusterSound.enabled = forward[0].activeSelf || backward[0].activeSelf || up[0].activeSelf || down[0].activeSelf || left[0].activeSelf || right[0].activeSelf;
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
    
    //Match the speed of the ship with the locked planet
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
            
            // Activate the thrusters left and right
         
            foreach (GameObject thruster in left)
            {
                thruster.SetActive(true);
            }
            
            foreach (GameObject thruster in right)
            {
                thruster.SetActive(true);
            }
            
            thrusterSound.pitch = 0.5f;
            thrusterSound.enabled = true;
        }
    }
    
    public void Explode()
    {
        explosion.Play();
        explosionSound.Play();
    }
    
    public void GoInside(){
        //Switch to ship interior scene
        SceneManager.LoadScene("Ship WIP");
    }
    
    public void BakcToMenu(){
        //Switch to main menu
        SceneManager.LoadScene("Menu");
    }
    
    //Toggle thrusters on and off
    public void ToggleThruster(bool state)
    {
        foreach (GameObject thruster in forward)
        {
            thruster.SetActive(state);
        }
        
        foreach (GameObject thruster in backward)
        {
            thruster.SetActive(state);
        }
        
        foreach (GameObject thruster in up)
        {
            thruster.SetActive(state);
        }
        
        foreach (GameObject thruster in down)
        {
            thruster.SetActive(state);
        }
        
        foreach (GameObject thruster in left)
        {
            thruster.SetActive(state);
        }
        
        foreach (GameObject thruster in right)
        {
            thruster.SetActive(state);
        }
    }
}
