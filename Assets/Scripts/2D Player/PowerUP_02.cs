using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP_02 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Si le joueur entre en collision avec le power-up
        {
            // Trouver tous les objets avec le tag "Red"
            GameObject[] redObjects = GameObject.FindGameObjectsWithTag("Red");
            // Pour chaque objet avec le tag "Red", augmenter le scale à 5
            foreach (GameObject obj in redObjects)
            {
                obj.transform.localScale = new Vector3(0.1f, 5f, 1f);
            }

            // Trouver tous les objets avec le tag "Blue"
            GameObject[] blueObjects = GameObject.FindGameObjectsWithTag("Blue");
            // Pour chaque objet avec le tag "Blue", augmenter le scale à 5
            foreach (GameObject obj in blueObjects)
            {
                obj.transform.localScale = new Vector3(0.1f, 5f, 1f);
            }

            // Détruire le power-up
            Destroy(gameObject);
        }
    }
}