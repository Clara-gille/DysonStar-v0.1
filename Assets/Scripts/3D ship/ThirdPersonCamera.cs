using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target; // Ship's transform to follow
    [SerializeField] private float smoothSpeed = 10f; // Smoothing speed for camera movement
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -5f); // Offset from the ship

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned for ThirdPersonCamera script.");
            return;
        }

        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Rotate the camera to match the ship's rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed * Time.deltaTime);
    }
}