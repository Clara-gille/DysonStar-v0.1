using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject TriggerToDeactivate;
    [SerializeField] private GameObject PlatformToMove;
    [SerializeField] private Vector3 TargetPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerToDeactivate.SetActive(false);
            MoveObject();

        }
    }


    private void MoveObject()
    {
        PlatformToMove.transform.position = TargetPosition;
    }



}
