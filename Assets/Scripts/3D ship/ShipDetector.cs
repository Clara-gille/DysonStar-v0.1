using System;
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
    [SerializeField] private TMP_Text destroyText;
    
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
        if ((spaceShip.transform.position - transform.position).magnitude < destroyDistance)
        {
            RespawnShip();
        }
        else if ((spaceShip.transform.position - transform.position).magnitude < landingDistance)
        {
            landingText.text = landable ? "Press [Enter] to land on " + name : "You can't land on " + name;
            _isClose = true;
            landingText.gameObject.SetActive(true);
            
        }
        else if (_isClose)
        {
            landingText.gameObject.SetActive(false);
            _isClose = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        //Draw ranges in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, landingDistance);
    }
    
    private void RespawnShip()
    {
        //Reset position
        spaceShip.transform.position = _spawnPointThreeD.transform.position;
        //Reset velocity
        spaceShip.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Reset angular velocity
        spaceShip.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        destroyText.text = "You flew too close to " + name + " and were destroyed!";
        destroyText.gameObject.SetActive(true);
        StartCoroutine(HideText());
    }
    
    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(3);
        destroyText.gameObject.SetActive(false);
    }
    
    private void Land()
    {
        if (landable && _isClose)
        {
            Debug.Log("Landed on " + name);
        }
    }
}
