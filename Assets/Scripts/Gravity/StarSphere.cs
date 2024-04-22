using UnityEngine;

namespace Gravity
{
    public class StarSphere : MonoBehaviour {

        public MeshRenderer starPrefab;
        public Vector2 radiusMinMax;
        public int count = 1000;
        const float CalibrationDst = 2000;
        public Vector2 brightnessMinMax;

        Camera cam;
        
        void Start () {
            cam = Camera.main;
            float starDst = cam.farClipPlane - radiusMinMax.y;
            float scale = starDst / CalibrationDst;
            
            // Create the stars in the sphere around the camera with random positions and sizes
            for (int i = 0; i < count; i++) {
                MeshRenderer star = Instantiate (starPrefab, Random.onUnitSphere * starDst, Quaternion.identity, transform);
                float t = Mathf.Max(0.1f,(float)(Random.value - 0.3));
                star.transform.localScale = Vector3.one * Mathf.Lerp (radiusMinMax.x, radiusMinMax.y, t) * scale;
                star.material.color = Color.Lerp (Color.black, star.material.color, Mathf.Lerp (brightnessMinMax.x, brightnessMinMax.y, t));
            }
        }
        
        // Update the position of the sphere to follow the camera
        void LateUpdate () {
            transform.position = cam.transform.position;
        }
    }
}