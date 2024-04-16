using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparition : MonoBehaviour
{
    private bool destroying = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!destroying)
        {
            destroying = true;
            Invoke("DestroyObject", 3f);
        }

    }

    private void DestroyObject()
    {

        Destroy(gameObject);
    }


}
