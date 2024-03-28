using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetArmShooter : MonoBehaviour
{
    [SerializeField] private GameObject magnetArm;
    [SerializeField] private GameObject emitPoint;
    [SerializeField] private GameObject blueRay;
    [SerializeField] private GameObject redRay;
    
    private bool _shootingBlue = false;
    private bool _shootingRed = false;
    private MagnetInputs _inputs;
    
    void Awake()
    {
        _inputs = new MagnetInputs();
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
        GetInputs();
        Shoot();
    }
    
    private void GetInputs()
    {
        _shootingBlue = _inputs.Player.ShootBlue.IsPressed();
        _shootingRed = _inputs.Player.ShootRed.IsPressed();
    }

    private void Shoot()
    {
        if (_shootingBlue)
        {
            ShootBlue();
        }
        else
        {
            blueRay.SetActive(false);
        }
        
        if (_shootingRed)
        {
            ShootRed();
        }
        else
        {
            redRay.SetActive(false);
        }
    }

    private void ShootBlue()
    {
        blueRay.SetActive(true);
    }
    
    private void ShootRed()
    {
        redRay.SetActive(true);
    }
}