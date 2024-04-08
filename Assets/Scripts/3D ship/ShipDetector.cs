using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDetector : MonoBehaviour
{
    [SerializeField] private float landingDistance = 500f;
    [SerializeField] private float destroyDistance = 100f;
    [SerializeField] private GameObject spaceShip;
    private SpawnPointThreeD _spawnPointThreeD;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnPointThreeD = FindObjectOfType<SpawnPointThreeD>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if ((spaceShip.transform.position - transform.position).magnitude < destroyDistance)
        {
            RespawnShip();
        }
        else if ((spaceShip.transform.position - transform.position).magnitude < landingDistance)
        {
            Debug.Log("Landing distance" + name);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, landingDistance);
    }
    
    private void RespawnShip()
    {
        spaceShip.transform.position = _spawnPointThreeD.transform.position;
    }
}
