using System;
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
        //Suscribe to the shoot events
        _inputs.Player.ShootBlue.started += _ => StartBlueRay();
        _inputs.Player.ShootRed.started += _ => StartRedRay();
        _inputs.Player.ShootBlue.canceled += _ => StopBlueRay();
        _inputs.Player.ShootRed.canceled += _ => StopRedRay();
    }

    private void OnDestroy()
    {
        //Unsubscribe from the shoot events
        _inputs.Player.ShootBlue.started -= _ => StartBlueRay();
        _inputs.Player.ShootRed.started -= _ => StartRedRay();
        _inputs.Player.ShootBlue.canceled -= _ => StopBlueRay();
        _inputs.Player.ShootRed.canceled -= _ => StopRedRay();
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
        Shoot();
    }
    
    private void StartBlueRay()
    {
        _shootingBlue = true;
        _shootingRed ^= _shootingRed;
    }
    
    private void StartRedRay()
    {
        _shootingRed = true;
        _shootingBlue ^= _shootingBlue;
    }
    
    private void StopBlueRay()
    {
        _shootingBlue = false;
        if (_inputs.Player.ShootRed.IsPressed())
        {
            StartRedRay();
        }
    }
    
    private void StopRedRay()
    {
        _shootingRed = false;
        if (_inputs.Player.ShootBlue.IsPressed())
        {
            StartBlueRay();
        }
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