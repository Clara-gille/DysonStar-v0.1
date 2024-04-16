using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunglePlatformController : MonoBehaviour
{

    [SerializeField] private jungleMovingPlatform platform;
   


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            platform.canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            platform.canMove = false;
        }
    }
}
