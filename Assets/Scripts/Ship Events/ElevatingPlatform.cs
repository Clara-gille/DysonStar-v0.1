using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatingPlatform : MonoBehaviour
{
    public bool canMove;

    [SerializeField] private float speed = 10f;
    [SerializeField] private int startPoint;
    [SerializeField] public Transform[] points;
    [SerializeField] public GameObject[] TriggerToDeactivate;
    [SerializeField] private InputActionReference move;

    public int destinationPoint;
    public bool reverse;
    public bool TriggerStatus;

    private void Start()
    {
        transform.position = points[startPoint].position;
        destinationPoint = startPoint;
        TriggerStatus = true;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, points[destinationPoint].position) < 0.01f)
        {
            canMove = false;
            if (!TriggerStatus)
            {
                
                foreach (GameObject trigger in TriggerToDeactivate)
                {
                    trigger.SetActive(true);
                }
                TriggerStatus = true;
            }

        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[destinationPoint].position, speed * Time.deltaTime);
        }
    }


}
