using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmGenerator : MonoBehaviour
{
    [SerializeField] private MeshRenderer panelPrefab;
    [SerializeField] private float radius = 100;
    [SerializeField] private int slices = 10; // Number of slices (latitude)
    [SerializeField] private int stacks = 10; // Number of stacks (longitude)

    // Start is called before the first frame update
    void Start()
    {
        GeneratePanels();
    }

    // Generate panels on a sphere around the object
    void GeneratePanels()
    {
        for (int i = 0; i < slices; i++)
        {
            for (int j = 0; j < stacks; j++)
            {
                float theta = 2 * Mathf.PI * i / slices; // Azimuthal angle
                float phi = Mathf.PI * j / stacks; // Polar angle

                float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
                float z = radius * Mathf.Cos(phi);

                Vector3 panelPosition = new Vector3(x, y, z) + transform.position;
                Quaternion panelRotation = Quaternion.LookRotation(transform.position - panelPosition, Vector3.up);

                MeshRenderer panel = Instantiate(panelPrefab, panelPosition, panelRotation);
                panel.transform.SetParent(transform); //Parent the panels to the object
            }
        }
    }
}