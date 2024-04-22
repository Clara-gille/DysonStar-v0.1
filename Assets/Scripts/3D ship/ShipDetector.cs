using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipDetector : MonoBehaviour
{
    [SerializeField] private float landingDistance = 500f;
    [SerializeField] private float destroyDistance = 100f;
    [SerializeField] private bool landable = true;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private TMP_Text landingText;
    [SerializeField] private TMP_Text destroyText;
    [SerializeField] private bool drawGizmos = false;
    
    private SpawnPointThreeD _spawnPointThreeD;
    private bool _isClose = false;
    
    private SpacePlayerInputs _inputs;
    
    private void Awake()
    {
        _inputs = new SpacePlayerInputs();
        _inputs.Player.Land.performed += _ => Land();
    }

    private void OnDestroy()
    {
        _inputs.Player.Land.performed -= _ => Land();
    }
    
    private void OnEnable()
    {
        _inputs.Enable();
    }
    
    private void OnDisable()
    {
        _inputs.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnPointThreeD = FindObjectOfType<SpawnPointThreeD>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Check if the ship is close to the planet
        if ((spaceShip.transform.position - transform.position).magnitude < destroyDistance)
        {
            StartCoroutine(RespawnShip());
        }
        //Check if the ship is close enough to land
        else if ((spaceShip.transform.position - transform.position).magnitude < landingDistance)
        {
            landingText.text = landable ? "Press [Enter] to land on " + name : "You can't land on " + name;
            _isClose = true;
            landingText.gameObject.SetActive(true);
            
        }
        //Hide landing text if the ship is not close anymore
        else if (_isClose)
        {
            landingText.gameObject.SetActive(false);
            _isClose = false;
        }
    }
    
    //Draw gizmos to show the landing and destroy distances in the editor
    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, destroyDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, landingDistance);
        }
    }
    
    //Respawn the ship and show a message for a few seconds
    private IEnumerator RespawnShip()
    {
        //trigger explosion using method
        spaceShip.GetComponent<SpaceShipController>().Explode();
        //Hide ship mesh
        spaceShip.GetComponent<MeshRenderer>().enabled = false;
        //Disable thrusters
        spaceShip.GetComponent<SpaceShipController>().ToggleThruster(false);
        //Reset velocity
        spaceShip.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Reset angular velocity
        spaceShip.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        destroyText.text = "You flew too close to " + name + " and were destroyed!";
        destroyText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3);
        
        //Reset position
        spaceShip.transform.position = _spawnPointThreeD.transform.position;
        //Show ship mesh
        spaceShip.GetComponent<MeshRenderer>().enabled = true;
        //Enable thrusters
        spaceShip.GetComponent<SpaceShipController>().ToggleThruster(true);
        StartCoroutine(HideText());
    }
    
    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(2);
        destroyText.gameObject.SetActive(false);
    }
    
    //Land on the planets
    private void Land()
    {
        if (landable && _isClose)
        {
            if (gameObject.name == "Corellia")
            {
                SceneManager.LoadScene("Jungle");
            }

            if (gameObject.name == "Honoghr")
            {
                SceneManager.LoadScene("Industrielle");
            }
        }
    }
}
