using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WakingUp : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    private const string WAKEUP = "WakeUp";
    // Start is called before the first frame update

    private void Awake()
    {
        if(!EditorPrefs.HasKey(WAKEUP))
        {
            //play audio
            audioSource.Play();
            EditorPrefs.SetBool(WAKEUP, true);
        }

    }
}
