using UnityEngine;

namespace Gravity
{
    public class GravitySimulator : MonoBehaviour
    {
        SpaceObject[] bodies;
        [SerializeField] float timeStepRatio = 1f;
    
        void Awake () {
            bodies = FindObjectsOfType<SpaceObject> ();
            Time.fixedDeltaTime = Constants.PhysicsTimeStep / timeStepRatio;
        }

        private void FixedUpdate()
        {
            //Calculate the acceleration of each space object (!exponential complexity!)
            foreach (var t in bodies)
            {
                Vector3 acceleration = CalculateAcceleration (t.Position, t);
                t.UpdateVelocity (acceleration, Constants.PhysicsTimeStep);
            }

            foreach (var t in bodies)
            {
                t.UpdatePosition (Constants.PhysicsTimeStep);
            }
        }
        
        // Calculate the acceleration of a point in space due to all the bodies
        private  Vector3 CalculateAcceleration (Vector3 point, SpaceObject ignoreBody = null) {
            Vector3 acceleration = Vector3.zero;
            foreach (var body in bodies) {
                if (body != ignoreBody) {
                    float sqrDst = (body.Position - point).sqrMagnitude;
                    Vector3 forceDir = (body.Position - point).normalized;
                    acceleration += forceDir * (Constants.GravitationalConstant * body.Mass) / sqrDst;
                }
            }
            return acceleration;
        }
    }
}
