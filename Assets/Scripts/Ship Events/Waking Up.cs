using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakingUp : MonoBehaviour
{

    public AudioClip audioClip;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
