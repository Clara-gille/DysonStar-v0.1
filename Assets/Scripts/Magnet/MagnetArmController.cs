using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetArmController : MonoBehaviour
{
    [SerializeField] private GameObject magnetArm;
    [SerializeField] private GameObject emitPoint;
    
    void Awake()
    {
        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        RotateArm();
    }
    
    private void RotateArm()
    {
        // Get the direction vector from emit point to mouse position
        Vector3 direction = GetMouseDirection();

        // Calculate the angle between the direction vector and the upward direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the arm accordingly
        magnetArm.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    private Vector3 GetMouseDirection()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert mouse position to world coordinates
        mousePos.z = emitPoint.transform.position.z - Camera.main.transform.position.z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculate direction from emit point to mouse position
        return worldMousePos - emitPoint.transform.position;
    }
}