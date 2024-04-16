using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchClose : MonoBehaviour
{

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().isTrigger = true;
  
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //activate the hatch's sprite renderer
            GetComponent<SpriteRenderer>().enabled = true;
            //Remove the "IsTrigger" property from the collider
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
