using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceLines : MonoBehaviour
{
    [SerializeField] private AudioSource[] voiceLine;
    private AudioSource[] AllAudioSources;
    private bool hasPlayed = false;

    private void Start()
    {
        AllAudioSources = FindObjectsOfType<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !hasPlayed)
        {
            // Check first if there's another voice line playing
            foreach (AudioSource audioSource in AllAudioSources)
            {
                // Check if the audio source is playing and is not the one attached to the main camera
                if (audioSource.isPlaying && audioSource != Camera.main.GetComponent<AudioSource>())
                {
                    audioSource.Stop();
                }
            }
            hasPlayed = true;
            foreach (AudioSource voiceLine in voiceLine)
            {
                voiceLine.Play();
            }
            
        }
    }
}
