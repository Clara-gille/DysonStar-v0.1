using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceLines : MonoBehaviour
{
    [SerializeField] private AudioSource[] voiceLine;
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            foreach (AudioSource voiceLine in voiceLine)
            {
                voiceLine.Play();
            }
            
        }
    }
}
