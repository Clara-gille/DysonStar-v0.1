using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointThreeD : MonoBehaviour
{
    [SerializeField] private GameObject spaceShip;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the spawn point to the spaceship's position
        transform.position = spaceShip.transform.position;
    }
}
