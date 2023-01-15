using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource audioSource;

    public AudioClip buttonSFX;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void StopAudio() 
    {
        audioSource.Stop();
    }

    public void PlayAudioOneShot(AudioClip clip) 
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayButtonSound() 
    {
        PlayAudioOneShot(buttonSFX);
    }

}
