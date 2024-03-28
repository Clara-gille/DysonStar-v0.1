using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetArmController : MonoBehaviour
{
    [SerializeField] private GameObject magnetArm;
    [SerializeField] private GameObject emitPoint;
    [SerializeField] private float armSpeed = 1f;
    
    private MagnetInputs _inputs;
    private float _armRotation;
    
    void Awake()
    {
        _inputs = new MagnetInputs();
        
        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnEnable()
    {
        _inputs.Enable();
    }
    
    private void OnDisable()
    {
        _inputs.Disable();
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        GetArmInput();
        
    }
    
    void FixedUpdate()
    {
        MoveArm();
    }
    
    private void GetArmInput()
    {
        // Get the input for the arm movement
        _armRotation += _inputs.Player.Arm.ReadValue<Vector2>().x * armSpeed;
    }
    
    private void MoveArm()
    {
        // Rotate the arm
        magnetArm.transform.rotation = Quaternion.Euler(0, 0, _armRotation);
    }
}