using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle fullscreenToggle;

    public void Start()
    {
         volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0);
         fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
         
    }

    public void SetVolume(float volume)
    {
        float newVolume = volume == -20 ? -80 : volume;
        audioMixer.SetFloat("Volume", newVolume);
        PlayerPrefs.SetFloat("Volume", newVolume);
    }
    
    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
    
}
