using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceLines : MonoBehaviour
{
    [SerializeField] private AudioSource[] voiceLine;
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            Debug.Log("Playing");
            foreach (AudioSource voiceLine in voiceLine)
            {
                voiceLine.Play();
                Debug.Log("Played");
            }
            
        }
    }
}
