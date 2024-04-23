using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jungleMovingPlatform : MonoBehaviour
{

    public bool canMove;

    [SerializeField] private float speed = 10f;
    [SerializeField] private int startPoint;
    [SerializeField] public Transform[] points;
    
    private AudioSource audioSource;

    public int destinationPoint;

    private void Start()
    {
        transform.position = points[startPoint].position;
        destinationPoint = startPoint;
        audioSource = GetComponent<AudioSource>();
    }

    // This scripts moves the associated platform following the same logic as the ElevatingPlatform script but does so automatically and without handling any triggers
    private void Update()
    {
        if (Vector3.Distance(transform.position, points[destinationPoint].position) < 0.01f)
        {
            if (destinationPoint == points.Length - 1)
            {
                destinationPoint--;
            }
            else if (destinationPoint == 0)
            {
                destinationPoint++;
            }
        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[destinationPoint].position, speed * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
