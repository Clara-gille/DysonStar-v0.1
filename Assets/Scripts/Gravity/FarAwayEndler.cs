using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAwayEndlerManager : MonoBehaviour {

    public float distanceThreshold = 1000;
    List<Transform> physicsObjects;
    SpaceShipController ship;
    Camera playerCamera;

    public event System.Action PostFloatingOriginUpdate;

    void Awake () {
        ship = FindObjectOfType<SpaceShipController> ();
        var bodies = FindObjectsOfType<CelestialBody> ();

        physicsObjects = new List<Transform> ();
        physicsObjects.Add (ship.transform);
        foreach (var c in bodies) {
            physicsObjects.Add (c.transform);
        }

        playerCamera = Camera.main;
    }

    void LateUpdate () {
        UpdateFloatingOrigin ();
        if (PostFloatingOriginUpdate != null) {
            PostFloatingOriginUpdate ();
        }
    }

    void UpdateFloatingOrigin () {
        Vector3 originOffset = playerCamera.transform.position;
        float dstFromOrigin = originOffset.magnitude;

        if (dstFromOrigin > distanceThreshold) {
            foreach (Transform t in physicsObjects) {
                t.position -= originOffset;
            }
        }
    }

}