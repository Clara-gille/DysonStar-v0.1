using Gravity;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitDisplay : MonoBehaviour {
    
    //number of steps to simulate
    [SerializeField] int numSteps = 1000;
    //time between steps
    [SerializeField] float timeStep = 0.1f;
    //use the physics time step instead of the time step
    [SerializeField] bool usePhysicsTimeStep;
    
    //draw the orbits relative to the central body or the origin
    [SerializeField] bool relativeToBody;
    //the central body to draw the orbits relative to
    [SerializeField] CelestialBody centralBody;
    
    void Update () {
        //stop the preview from running in play mode
        if (!Application.isPlaying) {
            DrawOrbits ();
        }
    }

    void DrawOrbits () {
        //get all the bodies in the scene
        CelestialBody[] bodies = FindObjectsOfType<CelestialBody> ();
        var virtualBodies = new VirtualBody[bodies.Length];
        var drawPoints = new Vector3[bodies.Length][];
        int referenceFrameIndex = 0;
        Vector3 referenceBodyInitialPosition = Vector3.zero;

        // Initialize virtual bodies so that we don't move the actual bodies
        for (int i = 0; i < virtualBodies.Length; i++) {
            virtualBodies[i] = new VirtualBody (bodies[i]);
            drawPoints[i] = new Vector3[numSteps];
            
            //set the reference frame to the central body if we are drawing relative to a body
            if (bodies[i] == centralBody && relativeToBody) {
                referenceFrameIndex = i;
                referenceBodyInitialPosition = virtualBodies[i].position;
            }
        }

        // Simulate the movement
        for (int step = 0; step < numSteps; step++) {
            //set either the reference body position or the origin as the reference frame
            Vector3 referenceBodyPosition = (relativeToBody) ? virtualBodies[referenceFrameIndex].position : Vector3.zero;
            
            // Update velocities
            for (int i = 0; i < virtualBodies.Length; i++) {
                virtualBodies[i].velocity += CalculateAcceleration (i, virtualBodies) * timeStep;
            }
            
            // Update positions
            for (int i = 0; i < virtualBodies.Length; i++) {
                Vector3 newPos = virtualBodies[i].position + virtualBodies[i].velocity * timeStep;
                virtualBodies[i].position = newPos;
                //if we are drawing relative to a body, offset the position by the reference frame
                if (relativeToBody) {
                    var referenceFrameOffset = referenceBodyPosition - referenceBodyInitialPosition;
                    newPos -= referenceFrameOffset;
                }
                
                //if we are drawing relative to a body, set the reference body's position to the origin so that it doesn't move
                if (relativeToBody && i == referenceFrameIndex) {
                    newPos = referenceBodyInitialPosition;
                }
                
                //store the position in the array of points to draw
                drawPoints[i][step] = newPos;
            }
        }

        // Draw paths
        for (int bodyIndex = 0; bodyIndex < virtualBodies.Length; bodyIndex++) {
            var pathColour = bodies[bodyIndex].gameObject.GetComponentInChildren<MeshRenderer> ().sharedMaterial.color; 
                //draw a line between each point in the array
                for (int i = 0; i < drawPoints[bodyIndex].Length - 1; i++) {
                    Debug.DrawLine (drawPoints[bodyIndex][i], drawPoints[bodyIndex][i + 1], pathColour);
                }
        }
    }
    
    //TODO : use Barnes-Hut algorithm and don't duplicate code
    Vector3 CalculateAcceleration (int i, VirtualBody[] virtualBodies) {
        Vector3 acceleration = Vector3.zero;
        for (int j = 0; j < virtualBodies.Length; j++) {
            if (i == j) {
                continue;
            }
            Vector3 forceDir = (virtualBodies[j].position - virtualBodies[i].position).normalized;
            float sqrDst = (virtualBodies[j].position - virtualBodies[i].position).sqrMagnitude;
            acceleration += forceDir * Constants.GravitationalConstant * virtualBodies[j].mass / sqrDst;
        }
        return acceleration;
    }
    

    void OnValidate () {
        if (usePhysicsTimeStep) {
            timeStep = Constants.PhysicsTimeStep;
        }
    }

    class VirtualBody {
        public Vector3 position;
        public Vector3 velocity;
        public float mass;

        public VirtualBody (CelestialBody body) {
            position = body.transform.position;
            velocity = body.initialVelocity;
            mass = body.mass;
        }
    }
}