using System;
using System.Collections;
using System.Collections.Generic;
using Gravity;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof (Rigidbody))]
public class CelestialBody : MonoBehaviour
{
    [SerializeField] public float radius;
    [SerializeField] float surfaceGravity;
    [SerializeField] public Vector3 initialVelocity;
    [SerializeField] public string bodyName = "No Name";
    Transform meshHolder;
    
    [SerializeField]  public Vector3 velocity { get; set; }
    [SerializeField]  public float mass { get; private set; }
    Rigidbody rb;
    
    void Awake () {
        rb = GetComponent<Rigidbody> ();
        rb.mass = mass;
        velocity = initialVelocity;
    }
    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        velocity += acceleration * timeStep;
    }
    
    public void UpdatePosition(float timeStep)
    {
        rb.MovePosition(rb.position + velocity * timeStep);
    }

    private void OnValidate()
    {
        mass = surfaceGravity * radius * radius / Constants.GravitationalConstant;
        meshHolder = transform.GetChild (0);
        meshHolder.localScale = Vector3.one * radius;
        gameObject.name = bodyName;
    }
    
    public Rigidbody Rigidbody {
        get {
            return rb;
        }
    }
    
    public Vector3 Position {
        get {
            return rb.position;
        }
    }
}
