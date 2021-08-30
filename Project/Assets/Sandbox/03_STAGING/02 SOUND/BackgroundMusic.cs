using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPlaying = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBackgroundMusic()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
        else
        {
            audioSource.Pause();
            isPlaying = false;
        }
        
    }
}
