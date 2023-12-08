using System;
using System.Collections;
using System.Collections.Generic;
using Gravity;
using UnityEngine;

public class NBodySimulator : MonoBehaviour
{
    CelestialBody[] bodies;
    static NBodySimulator instance;
    [SerializeField] float timeStepRatio = 1f;
    
    void Awake () {
        bodies = FindObjectsOfType<CelestialBody> ();
        Time.fixedDeltaTime = Constants.PhysicsTimeStep / timeStepRatio;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++) {
            Vector3 acceleration = CalculateAcceleration (bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity (acceleration, Constants.PhysicsTimeStep);
        }
        
        for (int i = 0; i < bodies.Length; i++) {
            bodies[i].UpdatePosition (Constants.PhysicsTimeStep);
        }
    }
    
    //TODO : use Barnes-Hut algorithm 
    public static Vector3 CalculateAcceleration (Vector3 point, CelestialBody ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector3 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * Constants.GravitationalConstant * body.mass / sqrDst;
            }
        }
        return acceleration;
    }

    public static CelestialBody[] Bodies {
        get {
            return Instance.bodies;
        }
    }
    
    static NBodySimulator Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<NBodySimulator> ();
            }
            return instance;
        }
    }
}
