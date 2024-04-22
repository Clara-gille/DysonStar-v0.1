using UnityEngine;

namespace Gravity
{
    [ExecuteInEditMode]
    [RequireComponent (typeof (Rigidbody))]
    public class SpaceObject : MonoBehaviour
    {
        [SerializeField] public float radius;
        [SerializeField] float surfaceGravity;
        [SerializeField] public Vector3 initialVelocity;
        [SerializeField] public string bodyName = "No Name";
        Transform meshHolder;
    
        public Vector3 Velocity { get; set; }
        public float Mass { get; private set; }

        void Awake () {
            Rigidbody = GetComponent<Rigidbody> ();
            Rigidbody.mass = Mass;
            Velocity = initialVelocity;
        }
        public void UpdateVelocity(Vector3 acceleration, float timeStep)
        {
            Velocity += acceleration * timeStep;
        }
    
        public void UpdatePosition(float timeStep)
        {
            Rigidbody.MovePosition(Rigidbody.position + Velocity * timeStep);
            //rotate the body on itself depending on its velocity
            meshHolder.Rotate(Vector3.up,  Velocity.magnitude * Time.fixedDeltaTime / radius * 360);
        }

        private void OnValidate()
        {
            //Automatically calculate the mass of the object and apply its radius to the mesh
            Mass = surfaceGravity * radius * radius / Constants.GravitationalConstant;
            meshHolder = transform.GetChild (0);
            meshHolder.localScale = Vector3.one * radius * 2;
            gameObject.name = bodyName;
        }

        private Rigidbody Rigidbody { get; set; }

        public Vector3 Position => Rigidbody.position;
    }
}
