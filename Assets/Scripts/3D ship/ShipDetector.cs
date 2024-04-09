using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipDetector : MonoBehaviour
{
    [SerializeField] private float landingDistance = 500f;
    [SerializeField] private float destroyDistance = 100f;
    [SerializeField] private bool landable = true;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private TMP_Text landingText;
    private SpawnPointThreeD _spawnPointThreeD;
    private bool _wasClose = false;
    
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
            landingText.text = landable ? "Press [Enter] to land on " + name : "You can't land on " + name;
            _wasClose = true;
            landingText.gameObject.SetActive(true);
            
        }
        else if (_wasClose)
        {
            landingText.gameObject.SetActive(false);
            _wasClose = false;
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
